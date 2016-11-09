﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Extensions;
using Library.Tools.Debug;

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
                MyDebug.BreakForDebug();
                //return "Le nombre de ligne est différent";

            if (tableData1.GetLength(1) != tableData2.GetLength(1))
                MyDebug.BreakForDebug();
            //return "Le nombre de colonne est différent";

            tableData1 = RemoveEmptyRowColumnDataTable(iImportedDataTable1);
            tableData2 = RemoveEmptyRowColumnDataTable(iImportedDataTable2);

            //Bouclage sur les lignes
            for (int rowIndex = 0; rowIndex <= tableData1.GetLength(0) - 1; rowIndex++)
            {
                //Bouclage sur les lignes
                for (int columnIndex = 0; columnIndex <= tableData1.GetLength(1) - 1; columnIndex++)
                {
                    if (tableData1[rowIndex, columnIndex].ToString() != tableData2[rowIndex, columnIndex].ToString())
                        return "La valeur : colonne {0}, ligne {1} est différente.".FormatString(columnIndex+1,rowIndex+1);
                }
            }
            return null;
        }

        private static string[,] RemoveEmptyRowColumnDataTable(ImportedDataTable iImportedDataTable)
        {
            var rowIndexMax =0;
            var columnIndexMax = 0;

            var tableData = iImportedDataTable.GetCachedTableData();
            
            //Bouclage à l'envers pour trouver les lignes vides
            for (int rowIndex = tableData.GetLength(0)-1; rowIndex >=0;rowIndex --)
            {
                //Bouclage sur les celulles de la ligne
                for(int columnIndex =0; columnIndex<= tableData.GetLength(1)-1; columnIndex++)
                {
                    if (tableData[rowIndex, columnIndex].ToString().IsNotNullAndNotEmpty())
                    {
                        rowIndexMax = rowIndex;
                        break;
                    }                       
                }
                if (rowIndexMax != 0)
                    break;
            }

            //Bouclage à l'envers pour trouver les colonnes vides
            for (int columnIndex = tableData.GetLength(1)-1; columnIndex >= 0; columnIndex--)
            {
                //Bouclage sur les celulles de la ligne
                for (int rowIndex = 0; rowIndex <= tableData.GetLength(0)-1; rowIndex++)
                {
                    if (tableData[rowIndex, columnIndex].ToString().IsNotNullAndNotEmpty())
                    {
                        columnIndexMax = columnIndex;
                        break;
                    }       
                }
                if (columnIndexMax != 0)
                    break;
            }

            var resultArray = new string[rowIndexMax+1, columnIndexMax+1];

            //Remplissage du tableau sans les lignes vides ou colonne vides
            for( int rowIndex = 0; rowIndex <= rowIndexMax; rowIndex++)
            {
                for(int columnIndex = 0; columnIndex <= columnIndexMax; columnIndex++)
                    resultArray[rowIndex, columnIndex] = tableData[rowIndex, columnIndex].ToString();
            }

            return resultArray;
        }

    }
}
