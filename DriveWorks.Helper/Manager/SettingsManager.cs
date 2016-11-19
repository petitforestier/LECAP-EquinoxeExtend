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
            var result = new ProjectSettings();

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

            result = new ProjectSettings();
            result.ProjectVersion = Convert.ToDecimal(tableDic[PROJECTVERSION]);
            result.TagIgnore = tableDic[TAGIGNORE];
            result.ErrorColorName = tableDic[ERRORCOLORNAME];
            result.NoErrorColorName = tableDic[NOERRORCOLORNAME];
            result.UserDebugControlName = tableDic[USERDEBUGCONTROLNAME];
            result.ErrorConstantName = tableDic[ERRORCONSTANTNAME];
            result.ProjectName = iProject.Name;

            return result;
        }

        public static List<string> GetTableSettingsList(this Project iProject)
        {
            var result = new List<string>();
            result.Add(PROJECTSETTINGSDATATABLENAME);
            return result;
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
        private const string PROJECTVERSION = "ProjectVersion";
        private const string TAGIGNORE = "IgnoreTag";
        private const string ERRORCOLORNAME = "ErrorColorName";
        private const string NOERRORCOLORNAME = "NoErrorColorName";
        private const string USERDEBUGCONTROLNAME = "UserDebugControlName";
        private const string ERRORCONSTANTNAME = "ErrorConstantName";

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