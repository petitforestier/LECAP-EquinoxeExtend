using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper;
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

        public List<Tuple<string, ProjectDetails, ImportedDataTable>> GetImportedDataTableFromPackage(IApplication iApplication, Package iPackageDeployToStaging)
        {
            var result = new List<Tuple<string, ProjectDetails, ImportedDataTable>>();

            var host = new EngineHost(HostEnvironment.CreateDefaultEnvironment(false));
            var devGroupManager = host.CreateGroupManager();
            var devProjectManager = host.CreateProjectManager();
            var devGroup = devGroupManager.OpenGroup(EnvironmentEnum.Developpement);

            var packageDistinctProjectGUIDList = iPackageDeployToStaging.SubTasks.Where(x => x.ProjectGUID != null).GroupBy(x => x.ProjectGUID).Select(x => (Guid?)x.First().ProjectGUID).ToList();
            var packageDistinctProjectDetailsList = new List<ProjectDetails>();
            foreach (var item in packageDistinctProjectGUIDList)
            {
                if (item != null)
                    packageDistinctProjectDetailsList.Add(devGroup.Projects.GetProject((Guid)item));                   
            }

            //Bouclage sur les projets en dev inclus dans le package
            var projectDevCounter = 1;
            foreach (var projectItem in packageDistinctProjectDetailsList.Enum())
            {
                var message = "Récupération des tables dev du package, Projet {0}/{1} : {2}".FormatString(projectDevCounter, packageDistinctProjectDetailsList.Count(), projectItem.Name);
                ReportProgress(message);
                devProjectManager.OpenProject(devGroup, projectItem);
                var project = devProjectManager.Project;

                var importedDataTables = project.GetImportedDataTableList();
                foreach (var tableItem in importedDataTables.Enum())
                    result.Add(new Tuple<string, ProjectDetails, ImportedDataTable>(EnvironmentEnum.Developpement.GetName("FR"), projectItem, tableItem));

                devProjectManager.CloseProject(false);
                projectDevCounter++;
            }

            //Récupère les projets de préprod non impacté par le package
            var stagingGroupManager = host.CreateGroupManager();
            var stagingProjectManager = host.CreateProjectManager();
            var stagingGroup = stagingGroupManager.OpenGroup(EnvironmentEnum.Staging);

            var openedStagingProjectlist = stagingGroup.GetOpenedProjectList();
            if (openedStagingProjectlist.IsNotNullAndNotEmpty())
                throw new Exception("Certains projets du groupe '{0}' sont ouverts. L'analyse n'est donc pas possible.".FormatString(stagingGroup.Name) + Environment.NewLine + Environment.NewLine + openedStagingProjectlist.Select(x => x.Name).Concat(Environment.NewLine));

            var projectStagingComparator = new ListComparator<ProjectDetails, ProjectDetails>(stagingGroup.GetProjectList(), x => x.Name, packageDistinctProjectDetailsList, x => x.Name);

            var projetPreprodCounter = 1;
            foreach (var projectItem in projectStagingComparator.RemovedList.Enum())
            {
                var message = "Récupération des tables préprod du package, Projet {0}/{1} : {2}".FormatString(projetPreprodCounter, projectStagingComparator.RemovedList.Count(), projectItem.Name);
                ReportProgress(message);

                stagingProjectManager.OpenProject(stagingGroup, projectItem);
                var project = stagingProjectManager.Project;

                var importedDataTables = project.GetImportedDataTableList();
                foreach (var tableItem in importedDataTables.Enum())
                    result.Add(new Tuple<string, ProjectDetails, ImportedDataTable>(EnvironmentEnum.Staging.GetName("FR"), projectItem, tableItem));
                stagingProjectManager.CloseProject(false);
                projetPreprodCounter++;
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
   

        #endregion
    }
}