using DriveWorks.Applications;
using DriveWorks.Security;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using DriveWorks.GroupMaintenance;
using System.IO;
using DriveWorks.Hosting;
using EquinoxeExtend.Shared.Enum;
using DriveWorks.Helper.Manager;

namespace DriveWorks.Helper
{
    public static class GroupHelper
    {
        #region Public METHODS

        public static List<ProjectDetails> GetProjectList(this Group iGroup)
        {
            return iGroup.Projects.GetProjects().ToList();
        }

        public static List<UserDetails> GetUserList(this Group iGroup, bool iOnlyEnabled = true)
        {
            return iGroup.Security.GetUsers().Enum().Where(x => x.IsEnabled == iOnlyEnabled).Enum().ToList();
        }

        public static List<UserDetails> GetUserAllowedCaptureList(this Group iGroup, bool iOnlyEnabled = true)
        {
            var result = new List<UserDetails>();
            var teamList = iGroup.Security.GetTeams().Enum().Where(x => x.IsAllowedCapture).Enum().ToList();

            foreach (var teamItem in teamList.Enum())
                result.AddRange(iGroup.Security.GetUsersInTeam(teamItem));

            return result.GroupBy(x => x.LoginName).Select(g => g.First()).Enum().ToList();
        }

        public static string GetUserNameFromUserId(this Group iGroup, string iUserId)
        {
            return iGroup.Security.GetUsers().Single(x => x.Id.ToString() == iUserId).DisplayName;
        }

        public static UserDetails GetUserById(this Group iGroup, Guid iUserId)
        {
            return iGroup.Security.GetUsers().Single(x => x.Id == iUserId);
        }

        public static string GetSpecificationMetadataFilePath(this Group iGroup, string iSpecificationName)
        {
            var theSpecification = iGroup.Specifications.GetSpecification(iSpecificationName);

            if (theSpecification == null)
                throw new Exception("La spécification est inexistante");

            return theSpecification.Directory + "\\" + theSpecification.MetadataDirectory + "\\" + theSpecification.OriginalProjectName + theSpecification.OriginalProjectExtension;
        }

        public static string GetProjectId(this Group iGroup, string iProjectName)
        {
            var theMasterProject = iGroup.Projects.GetProject(iProjectName);
            if (theMasterProject == null)
                throw new Exception("Le projet est inexistant");

            return theMasterProject.Id.ToString();
        }

        /// <summary>
        /// Affecte les droits à la liste des projects en argument
        /// </summary>
        /// <param name="iGroup"></param>
        /// <param name="iTeam"></param>
        /// <param name="iAllowedProjectList"></param>
        public static void AddPermissionToTeam(this Group iGroup, TeamDetails iTeam, List<Guid> iAllowedProjectList)
        {
            //Bouclage sur les projets à autoriser
            foreach (var item in iAllowedProjectList.Enum())
            {
                if (!iGroup.Security.TryAddProjectPermissionToTeam(iTeam.Id, item, StandardProjectPermissions.EditPermission))
                    throw new Exception("Erreur lors de l'ajout du droit d'édition");
            }
        }

        /// <summary>
        /// Supprime les droits de la team sur la liste de projets
        /// </summary>
        /// <param name="iGroup"></param>
        /// <param name="iTeam"></param>
        /// <param name="iForbidenProjectList"></param>
        public static void RemoveProjectPermissionsToTeam(this Group iGroup, TeamDetails iTeam, List<Guid> iForbidenProjectList)
        {
            //Bouclage sur les projets à enlever les droits
            foreach (var projectItem in iForbidenProjectList.Enum())
                iGroup.Security.TryRemoveProjectPermissionFromTeam(iTeam.Id, projectItem, StandardProjectPermissions.EditPermission);
        }

        /// <summary>
        /// Supprime tous les droits de la team et réaffecte les droits à la liste des projects en argument
        /// </summary>
        /// <param name="iGroup"></param>
        /// <param name="iTeam"></param>
        /// <param name="iAllowedProjectList"></param>
        public static void SetExclusitivelyPermissionToTeam(this Group iGroup, TeamDetails iTeam, List<Guid> iAllowedProjectList)
        {
            var completeProjectList = iGroup.Projects.GetProjects().Enum().Select(x => x.Id).Enum().ToList();
            RemoveProjectPermissionsToTeam(iGroup, iTeam, completeProjectList);

            //Bouclage sur les projets à autoriser
            foreach (var item in iAllowedProjectList.Enum())
                iGroup.Security.TryAddProjectPermissionToTeam(iTeam.Id, item, StandardProjectPermissions.EditPermission);
        }

        /// <summary>
        /// Retourne la liste des projets ouvert dans un groupe
        /// </summary>
        /// <param name="iGroup"></param>
        /// <returns></returns>
        public static List<ProjectDetails> GetOpenedProjectList(this Group iGroup)
        {
            if (iGroup == null)
                throw new Exception("Le groupe est null");

            var resultList = new List<ProjectDetails>();
            foreach (var projectItem in iGroup.Projects.GetProjects().Enum())
            {
                if (projectItem.ProjectIsAlreadyOpen())
                    resultList.Add(projectItem);
            }
            return resultList;
        }

        /// <summary>
        /// Copie un projet d'un groupe vers un autre groupe
        /// </summary>
        /// <param name="iSourceGroup"></param>
        /// <param name="iProjectNameToCopy"></param>
        /// <param name="iSourceProjectPath"></param>
        /// <param name="iDestinationProjectPath"></param>
        public static void CopyProjetToOtherGroup(this Group iSourceGroup, string iProjectNameToCopy, EnvironmentEnum iDestinationGroupEnum)
        {
            var projectToCopy = iSourceGroup.Projects.GetProject(iProjectNameToCopy);

            //Définition du dossier de destination
            var destinationProjectPath = projectToCopy.Directory.Replace(iSourceGroup.GetEnvironment().GetProjectDirectory(), iDestinationGroupEnum.GetProjectDirectory());

            if (!Directory.Exists(destinationProjectPath))
                Directory.CreateDirectory(destinationProjectPath);

            //Définition destination
            var options = new CopyGroupOptions();
            options.TargetFolder = destinationProjectPath;
                   
            //Ajout du projet à copier
            options.Projects.Add(projectToCopy);
            var fileOptions =  new FilePickingOptions(projectToCopy.Directory);
            options.FileOptions = fileOptions;

            //Ouverture de l'autre groupe via un host
            var host = new EngineHost(HostEnvironment.CreateDefaultEnvironment(false));
            var groupManager = host.CreateGroupManager();
            var destinationGroup = groupManager.OpenGroup(iDestinationGroupEnum);

            //Process de copie
            using (var copyProcess = CopyGroupProcess.CreateCopyGroupProcess(iSourceGroup,destinationGroup,options))
            {
                try
                {
                    if (!copyProcess.Start())
                        throw new Exception("Erreur lors de l'importation du projet '{0}' dans le groupe '{1}'".FormatString(iProjectNameToCopy, destinationGroup.Name));
                }
                catch( Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    groupManager.CloseGroup();
                }
            }
        }

        public static ProjectDetails GetProjectFromGUID(this Group iGroup, Guid iProjectGUID)
        {
            if (iGroup == null)
                throw new Exception("Le groupe est null");

            if (iProjectGUID == null)
                throw new Exception("Le GUID est invalide");

            try
            {
                return iGroup.Projects.GetProject(iProjectGUID);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void DeleteSpecification(this Group iGroup, string iSpecificationName)
        {
            if (iSpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est invalide");

            var theSpecification = iGroup.Specifications.GetSpecification(iSpecificationName);
            if (theSpecification == null)
                throw new Exception("La spécification DW n'existe pas");

            iGroup.Specifications.DeleteSpecification(theSpecification);
        }

        #endregion
    }
}