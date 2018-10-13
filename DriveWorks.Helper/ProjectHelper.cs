using DriveWorks.Forms;
using DriveWorks.Specification;
using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Dynamic;
using DriveWorks.Forms.DataModel;

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

        public static List<List<string>> GetComponentsFilePathList(this Project iProject)
        {
            var result = new List<List<string>>();
            var componentSetList = iProject.ComponentSets;

            foreach (var componentItem in componentSetList.Enum())
            {
                var componentList = new List<string>();

                if (!componentItem.LoadComponent())
                    throw new Exception("Erreur lors du chargement du composant set'{0}'".FormatString(componentItem.Name));

                RecursiveComponenFilePath(componentItem.Component, ref componentList);

                result.Add(componentList);
            }
            return result;
        }

        private static void RecursiveComponenFilePath(Components.ProjectComponent iComponent, ref List<string> iPathList)
        {
            if (iComponent.GetType() == typeof(DriveWorks.SolidWorks.Components.ProjectAssembly))
            {
                var capturedComponent = (DriveWorks.SolidWorks.Components.ProjectAssembly)iComponent;
                iPathList.Add(capturedComponent.MasterPath);

                //bouclage sur les enfantes
                foreach (var item in capturedComponent.Children.Enum())
                    RecursiveComponenFilePath(item, ref iPathList);
            }
            else if (iComponent.GetType() == typeof(DriveWorks.SolidWorks.Components.ProjectPart))
            {
                var capturedComponent = (DriveWorks.SolidWorks.Components.ProjectPart)iComponent;
                iPathList.Add(capturedComponent.MasterPath);
            }
            else if (iComponent.GetType() == typeof(DriveWorks.SolidWorks.Components.ProjectDrawing))
            {
                var capturedComponent = (DriveWorks.SolidWorks.Components.ProjectDrawing)iComponent;
                iPathList.Add(capturedComponent.MasterPath);
            }
            else
                throw new Exception("Ce type de composant n'est pas supporté {0}".FormatString(iComponent.GetType().ToString()));
        }
        

        #endregion
    }
}