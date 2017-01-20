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
namespace EquinoxeExtendPlugin.Tools
{
    public static class Tools
    {
        #region Public METHODS

        public static void ReleaseProjectsRights(Group iGroup)
        {
            using (var releaseService = new Service.Release.Front.ReleaseService(iGroup.GetEnvironment().GetExtendConnectionString()))
            {
                var devSubTasks = releaseService.GetDevSubTasks();

                var devProjects = devSubTasks.Enum().Where(x => x.ProjectGUID != null).Enum().Select(x => (Guid)x.ProjectGUID).Enum().ToList();

                //Supprime les doublons
                devProjects = devProjects.Enum().GroupBy(x => x).Enum().Select(x => x.First()).Enum().ToList();

                //Applique les droits seulement sur ces projets ouverts
                iGroup.SetExclusitivelyPermissionToTeam(iGroup.Security.GetTeams().Single(x => x.DisplayName == EnvironmentEnum.Developpement.GetDevelopperTeam()), devProjects);
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

        public static List<Tuple<string, ProjectDetails, ImportedDataTable>> GetImportedDataTableFromPackage(IApplication iApplication, Package iPackageDeployToStaging)
        {
            var result = new List<Tuple<string, ProjectDetails, ImportedDataTable>>();

            var groupService = iApplication.ServiceManager.GetService<IGroupService>();
            var devgroup = groupService.ActiveGroup;

            var packageDistinctProjectGUIDList = iPackageDeployToStaging.SubTasks.Where(x => x.ProjectGUID != null).GroupBy(x => x.ProjectGUID).Select(x => (Guid?)x.First().ProjectGUID).ToList();
            var packageDistinctProjectDetailsList = new List<ProjectDetails>();
            foreach (var item in packageDistinctProjectGUIDList)
            {
                if (item != null)
                    packageDistinctProjectDetailsList.Add(devgroup.Projects.GetProject((Guid)item));
            }

            //Bouclage sur les projets en dev inclus dans le package
            foreach (var projectItem in packageDistinctProjectDetailsList.Enum())
            {
                var projectService = iApplication.ServiceManager.GetService<IProjectService>();
                projectService.OpenProject(projectItem.Name);

                var project = projectService.ActiveProject;
                var importedDataTables = project.GetImportedDataTableList();
                foreach (var tableItem in importedDataTables.Enum())
                    result.Add(new Tuple<string, ProjectDetails, ImportedDataTable>(EnvironmentEnum.Developpement.GetName("FR"), projectItem, tableItem));
                projectService.CloseProject();
            }

            //Récupère les projets de préprod non impacté par le package
            var stagingGroup = groupService.OpenGroup(EnvironmentEnum.Staging);
            var openedStagingProjectlist = stagingGroup.GetOpenedProjectList();
            if (openedStagingProjectlist.IsNotNullAndNotEmpty())
                throw new Exception("Certains projets du groupe '{0}' sont ouverts. L'analyse n'est donc pas possible.".FormatString(stagingGroup.Name) + Environment.NewLine + Environment.NewLine + openedStagingProjectlist.Select(x => x.Name).Concat(Environment.NewLine));

            var projectStagingComparator = new ListComparator<ProjectDetails, ProjectDetails>(stagingGroup.GetProjectList(), x => x.Name, packageDistinctProjectDetailsList, x => x.Name);

            foreach (var projectItem in projectStagingComparator.RemovedList.Enum())
            {
                var projectService = iApplication.ServiceManager.GetService<IProjectService>();
                projectService.OpenProject(projectItem.Name);

                var project = projectService.ActiveProject;
                var importedDataTables = project.GetImportedDataTableList();
                foreach (var tableItem in importedDataTables.Enum())
                    result.Add(new Tuple<string, ProjectDetails, ImportedDataTable>(EnvironmentEnum.Staging.GetName("FR"), projectItem, tableItem));
                projectService.CloseProject();
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
                            errorControlState.Add(controlState);
                        else if (controlState.Value2 != controlStateItem.Value2)
                            errorControlState.Add(controlState);
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