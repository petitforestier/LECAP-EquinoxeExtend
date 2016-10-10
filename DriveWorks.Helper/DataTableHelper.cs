using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Extensions;

namespace DriveWorks.Helper
{
    public static class DataTableHelper
    {
        public static List<string> GetProjectDataTableDifference(List<Tuple<string, ProjectDetails, ImportedDataTable>> iProjectDataTableList)
        {
            var result = new List<string>();

            if (iProjectDataTableList.IsNullOrEmpty())
                throw new Exception("La liste des tables est nulle");

            if (iProjectDataTableList.Count < 2)
                throw new Exception("La liste des tables doit au moins contenir 2 tables");

            var firstProjectTableData = iProjectDataTableList.First();
            iProjectDataTableList.RemoveAt(0);

            //Bouclage sur toutes les tables pour comparer à la première, suffisant pour détecter les erreurs
            foreach (var projectTableItem in iProjectDataTableList)
            {
                var difference = DataTableDifference(projectTableItem.Item3, firstProjectTableData.Item3);
                if (difference.IsNotNullAndNotEmpty())
                    result.Add("Le projet '{0} => {1}' et '{2} => {3}' ont respectivement une différence de table '{4}' et '{5}'. La différence est : {6}"
                        .FormatString(firstProjectTableData.Item1, firstProjectTableData.Item2.Name, projectTableItem.Item1, projectTableItem.Item2.Name, firstProjectTableData.Item3.DisplayName, projectTableItem.Item3.DisplayName, difference));
            }
            return result;
        }

        private static string DataTableDifference(ImportedDataTable iImportedDataTable1, ImportedDataTable iImportedDataTable2 )
        {
            var tableData1 = iImportedDataTable1.GetCachedTableData();
            var tableData2 = iImportedDataTable2.GetCachedTableData();

            if (tableData1.GetLength(0) != tableData2.GetLength(0))
                return "Le nombre de ligne est différent";

            if (tableData1.GetLength(1) != tableData2.GetLength(1))
                return "Le nombre de colonne est différent";

            //Bouclage sur les lignes
            for (int rowIndex = 0; rowIndex <= tableData1.GetLength(0) - 1; rowIndex++)
            {
                //Bouclage sur les lignes
                for (int columnIndex = 0; columnIndex <= tableData1.GetLength(1) - 1; columnIndex++)
                {
                    if (tableData1[rowIndex, columnIndex] != tableData2[rowIndex, columnIndex])
                        return "La valeur : colonne {0}, ligne {1} est différente.".FormatString(columnIndex+1,rowIndex+1);
                }
            }
            return null;
        }

    }
}
