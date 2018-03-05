using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Extensions;
using Library.Tools.Debug;
using DriveWorks.Helper.Object;
using Library.Tools.Attributes;
using Library.Control.Datagridview;
using DriveWorks.Forms;
using DriveWorks.Forms.DataModel;

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
                var difference = DataTableDifference(projectTableItem.Item2.Name, projectTableItem.Item3, firstProjectTableData.Item2.Name, firstProjectTableData.Item3);
                if (difference.IsNotNullAndNotEmpty())
                    result.Add("Le projet '{0} => {1}' et '{2} => {3}' ont respectivement une différence de table '{4}' et '{5}'. La différence est : {6}"
                        .FormatString(firstProjectTableData.Item1, firstProjectTableData.Item2.Name, projectTableItem.Item1, projectTableItem.Item2.Name, firstProjectTableData.Item3.DisplayName, projectTableItem.Item3.DisplayName, difference));
            }
            return result;
        }

        private static string DataTableDifference(string iProjectName1, ImportedDataTable iImportedDataTable1, string iProjectName2, ImportedDataTable iImportedDataTable2)
        {
            object[,] tableData1;
            object[,] tableData2;

            //Importation des données des tables
            try
            {
                tableData1 = iImportedDataTable1.GetCachedTableData();
                tableData1 = RemoveLastEmptyRowColumnDataTable(iImportedDataTable1);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'importation des données de la table {0}, projet {1}".FormatString(iImportedDataTable1.DisplayName, iProjectName1), ex);
            }

            try
            {
                tableData2 = iImportedDataTable2.GetCachedTableData();
                tableData2 = RemoveLastEmptyRowColumnDataTable(iImportedDataTable2);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'importation des données de la table {0}, projet {1}".FormatString(iImportedDataTable1.DisplayName, iProjectName2), ex);
            }

            if(tableData1.GetUpperBound(0) != tableData2.GetUpperBound(0))
                return "Le nombre de ligne est différent";

            if (tableData1.GetUpperBound(1) != tableData2.GetUpperBound(1))
                return "Le nombre de colonne est différent";

            //Bouclage sur les lignes
            for (int rowIndex = 0; rowIndex <= tableData1.GetLength(0) - 1; rowIndex++)
            {
                //Bouclage sur les lignes
                for (int columnIndex = 0; columnIndex <= tableData1.GetLength(1) - 1; columnIndex++)
                {
                    if (tableData1[rowIndex, columnIndex].ToString() != tableData2[rowIndex, columnIndex].ToString())
                        return "La valeur : colonne {0}, ligne {1} est différente.".FormatString(columnIndex + 1, rowIndex + 1);
                }
            }
            return null;
        }

        /// <summary>
        /// Supprime les éventuelles dernières lignes et colonnes vides 
        /// </summary>
        /// <param name="iImportedDataTable"></param>
        /// <returns></returns>
        private static string[,] RemoveLastEmptyRowColumnDataTable(ImportedDataTable iImportedDataTable)
        {
            var rowIndexMax = 0;
            var columnIndexMax = 0;

            var tableData = iImportedDataTable.GetCachedTableData();

            //Bouclage à l'envers pour trouver les lignes vides
            for (int rowIndex = tableData.GetLength(0) - 1; rowIndex >= 0; rowIndex--)
            {
                //Bouclage sur les celulles de la ligne
                for (int columnIndex = 0; columnIndex <= tableData.GetLength(1) - 1; columnIndex++)
                {
                    if (tableData[rowIndex, columnIndex] != null)
                    {
                        if (tableData[rowIndex, columnIndex].ToString() != string.Empty)
                        {
                            rowIndexMax = rowIndex;
                            break;
                        }
                    }
                }
                if (rowIndexMax != 0)
                    break;
            }

            //Bouclage à l'envers pour trouver les colonnes vides
            for (int columnIndex = tableData.GetLength(1) - 1; columnIndex >= 0; columnIndex--)
            {
                //Bouclage sur les celulles de la ligne
                for (int rowIndex = 0; rowIndex <= tableData.GetLength(0) - 1; rowIndex++)
                {
                    if (tableData[rowIndex, columnIndex] != null)
                    {
                        if (tableData[rowIndex, columnIndex].ToString() != string.Empty)
                        {
                            columnIndexMax = columnIndex;
                            break;
                        }
                    }
                }
                if (columnIndexMax != 0)
                    break;
            }

            var resultArray = new string[rowIndexMax + 1, columnIndexMax + 1];

            //Remplissage du tableau sans les lignes vides ou colonne vides
            for (int rowIndex = 0; rowIndex <= rowIndexMax; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex <= columnIndexMax; columnIndex++)
                {
                    if (tableData[rowIndex, columnIndex] != null)
                        resultArray[rowIndex, columnIndex] = tableData[rowIndex, columnIndex].ToString();
                    else
                        resultArray[rowIndex, columnIndex] = null;
                }
            }

            return resultArray;
        }

        public static void AddDataToSimpleDataTable(SimpleDataTable iSimpleDataTable, List<List<string>> iNewDataList, bool iIsNewDataCanBeWider = false)
        {
            if (iNewDataList.IsNullOrEmpty())
                throw new Exception("La liste ne peut pas être null");

            var originalData = iSimpleDataTable.GetCachedTableData();

            var newDataIsWider = iNewDataList.First().Count() > originalData.GetLength(0) + 1;

            if (iIsNewDataCanBeWider && newDataIsWider)
                throw new Exception("Les nouvelles données contiennent plus de colonnes que le tableau initial");

            //Détermine les dimensions du tableau final
            var columnCount = 0;
            if (originalData.GetLength(1) > 0)
                columnCount = (iNewDataList.First().Count() > originalData.GetLength(1)) ? iNewDataList.First().Count() : originalData.GetLength(1);
            else
                columnCount = iNewDataList.First().Count();

            var mergeData = new object[iNewDataList.Count + originalData.GetLength(0), columnCount];

            //Ajout données existant dans le tableau
            for (int j = 0; j <= originalData.GetLength(0) - 1; j++)
            {
                for (int i = 0; i <= originalData.GetLength(1) - 1; i++)
                    mergeData[j, i] = originalData[j, i];
            }

            //Ajout des nouvelles données
            var rowIndex = originalData.GetLength(0);
            foreach (var iRowItem in iNewDataList.Enum())
            {
                var columnIndex = 0;
                foreach (var iColumnItem in iRowItem.Enum())
                {
                    mergeData[rowIndex, columnIndex] = iColumnItem;
                    columnIndex++;
                }
                rowIndex++;
            }

            iSimpleDataTable.SetTableData(mergeData);
        }

        /// <summary>
        /// Remplace la totalité des données dans le tableau
        /// </summary>
        /// <param name="iSimpleDataTable"></param>
        /// <param name="iNewDataList"></param>
        public static void ReplaceCompleteDataToSimpleDateTable(SimpleDataTable iSimpleDataTable, List<List<string>> iNewDataList)
        {
            if (iNewDataList.IsNullOrEmpty())
                throw new Exception("La nouvelle liste est vide ou null");

            var mergeData = new object[iNewDataList.Count, iNewDataList.First().Count];

            //Création du tableau
            int rowIndex = 0;
            foreach (var iRowItem in iNewDataList.Enum())
            {
                var columnIndex = 0;
                foreach (var iColumnItem in iRowItem.Enum())
                {
                    mergeData[rowIndex, columnIndex] = iColumnItem;
                    columnIndex++;
                }
                rowIndex++;
            }

            //Ecriture des données dans le tableau
            iSimpleDataTable.SetTableData(mergeData);
        }

        public static void SetDataTableValuesFromList<T>(this Project iProject, List<T> iDataList,string iDataTableName)
        {
            //Retourne une table vide si vide
            if (iDataList.IsNullOrEmpty())
                iProject.SetTableControlItems(iDataTableName, null);

            //Mise en forme du tableau et de l'entête via la classe DossierView
            Type dataViewType = typeof(T);
            var arraySize = dataViewType.GetProperties().Count();
            var objectArray = new object[iDataList.Count + 1, arraySize];

            var columnIndex = 0;
            foreach (var propertyItem in dataViewType.GetProperties().Enum())
            {
                objectArray[0, columnIndex] = dataViewType.GetName(propertyItem.Name, "FR");
                columnIndex++;
            }

            //Convertion list des objects en tableau et intégration dans le tableau final
            var dataArray = iDataList.ToStringArray();
            Array.Copy(dataArray, 0, objectArray, objectArray.GetLength(1), dataArray.Length);

            //Dimension des colonnes
            var widthList = new List<string>();
            foreach (var propertyItem in dataViewType.GetProperties().Enum())
            {
                var width = dataViewType.GetWidthColumn(propertyItem.Name);
                if (width != null)
                    widthList.Add(Convert.ToString(width));
                else
                    widthList.Add("120");
            }

            //Remplissage tableau
            var tableValue = new TableValue();
            tableValue.Data = objectArray;

            iProject.SetTableControlItems(iDataTableName, tableValue, widthList);

        }

        public static void SetTableControlItems(this Project iProject, string iControlName, ITableValue iTableValue, List<string> iColumnWidthList = null)
        {
            var tableControl = (DataTableControl)iProject.Navigation.GetControl(iControlName);

            if (tableControl == null)
                throw new Exception("Le controle datatable nommé '{0}' est introuvable");

            tableControl.Items = iTableValue;

            //Définition des largeurs de colonnes
            if (iColumnWidthList.IsNotNullAndNotEmpty())
            {
                var properties = DynamicProperty.GetProperties(typeof(DataTableControl));
                var property = properties.Single(x => x.CustomStoreName == "ColumnWidths");
                property.SetValue(tableControl, iColumnWidthList.ToArray());
            }

        }

    }
}
