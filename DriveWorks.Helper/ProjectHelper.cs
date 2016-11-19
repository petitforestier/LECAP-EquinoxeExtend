using DriveWorks.Forms;
using DriveWorks.Specification;
using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DriveWorks.Helper
{
    public static partial class ProjectHelper
    {
        #region Public METHODS

        public static string GetEditingSpecificationName(this SpecificationContext iContext)
        {
            return Path.GetFileNameWithoutExtension(iContext.SpecificationFilePath);
        }

        public static bool ProjectIsAlreadyOpen(this ProjectDetails iProjectDetails)
        {
            if (iProjectDetails == null)
                throw new Exception("Le projet est null");

            var projectFilePath = iProjectDetails.GetProjectFilePath();
            var lockFillPath = Path.GetDirectoryName(projectFilePath) + "\\" + Path.GetFileNameWithoutExtension(projectFilePath) + ".~driveproj";

            return File.Exists(lockFillPath);
        }

        public static string GetProjectMetadataFilePath(this Project iProject, string iProjectId)
        {
            var theMasterProject = iProject.Group.Projects.GetProject(new Guid(iProjectId));

            if (theMasterProject == null)
                throw new Exception("Le projet est inexistante");

            return theMasterProject.Directory + "\\" + theMasterProject.Name + theMasterProject.Extension;
        }

        public static List<GroupDataTable> GetUsedGroupTableList(this Project iProject, List<GroupDataTable> iGroupTableList)
        {
            var result = new List<GroupDataTable>();

            //Tables de groupes
            foreach (var groupTableItem in iGroupTableList.Enum())
            {
                var searchProcess = new SearchRuleProcess(iProject);
                if (searchProcess.GetSearchResult("DWGroupTable" + groupTableItem.Name).IsNotNullAndNotEmpty())
                    result.Add(groupTableItem);
            }

            return result;
        }

        public static List<ImportedDataTable> GetImportedDataTableList(this Project iProject)
        {
            var result = new List<ImportedDataTable>();

            //Bouclage sur les tables de projets
            foreach (var tableItem in iProject.DataTables.Enum())
            {
                if (tableItem.GetType() == typeof(DriveWorks.ImportedDataTable))
                {
                    var importedDataTable = (DriveWorks.ImportedDataTable)tableItem;
                    result.Add(importedDataTable);
                }
            }

            return result;
        }

        public static void SetTableControlItems(this Project iProject, string iControlName, ITableValue iTableValue)
        {
            var tableControl = (DataTableControl)iProject.Navigation.GetControl(iControlName);

            if (tableControl == null)
                throw new Exception("Le controle datatable nommé '{0}' est introuvable");

            tableControl.Items = iTableValue;
        }

        #endregion
    }
}