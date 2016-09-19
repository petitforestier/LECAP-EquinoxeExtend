using DriveWorks.Forms;
using DriveWorks.Helper.Object;
using DriveWorks.Reporting;
using DriveWorks.Specification;
using Library.Tools.Comparator;
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

        public static List<string> GetTableSettingsList(this Project iProject)
        {
            var result = new List<string>();

            result.Add(PROJECTADDEDCONSTANTDATATABLENAME);
            result.Add(PROJECTDELETEDCONSTANTDATATABLENAME);
            result.Add(PROJECTDELETEDCONTROLDATATABLENAME);
            result.Add(PROJECTADDDEDCONTROLATATABLENAME);
            result.Add(PROJECTSETTINGSDATATABLENAME);
            return result;
        }

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

        public static string GetProjectMetadataFilePath(this Project iProject, string iProjectId)
        {
            var theMasterProject = iProject.Group.Projects.GetProject(new Guid(iProjectId));

            if (theMasterProject == null)
                throw new Exception("Le projet est inexistante");

            return theMasterProject.Directory + "\\" + theMasterProject.Name + theMasterProject.Extension;
        }

        public static List<string> GetErrorMessageList(this Project iProject)
        {
            var result = new List<string>();
            var formList = iProject.Navigation.GetForms(true, true);

            //Bouclage sur les forms
            foreach (var item in formList)
            {
                //Ignore les formes si besoin
                if (item.Form.Tag.ToLower().Trim().Contains(GetProjectSettings(iProject).TagIgnore))
                    continue;

                var formControlList = item.Form.Controls;
                //Bouclage sur les controls
                foreach (var controlItem in formControlList)
                {
                    if (controlItem.Enabled == false)
                        continue;

                    var errorMessage = iProject.GetErrorMessageFromControlBase(controlItem, GetProjectSettings(iProject).ErrorColorName);
                    if (errorMessage.IsNotNullAndNotEmpty())
                        result.Add(errorMessage);
                }
            }
            return result;
        }

        public static Tuple<List<AddedControlManaged>, List<DeletedControlManaged>, List<AddedConstantManaged>, List<DeletedConstantManaged>> GetAddedDeletedControlConstant(this Project iProject)
        {
            //Control
            var addedControlList = iProject.GetAddedControlList();
            var deletedControlList = iProject.GetDeletedControlList();

            if (addedControlList.Exists2(x => x.ProjectVersion > GetProjectSettings(iProject).ProjectVersion))
                throw new Exception("Il existe dans la table des controles ajoutés, des controles de version supérieur");

            if (deletedControlList.Exists2(x => x.ProjectVersion > GetProjectSettings(iProject).ProjectVersion))
                throw new Exception("Il existe dans la table des controles supprimés, des controles de version supérieur");

            var controlBuildResult = iProject.BuildControlListFromVersion(new List<ControlState>(), Math.Truncate(GetProjectSettings(iProject).ProjectVersion));

            var controlComparator = new ListComparator<ControlState, ControlState>(controlBuildResult.Item1, x => x.Name, GetCurrentControlStateList(iProject), x => x.Name);

            var addedControlManagedList = new List<AddedControlManaged>();
            foreach (var item in controlComparator.NewList.Enum())
                addedControlManagedList.Add(new AddedControlManaged() { ControlName = item.Name, Message = "", ProjectVersion = GetProjectSettings(iProject).ProjectVersion });

            var deletedControlManagedList = new List<DeletedControlManaged>();
            foreach (var item in controlComparator.RemovedList.Enum())
                deletedControlManagedList.Add(new DeletedControlManaged() { ControlName = item.Name, Message = "", ProjectVersion = GetProjectSettings(iProject).ProjectVersion });

            //Constant
            var addedConstantList = iProject.GetAddedConstantList();
            var deletedConstantList = iProject.GetDeletedConstantList();

            if (addedConstantList.Exists2(x => x.ProjectVersion > GetProjectSettings(iProject).ProjectVersion))
                throw new Exception("Il existe dans la table des constantes ajoutées, des controles de version supérieur");

            if (deletedConstantList.Exists2(x => x.ProjectVersion > GetProjectSettings(iProject).ProjectVersion))
                throw new Exception("Il existe dans la table des constantes supprimées, des controles de version supérieur");

            var constantBuildResult = iProject.BuildConstantListFromVersion(new List<ConstantState>(), Math.Truncate(GetProjectSettings(iProject).ProjectVersion));

            var constantComparator = new ListComparator<ConstantState, ConstantState>(constantBuildResult, x => x.Name, GetCurrentConstantList(iProject), x => x.Name);

            var addedConstantManagedList = new List<AddedConstantManaged>();
            foreach (var item in constantComparator.NewList.Enum())
                addedConstantManagedList.Add(new AddedConstantManaged() { ConstantName = item.Name, ProjectVersion = GetProjectSettings(iProject).ProjectVersion });

            var deletedConstantManagedList = new List<DeletedConstantManaged>();
            foreach (var item in constantComparator.NewList.Enum())
                deletedConstantManagedList.Add(new DeletedConstantManaged() { ConstantName = item.Name, ProjectVersion = GetProjectSettings(iProject).ProjectVersion });

            return new Tuple<List<AddedControlManaged>, List<DeletedControlManaged>, List<AddedConstantManaged>, List<DeletedConstantManaged>>(addedControlManagedList, deletedControlManagedList, addedConstantManagedList, deletedConstantManagedList);
        }

        /// <summary>
        /// Récupération des controls du projet ouvert en ignorant les form et controls à ignorer
        /// </summary>
        /// <returns></returns>
        public static List<ControlState> GetCurrentControlStateList(this Project iProject)
        {
            var specificationControlStateList = new List<ControlState>();
            var formList = iProject.Navigation.GetForms(true, true);

            //Bouclage sur les forms
            foreach (var item in formList)
            {
                //Ignore les formes si besoin
                if (item.Form.Tag.ToLower().Trim().Contains((string)GetProjectSettings(iProject).TagIgnore))
                    continue;

                var formControlList = item.Form.Controls;
                //Bouclage sur les controls
                foreach (var controlItem in formControlList)
                {
                    //Ignore les controles si besoin
                    if (controlItem.Tag.ToLower().Trim().Contains((string)GetProjectSettings(iProject).TagIgnore))
                        continue;

                    var controlState = iProject.GetControlStateFormControlBase(controlItem);

                    if (controlState != null)
                        specificationControlStateList.Add(controlState);
                }
            }

            return specificationControlStateList;
        }

        /// <summary>
        /// Récupération des constantes du project en ignorant les paramètres d'entrée (commencant par paramètre, utilisé par le module intégration)
        /// </summary>
        /// <returns></returns>
        public static List<ConstantState> GetCurrentConstantList(this Project iProject)
        {
            var constants = iProject.Constants;
            var constantsList = new List<ConstantState>();

            foreach (var item in constants.GetConstants().Enum())
            {
                if (item.DisplayName.StartsWith("Parameter") == false)
                {
                    var newConstant = new ConstantState();
                    newConstant.Name = item.DisplayName;
                    newConstant.Value = item.Value.ToString();
                }
            }

            return constantsList;
        }

        public static void CheckControlConstantManagement(this Project iProject)
        {
            var tuple = GetAddedDeletedControlConstant(iProject);

            if (tuple.Item1.Any() || tuple.Item2.Any() || tuple.Item3.Any() || tuple.Item4.Any())
                throw new Exception("Le projet actuel possède des controles non managés. Contacter l'administrateur");
        }

        public static void SetTableControlItems(this Project iProject, string iControlName, ITableValue iTableValue)
        {
            var tableControl = (DataTableControl)iProject.Navigation.GetControl(iControlName);

            if (tableControl == null)
                throw new Exception("Le controle datatable nommé '{0}' est introuvable");

            tableControl.Items = iTableValue;
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

        public static Tuple<List<ControlState>, List<string>> BuildControlListFromVersion(this Project iProject, List<ControlState> iOriginalList, decimal iSpecificationVersion)
        {
            if (Math.Truncate(iSpecificationVersion) != Math.Truncate(iProject.GetProjectSettings().ProjectVersion))
                throw new Exception("La version majeure de début et de fin ne peuvent pas être différent");

            var messageList = new List<string>();

            var deletedList = iProject.GetDeletedControlList().Enum().Where(x => x.ProjectVersion <= iProject.GetProjectSettings().ProjectVersion).Enum().ToList().OrderBy(x => x.ProjectVersion).Enum().ToList();
            var addedList = iProject.GetAddedControlList().Enum().Where(x => x.ProjectVersion <= iProject.GetProjectSettings().ProjectVersion).Enum().ToList().OrderBy(x => x.ProjectVersion).Enum().ToList();

            var groupAdded = addedList.Enum().Where(x => x.ProjectVersion > iSpecificationVersion && x.ProjectVersion <= iProject.GetProjectSettings().ProjectVersion).Enum().OrderBy(x => x.ProjectVersion).Enum().GroupBy(x => x.ProjectVersion);
            var groupDeleted = deletedList.Enum().Where(x => x.ProjectVersion > iSpecificationVersion && x.ProjectVersion <= iProject.GetProjectSettings().ProjectVersion).Enum().OrderBy(x => x.ProjectVersion).Enum().GroupBy(x => x.ProjectVersion);

            List<decimal> versionList = groupAdded.Select(x => x.First().ProjectVersion).ToList();
            versionList.AddRange(groupDeleted.Select(x => x.First().ProjectVersion).ToList());
            versionList = versionList.GetWithoutDuplicates(x => x).Enum().ToList().OrderBy(x => x).Enum().ToList();

            //Bouclage sur toutes les versions possibles entre la version de sauvegarde et celle du config pour rejouer les modifications sans erreur
            foreach (var versionItem in versionList.Enum())
            {
                //Création des nouveaux controles d'abord si transfert supprimé vers nouveau
                var addedControlList = addedList.Enum().Where(x => x.ProjectVersion == versionItem).Enum().ToList();

                foreach (var addedItem in addedControlList.Enum())
                {
                    if (iOriginalList.NotExists2(x => x.Name == addedItem.ControlName))
                    {
                        var newControlState = new ControlState();
                        newControlState.Name = addedItem.ControlName;
                        newControlState.Message = addedItem.Message;

                        iOriginalList.Add(newControlState);
                    }
                    else
                        throw new Exception("Le controle existe déjà, données corrompues, Contacter l'administrateur");
                }

                //Suppression des controles
                var deletedControlList = deletedList.Enum().Where(x => x.ProjectVersion == versionItem).Enum().ToList();
                foreach (var deletedItem in deletedControlList.Enum())
                {
                    var theControlState = iOriginalList.Single(x => x.Name == deletedItem.ControlName);

                    if (deletedItem.TransfertControlName.IsNotNullAndNotEmpty())
                    {
                        var theTransfertControlState = iOriginalList.Single(x => x.Name == deletedItem.TransfertControlName);
                        theTransfertControlState.Value = theControlState.Value;
                    }
                    if (deletedItem.Message.IsNotNullAndNotEmpty())
                    {
                        messageList.Add(deletedItem.Message);
                    }
                    iOriginalList.Remove(theControlState);
                }
            }
            return new Tuple<List<ControlState>, List<string>>(iOriginalList, messageList);
        }

        public static List<ConstantState> BuildConstantListFromVersion(this Project iProject, List<ConstantState> iOriginalList, decimal iSpecificationVersion)
        {
            if (Math.Truncate(iSpecificationVersion) != Math.Truncate(iProject.GetProjectSettings().ProjectVersion))
                throw new Exception("La version majeure de début et de fin ne peuvent pas être différent");

            var deletedList = iProject.GetDeletedConstantList().Enum().Where(x => x.ProjectVersion <= iProject.GetProjectSettings().ProjectVersion).Enum().ToList().OrderBy(x => x.ProjectVersion).Enum().ToList();
            var addedList = iProject.GetAddedConstantList().Enum().Where(x => x.ProjectVersion <= iProject.GetProjectSettings().ProjectVersion).Enum().ToList().OrderBy(x => x.ProjectVersion).Enum().ToList();

            var messageList = new List<string>();

            var groupAdded = addedList.Enum().Where(x => x.ProjectVersion > iSpecificationVersion && x.ProjectVersion <= iProject.GetProjectSettings().ProjectVersion).Enum().OrderBy(x => x.ProjectVersion).Enum().GroupBy(x => x.ProjectVersion);
            var groupDeleted = deletedList.Enum().Where(x => x.ProjectVersion > iSpecificationVersion && x.ProjectVersion <= iProject.GetProjectSettings().ProjectVersion).Enum().OrderBy(x => x.ProjectVersion).Enum().GroupBy(x => x.ProjectVersion);

            List<decimal> versionList = groupAdded.Select(x => x.First().ProjectVersion).ToList();
            versionList.AddRange(groupDeleted.Select(x => x.First().ProjectVersion).ToList());
            versionList = versionList.GetWithoutDuplicates(x => x).Enum().ToList().OrderBy(x => x).Enum().ToList();

            //Bouclage sur toutes les versions possibles entre la version de sauvegarde et celle du config pour rejouer les modifications sans erreur
            foreach (var versionItem in versionList.Enum())
            {
                //Création des nouveaux constantes d'abord si transfert supprimé vers nouveau
                var addedConstantList = addedList.Enum().Where(x => x.ProjectVersion == versionItem).Enum().ToList();

                foreach (var addedItem in addedConstantList.Enum())
                {
                    if (iOriginalList.NotExists2(x => x.Name == addedItem.ConstantName))
                    {
                        var newControlState = new ConstantState();
                        newControlState.Name = addedItem.ConstantName;

                        iOriginalList.Add(newControlState);
                    }
                    else
                        throw new Exception("La constante existe déjà, données corrompues, Contacter l'administrateur");
                }

                //Suppression des controles
                var deletedConstantList = deletedList.Enum().Where(x => x.ProjectVersion == versionItem).Enum().ToList();
                foreach (var deletedItem in deletedConstantList.Enum())
                {
                    var theControlState = iOriginalList.Single(x => x.Name == deletedItem.ConstantName);

                    if (deletedItem.TransfertConstantName.IsNotNullAndNotEmpty())
                    {
                        var theTransfertControlState = iOriginalList.Single(x => x.Name == deletedItem.TransfertConstantName);
                        theTransfertControlState.Value = theControlState.Value;
                    }
                    iOriginalList.Remove(theControlState);
                }
            }
            return iOriginalList;
        }

        #endregion

        #region Private FIELDS

        private const string PROJECTADDEDCONSTANTDATATABLENAME = "PrjAddedConstant";

        private const string PROJECTDELETEDCONSTANTDATATABLENAME = "PrjDeletedConstant";

        private const string PROJECTDELETEDCONTROLDATATABLENAME = "PrjDeletedControl";

        private const string PROJECTADDDEDCONTROLATATABLENAME = "PrjAddedControl";

        private const string PROJECTSETTINGSDATATABLENAME = "PrjSettings";

        private const string PROJECTVERSION = "ProjectVersion";

        private const string TAGIGNORE = "IgnoreTag";

        private const string ERRORCOLORNAME = "ErrorColorName";

        private const string NOERRORCOLORNAME = "NoErrorColorName";

        private const string USERDEBUGCONTROLNAME = "UserDebugControlName";

        private const string ERRORCONSTANTNAME = "ErrorConstantName";

        #endregion

        #region Private METHODS

        private static List<AddedControlManaged> GetAddedControlList(this Project iProject)
        {
            var result = new List<AddedControlManaged>();
            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTADDDEDCONTROLATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTADDDEDCONTROLATATABLENAME + " est inexistante");

            var projectTableDataArray = projectTable.GetCachedTableData();

            //Bouclage sur les lignes, mais ne tient pas compte de la première qui est l'entete
            for (int rowIndex = 1; rowIndex <= projectTableDataArray.GetLength(0) - 1; rowIndex++)
            {
                var newAddedControl = new AddedControlManaged();

                newAddedControl.ControlName = projectTableDataArray[rowIndex, 0].ToString();
                if (newAddedControl.ControlName.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' le nom du control est invalide".FormatString(PROJECTADDDEDCONTROLATATABLENAME, rowIndex + 1));

                newAddedControl.ProjectVersion = Convert.ToDecimal(projectTableDataArray[rowIndex, 1].ToString());
                if (newAddedControl.ProjectVersion < 1)
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' la version de projet est invalide".FormatString(PROJECTADDDEDCONTROLATATABLENAME, rowIndex + 1));

                newAddedControl.Message = projectTableDataArray[rowIndex, 2].ToString();

                result.Add(newAddedControl);
            }

            return result;
        }

        private static List<DeletedControlManaged> GetDeletedControlList(this Project iProject)
        {
            var result = new List<DeletedControlManaged>();
            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTDELETEDCONTROLDATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTDELETEDCONTROLDATATABLENAME + " est inexistante");

            var projectTableDataArray = projectTable.GetCachedTableData();

            //Bouclage sur les lignes, mais ne tient pas compte de la première qui est l'entete
            for (int rowIndex = 1; rowIndex <= projectTableDataArray.GetLength(0) - 1; rowIndex++)
            {
                var newDeletedControl = new DeletedControlManaged();

                newDeletedControl.ControlName = projectTableDataArray[rowIndex, 0].ToString();
                if (newDeletedControl.ControlName.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' le nom du control est invalide".FormatString(PROJECTDELETEDCONTROLDATATABLENAME, rowIndex + 1));

                newDeletedControl.ControlDescription = projectTableDataArray[rowIndex, 1].ToString();
                if (newDeletedControl.ControlDescription.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' la description du control est invalide".FormatString(PROJECTDELETEDCONTROLDATATABLENAME, rowIndex + 1));

                newDeletedControl.ProjectVersion = Convert.ToDecimal(projectTableDataArray[rowIndex, 2].ToString());
                if (newDeletedControl.ProjectVersion <= 1)
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' la version de projet est invalide".FormatString(PROJECTDELETEDCONTROLDATATABLENAME, rowIndex + 1));

                newDeletedControl.TransfertControlName = projectTableDataArray[rowIndex, 3].ToString();
                newDeletedControl.Message = projectTableDataArray[rowIndex, 4].ToString();

                result.Add(newDeletedControl);
            }

            return result;
        }

        private static List<AddedConstantManaged> GetAddedConstantList(this Project iProject)
        {
            var result = new List<AddedConstantManaged>();
            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTADDEDCONSTANTDATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTADDEDCONSTANTDATATABLENAME + " est inexistante");

            var projectTableDataArray = projectTable.GetCachedTableData();

            //Bouclage sur les lignes, mais ne tient pas compte de la première qui est l'entete
            for (int rowIndex = 1; rowIndex <= projectTableDataArray.GetLength(0) - 1; rowIndex++)
            {
                var newAddedConstant = new AddedConstantManaged();

                newAddedConstant.ConstantName = projectTableDataArray[rowIndex, 0].ToString();
                if (newAddedConstant.ConstantName.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' le nom du control est invalide".FormatString(PROJECTADDEDCONSTANTDATATABLENAME, rowIndex + 1));

                newAddedConstant.ProjectVersion = Convert.ToDecimal(projectTableDataArray[rowIndex, 1].ToString());
                if (newAddedConstant.ProjectVersion < 1)
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' la version mineure de projet est invalide".FormatString(PROJECTADDEDCONSTANTDATATABLENAME, rowIndex + 1));

                result.Add(newAddedConstant);
            }

            return result;
        }

        private static List<DeletedConstantManaged> GetDeletedConstantList(this Project iProject)
        {
            var result = new List<DeletedConstantManaged>();
            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTDELETEDCONSTANTDATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTDELETEDCONSTANTDATATABLENAME + " est inexistante");

            var projectTableDataArray = projectTable.GetCachedTableData();

            //Bouclage sur les lignes, mais ne tient pas compte de la première qui est l'entete
            for (int rowIndex = 1; rowIndex <= projectTableDataArray.GetLength(0) - 1; rowIndex++)
            {
                var newDeletedConstant = new DeletedConstantManaged();

                newDeletedConstant.ConstantName = projectTableDataArray[rowIndex, 0].ToString();
                if (newDeletedConstant.ConstantName.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' le nom du control est invalide".FormatString(PROJECTDELETEDCONSTANTDATATABLENAME, rowIndex + 1));

                newDeletedConstant.ProjectVersion = Convert.ToDecimal(projectTableDataArray[rowIndex, 1].ToString());
                if (newDeletedConstant.ProjectVersion <= 1)
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' la version majeure de projet est invalide".FormatString(PROJECTDELETEDCONSTANTDATATABLENAME, rowIndex + 1));

                newDeletedConstant.TransfertConstantName = projectTableDataArray[rowIndex, 2].ToString();

                result.Add(newDeletedConstant);
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// Gestion message
    /// </summary>
    public partial class ProjectHelper
    {
        #region Public METHODS

        public static void ClearMessages(this Project iProject)
        {
            var resultControl = iProject.Navigation.GetControl(iProject.GetProjectSettings().UserDebugControlName);
            resultControl.SetInputValue("");
        }

        public static void AddErrorMessage(this Project iProject, string iMessage, Exception iException)
        {
            iProject.AddMessage(iMessage);
            iProject.AddMessage(iException.Message);
            iProject.SpecificationContext.Report.WriteEntry(ReportingLevel.Minimal, ReportEntryType.Error, "Specification Management", iMessage, iException.Message, null);

            iProject.SetErrorConstant(true);
        }

        public static void AddMessage(this Project iProject, string iMessage)
        {
            TextBox theControl = null;
            iProject.Navigation.TryGetControl<TextBox>(iProject.GetProjectSettings().UserDebugControlName, ref theControl);
            var message = theControl.Text;
            if (message.IsNotNullAndNotEmpty())
                message += Environment.NewLine;
            message += iMessage;
            theControl.Text = message;
        }

        public static void SetErrorConstant(this Project iProject, bool iIsError)
        {
            ProjectConstant theConstant = null;
            iProject.Constants.TryGetConstant(iProject.GetProjectSettings().ErrorConstantName, ref theConstant);
            theConstant.Value = iIsError;
        }

        #endregion
    }

    /// <summary>
    /// Gestion control
    /// </summary>
    public partial class ProjectHelper
    {
        #region Public METHODS

        public static ControlState GetControlStateFormControlBase(this Project iProject, ControlBase iControlBase)
        {
            //Textbox
            if (iControlBase.GetType() == typeof(TextBox))
            {
                var textboxControl = (TextBox)iControlBase;
                return new ControlState() { Name = textboxControl.Name, Value = textboxControl.Text };
            }
            //Checkbox
            else if (iControlBase.GetType() == typeof(CheckBox))
            {
                var checkBox = (CheckBox)iControlBase;
                return new ControlState() { Name = checkBox.Name, Value = checkBox.Checked.ToString() };
            }
            //Combobox
            else if (iControlBase.GetType() == typeof(ComboBox))
            {
                var comboBox = (ComboBox)iControlBase;
                return new ControlState() { Name = comboBox.Name, Value = comboBox.SelectedItem.ToString() };
            }
            //DataTable
            else if (iControlBase.GetType() == typeof(DataTableControl))
            {
                var datatable = (DataTableControl)iControlBase;
                if (datatable.SelectedRowIndex == 0)
                    return new ControlState() { Name = datatable.Name, Value = string.Empty };
                else
                    return new ControlState() { Name = datatable.Name, Value = datatable.SelectedRowIdentity.ToString() };
            }
            //DatePicker
            else if (iControlBase.GetType() == typeof(DatePicker))
            {
                var dataPicker = (DatePicker)iControlBase;
                return new ControlState() { Name = dataPicker.Name, Value = dataPicker.DateValue.ToString() };
            }
            //Listbox
            else if (iControlBase.GetType() == typeof(ListBox))
            {
                var listBox = (ListBox)iControlBase;
                return new ControlState() { Name = listBox.Name, Value = listBox.SelectedItem.ToString() };
            }
            //Numeric
            else if (iControlBase.GetType() == typeof(NumericTextBox))
            {
                var numeric = (NumericTextBox)iControlBase;
                return new ControlState() { Name = numeric.Name, Value = numeric.Value.ToString() };
            }
            //Optionbutton
            else if (iControlBase.GetType() == typeof(OptionButton))
            {
                var optionButton = (OptionButton)iControlBase;
                return new ControlState() { Name = optionButton.Name, Value = optionButton.SelectedOption.ToString() };
            }
            //Slider
            else if (iControlBase.GetType() == typeof(Slider))
            {
                var slider = (Slider)iControlBase;
                return new ControlState() { Name = slider.Name, Value = slider.Value.ToString() };
            }
            //Spin
            else if (iControlBase.GetType() == typeof(SpinButton))
            {
                var spin = (SpinButton)iControlBase;
                return new ControlState() { Name = spin.Name, Value = spin.Value.ToString() };
            }
            //Measurement
            else if (iControlBase.GetType() == typeof(MeasurementTextBox))
            {
                var measurement = (MeasurementTextBox)iControlBase;
                return new ControlState() { Name = measurement.Name, Value = measurement.GetInputValue().ToString(), Value2 = measurement.DisplayUnits.ToString() };
            }
            else if (iControlBase.GetType() == typeof(Hyperlink)
                    || iControlBase.GetType() == typeof(Label)
                    || iControlBase.GetType() == typeof(PictureBox)
                    || iControlBase.GetType() == typeof(PictureBox)
                    || iControlBase.GetType() == typeof(ChildSpecificationList)
                    || iControlBase.GetType() == typeof(DataGrid)
                    || iControlBase.GetType() == typeof(DialogButton)
                    || iControlBase.GetType() == typeof(FrameControl)
                    || iControlBase.GetType() == typeof(ItemList)
                    || iControlBase.GetType() == typeof(MacroButton)
                    || iControlBase.GetType() == typeof(OptionGroup)
                    || iControlBase.GetType() == typeof(UploadControl)
                    || iControlBase.GetType().ToString() == "DriveWorks.Forms.WebFrameControl") // La classe n'existe pas...
            {
                return null;
            }
            else
                throw new Exception("Le controle '{0}' de type '{1}', n'est pas géré, contacter l'administrateur".FormatString(iControlBase.Name, iControlBase.GetType().ToString()));
        }

        public static void SetControlBaseFromControlState(this Project iProject, ControlBase iControlBase, ControlState iControlState)
        {
            //MeasurementTextBox
            if (iControlBase.GetType() == typeof(MeasurementTextBox))
            {
                var measureControl = (MeasurementTextBox)iControlBase;
                measureControl.DisplayValue = iControlState.Value;
                measureControl.DisplayUnits = (DistanceMeasurementUnitOptions)Enum.Parse(typeof(DistanceMeasurementUnitOptions), iControlState.Value2, true);
            }
            //DataTableControl
            else if (iControlBase.GetType() == typeof(DataTableControl))
            {
                var datatableControl = (DataTableControl)iControlBase;
                if (iControlState.Value != string.Empty)
                    datatableControl.SelectedRowIdentity = iControlState.Value;
            }
            //Reste géré simplement
            else if (iControlBase.GetType() == typeof(TextBox)
                || iControlBase.GetType() == typeof(TextBox)
                || iControlBase.GetType() == typeof(ComboBox)
                || iControlBase.GetType() == typeof(ListBox)
                || iControlBase.GetType() == typeof(DatePicker)
                || iControlBase.GetType() == typeof(NumericTextBox)
                || iControlBase.GetType() == typeof(OptionButton)
                || iControlBase.GetType() == typeof(Slider)
                || iControlBase.GetType() == typeof(SpinButton)
                || iControlBase.GetType() == typeof(CheckBox))
            {
                iControlBase.SetInputValue(iControlState.Value);
            }
            //Reste des controls sans sauvegarde (permettra d'identifier les nouveaux controles dans les prochaines versions sans créer d'erreur
            else if (iControlBase.GetType() == typeof(Hyperlink)
                || iControlBase.GetType() == typeof(Label)
                || iControlBase.GetType() == typeof(PictureBox)
                || iControlBase.GetType() == typeof(PictureBox)
                || iControlBase.GetType() == typeof(ChildSpecificationList)
                || iControlBase.GetType() == typeof(DataGrid)
                || iControlBase.GetType() == typeof(DialogButton)
                || iControlBase.GetType() == typeof(FrameControl)
                || iControlBase.GetType() == typeof(ItemList)
                || iControlBase.GetType() == typeof(MacroButton)
                || iControlBase.GetType() == typeof(OptionGroup)
                || iControlBase.GetType() == typeof(UploadControl)
                || iControlBase.GetType().ToString() == "DriveWorks.Forms.WebFrameControl") // La classe n'existe pas...
            {
            }
            else
                throw new Exception("Le controle '{0}' de type '{1}', n'est pas géré, contacter l'administrateur".FormatString(iControlBase.Name, iControlBase.GetType().ToString()));
        }

        public static string GetErrorMessageFromControlBase(this Project iProject, ControlBase iControlBase, string iErrorColorName)
        {
            //Textbox
            if (iControlBase.GetType() == typeof(TextBox))
            {
                var textboxControl = (TextBox)iControlBase;
                if (textboxControl.InputBackgroundColor != null)
                    if (textboxControl.InputBackgroundColor.Name == iErrorColorName)
                        return textboxControl.TooltipText;
            }
            //Checkbox
            else if (iControlBase.GetType() == typeof(CheckBox))
            {
                var checkBox = (CheckBox)iControlBase;
                if (checkBox.BackgroundColor != null)
                    if (checkBox.BackgroundColor.Name == iErrorColorName)
                        return checkBox.TooltipText;
            }
            //Combobox
            else if (iControlBase.GetType() == typeof(ComboBox))
            {
                var comboBox = (ComboBox)iControlBase;
                if (comboBox.BackgroundColor != null)
                    if (comboBox.BackgroundColor.Name == iErrorColorName)
                        return comboBox.TooltipText;
            }
            //DataTable
            else if (iControlBase.GetType() == typeof(DataTableControl))
            {
                var datatable = (DataTableControl)iControlBase;
                if (datatable.BackgroundColor != null)
                    if (datatable.BackgroundColor.Name == iErrorColorName)
                        return datatable.TooltipText;
            }
            //DatePicker
            else if (iControlBase.GetType() == typeof(DatePicker))
            {
                var dataPicker = (DatePicker)iControlBase;
                if (dataPicker.BackgroundColor != null)
                    if (dataPicker.BackgroundColor.Name == iErrorColorName)
                        return dataPicker.TooltipText;
            }
            //Listbox
            else if (iControlBase.GetType() == typeof(ListBox))
            {
                var listBox = (ListBox)iControlBase;
                if (listBox.BackgroundColor != null)
                    if (listBox.BackgroundColor.Name == iErrorColorName)
                        return listBox.TooltipText;
            }
            //Numeric
            else if (iControlBase.GetType() == typeof(NumericTextBox))
            {
                var numeric = (NumericTextBox)iControlBase;
                if (numeric.BackgroundColor != null)
                    if (numeric.BackgroundColor.Name == iErrorColorName)
                        return numeric.TooltipText;
            }
            //Optionbutton
            else if (iControlBase.GetType() == typeof(OptionButton))
            {
                var optionButton = (OptionButton)iControlBase;
                if (optionButton.BackgroundColor != null)
                    if (optionButton.BackgroundColor.Name == iErrorColorName)
                        return optionButton.TooltipText;
            }
            //Slider
            else if (iControlBase.GetType() == typeof(Slider))
            {
                var slider = (Slider)iControlBase;
                if (slider.BackgroundColor != null)
                    if (slider.BackgroundColor.Name == iErrorColorName)
                        return slider.TooltipText;
            }
            //Spin
            else if (iControlBase.GetType() == typeof(SpinButton))
            {
                var spin = (SpinButton)iControlBase;
                if (spin.BackgroundColor != null)
                    if (spin.BackgroundColor.Name == iErrorColorName)
                        return spin.TooltipText;
            }
            //Measurement
            else if (iControlBase.GetType() == typeof(MeasurementTextBox))
            {
                var measurement = (MeasurementTextBox)iControlBase;
                if (measurement.BackgroundColor != null)
                    if (measurement.BackgroundColor.Name == iErrorColorName)
                        return measurement.TooltipText;
            }
            //Reste
            else if (iControlBase.GetType() == typeof(Hyperlink)
                    || iControlBase.GetType() == typeof(Label)
                    || iControlBase.GetType() == typeof(PictureBox)
                    || iControlBase.GetType() == typeof(PictureBox)
                    || iControlBase.GetType() == typeof(ChildSpecificationList)
                    || iControlBase.GetType() == typeof(DataGrid)
                    || iControlBase.GetType() == typeof(DialogButton)
                    || iControlBase.GetType() == typeof(FrameControl)
                    || iControlBase.GetType() == typeof(ItemList)
                    || iControlBase.GetType() == typeof(MacroButton)
                    || iControlBase.GetType() == typeof(OptionGroup)
                    || iControlBase.GetType() == typeof(UploadControl)
                    || iControlBase.GetType().ToString() == "DriveWorks.Forms.WebFrameControl") // La classe n'existe pas...
            {
                return null;
            }
            else
                throw new Exception("Le controle '{0}' de type '{1}', n'est pas géré, contacter l'administrateur".FormatString(iControlBase.Name, iControlBase.GetType().ToString()));
            return null;
        }

        #endregion
    }
}