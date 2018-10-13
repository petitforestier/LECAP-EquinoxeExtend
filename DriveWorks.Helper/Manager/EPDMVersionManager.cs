using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;
using DriveWorks.Helper.Object;
using Library.Tools.Extensions;

namespace DriveWorks.Helper.Manager
{
    public static class EPDMVersionManager
    {
        #region Public METHODS

        public static void UpdateOrCreateEPDMVersionDataTable(this Project iProject, string iTablePrefixeName, string iComponentName, List<EPDMVersion> iEPDMVersionList)
        {
            if (iEPDMVersionList.IsNullOrEmpty())
                throw new Exception("La liste des versions EPDM est invalide");

            if (iProject == null)
                throw new Exception("Le projet est null");

            if (iComponentName.IsNullOrEmpty())
                throw new Exception("Le nom du composant est vide");

            var tableName = iTablePrefixeName;
            if (tableName.IsNullOrEmpty())
                throw new Exception("Le préfixe de table de version est vide");

            var flattenList = new List<List<string>>();

            var headerList = new List<string>();

            //Attention ordre important
            headerList.Add("CodeDocument");
            headerList.Add("Version2D");
            headerList.Add("Version3D");
            flattenList.Add(headerList);

            foreach (var item in iEPDMVersionList)
            {
                var newList = new List<string>
                {
                    //Attention ordre important
                    item.CodeDocument,
                    item.Version2D.ToString(),
                    item.Version3D.ToString(),
                    //si modification penser à modifier au dessus les entêtes
                };

                flattenList.Add(newList);
            }

            tableName = tableName + iComponentName;

            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == tableName);

            DriveWorks.SimpleDataTable simpleDataTable;
            if (projectTable == null)
                simpleDataTable = (DriveWorks.SimpleDataTable)iProject.DataTables.CreateDataTable(typeof(DriveWorks.SimpleDataTable), tableName);
            else
                simpleDataTable = (DriveWorks.SimpleDataTable)projectTable;

            DataTableHelper.ReplaceCompleteDataToSimpleDateTable(simpleDataTable, flattenList);
        }

        #endregion
    }
}