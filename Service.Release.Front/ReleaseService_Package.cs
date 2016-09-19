using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
using Library.Tools.Enums;
using Library.Tools.Extensions;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Release.Front
{
    public partial class ReleaseService
    {
        #region Public METHODS

        public long AddPackage()
        {
            var newPackage = new Package();
            newPackage.PackageId = -1;
            newPackage.ReleaseNumber = null;
            newPackage.IsLocked = true;
            newPackage.Status = PackageStatusEnum.Waiting;
            var newEntity = new T_E_Package();
            newEntity.Merge(newPackage);
            return DBReleaseDataService.AddPackage(newEntity);
        }

        public void UpdatePackage(Package iPackage)
        {
            if (iPackage == null)
                throw new Exception("Le package est null");

            var originalPackage = GetPackageById(iPackage.PackageId, GranularityEnum.Nude);
            if (originalPackage == null)
                throw new Exception("Le package est null");

            if (iPackage.Status != originalPackage.Status)
                throw new Exception("La fonction n'est pas supportée pour le changement de statut");

            var entity = new T_E_Package();
            entity.Merge(iPackage);
            DBReleaseDataService.UpdatePackage(entity);
        }

        public void OpenPackage(Package iPackage)
        {
            if (iPackage == null)
                throw new Exception("Le package est null");

            var originalPackage = GetPackageById(iPackage.PackageId, GranularityEnum.Full);

            if (originalPackage == null)
                throw new Exception("Le package est null");

            //Vérification qu'il n'y a pas de conflit avec les projets ouverts
            var conflicProjects = IsPackageCanBeOpen(iPackage.PackageId);
            if (conflicProjects.IsNotNullAndNotEmpty())
                throw new Exception("Le package ne peut pas être ouvert car contient des projets actuellement ouvert");

            //Vérification des status des tâches
            if (originalPackage.MainTasks.Enum().Exists2(x => x.Status != MainTaskStatusEnum.Waiting))
                throw new Exception("Toutes les tâches de ce packages doivent 'En attente'");

            using (var ts = new System.Transactions.TransactionScope())
            {
                //Changement sur le package
                UpdatePackageStatus(originalPackage, PackageStatusEnum.Developpement);

                //Ouverture des tâches associées
                foreach (var mainTaskItem in originalPackage.MainTasks.Enum())
                    UpdateMainTaskStatus(mainTaskItem, MainTaskStatusEnum.InProgress);

                ts.Complete();
            }
        }

        public Package GetPackageById(long iPackageId, GranularityEnum iGranularity)
        {
            if (iPackageId < 1) throw new Exception("L'id du package n'est pas valide");

            var thePackage = DBReleaseDataService.GetPackageById(iPackageId).Convert();

            if (iGranularity == GranularityEnum.Full)
            {
                //Deployement
                thePackage.Deployements = GetDeployementByPackageId(iPackageId);

                //MainTask
                thePackage.MainTasks = GetMaintaskListByPackageId(iPackageId).Enum().Where(x => x.Status != MainTaskStatusEnum.Canceled).Enum().ToList();

                //ProjectTask
                thePackage.SubTasks = new List<SubTask>();
                foreach (var mainTaskItem in thePackage.MainTasks.Enum())
                {
                    mainTaskItem.SubTasks = GetSubTaskByMainTaskId(mainTaskItem.MainTaskId);
                    thePackage.SubTasks.AddRange(mainTaskItem.SubTasks);
                }
            }

            return thePackage;
        }

        public List<Package> GetPackageList(PackageStatusSearchEnum iPackageEnvironmentSearch)
        {
            var theQuery = DBReleaseDataService.GetQuery<T_E_Package>(null);

            if (iPackageEnvironmentSearch == PackageStatusSearchEnum.Developpement)
            {
                theQuery = theQuery.Where(x => x.StatusRef == (short)PackageStatusEnum.Developpement).AsQueryable();
            }
            else if (iPackageEnvironmentSearch == PackageStatusSearchEnum.Production)
            {
                theQuery = theQuery.Where(x => x.StatusRef == (short)PackageStatusEnum.Production).AsQueryable();
            }
            else if (iPackageEnvironmentSearch == PackageStatusSearchEnum.Quality)
            {
                theQuery = theQuery.Where(x => x.StatusRef == (short)PackageStatusEnum.Test).AsQueryable();
            }
            else if (iPackageEnvironmentSearch == PackageStatusSearchEnum.InProgress)
            {
                theQuery = theQuery.Where(x => x.StatusRef == (short)PackageStatusEnum.Developpement
                || x.StatusRef == (short)PackageStatusEnum.Test).AsQueryable();
            }
            else if (iPackageEnvironmentSearch == PackageStatusSearchEnum.All)
            {
                //ne filtre rien
            }
            else if (iPackageEnvironmentSearch == PackageStatusSearchEnum.NotCompleted)
            {
                theQuery = theQuery.Where(x => x.StatusRef == (short)PackageStatusEnum.Developpement
                || x.StatusRef == (short)PackageStatusEnum.Test
                || x.StatusRef == (short)PackageStatusEnum.Waiting).AsQueryable();
            }
            else
                throw new Exception(iPackageEnvironmentSearch.ToStringWithEnumName());

            var packageList = theQuery.Enum().ToList();
            var result = new List<Package>();

            foreach (var item in packageList.Enum())
                result.Add(GetPackageById(item.PackageId, GranularityEnum.Full));

            return result;
        }

        public List<Package> GetAllowedPackagesForMainTask(MainTask iMainTask)
        {
            var result = new List<Package>();
            //Récupération des packages non ouvert déverrouillé
            var unopenUnlockPackages = DBReleaseDataService.GetQuery<T_E_Package>(null).Where(x => !x.IsLocked && x.StatusRef == (short)PackageStatusEnum.Waiting).Enum().ToList().Select(x => GetPackageById(x.PackageId, GranularityEnum.Full)).Enum().ToList();
            result.AddRange(unopenUnlockPackages.Enum());

            if (iMainTask != null)
            {
                //Récupération des packages ouvert déverrouillé
                var openUnlockPackages = DBReleaseDataService.GetQuery<T_E_Package>(null).Where(x => !x.IsLocked && x.StatusRef == (short)PackageStatusEnum.Developpement).Enum().ToList().Select(x => GetPackageById(x.PackageId, GranularityEnum.Full)).Enum().ToList();

                //Garde les packages ouvert dont les projet sont commun
                var packageWithCommonProject = new List<Package>();
                foreach (var item in openUnlockPackages.Enum())
                {
                    if (item.SubTasks.Exists(x => iMainTask.SubTasks.Exists(y => y.ProjectGUID == x.ProjectGUID)))
                        packageWithCommonProject.Add(item);
                }

                //Si un seul projet alors seule solution
                if (packageWithCommonProject.Count == 1)
                    result.Add(packageWithCommonProject.Single());
                //Si aucun package de projet en commun alors tous les projets sont possibles
                else if (packageWithCommonProject.Count == 0)
                    result.AddRange(openUnlockPackages.Enum());
            }
            return result;
        }

        public bool IsMainTaskCanJoinThisPackage(long iMainTaskId, long iPackageId)
        {
            var theMainTask = GetMainTaskById(iMainTaskId, GranularityEnum.Full);
            return GetAllowedPackagesForMainTask(theMainTask).Exists(x => x.PackageId == iPackageId);
        }

        /// <summary>
        /// Retourne la liste des tâches en conflit avec le projet
        /// </summary>
        /// <param name="iPackageId"></param>
        /// <returns></returns>
        public List<SubTask> IsPackageCanBeOpen(long iPackageId)
        {
            var result = new List<SubTask>();

            var thePackage = GetPackageById(iPackageId, GranularityEnum.Full);
            if (thePackage == null)
                throw new Exception("Le package est null");

            var packageSubTasks = thePackage.SubTasks.Enum().Where(x => x.ProjectGUID != null).Enum().ToList();
            var openedSubTasks= GetOpenedTask().Where(x=>x.ProjectGUID != null).Enum().ToList();

            //Bouclage sur les packages
            foreach(var packageSubTaskItem in packageSubTasks.Enum())
            {
                foreach(var openedSubTaskItem in openedSubTasks.Enum())
                {
                    if (openedSubTaskItem.ProjectGUID == packageSubTaskItem.ProjectGUID)
                        result.Add(packageSubTaskItem);
                }
            }

            return result;      
        }

        public bool IsProjectCanJoinThisMainTask(long iMainTaskId, Guid iProjectGuid)
        {
            var theMainTask = GetMainTaskById(iMainTaskId, GranularityEnum.Full);
            if (theMainTask == null)
                throw new Exception("La tâche est null");

            if(theMainTask.Status == MainTaskStatusEnum.InProgress)
            {
                var openedPackages = GetPackageList(PackageStatusSearchEnum.InProgress);

                //Bouclage sur les packages
                foreach (var packageItem in openedPackages.Enum())
                {
                    if (theMainTask.PackageId != null)
                        if (packageItem.PackageId == theMainTask.PackageId)
                            continue;

                    if (packageItem.SubTasks.Exists2(x => x.ProjectGUID == iProjectGuid))
                        return false;
                }
            }

            return true;
        }

        #endregion

        #region Private METHODS

        private void UpdatePackageStatus(Package iPackage, PackageStatusEnum iNewPackageStatus)
        {
            if (iPackage == null)
                throw new Exception("Le package est null");

            var originalPackage = GetPackageById(iPackage.PackageId, GranularityEnum.Nude);
            if (originalPackage == null)
                throw new Exception("Le package est null");

            if (iPackage.Status != originalPackage.Status)
                throw new Exception("Le status en base de données est différent, veuillez recharger les packages");

            //Vérification workflow
            if (iPackage.Status == PackageStatusEnum.Waiting
                && iNewPackageStatus != PackageStatusEnum.Developpement
                 && iNewPackageStatus != PackageStatusEnum.Canceled)
                throw new Exception("Ce changement de statut n'est pas permis");
            else if (iPackage.Status == PackageStatusEnum.Developpement
                && iNewPackageStatus != PackageStatusEnum.Test
                 && iNewPackageStatus != PackageStatusEnum.Canceled)
                throw new Exception("Ce changement de statut n'est pas permis");
            else if (iPackage.Status == PackageStatusEnum.Test
                && iNewPackageStatus != PackageStatusEnum.Production
                 && iNewPackageStatus != PackageStatusEnum.Canceled)
                throw new Exception("Ce changement de statut n'est pas permis");

            //Condition  de changement
            if (iNewPackageStatus == PackageStatusEnum.Waiting)
            {
                throw new Exception("Ce changementde statut n'est pas possible");
            }
            else if (iNewPackageStatus == PackageStatusEnum.Developpement)
            {
                //Rien à faire
            }
            else if (iNewPackageStatus == PackageStatusEnum.Test)
            {
                throw new NotSupportedException("Pour l'instant");
            }
            else if (iNewPackageStatus == PackageStatusEnum.Production)
            {
                throw new NotSupportedException("Pour l'instant");
            }
            else
                throw new NotSupportedException(iPackage.Status.ToStringWithEnumName());

            //Modification du status
            var entity = new T_E_Package();
            originalPackage.Status = iNewPackageStatus;
            entity.Merge(originalPackage);
            DBReleaseDataService.UpdatePackage(entity);
        }

        #endregion
    }
}
