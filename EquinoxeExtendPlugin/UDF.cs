using DriveWorks.Extensibility;
using DriveWorks.Helper;
using Library.Tools.Extensions;
using DriveWorks.Helper.Manager;
using EquinoxeExtend.Shared.Enum;
using Service.Pool.Front;
using Service.Record.Front;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Rules.Execution;

namespace EquinoxeExtendPlugin
{
    /// <summary>
    /// Misc
    /// </summary>
    public partial class UDF : SharedProjectExtender
    {
        #region Public METHODS

        [Udf]
        [FunctionInfo("Retourne la liste de tous les utilisateurs existant dans le groupe en cours.")]
        public string UDFGetUserItems()
        {
            try
            {
                return this.Project.Group.GetUserList().Enum().Select(x => x.DisplayName).Enum().Concat("|");
            }
            catch (Exception ex)
            {
                return "#Error! " + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne si la liste contient l'item")]
        public bool UDFContainsItem([ParamInfo("Liste des items", "Texte des items séparé par '|'")] string iList,
            [ParamInfo("Liste des items", "Texte des items séparé par '|'")] string iSearchItem,
            [ParamInfo("Sensible à la case", "Sensible à case")] bool iCaseSensible = true)
        {
            if (iList.IsNullOrEmpty())
                return false;

            if (iSearchItem.IsNullOrEmpty())
                return false;

            var list = iList.Split("|").Enum().ToList();

            if (iCaseSensible)
                return list.Any(x => x == iSearchItem);
            else
                return list.Any(x => x.ToLower() == iSearchItem.ToLower());
        }

        [Udf]
        [FunctionInfo("Retourne un texte contient le texte recherché")]
        public bool UDFContainsText([ParamInfo("Liste des items", "Texte des items séparé par '|'")] string iText,
            [ParamInfo("Liste des items", "Texte des items séparé par '|'")] string iSearchText,
            [ParamInfo("Sensible à la case", "Sensible à case")] bool iCaseSensible = true)
        {
            if (iText.IsNullOrEmpty())
                return false;

            if (iSearchText.IsNullOrEmpty())
                return false;

            if (iCaseSensible)
                return iText.Contains(iSearchText);
            else
                return iText.ToLower().Contains(iSearchText.ToLower());
        }

        [Udf]
        [FunctionInfo("Retourne un tableau avec les éléments séparé par '|'")]
        public string UDFSplit([ParamInfo("Text à séparer", "Text complet à séparer")] string iText,
            [ParamInfo("Séparateur", "Separateur")] string iSeparator)
        {
            try
            {
                if (iText.IsNullOrEmpty())
                    throw new Exception("Le texte d'entrée est null ou vide");

                if (iSeparator.IsNullOrEmpty())
                    throw new Exception("Le séparateur est null ou vide");

                return iText.Replace(iSeparator, "|");
            }
            catch (Exception ex)
            {
                return "#Error! " + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne l'élément demandé")]
        public string UDFGetOccurence([ParamInfo("Text à séparer", "Text complet à séparer")] string iText,
            [ParamInfo("Séparateur", "Separateur")] string iSeparator,
            [ParamInfo("Index occurence", "Numéro de l'index à retourner. Retourner le 1er depuis le début, mettre 1. Pour retourner le 1er depuis la fin mettre -1")] double iOccurenceIndex)
        {
            try
            {
                if (iText.IsNullOrEmpty())
                    throw new Exception("Le texte d'entrée est null ou vide");

                if (iSeparator.IsNullOrEmpty())
                    throw new Exception("Le séparateur est null ou vide");

                if (iOccurenceIndex == 0)
                    throw new Exception("L'index d'occurence doit être différent de 0");

                var integerIndex = (int)iOccurenceIndex;
                if (integerIndex != iOccurenceIndex)
                    throw new Exception("L'index doit être un nombre entier");

                var list = iText.Split(iSeparator);

                if (iOccurenceIndex > 0)
                    return list[integerIndex - 1];
                else
                    return list[list.Count() + integerIndex];
            }
            catch (Exception ex)
            {
                return "#Error! " + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne un tableau sans les ID de chaque item")]
        public string UDFGetArrayWithoutID([ParamInfo("List source", "Texte des items séparé par '|'")] string iList)
        {
            try
            {
                if (iList.IsNullOrEmpty())
                    throw new Exception("Le texte d'entrée est null ou vide");

                var listItems = iList.Split(Consts.Consts.ItemSeparator);

                var resultList = new List<string>();

                foreach (var item in listItems.Enum())
                    resultList.Add(GetStringWithoutID(item));

                return resultList.Concat(Consts.Consts.ItemSeparator);
            }
            catch (Exception ex)
            {
                return "#Error! " + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne le texte sans l'ID")]
        public string UDFGetStringWithoutID([ParamInfo("Texte à transformer", "Texte à transformer")] string iText)
        {
            try
            {
                return GetStringWithoutID(iText);
            }
            catch (Exception ex)
            {
                return "#Error! " + ex.Message;
            }
        }

        #endregion

        #region Private METHODS

        private string GetStringWithoutID(string iText)
        {
            if (iText.IsNullOrEmpty())
                throw new Exception("Le texte d'entrée est null ou vide");

            var splitList = iText.Split(Consts.Consts.IDSeparator);
            if (splitList.Count() != 2)
                throw new Exception("L'item '{0}' est invalide".FormatString(iText));

            return splitList.First();
        }

        #endregion
    }

    /// <summary>
    /// Table tools
    /// </summary>
    public partial class UDF
    {
        #region Public METHODS

        [Udf]
        [FunctionInfo("Retourne la valeur de la cellule par nom de colonne et numéro de ligne")]
        public string UDFGetValueFromDataTable([ParamInfo("Valeur DataTable", "Valeurs de la table")] IArrayValue iDatatable,
            [ParamInfo("Nom Colonne retour", "Nom de la colonne, texte dans la premiere ligne du tableau")] string iColumnName,
            [ParamInfo("Index Ligne", "Index de la ligne à retourner")] double iRowIndex)
        {
            try
            {
                return GetValueFromGetValueFromDataTable(iDatatable, iColumnName, iRowIndex);
            }
            catch (Exception ex)
            {
                return "#Error! " + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne la valeur de la cellule par nom de la colonne et valeur à rechercher dans une colonne nommée")]
        public string UDFGetValueFromDataTable([ParamInfo("Valeur DataTable", "Valeurs de la table")] IArrayValue iDatatable,
            [ParamInfo("Nom Colonne retour", "Nom de la colonne à retourner")] string iColumnName,
            [ParamInfo("Nom Colonne recherche", "Nom de la colonne à rechercher")] string iColumnSearchName,
            [ParamInfo("Valeur Colonne recherche", "Valeur à rechercher dans la colonne à rechercher")] string iSearchValue)
        {
            try
            {
                var rowIndex = GetRowIndexOfValueInNamedColumn(iDatatable, iColumnSearchName, iSearchValue);
                return GetValueFromGetValueFromDataTable(iDatatable, iColumnName, rowIndex);
            }
            catch (Exception ex)
            {
                return "#Error! " + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne l'index de la colonne par le nom de la colonne sur la première ligne")]
        public string UDFGetColumnIndexFromDataTable([ParamInfo("Valeur DataTable", "Valeurs de la table")] IArrayValue iDatatable,
           [ParamInfo("Nom Colonne retour", "Nom de la colonne à retourner")] string iColumnName)
        {
            try
            {
                var index = GetColumnIndexFromHeaderName(iDatatable, iColumnName);
                if (index == null)
                    throw new Exception("La colonne '{0}' est inexistante".FormatString(iColumnName));
                else
                    return (index + 1).ToString();
            }
            catch (Exception ex)
            {
                return "#Error! " + ex.Message;
            }
        }

        #endregion

        #region Private METHODS

        private int GetRowIndexOfValueInNamedColumn(IArrayValue iTableValue, string iHeaderName, string iValueName)
        {
            if (iTableValue == null)
                throw new Exception("Le controle DataTable est null");

            if (iHeaderName.IsNullOrEmpty())
                throw new Exception("Le nom de la colonne est invalide");

            if (iValueName.IsNullOrEmpty())
                throw new Exception("La valeur à rechercher est invalide");

            var searchColumnIndex = GetColumnIndexFromHeaderName(iTableValue, iHeaderName);
            if (searchColumnIndex == null)
                throw new Exception("Le nom de la colonne n'existe pas");
            for (int rowIndex = 1; rowIndex <= iTableValue.Rows - 1; rowIndex++)
            {
                if (iValueName == iTableValue.ToArray()[rowIndex, (int)searchColumnIndex].ToString())
                    return rowIndex;
            }

            throw new Exception("La valeur recherchée n'existe pas");
        }

        private int? GetColumnIndexFromHeaderName(IArrayValue iTableValue, string iHeaderName)
        {
            if (iTableValue == null)
                throw new Exception("La table est nulle");

            if (iHeaderName.IsNullOrEmpty())
                throw new Exception("Le nom de la colonne est invalide");

            var groupTableDataArray = iTableValue.ToArray();

            for (int columnIndex = 0; columnIndex <= iTableValue.Columns - 1; columnIndex++)
            {
                if (iHeaderName == groupTableDataArray[0, columnIndex].ToString())
                    return columnIndex;
            }
            return null;
        }

        private string GetValueFromGetValueFromDataTable(IArrayValue iTableValue, string iColumnName, double iRowIndex)
        {
            if (iTableValue == null)
                throw new Exception("Le controle DataTable est null");

            if (iRowIndex < 1)
                throw new Exception("L'index est invalide, il doit être supérieur ou égal à 1");

            var columnIndex = GetColumnIndexFromHeaderName(iTableValue, iColumnName);

            if (columnIndex == null)
                throw new Exception("La colonne demandé n'existe pas '{0}'".FormatString(iColumnName));
            else
                return iTableValue.ToArray()[(int)iRowIndex, (int)columnIndex].ToString();
        }

        #endregion
    }

    /// <summary>
    /// Controls shorthand
    /// </summary>
    public partial class UDF
    {
        #region Public METHODS

        [Udf]
        [FunctionInfo("Retourne si le controle doit être enable ou non. Si le texte d'explication est vide alors le controle doit être actif")]
        public bool UDFEnabled([ParamInfo("VariableEnabled", "Contient le texte expliquant pourquoi le controle est désactivé")] string iVariableEnabled)
        {
            if (iVariableEnabled != "")
                return false;
            else
                return true;
        }

        [Udf]
        [FunctionInfo("Retourne la couleur en fonction du texte d'erreur. Si le texte n'est pas vide, alors erreur donc retourne la couleur erreur définie dans la table settings")]
        public string UDFBackgroundColor([ParamInfo("VariableError", "Contient le texte expliquant pourquoi le controle est en erreur")]string iVariableError)
        {
            try
            {
                if (iVariableError != "")
                {
                    return this.Project.GetProjectSettings().ErrorColorName;
                }
                else
                    return "Transparent";
            }
            catch (Exception ex)
            {
                return "#Error!" + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne la couleur en fonction du texte d'erreur. Si le texte n'est pas vide, alors erreur donc retourne la couleur erreur définie dans la table settings")]
        public string UDFInputBackgroundColor([ParamInfo("VariableError", "Contient le texte expliquant pourquoi le controle est en erreur")]string iVariableError)
        {
            try
            {
                if (iVariableError != "")
                    return this.Project.GetProjectSettings().ErrorColorName;
                else
                    return this.Project.GetProjectSettings().NoErrorColorName;
            }
            catch (Exception ex)
            {
                return "#Error!" + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne le texte à afficher dans le tooltiptexte, en fonction du texte erreur ou disable. Le disable est prioritaire sur l'erreur")]
        public string UDFTooltipText([ParamInfo("VariableError", "Contient le texte expliquant pourquoi le controle est en erreur")] string iVariableError,
            [ParamInfo("VariableEnabled", "Contient le texte expliquant pourquoi le controle est désactivé")]string iVariableEnabled)
        {
            try
            {
                if (iVariableEnabled != "")
                    return iVariableEnabled;
                else if (iVariableError != "")
                    return iVariableError;
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return "#Error!" + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne la couleur en fonction du texte d'erreur. Si le texte n'est pas vide, alors erreur donc retourne la couleur erreur définie dans la table settings")]
        public string UDFTextColor([ParamInfo("VariableError", "Contient le texte expliquant pourquoi le controle est en erreur")] string iVariableError)
        {
            try
            {
                if (iVariableError != "")
                    return this.Project.GetProjectSettings().ErrorColorName;
                else
                    return this.Project.GetProjectSettings().NoErrorColorName;
            }
            catch (Exception ex)
            {
                return "#Error!" + ex.Message;
            }
        }

        #endregion
    }

    /// <summary>
    /// Pool
    /// </summary>
    public partial class UDF
    {
        #region Public METHODS

        [Udf]
        [FunctionInfo("Incrémente un compteur et retourne la nouvelle valeur. Si le compteur n'existe pas il sera créé automatiquement et commencera à 1. Des 0 seront ajouté devant pour obtenir la longueur")]
        public string UDFGetPoolCursorWithLenght([ParamInfo("PoolName", "Nom du compteur")] string iPoolName, [ParamInfo("Longueur", "Nombre fixe de digit du compteur")] double iLenght)
        {
            try
            {
                using (var poolService = new PoolService(this.Project.Group.GetEnvironment().GetSQLExtendConnectionString()))
                {
                    var shortCounter = poolService.GetPoolCursor(iPoolName);
                    return shortCounter.PadLeft(7, '0');
                }
            }
            catch (Exception ex)
            {
                return "#Error!" + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Incrémente un compteur sans préfixe et retourne la nouvelle valeur. Si le compteur n'existe pas il sera créé automatiquement et commencera à 1")]
        public string UDFGetPoolCursor([ParamInfo("PoolName", "Nom du compteur")] string iPoolName)
        {
            try
            {
                using (var poolService = new PoolService(this.Project.Group.GetEnvironment().GetSQLExtendConnectionString()))
                    return poolService.GetPoolCursor(iPoolName);
            }
            catch (Exception ex)
            {
                return "#Error!" + ex.Message;
            }
        }

        #endregion
    }

    /// <summary>
    /// Specification
    /// </summary>
    public partial class UDF
    {
        #region Public METHODS

        [Udf]
        [FunctionInfo("Retourne s'il existe un controle en état d'erreur. L'erreur est détectée par la couleur du backGroundColor du Controle. La couleur associée est paramétrable dans le table Settings")]
        public bool UDFIsContainsError()
        {
            return this.Project.GetErrorMessageList().IsNotNullAndNotEmpty();
        }

        [Udf]
        [FunctionInfo("Retour le préfixe lié au groupe")]
        public string UDFGetSpecificationGroupPrefix()
        {
            try
            {
                return this.Project.Group.GetEnvironment().GetSpecificationPrefix();
            }
            catch (Exception ex)
            {
                return "#Error!" + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne le chemin du fichier des meta données de n'importe quelle spécification")]
        public string UDFGetSpecificationMetadataFilePath([ParamInfo("Nom specification", "Nom de la spécification")]string iSpecificationName)
        {
            try
            {
                return this.Project.Group.GetSpecificationMetadataFilePath(iSpecificationName);
            }
            catch (Exception ex)
            {
                return "#Error!" + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne le chemin du fichier des meta données de n'importe quel projet")]
        public string UDFGetMasterProjectMetadataFilePath([ParamInfo("Nom projet", "Nom du projet")]string iProjectName)
        {
            try
            {
                return this.Project.GetProjectMetadataFilePath(this.Project.Group.GetProjectId(iProjectName));
            }
            catch (Exception ex)
            {
                return "#Error!" + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne le nom de la specification edité en lisant le nom du fichier drivespec, c'est le seule moyen d'avoir la spec édité quand le fichier driveprjx est remplacé")]
        public string UDFGetEditingSpecificationName()
        {
            try
            {
                return this.Project.SpecificationContext.GetEditingSpecificationName();
            }
            catch (Exception ex)
            {
                return "#Error!k" + ex.Message;
            }
        }

        [Udf]
        [FunctionInfo("Retourne du modèle depuis la spécification")]
        public bool UDFIsTemplateNameExists([ParamInfo("Nom modèle", "Nom modèle")]string iTemplateName)
        {
            try
            {
                using (var recordService = new RecordService(this.Project.Group.GetEnvironment().GetSQLExtendConnectionString()))
                {
                    if (recordService.GetDossierByTemplateName(iTemplateName, false) != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Udf]
        [FunctionInfo("Retourne la liste des états existant sur un projet")]
        public string UDFGetStateNameListOfProject([ParamInfo("Nom Projet", "Nom projet")]string iProjectName)
        {
            try
            {
                if (iProjectName.IsNullOrEmpty())
                    throw new Exception("Le nom du projet est invalide");
                
                //Récupère le projet depuis son nom
                var project = DriveWorks.Helper.GroupHelper.GetProjectList(this.Project.Group).Enum().SingleOrDefault(x => x.Name == iProjectName);

                //Récupére la liste des états
                var dataBaseQuery = new DataBaseQuery(this.Project.Group.GetEnvironment().GetSQLConnectionString());
                var stateList = dataBaseQuery.GetStateNameListOfProject(project.Id.ToString());

                return stateList.Enum().Concat("|");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}