using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Extensions;

namespace DriveWorks.Helper
{
    public static class ImportedDataTableHelper
    {
        public static List<Tuple<ImportedDataTable, ImportedDataTable>> GetProjectDataTableDifferent(this List<ImportedDataTable> iDataTableList)
        {
            var result = new List<Tuple<ImportedDataTable, ImportedDataTable>>();

            if (iDataTableList.IsNullOrEmpty())
                throw new Exception("La liste des tables est nulle");

            if (iDataTableList.Count < 2)
                throw new Exception("La liste des tables doit au moins contenir 2 tables");

            var firstProjectTableData = iDataTableList.First().GetCachedTableData();
            iDataTableList.RemoveAt(0);

            foreach (var projectTableItem in iDataTableList)
            {
                var itemProjectTableData = projectTableItem.GetCachedTableData();

                if (firstProjectTableData.GetLength(0) != itemProjectTableData.GetLength(0))
                {
                    result.Add(new Tuple<ImportedDataTable, ImportedDataTable>(projectTableItem, iDataTableList.First()));
                    continue;
                }

                if (firstProjectTableData.GetLength(1) != itemProjectTableData.GetLength(1))
                {
                    result.Add(new Tuple<ImportedDataTable, ImportedDataTable>(projectTableItem, iDataTableList.First()));
                    continue;
                }

                //Bouclage sur les lignes
                var dataAreDifferent = false;
                for (int rowIndex = 0; rowIndex <= firstProjectTableData.GetLength(0) - 1; rowIndex++)
                {
                    if (dataAreDifferent)
                        break;
                    //Bouclage sur les lignes
                    for (int columnIndex = 0; columnIndex <= firstProjectTableData.GetLength(1) - 1; columnIndex++)
                    {
                        if (dataAreDifferent)
                            break;
                        if (firstProjectTableData[rowIndex, columnIndex] != itemProjectTableData[rowIndex, columnIndex])
                            dataAreDifferent = true;
                    }
                }

                if (dataAreDifferent)
                {
                    result.Add(new Tuple<ImportedDataTable, ImportedDataTable>(projectTableItem, iDataTableList.First()));
                    continue;
                }
            }

            return result;

        }

    }
}
