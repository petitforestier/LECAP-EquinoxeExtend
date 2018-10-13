using DriveWorks.Helper.Object;
using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DriveWorks.Helper.Manager
{
    public static partial class SettingsManager
    {
        #region Public METHODS

        public static ProjectSettings GetProjectSettings(this Project iProject)
        {
 
            var tableDic = new Dictionary<string, string>();
            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTSETTINGSDATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTSETTINGSDATATABLENAME + " est inexistante");

            var projectTableDataArray = projectTable.GetCachedTableData();

            for (int rowIndex = 0; rowIndex <= projectTableDataArray.GetLength(0) - 1; rowIndex++)
            {
                string key = projectTableDataArray[rowIndex, 0].ToString();
                if (key.IsNullOrEmpty())
                    throw new Exception("La table de projet : " + PROJECTSETTINGSDATATABLENAME + " possède une clé vide à la ligne '{0}'".FormatString(rowIndex));

                string value = projectTableDataArray[rowIndex, 1].ToString();
                if (key.IsNullOrEmpty())
                    throw new Exception("La table de projet : " + PROJECTSETTINGSDATATABLENAME + " possède une valeur de paramètre vide à la ligne '{0}'".FormatString(rowIndex));

                tableDic.Add(key, value);
            }

            var result = new ProjectSettings();
            string projectVersion;
            tableDic.TryGetValue(PROJECTVERSION, out projectVersion);
            result.ProjectVersion = projectVersion.IsNotNullAndNotEmpty() ? Convert.ToDecimal(projectVersion) : 0;

            string tagIgnore;
            tableDic.TryGetValue(TAGIGNORE, out tagIgnore);
            result.TagIgnore = tagIgnore;

            string errorColorName;
            tableDic.TryGetValue(ERRORCOLORNAME, out errorColorName);
            result.ErrorColorName = errorColorName;

            string noErrorColorName;
            tableDic.TryGetValue(NOERRORCOLORNAME, out noErrorColorName);
            result.NoErrorColorName = noErrorColorName;

            string userDebugControlName;
            tableDic.TryGetValue(USERDEBUGCONTROLNAME, out userDebugControlName);
            result.UserDebugControlName = userDebugControlName;

            string errorConstantName;
            tableDic.TryGetValue(ERRORCONSTANTNAME, out errorConstantName);
            result.ErrorConstantName = errorConstantName;

            result.ProjectName = iProject.Name;

            return result;
        }

        public static GroupSettings GetGroupSettings(this Group iGroup)
        {
            var tableDic = new Dictionary<string, string>();
            var groupTable = iGroup.DataTables.SingleOrDefault(x => x.Name == GROUPSETTINGSDATATABLENAME);
            if (groupTable == null)
                throw new Exception("La table de groupe : " + GROUPSETTINGSDATATABLENAME + " est inexistante");

            var groupTableDataArray = groupTable.GetTableData().ToArray();

            for (int rowIndex = 0; rowIndex <= groupTableDataArray.GetLength(0) - 1; rowIndex++)
            {
                string key = groupTableDataArray[rowIndex, 0].ToString();
                if (key.IsNullOrEmpty())
                    throw new Exception("La table de groupe : " + GROUPSETTINGSDATATABLENAME + " possède une clé vide à la ligne '{0}'".FormatString(rowIndex));

                string value = groupTableDataArray[rowIndex, 1].ToString();
                if (key.IsNullOrEmpty())
                    throw new Exception("La table de groupe : " + GROUPSETTINGSDATATABLENAME + " possède une valeur de paramètre vide à la ligne '{0}'".FormatString(rowIndex));

                tableDic.Add(key, value);
            }

            var result = new GroupSettings();
            
            string epdmMasterVersionPrefixe;
            tableDic.TryGetValue(EPDMMASTERVERSIONPREFIXE, out epdmMasterVersionPrefixe);
            result.EPDMMasterVersionPrefixe = epdmMasterVersionPrefixe;

            string epdmVaultName;
            tableDic.TryGetValue(EPDMVAULTNAME, out epdmVaultName);
            result.EPDMVaultName = epdmVaultName;

            return result;
        }

        public static List<string> GetTableSettingsList(this Project iProject)
        {
            var result = new List<string>();
            result.Add(PROJECTSETTINGSDATATABLENAME);
            return result;
        }

        public static void UpdateProjectVersionNumber(this Project iProject, decimal iNewVersionNumber)
        {
            //Récupération des settings actuels
            var currentProjectSettings = GetProjectSettings(iProject);

            //Incrémentation de la version
            currentProjectSettings.ProjectVersion = iNewVersionNumber;              

            //Génération de la table
            var flatDataList = new List<List<string>>();

            var projectVersionList = new List<string>();
            projectVersionList.Add(PROJECTVERSION);
            projectVersionList.Add(currentProjectSettings.ProjectVersion.ToString());
            flatDataList.Add(projectVersionList);

            var tagList = new List<string>();
            tagList.Add(TAGIGNORE);
            tagList.Add(currentProjectSettings.TagIgnore);
            flatDataList.Add(tagList);

            var errorColorNameList = new List<string>();
            errorColorNameList.Add(ERRORCOLORNAME);
            errorColorNameList.Add(currentProjectSettings.ErrorColorName);
            flatDataList.Add(errorColorNameList);

            var noErrorColorNameList = new List<string>();
            noErrorColorNameList.Add(NOERRORCOLORNAME);
            noErrorColorNameList.Add(currentProjectSettings.NoErrorColorName);
            flatDataList.Add(noErrorColorNameList);

            var userDebugControlNameList = new List<string>();
            userDebugControlNameList.Add(USERDEBUGCONTROLNAME);
            userDebugControlNameList.Add(currentProjectSettings.UserDebugControlName);
            flatDataList.Add(userDebugControlNameList);

            var errorConstantNameList = new List<string>();
            errorConstantNameList.Add(ERRORCONSTANTNAME);
            errorConstantNameList.Add(currentProjectSettings.ErrorConstantName);
            flatDataList.Add(errorConstantNameList);

            //Réinjection de la table
            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTSETTINGSDATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTSETTINGSDATATABLENAME + " est inexistante");

            DataTableHelper.ReplaceCompleteDataToSimpleDateTable((DriveWorks.SimpleDataTable)projectTable, flatDataList);
        }

        public static Dictionary<string, string> GetProjectAliasDic(this Group iGroup)
        {
            var projectList = iGroup.Projects.GetProjects().ToList();
            var projectDic = new Dictionary<string, string>();

            //recherche des alias
            foreach (var projectItem in projectList.Enum())
            {
                var alias = GetProjectAlias(iGroup, projectItem.Id.ToString());
                projectDic.Add(projectItem.Id.ToString(), (alias ?? projectItem.Name));
            }
            return projectDic;
        }

        #endregion

        #region Private FIELDS

        private const string PROJECTALIASFILENAME = "LiveProjectAlias.txt";
        private const string PROJECTSETTINGSDATATABLENAME = "PrjSettings";
        private const string GROUPSETTINGSDATATABLENAME = "GrpSettings";
        private const string PROJECTVERSION = "ProjectVersion";
        private const string TAGIGNORE = "IgnoreTag";
        private const string ERRORCOLORNAME = "ErrorColorName";
        private const string NOERRORCOLORNAME = "NoErrorColorName";
        private const string USERDEBUGCONTROLNAME = "UserDebugControlName";
        private const string ERRORCONSTANTNAME = "ErrorConstantName";
        private const string EPDMVAULTNAME = "EPDMVaultName";
        private const string EPDMMASTERVERSIONPREFIXE = "EPDMMasterVersionPrefixe";

        #endregion

        #region Private METHODS

        private static string GetProjectAlias(Group iGroup, string iProjectId)
        {
            var theMasterProject = iGroup.Projects.GetProject(new Guid(iProjectId));
            if (theMasterProject == null)
                throw new Exception("Le projet est inexistante");

            var filePath = theMasterProject.Directory + "\\" + PROJECTALIASFILENAME;
            try
            {
                return File.ReadAllText(filePath);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}