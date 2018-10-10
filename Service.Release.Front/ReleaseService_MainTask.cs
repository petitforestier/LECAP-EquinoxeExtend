using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Product;
using EquinoxeExtend.Shared.Object.Release;
using Library.Tools.Comparator;
using Library.Tools.Enums;
using Library.Tools.Extensions;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Release.Front
{
    public partial class ReleaseService
    {
        #region Public METHODS

        public long AddMainTask(MainTask iMainTask)
        {
            if (iMainTask == null) throw new Exception("La tâche principale est null");

            if (iMainTask.TaskType == MainTaskTypeEnum.ProjectDeveloppement && iMainTask.ExternalProjectId == null)
                throw new Exception("Une tâche de type projet requiert un numéro de projet");

            if (iMainTask.Status != MainTaskStatusEnum.Requested
                && iMainTask.Status != MainTaskStatusEnum.Waiting )
                throw new Exception("Le statut ne permet pas la création de la tâche");

            if (iMainTask.Status == MainTaskStatusEnum.Requested && iMainTask.PackageId != null)
                throw new Exception("Le statut requested ne permet l'attachement à un package");

            long newMainTaskId = 0;

            using (var ts = new TransactionScope())
            {
                if (iMainTask.PackageId != null)
                {
                    var affectedPackage = GetPackageById((long)iMainTask.PackageId, GranularityEnum.Nude);
                    if (affectedPackage.Status == PackageStatusEnum.Developpement)
                    {
                        iMainTask.Status = MainTaskStatusEnum.Dev;
                        iMainTask.OpenedDate = DateTime.Now;
                    }
                    else if (affectedPackage.Status == PackageStatusEnum.Canceled ||
                        affectedPackage.Status == PackageStatusEnum.Production ||
                        affectedPackage.Status == PackageStatusEnum.Staging)
                    {
                        throw new Exception("Il n'est pas possible d'affecter une tâche au package avec ce status");
                    }
                    else if (affectedPackage.Status == PackageStatusEnum.Waiting)
                    {
                        //ne rien faire
                    }
                    else
                        throw new Exception(affectedPackage.Status.ToStringWithEnumName());
                }

                var entity = new T_E_MainTask();
                entity.Merge(iMainTask);
                newMainTaskId = DBReleaseDataService.AddMainTask(entity);

                //SubTask
                foreach (var subTaskItem in iMainTask.SubTasks.Enum())
                {
                    subTaskItem.MainTaskId = newMainTaskId;
                    AddSubTask(subTaskItem);
                }

                //ProductLine
                foreach (var productLineItem in iMainTask.ProductLines.Enum())
                {
                    var productLineTaskEntity = new T_E_ProductLineTask();
                    productLineTaskEntity.ProductLineTaskId = -1;
                    productLineTaskEntity.MainTaskId = newMainTaskId;
                    productLineTaskEntity.ProductLineId = productLineItem.ProductLineId;
                    DBProductDataService.Add<T_E_ProductLineTask>(productLineTaskEntity);
                }

                ts.Complete();
            }
            return newMainTaskId;
        }

        public void UpdateMainTask(MainTask iMainTask)
        {
            if (iMainTask == null) throw new Exception("La tâche principale est null");
            if (iMainTask.MainTaskId < 1) throw new Exception("L'id de la tâche est invalide");
            if (iMainTask.TaskType == MainTaskTypeEnum.ProjectDeveloppement && iMainTask.ExternalProjectId == null)
                throw new Exception("Une tâche de type projet requiert un numéro de projet");

            var originalMainTask = GetMainTaskById(iMainTask.MainTaskId, GranularityEnum.Full);

            if (originalMainTask.Status != iMainTask.Status)
                throw new Exception("Pour un changement de status, cette fonction n'est pas supportée");

            if (originalMainTask.Status != MainTaskStatusEnum.Dev &&
                originalMainTask.Status != MainTaskStatusEnum.Requested &&
                originalMainTask.Status != MainTaskStatusEnum.Waiting)
                throw new Exception("Le statut actuel de la tâche ne permet pas de modification");

            //Suppression du package
            if (originalMainTask.PackageId != null && iMainTask.PackageId == null)
            {
                //Package lock
                if (originalMainTask.Package.IsLocked)
                    throw new Exception("La tâche ne peut pas sortir d'un package verrouillé");

                //Tâche entammée
                if(originalMainTask.Status == MainTaskStatusEnum.Dev)
                {
                    if (originalMainTask.SubTasks.Any(x => x.Progression != 0))
                        throw new Exception("Le Package ne peut pas être retiré de la tâche avec un avancement différent de 0");
                    else
                        iMainTask.Status = MainTaskStatusEnum.Waiting;
                }
                else if (originalMainTask.Status != MainTaskStatusEnum.Waiting && originalMainTask.Status != MainTaskStatusEnum.Requested)
                {
                    throw new Exception("Le statut de la tâche ne permet pas de sortir du package");
                }
                                    
            }
            //Affectation de package
            else if (originalMainTask.PackageId == null && iMainTask.PackageId != null)
            {
                var affectedPackage = GetPackageById((long)iMainTask.PackageId, GranularityEnum.Nude);
                if (affectedPackage.Status == PackageStatusEnum.Developpement)
                {
                    iMainTask.Status = MainTaskStatusEnum.Dev;
                    iMainTask.OpenedDate = DateTime.Now;
                }
                else if (affectedPackage.Status == PackageStatusEnum.Canceled ||
                    affectedPackage.Status == PackageStatusEnum.Production ||
                    affectedPackage.Status == PackageStatusEnum.Staging)
                {
                    throw new Exception("Il n'est pas possible d'affecter une tâche au package avec ce status");
                }
                else if (affectedPackage.Status == PackageStatusEnum.Waiting)
                {
                    //ne rien faire
                }
                else
                    throw new Exception(affectedPackage.Status.ToStringWithEnumName());
            }
            //Changement de package
            else if (originalMainTask.PackageId != iMainTask.PackageId && iMainTask.PackageId != null)
            {
                iMainTask.SubTasks = originalMainTask.SubTasks;
                if (!IsMainTaskCanJoinThisPackage(iMainTask.MainTaskId, (long)iMainTask.PackageId))
                    throw new Exception("Cette tâche ne peut pas être attachée à ce package");
            }

            using (var ts = new TransactionScope())
            {
                //MAINTASK
                var mainTaskEntity = new T_E_MainTask();
                mainTaskEntity.Merge(iMainTask);
                DBReleaseDataService.UpdateMainTask(mainTaskEntity);

                //PRODUCTLINE
                var originalProductLineTaskList = GetProductLineByMainTaskId(iMainTask.MainTaskId);
                var productLineComparator = new ListComparator<ProductLine, ProductLine>(originalProductLineTaskList, x => x.ProductLineId, iMainTask.ProductLines, x => x.ProductLineId);

                //Add
                foreach (var productLineItem in productLineComparator.NewList.Enum())
                {
                    var productLineTaskEntity = new T_E_ProductLineTask();
                    productLineTaskEntity.ProductLineTaskId = -1;
                    productLineTaskEntity.MainTaskId = iMainTask.MainTaskId;
                    productLineTaskEntity.ProductLineId = productLineItem.ProductLineId;
                    DBProductDataService.Add<T_E_ProductLineTask>(productLineTaskEntity);
                }

                //Delete
                foreach (var productLineItem in productLineComparator.RemovedList.Enum())
                {
                    var productLineTaskEntity = DBProductDataService.GetSingleOrDefault<T_E_ProductLineTask>(x => x.MainTaskId == iMainTask.MainTaskId && x.ProductLineId == productLineItem.ProductLineId);
                    DBReleaseDataService.DeleteProductLineTask(productLineTaskEntity.ProductLineTaskId);
                }

                ts.Complete();
            }
        }

        public void AcceptMainTaskRequest(long iMainTaskId)
        {
            if (iMainTaskId < 1) throw new Exception("L'id de la tâche est invalide");

            var originalTask = GetMainTaskById(iMainTaskId, GranularityEnum.Full);

            if (originalTask.Status != MainTaskStatusEnum.Requested)
                throw new Exception("Le statut de la tâche n'est plus 'Demande'");

            UpdateMainTaskStatus(originalTask, MainTaskStatusEnum.Waiting);
        }

        public void CancelMainTask(long iMainTaskId)
        {
            if (iMainTaskId < 1) throw new Exception("L'id de la tâche est invalide");

            var originalTask = GetMainTaskById(iMainTaskId, GranularityEnum.Full);

            if (originalTask.Status == MainTaskStatusEnum.Canceled)
                throw new Exception("La tâche est déjà annulée");

            if (originalTask.SubTasks.Exists(x => x.Progression != 0))
                throw new Exception("La tâche contient des sous-tâches avec un avancement différent de 0. Il n'est pas possible de l'annuler");

            UpdateMainTaskStatus(originalTask, MainTaskStatusEnum.Canceled);
        }

        public void MoveMainTaskToStaging(long iMainTaskId)
        {
            if (iMainTaskId < 1) throw new Exception("L'id de la tâche est invalide");

            var originalTask = GetMainTaskById(iMainTaskId, GranularityEnum.Full);

            if (originalTask.Status == MainTaskStatusEnum.Staging)
                throw new Exception("La tâche est déjà en test");

            if (originalTask.SubTasks.Exists(x => x.Progression != 100))
                throw new Exception("La tâche contient des sous-tâches avec un avancement différent de 100. Il n'est pas possible de passer en test");

            UpdateMainTaskStatus(originalTask, MainTaskStatusEnum.Staging);
        }

        public void MoveMainTaskToProduction(long iMainTaskId)
        {
            if (iMainTaskId < 1) throw new Exception("L'id de la tâche est invalide");

            var originalTask = GetMainTaskById(iMainTaskId, GranularityEnum.Full);

            if (originalTask.Status != MainTaskStatusEnum.Staging)
                throw new Exception("Seul une tâche en préprod peut aller en production");

            if (originalTask.Status == MainTaskStatusEnum.Completed)
                throw new Exception("La tâche est déjà en production");

            if (originalTask.SubTasks.Exists(x => x.Progression != 100))
                throw new Exception("La tâche contient des sous-tâches avec un avancement différent de 100. Il n'est pas possible de passer en production");

            UpdateMainTaskStatus(originalTask, MainTaskStatusEnum.Completed);
        }

        public void MoveUpMainTaskPriority(MainTask iMainTask)
        {
            if (iMainTask == null)
                throw new Exception("La tâche est nulle");

            if (iMainTask.Priority != null)
            {
                if (iMainTask.Priority < 0)
                    throw new Exception("La priorité doit être positive");

                if (iMainTask.Priority == 1)
                    throw new Exception("La priorité est déjà maximale");
            }

            using (var ts = new TransactionScope())
            {
                var theTask = DBReleaseDataService.GetMainTaskById(iMainTask.MainTaskId);

                int? thePriority = 1;
                T_E_MainTask previousTask = null;

                if (theTask.Priority != null)
                {
                    thePriority = theTask.Priority - 1;
                    previousTask = DBReleaseDataService.GetSingleOrDefault<T_E_MainTask>(x => x.Priority == iMainTask.Priority - 1);
                }
                else
                {
                    previousTask = DBReleaseDataService.GetSingleOrDefault<T_E_MainTask>(x => x.Priority == 1);

                    var taskWithPriority = DBReleaseDataService.GetList<T_E_MainTask>(x => x.Priority != null && x.Priority != 1).Enum().OrderByDescending(x => x.Priority).Enum().Select(x => x.Convert()).Enum().ToList();

                    foreach (var taskPriority in taskWithPriority.Enum())
                        MoveDownMainTaskPriority(taskPriority);
                }

                theTask.Priority = thePriority;
                DBReleaseDataService.Update(theTask);

                if (previousTask != null)
                {
                    previousTask.Priority += 1;
                    DBReleaseDataService.Update(previousTask);
                }

                ts.Complete();
            }
        }

        public void MoveDownMainTaskPriority(MainTask iMainTask)
        {
            if (iMainTask == null)
                throw new Exception("La tâche est nulle");

            if (iMainTask.Priority < 0)
                throw new Exception("La priorité doit être positive");

            using (var ts = new TransactionScope())
            {
                var theTask = DBReleaseDataService.GetMainTaskById(iMainTask.MainTaskId);

                T_E_MainTask followingTask = null;
                int? thePriority;

                if (theTask.Priority != null)
                {
                    thePriority = theTask.Priority + 1;
                    followingTask = DBReleaseDataService.GetSingleOrDefault<T_E_MainTask>(x => x.Priority == iMainTask.Priority + 1);
                }
                else
                {
                    var lastPriority = DBReleaseDataService.GetQuery<T_E_MainTask>(null).Max(x => x.Priority);
                    if (lastPriority == null)
                        thePriority = 1;
                    else
                        thePriority = lastPriority + 1;
                }

                theTask.Priority = thePriority;
                DBReleaseDataService.Update(theTask);

                if (followingTask != null)
                {
                    followingTask.Priority -= 1;
                    DBReleaseDataService.Update(followingTask);
                }

                ts.Complete();
            }
        }

        public void SetTaskPriority(MainTask iMainTask, int iNewPriority)
        {
            if (iMainTask == null)
                throw new Exception("La tâche est nulle");

            if (iMainTask.Priority < 0)
                throw new Exception("La priorité doit être positive");

            if (iMainTask.Priority == iNewPriority)
                return;

            using (var ts = new TransactionScope())
            {
                var theTask = DBReleaseDataService.GetMainTaskById(iMainTask.MainTaskId);

                List<T_E_MainTask> upPriorityTasks = null;
                if (iNewPriority < theTask.Priority)
                    upPriorityTasks = DBReleaseDataService.GetList<T_E_MainTask>(null).Where(x => x.Priority >= iNewPriority).ToList();
                else
                    upPriorityTasks = DBReleaseDataService.GetList<T_E_MainTask>(null).Where(x => x.Priority > iNewPriority).ToList();

                //enleve le package concerné si présent
                upPriorityTasks.RemoveAll(x => x.MainTaskId == theTask.MainTaskId);

                //incrémente toutes les priorités
                foreach (var item in upPriorityTasks)
                {
                    item.Priority = item.Priority + 1;
                    DBReleaseDataService.Update(item);
                }

                //the package
                theTask.Priority = iNewPriority;
                DBReleaseDataService.Update(theTask);

                //Nettoyage des trous
                FillGapTaskPriority();

                ts.Complete();
            }
        }

        public void FillGapTaskPriority()
        {
            var taskList = DBReleaseDataService.GetQuery<T_E_MainTask>(null).Where(x => x.Priority != null).ToList().Enum().OrderBy(x => x.Priority).Enum().ToList();

            var priorityCounter = 1;

            using (var ts = new TransactionScope())
            {
                //boucle sur chaque tâche pour attribuer la bonne priorité
                foreach (var item in taskList.Enum())
                {
                    if (item.Priority != priorityCounter)
                    {
                        item.Priority = priorityCounter;
                        DBReleaseDataService.Update(item);
                    }

                    priorityCounter++;
                }
                ts.Complete();
            }
        }

        public MainTask GetMainTaskById(long iMainTaskId, GranularityEnum iGranularity)
        {
            if (iMainTaskId < 1) throw new Exception("L'id de la tâche est invalide");

            //MainTask
            var entity = DBReleaseDataService.GetMainTaskById(iMainTaskId);
            var theMainTask = entity.Convert();
            if (theMainTask == null) return null;

            if (iGranularity == GranularityEnum.Full)
            {
                //SubTask
                theMainTask.SubTasks = GetSubTaskByMainTaskId(iMainTaskId);

                //Package
                if (theMainTask.PackageId != null)
                    theMainTask.Package = GetPackageById((long)theMainTask.PackageId, GranularityEnum.Full);

                //ProductLine
                theMainTask.ProductLines = GetProductLineByMainTaskId(theMainTask.MainTaskId);

                //ExternalProject
                if (theMainTask.ExternalProjectId != null)
                    theMainTask.ExternalProject = GetExternalProject((long)theMainTask.ExternalProjectId);
            }

            return theMainTask;
        }

        public List<MainTask> GetMaintaskListByPackageId(long iPackageId)
        {
            return DBReleaseDataService.GetQuery<T_E_MainTask>(null).Where(x => x.PackageId == iPackageId).Enum().Select(x => x.Convert()).Enum().ToList();
        }

        public Tuple<List<MainTask>, int> GetMainTaskList(MainTaskStatusSearchEnum iMainTasksSearchEnum, MainTaskOrderByEnum iOrderBy, Guid? iProjectGUID, long? iProductLineId, MainTaskTypeEnum? iMainTaskType,Guid? iDevelopperGuid,long? iPackageId, int iSkip, int iTake, GranularityEnum iGranularity, long? iExternalProjectId)
        {
            if (iTake < 1) throw new Exception("Le nombre à prendre est invalide");

            //Choix de la table
            IQueryable<T_E_MainTask> theQuery = DBReleaseDataService.GetQuery<T_E_MainTask>(null);

            //Statut
            if (iMainTasksSearchEnum != MainTaskStatusSearchEnum.All)
            {
                if (iMainTasksSearchEnum == MainTaskStatusSearchEnum.Canceled)
                    theQuery = theQuery.Where(x => x.StatusRef == (short)MainTaskStatusEnum.Canceled);
                else if (iMainTasksSearchEnum == MainTaskStatusSearchEnum.Completed)
                    theQuery = theQuery.Where(x => x.StatusRef == (short)MainTaskStatusEnum.Completed);
                else if (iMainTasksSearchEnum == MainTaskStatusSearchEnum.InProgress)
                    theQuery = theQuery.Where(x => x.StatusRef == (short)MainTaskStatusEnum.Dev || x.StatusRef == (short)MainTaskStatusEnum.Staging);
                else if (iMainTasksSearchEnum == MainTaskStatusSearchEnum.Request)
                    theQuery = theQuery.Where(x => x.StatusRef == (short)MainTaskStatusEnum.Requested);
                else if (iMainTasksSearchEnum == MainTaskStatusSearchEnum.Waiting)
                    theQuery = theQuery.Where(x => x.StatusRef == (short)MainTaskStatusEnum.Waiting);
                else if (iMainTasksSearchEnum == MainTaskStatusSearchEnum.NotCompleted)
                    theQuery = theQuery.Where(x => x.StatusRef != (short)MainTaskStatusEnum.Completed && x.StatusRef != (short)MainTaskStatusEnum.Canceled);
                else
                    throw new NotSupportedException(iMainTasksSearchEnum.ToStringWithEnumName());
            }

            //Gamme,
            if (iProductLineId != null)
            {
                theQuery = theQuery.Where(x => x.T_E_ProductLineTask.Any(y => y.ProductLineId == (long)iProductLineId));
            }

            //Project
            if (iProjectGUID != null)
            {
                theQuery = theQuery.Where(x => x.T_E_SubTask.Any(y => y.ProjectGUID == iProjectGUID));
            }

            //Tasktype
            if (iMainTaskType != null)
            {
                theQuery = theQuery.Where(x => x.TaskTypeRef == (short)iMainTaskType);
            }

            //Developper
            if(iDevelopperGuid != null)
            {
                theQuery = theQuery.Where(x => x.T_E_SubTask.Any(y=>y.DevelopperGUID == iDevelopperGuid));              
            }

            //Package
            if (iPackageId != null)
            {
                theQuery = theQuery.Where(x => x.PackageId == iPackageId);
            }

            //External project
            if (iExternalProjectId != null)
            {
                theQuery = theQuery.Where(x => x.ExternalProjectId == iExternalProjectId);
            }

            var totalCount = theQuery.Count();
            List<T_E_MainTask> entities;

            if (iOrderBy == MainTaskOrderByEnum.MainTaskId)
                entities = theQuery.OrderBy(x => x.MainTaskId).Skip(iSkip).Take(iTake).ToList();
            else if (iOrderBy == MainTaskOrderByEnum.DateObjectif)
                entities = theQuery.OrderByDescending(x => x.ObjectifCloseDate.HasValue).ThenBy(x => x.ObjectifCloseDate).Skip(iSkip).Take(iTake).ToList();
            else if (iOrderBy == MainTaskOrderByEnum.TaskPriority)
                entities = theQuery.OrderByDescending(x => x.Priority.HasValue).ThenBy(x => x.Priority).Skip(iSkip).Take(iTake).ToList();
            else if (iOrderBy == MainTaskOrderByEnum.PackagePriority)
                entities = theQuery.OrderByDescending(x => x.T_E_Package.Priority.HasValue).ThenBy(x => x.T_E_Package.Priority).Skip(iSkip).Take(iTake).ToList();
            else if (iOrderBy == MainTaskOrderByEnum.ProjectNumber)
                entities = theQuery.OrderByDescending(x => x.T_E_ExternalProject.ProjectNumber).Skip(iSkip).Take(iTake).ToList();
            else if (iOrderBy == MainTaskOrderByEnum.CreationDate)
                entities = theQuery.OrderByDescending(x => x.CreationDate).Skip(iSkip).Take(iTake).ToList();
            else if (iOrderBy == MainTaskOrderByEnum.ProductionDeployementDate)
            {
                var environmentProductionShort = (short)EquinoxeExtend.Shared.Enum.EnvironmentEnum.Production;
                entities = theQuery.OrderByDescending(x => x.T_E_Package.T_E_Deployement.Where(y => y.EnvironmentDestinationRef == environmentProductionShort).OrderByDescending(t => t.DeployementDate).FirstOrDefault().DeployementDate).Skip(iSkip).Take(iTake).ToList();
            }              
            else
                throw new NotSupportedException(iOrderBy.ToStringWithEnumName());

            if (entities.IsNotNullAndNotEmpty())
            {
                var result = new List<MainTask>();

                if (iGranularity == GranularityEnum.Full)
                {
                    foreach (var mainTaskItem in entities)
                        result.Add(GetMainTaskById(mainTaskItem.MainTaskId, iGranularity));
                }
                else if (iGranularity == GranularityEnum.Nude)
                {
                    result = entities.Select(x => x.Convert()).ToList();
                }

                return new Tuple<List<MainTask>, int>(result, totalCount);
            }
            return null;
        }

        public List<SubTask> GetOpenedSubTasks()
        {
            var projectTasksList = new List<SubTask>();

            foreach (var mainTask in GetOpenedMainTasks(GranularityEnum.Full).Enum())
                projectTasksList.AddRange(GetMainTaskById(mainTask.MainTaskId, GranularityEnum.Full).SubTasks.Enum());

            return projectTasksList;
        }

        public List<SubTask> GetDevSubTasks()
        {
            var projectTasksList = new List<SubTask>();

            foreach (var mainTask in GetDevMainTasks(GranularityEnum.Full).Enum())
                projectTasksList.AddRange(GetMainTaskById(mainTask.MainTaskId, GranularityEnum.Full).SubTasks.Enum());

            return projectTasksList;
        }

        public List<MainTask> GetOpenedMainTasks(GranularityEnum iGranularity)
        {
            var query = DBReleaseDataService.GetQuery<T_E_MainTask>(null).Where(x => x.StatusRef == (short)MainTaskStatusEnum.Dev || x.StatusRef == (short)MainTaskStatusEnum.Staging).Enum().ToList();
            return query.Select(x => GetMainTaskById(x.MainTaskId,iGranularity)).Enum().ToList();
        }

        public List<MainTask> GetDevMainTasks(GranularityEnum iGranularity)
        {
            var query = DBReleaseDataService.GetQuery<T_E_MainTask>(null).Where(x => x.StatusRef == (short)MainTaskStatusEnum.Dev).Enum().ToList();
            return query.Select(x => GetMainTaskById(x.MainTaskId, iGranularity)).Enum().ToList();
        }

        #endregion

        #region Private METHODS

        private void UpdateMainTaskStatus(MainTask iMainTask, MainTaskStatusEnum iNewStatus)
        {
            if (iMainTask == null) throw new Exception("La tâche principale est null");
            if (iMainTask.MainTaskId < 1) throw new Exception("L'id de la tâche est invalide");

            var originalMainTask = GetMainTaskById(iMainTask.MainTaskId, GranularityEnum.Full);

            //Validation workflow status
            if (originalMainTask.Status == MainTaskStatusEnum.Requested
                && iNewStatus != MainTaskStatusEnum.Waiting && iNewStatus != MainTaskStatusEnum.Canceled)
            {
                throw new Exception("Le changement de status n'est pas permis");
            }
            else if (originalMainTask.Status == MainTaskStatusEnum.Waiting
                && iNewStatus != MainTaskStatusEnum.Dev && iNewStatus != MainTaskStatusEnum.Canceled)
            {
                throw new Exception("Le changement de status n'est pas permis");
            }
            else if (originalMainTask.Status == MainTaskStatusEnum.Dev
               && iNewStatus != MainTaskStatusEnum.Staging && iNewStatus != MainTaskStatusEnum.Completed && iNewStatus != MainTaskStatusEnum.Canceled)
            {
                throw new Exception("Le changement de status n'est pas permis");
            }
            else if (originalMainTask.Status == MainTaskStatusEnum.Staging
              && iNewStatus != MainTaskStatusEnum.Dev && iNewStatus != MainTaskStatusEnum.Completed && iNewStatus != MainTaskStatusEnum.Canceled)
            {
                throw new Exception("Le changement de status n'est pas permis");
            }
            else if (originalMainTask.Status == MainTaskStatusEnum.Completed)
            {
                throw new Exception("Le changement de status n'est pas permis");
            }
            else if (originalMainTask.Status == MainTaskStatusEnum.Canceled && iNewStatus != MainTaskStatusEnum.Waiting)
            {
                throw new Exception("Le changement de status n'est pas permis pour les tâches annulées");
            }

            //CONDITION DE CHANGEMENT DE STATUS
            //Demande
            if (iNewStatus == MainTaskStatusEnum.Requested)
            {
                throw new Exception("Ce status n'est pas possible en modification");
            }
            //En cours
            else if (iNewStatus == MainTaskStatusEnum.Dev)
            {
                //package obligatoire
                if (originalMainTask.PackageId == null)
                    throw new Exception("Le package est obligatoire pour ouvrir la tâche {0}".FormatString(originalMainTask.MainTaskIdString));

                //Gamme
                if (originalMainTask.ProductLines.IsNullOrEmpty())
                    throw new Exception("La gamme est obligatoire pour ouvrir la tâche {0}".FormatString(originalMainTask.MainTaskIdString));

                //Project
                if (originalMainTask.SubTasks.IsNullOrEmpty())
                    throw new Exception("La définition des sous tâches est obligatoire pour ouvrir la tâche {0}".FormatString(originalMainTask.MainTaskIdString));

                //Vérification qu'il y a pas d'autre
                if (IsProjectTaskAlreadyInProgress(originalMainTask))
                    throw new Exception("Cette tâche ne peut pas être ouverte car un projet est déjà utilisé sur une tâche ouverte");
            }
            //Terminée
            else if (iNewStatus == MainTaskStatusEnum.Completed)
            {
                //Sous tâche obligatoire
                if (!originalMainTask.SubTasks.Any())
                    throw new Exception("La tâche ne peut pas être complété sans sous tâches");

                //Progression de toutes les sous-tâches doivent être à 100%
                var sumProgression = decimal.Divide(originalMainTask.SubTasks.Enum().Sum(x => x.Progression), originalMainTask.SubTasks.Count);
                if (sumProgression != 100)
                    throw new Exception("Toutes les tâches doivent être terminées à 100%");
            }
            //Test
            else if (iNewStatus == MainTaskStatusEnum.Staging)
            {
                //Sous tâche obligatoire
                if (!originalMainTask.SubTasks.Any())
                    throw new Exception("La tâche ne peut pas être testé sans sous tâches");

                //Progression de toutes les sous-tâches doivent être à 100%
                var sumProgression = decimal.Divide(originalMainTask.SubTasks.Enum().Sum(x => x.Progression), originalMainTask.SubTasks.Count);
                if (sumProgression != 100)
                    throw new Exception("Toutes les tâches doivent être terminées à 100%");
            }

            //Modification
            if (iNewStatus == MainTaskStatusEnum.Dev)
            {
                originalMainTask.OpenedDate = DateTime.Now;
            }
            else if (iNewStatus == MainTaskStatusEnum.Staging)
            {
                originalMainTask.Priority = null;
            }
            else if (iNewStatus == MainTaskStatusEnum.Completed)
            {
                originalMainTask.CompletedDate = DateTime.Now;
                originalMainTask.Priority = null;
            }
            else if (iNewStatus == MainTaskStatusEnum.Canceled)
            {
                originalMainTask.Priority = null;
                originalMainTask.PackageId = null;
            }

            //Application du nouveau status
            var entity = new T_E_MainTask();
            originalMainTask.Status = iNewStatus;
            entity.Merge(originalMainTask);
            DBReleaseDataService.UpdateMainTask(entity);
        }

        private bool IsProjectTaskAlreadyInProgress(MainTask iMainTask)
        {
            //Package en cours
            var inProgressPackages = GetPackageList(PackageStatusSearchEnum.InProgress, PackageOrderByEnum.PackageId).Enum().Where(x => x.PackageId != iMainTask.PackageId).Enum().ToList();

            //Main task en cours sans iMaintask
            var inProgressMainTasks = new List<T_E_MainTask>();
            var inProgressProjectTasks = new List<SubTask>();
            foreach (var item in inProgressPackages.Enum())
                inProgressProjectTasks.AddRange(item.SubTasks.Enum().Where(x => x.MainTaskId != iMainTask.MainTaskId).Enum().ToList());

            //Bouclage sur les packages
            foreach (var packageSubTaskItem in inProgressProjectTasks.Enum())
            {
                foreach (var openedSubTaskItem in iMainTask.SubTasks.Enum())
                {
                    if (openedSubTaskItem.ProjectGUID == packageSubTaskItem.ProjectGUID)
                        return true;
                }
            }

            return false;
        }

        #endregion
    }
}