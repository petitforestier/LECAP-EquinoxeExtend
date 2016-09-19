using DriveWorks;
using DriveWorks.Security;
using Library.Tools.Extensions;
using EquinoxeExtend.Shared.Object.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Service.DWGroup.Data
{
    public class DataService
    {
        #region Public CONSTRUCTORS

        public DataService(Group iGroup)
        {
            if (iGroup == null)
                throw new Exception("Le groupe ne peut pas être null");
            _Group = iGroup;
        }

        #endregion

        #region Public METHODS

        public List<ProjectDetails> GetProjectList()
        {
            return _Group.Projects.GetProjects().ToList();
        }

        public void DeleteSpecification(string iSpecificationName)
        {
            if (iSpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est invalide");

            var theSpecification = _Group.Specifications.GetSpecification(iSpecificationName);
            if (theSpecification == null)
                throw new Exception("La spécification DW n'existe pas");

            _Group.Specifications.DeleteSpecification(theSpecification);
        }

        public Dictionary<string, string> GetProjectAliasDic()
        {
            var projectList = _Group.Projects.GetProjects().ToList();
            var projectDic = new Dictionary<string, string>();

            //recherche des alias
            foreach (var projectItem in projectList.Enum())
            {
                var alias = GetProjectAlias(projectItem.Id.ToString());
                projectDic.Add(projectItem.Id.ToString(), (alias ?? projectItem.Name));
            }
            return projectDic;
        }

        public List<UserDetails> GetUserList()
        {
            return _Group.Security.GetUsers().ToList();
        }

        public string GetUserNameFromUserId(string iUserId)
        {
            return GetUserList().Single(x => x.Id.ToString() == iUserId).DisplayName;
        }

        public UserDetails GetUserById(string iUserId)
        {
            return GetUserList().Single(x => x.Id.ToString() == iUserId);
        }

        public string GetSpecificationMetadataFilePath(string iSpecificationName)
        {
            var theSpecification = _Group.Specifications.GetSpecification(iSpecificationName);

            if (theSpecification == null)
                throw new Exception("La spécification est inexistante");

            return theSpecification.Directory + "\\" + theSpecification.MetadataDirectory + "\\" + theSpecification.OriginalProjectName + theSpecification.OriginalProjectExtension;
        }

        public string GetProjectId(string iProjectName)
        {
            var theMasterProject = _Group.Projects.GetProject(iProjectName);
            if (theMasterProject == null)
                throw new Exception("Le projet est inexistant");

            return theMasterProject.Id.ToString();
        }

        public GroupSettings GetGroupSettings()
        {
            if (_GroupSettings == null)
            {
                var valueDic = new Dictionary<string, string>();
                if (_Group.DataTables.IsNullOrEmpty())
                    throw new Exception("Le groupe ne contient pas de table de groupe");

                var groupTable = _Group.DataTables.SingleOrDefault(x => x.Name == GROUPSETTINGSDATATABLENAME);
                if (groupTable == null)
                    throw new Exception("La table de groupe : " + GROUPSETTINGSDATATABLENAME + " est inexistante");

                var groupeTableData = groupTable.GetTableData();

                for (int rowIndex = 0; rowIndex <= groupeTableData.Rows - 1; rowIndex++)
                {
                    var groupTableDataArray = groupeTableData.ToArray();

                    string key = groupTableDataArray[rowIndex, 0].ToString();
                    if (key.IsNullOrEmpty())
                        throw new Exception("La table de groupe : " + GROUPSETTINGSDATATABLENAME + " possède une clé vide à la ligne '{0}'".FormatString(rowIndex));

                    string value = groupTableDataArray[rowIndex, 1].ToString();
                    if (key.IsNullOrEmpty())
                        throw new Exception("La table de groupe : " + GROUPSETTINGSDATATABLENAME + " possède une valeur de paramètre vide à la ligne '{0}'".FormatString(rowIndex));

                    valueDic.Add(key, value);
                }

                _GroupSettings = new GroupSettings();
                _GroupSettings.ExtendDataBaseConnectionString = valueDic[EXTENDDATABASECONNECTIONSTRING];
                _GroupSettings.SpecificationPrefix = valueDic[SPECIFICATIONPREFIX];
            }
            return _GroupSettings;
        }

        public DriveWorks.Specification.SpecificationDetails GetSpecificationDetails(string iSpecificationName)
        {
            return _Group.Specifications.GetSpecification(iSpecificationName);
        }

        public List<DriveWorks.Specification.SpecificationDetails> GetSpecificationsByModifiedDate(bool iDescending)
        {
           return _Group.Specifications.GetSpecificationsByModifiedDate(iDescending).ToList();
        }

        public IGroupResultEnumerator<DriveWorks.Components.ReleasedComponentDetails> GetComponents(bool iTopLevelOnly, bool iIncludeGenerated, bool iIncludeNotGenerated, bool iIncludeFailed)
        {
            return _Group.ReleasedComponents.GetComponents(iTopLevelOnly, iIncludeGenerated, iIncludeNotGenerated, iIncludeFailed);
        }

        public List<GroupDataTable> GetGroupDataTable()
        {
            return _Group.DataTables.Enum().ToList();
        }

        #endregion

        #region Private FIELDS

        private const string PROJECTALIASFILENAME = "LiveProjectAlias.txt";
        private const string GROUPSETTINGSDATATABLENAME = "GrpSettings";
        private const string EXTENDDATABASECONNECTIONSTRING = "ExtendDataBaseConnectionString";
        private const string SPECIFICATIONPREFIX = "SpecificationPrefix";

        private Group _Group;
        private GroupSettings _GroupSettings;

        #endregion

        #region Private METHODS

        private string GetProjectAlias(string iProjectId)
        {
            var theMasterProject = _Group.Projects.GetProject(new Guid(iProjectId));
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