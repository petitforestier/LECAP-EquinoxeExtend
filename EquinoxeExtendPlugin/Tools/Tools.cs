using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper;
using DriveWorks.Forms;
using DriveWorks.Helper.Manager;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
using Library.Tools.Attributes;
using Library.Tools.Comparator;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveWorks.Helper.Object;
using DriveWorks.Specification;
using Library.Tools.Tasks;
using DriveWorks.Hosting;
using System.IO;

namespace EquinoxeExtendPlugin.Tools
{
    public class Tools
    {
        #region Public METHODS

        public Tools(Action<string> iNotifierAction)
        {
            notifierAction = iNotifierAction;
        }

        public Tools()
        {
        }

        private Action<string> notifierAction;

        private void ReportProgress(string iMessage)
        {
            if (notifierAction != null)
                notifierAction(iMessage);
        }

        public static void ReleaseProjectsRights(Group iGroup)
        {
            using (var releaseService = new Service.Release.Front.ReleaseService(EnvironmentEnum.Developpement.GetSQLExtendConnectionString()))
            {
                var devSubTasks = releaseService.GetDevSubTasks();

                var devProjects = devSubTasks.Enum().Where(x => x.ProjectGUID != null).Enum().Select(x => (Guid)x.ProjectGUID).Enum().ToList();

                //Supprime les doublons
                devProjects = devProjects.Enum().GroupBy(x => x).Enum().Select(x => x.First()).Enum().ToList();

                //Applique les droits seulement sur ces projets ouverts
                iGroup.SetExclusitivelyPermissionToTeam(iGroup.Security.GetTeams().Single(x => x.DisplayName == iGroup.GetEnvironment().GetDevelopperTeam()), devProjects);
            }
        }

        public static void ThrowExceptionIfCurrentUserIsNotAdmin(Group iGroup)
        {
            if (!IsAdminCurrentUser(iGroup))
                throw new Exception("Seul le login Admin peut effectuer cette action");
        }

        public static bool IsAdminCurrentUser(Group iGroup)
        {
            return (iGroup.CurrentUser.LoginName == iGroup.GetEnvironment().GetLoginPassword().Item1);
        }

        public List<Tuple<string, ProjectDetails, ImportedDataTable>> GetImportedDataTableFromPackage(EnvironmentEnum iSourceEnvironment, EnvironmentEnum iDestinationEnvironment, Package iPackageDeployToStaging)
        {
            var result = new List<Tuple<string, ProjectDetails, ImportedDataTable>>();

            var host = new EngineHost(HostEnvironment.CreateDefaultEnvironment(false));
            var sourceGroupManager = host.CreateGroupManager();
            var sourceProjectManager = host.CreateProjectManager();
            var sourceGroup = sourceGroupManager.OpenGroup(iSourceEnvironment);

            var packageDistinctProjectGUIDList = iPackageDeployToStaging.SubTasks.Where(x => x.ProjectGUID != null).GroupBy(x => x.ProjectGUID).Select(x => (Guid?)x.First().ProjectGUID).ToList();
            var packageDistinctProjectDetailsList = new List<ProjectDetails>();
            foreach (var item in packageDistinctProjectGUIDList)
            {
                if (item != null)
                    packageDistinctProjectDetailsList.Add(sourceGroup.Projects.GetProject((Guid)item));                   
            }

            //Bouclage sur les projets source inclus dans le package
            var projectDevCounter = 1;
            foreach (var projectItem in packageDistinctProjectDetailsList.Enum())
            {
                var message = "Récupération des tables dev du package, Projet {0}/{1} : {2}".FormatString(projectDevCounter, packageDistinctProjectDetailsList.Count(), projectItem.Name);
                ReportProgress(message);
                sourceProjectManager.OpenProject(sourceGroup, projectItem);
                var project = sourceProjectManager.Project;

                var importedDataTables = project.GetImportedDataTableList();
                foreach (var tableItem in importedDataTables.Enum())
                    result.Add(new Tuple<string, ProjectDetails, ImportedDataTable>(iSourceEnvironment.GetName("FR"), projectItem, tableItem));

                sourceProjectManager.CloseProject(false);
                projectDevCounter++;
            }

            //fermeture du groupe
            sourceGroupManager.CloseGroup();

            //Récupère les projets de la destination non impacté par le package
            var destinationGroupManager = host.CreateGroupManager();
            var destinationProjectManager = host.CreateProjectManager();
            var destinationGroup = destinationGroupManager.OpenGroup(iDestinationEnvironment);

            var openedStagingProjectlist = destinationGroup.GetOpenedProjectList();
            if (openedStagingProjectlist.IsNotNullAndNotEmpty())
                throw new Exception("Certains projets du groupe '{0}' sont ouverts. L'analyse n'est donc pas possible.".FormatString(destinationGroup.Name) + Environment.NewLine + Environment.NewLine + openedStagingProjectlist.Select(x => x.Name).Concat(Environment.NewLine));

            var projectStagingComparator = new ListComparator<ProjectDetails, ProjectDetails>(destinationGroup.GetProjectList(), x => x.Name, packageDistinctProjectDetailsList, x => x.Name);

            var projetPreprodCounter = 1;
            foreach (var projectItem in projectStagingComparator.RemovedList.Enum())
            {
                var message = "Récupération des tables préprod du package, Projet {0}/{1} : {2}".FormatString(projetPreprodCounter, projectStagingComparator.RemovedList.Count(), projectItem.Name);
                ReportProgress(message);

                destinationProjectManager.OpenProject(destinationGroup, projectItem);
                var project = destinationProjectManager.Project;

                var importedDataTables = project.GetImportedDataTableList();
                foreach (var tableItem in importedDataTables.Enum())
                    result.Add(new Tuple<string, ProjectDetails, ImportedDataTable>(iDestinationEnvironment.GetName("FR"), projectItem, tableItem));
                destinationProjectManager.CloseProject(false);
                projetPreprodCounter++;
            }

            //fermeture du groupe
            destinationGroupManager.CloseGroup();

            return result;
        }

        public List<ProjectDetails> OrderProjetByChildSpec(EnvironmentEnum iSourceEnvironment, List<ProjectDetails> iOriginalProjectDetailsList)
        {
            var result = new List<ProjectDetails>();

            var host = new EngineHost(HostEnvironment.CreateDefaultEnvironment(false));
            var sourceGroupManager = host.CreateGroupManager();
            var sourceGroup = sourceGroupManager.OpenGroup(iSourceEnvironment);
            
            //Réagencement des projets par les enfants d'abord
            var sourceProjectManager = host.CreateProjectManager();
            var noOrderProjetChildSpecList = new List<KeyValuePair<ProjectDetails, List<ChildSpecificationList>>>();
            foreach (var projectItem in iOriginalProjectDetailsList.Enum())
            {
                sourceProjectManager.OpenProject(sourceGroup, projectItem);
                noOrderProjetChildSpecList.Add(new KeyValuePair<ProjectDetails, List<ChildSpecificationList>>(projectItem, DriveWorks.Helper.Manager.ControlVersionManager.GetProjectChildSpecificationListLists(sourceProjectManager.Project)));
                sourceProjectManager.CloseProject(false);
            }
            
            var previousProjetChildSpecListCount = 0;
            var orderProject = new List<ProjectDetails>();
            //todo revoir
            while (iOriginalProjectDetailsList.Count != orderProject.Count)
            {
                foreach (var projectItem in iOriginalProjectDetailsList.Enum())
                {
                    //Si pas encore présent dans la liste
                    if (!noOrderProjetChildSpecList.Any(x => x.Key.Id == projectItem.Id))
                    {
                        var pairProject = noOrderProjetChildSpecList.Single(x => x.Key.Id == projectItem.Id);

                        //Bouclage sur toutes les child
                        foreach (var childListItem in pairProject.Value.Enum())
                        {
                            //Bouclage sur les projets de la child
                            foreach (var projectChildItem in childListItem.SelectedItem.ItemValues)
                            {
                                var test = "ergerlfk";
                            }
                           }




                    }



                }

                ////Vérification qu'il y a bien une progression pour éviter boucle infinit si référence circulaire
                //if (previousProjetChildSpecListCount == projetChildSpecList.Count)
                //    throw new Exception("Impossible de ranger les projets dans l'ordre des childspec, il y a surment une référence circulaire avec les childspec");

                //previousProjetChildSpecListCount = projetChildSpecList.Count;
            }
            return result;
        }

        public static List<ControlState> SetControlValueListToSpecification(SpecificationContext iContext, List<ControlState> iControlList)
        {
            var errorControlState = new List<ControlState>();

            //Bouclage sur les Etats de controles pour Application de la synthèse
            foreach (var controlStateItem in iControlList.Enum())
            {
                try
                {
                    var theControl = iContext.Project.Navigation.GetControl(controlStateItem.Name);
                    try
                    {
                        if (controlStateItem.Value != null)
                            iContext.Project.SetControlBaseFromControlState(theControl, controlStateItem);
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    throw new Exception("Le controle nommé '" + controlStateItem.Name + "' n'existe pas. Sa suppression n'est pas gérée", ex);
                }
            }

            //Bouclage de vérification car une règle du configurateur pourrait faire une modification après écriture
            foreach (var controlStateItem in iControlList.Enum())
            {
                try
                {
                    var theControl = iContext.Project.Navigation.GetControl(controlStateItem.Name);
                    var controlState = iContext.Project.GetControlStateFormControlBase(theControl);

                    if (controlStateItem.Value != null)
                    {
                        if (controlState.Value != controlStateItem.Value)
                            errorControlState.Add(controlStateItem);
                        else if (controlState.Value2 != controlStateItem.Value2)
                            errorControlState.Add(controlStateItem);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Le controle nommé '" + controlStateItem.Name + "' n'existe pas. Sa suppression n'est pas gérée", ex);
                }
            }

            return errorControlState;
        }
   
        public static bool IsPlugingFolderSame(EnvironmentEnum iSourceEnvironment, EnvironmentEnum iDestinationEnvironment)
        {
            var sourceFolderList = Directory.GetDirectories(iSourceEnvironment.GetPluginDirectory()).Enum().Select(x=>x+ "\\").Enum().ToList();
            sourceFolderList.Remove(iSourceEnvironment.GetPluginDirectoryArchive());
            var sourceFolderNameList = sourceFolderList.Select(x => x.ReplaceStart(iSourceEnvironment.GetPluginDirectory(), "", false, false)).ToList();

            var destinationFolderList = Directory.GetDirectories(iDestinationEnvironment.GetPluginDirectory()).Enum().Select(x => x + "\\").Enum().ToList();
            destinationFolderList.Remove(iDestinationEnvironment.GetPluginDirectoryArchive());
            var destinationFolderNameList = destinationFolderList.Select(x => x.ReplaceStart(iDestinationEnvironment.GetPluginDirectory(), "", false, false)).ToList();

            var comparaison = new Library.Tools.Comparator.ListComparator<string, string>(sourceFolderNameList, x=>x, destinationFolderNameList, x=>x);

            if (comparaison.RemovedList.Count != 0 || comparaison.NewList.Count != 0)
                return false;

            for (int i = 0; i < sourceFolderList.Count; i++)
            {
                if (!Library.Tools.IO.MyDirectory.IsSameContentFolders(sourceFolderList[i], destinationFolderList[i]))
                    return false;
            }

            return true;
        }
        #endregion
    }
}