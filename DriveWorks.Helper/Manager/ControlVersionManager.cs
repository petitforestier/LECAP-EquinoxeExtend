using DriveWorks.Forms;
using DriveWorks.Helper.Object;
using Library.Tools.Comparator;
using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Tools;

namespace DriveWorks.Helper.Manager
{
    public static partial class ControlVersionManager
    {
        #region Public METHODS

        public static void AddToAddedControlProjectDataTable(this Project iProject, List<AddedControlManaged> iNewControlList)
        {
            if (iNewControlList.IsNullOrEmpty())
                throw new Exception("La liste des nouveaux controles est vide");

            var addedControlFlattenList = new List<List<string>>();

            foreach (var item in iNewControlList)
            {
                var newList = new List<string>();

                //Attention ordre important
                newList.Add(item.ControlName);
                newList.Add(item.ProjectVersion.ToString());
                newList.Add(item.Message);

                addedControlFlattenList.Add(newList);
            }

            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTADDDEDCONTROLATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTADDDEDCONTROLATATABLENAME + " est inexistante");

            var simpleDataTable = (DriveWorks.SimpleDataTable)projectTable;

            DataTableHelper.AddDataToSimpleDataTable(simpleDataTable, addedControlFlattenList);
        }

        public static void AddToDeletedControlProjectDataTable(this Project iProject, List<DeletedControlManaged> iOldControlList)
        {
            if (iOldControlList.IsNullOrEmpty())
                throw new Exception("La liste des controles à supprimer est vide");

            var deletedControlFlattenList = new List<List<string>>();

            foreach (var item in iOldControlList)
            {
                var newList = new List<string>();

                //Attention ordre important
                newList.Add(item.ControlName);
                newList.Add(item.ControlDescription);
                newList.Add(item.ProjectVersion.ToString());
                newList.Add(item.TransfertControlName);
                newList.Add(item.Message);

                deletedControlFlattenList.Add(newList);
            }

            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTDELETEDCONTROLDATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTDELETEDCONTROLDATATABLENAME + " est inexistante");

            var simpleDataTable = (DriveWorks.SimpleDataTable)projectTable;

            DataTableHelper.AddDataToSimpleDataTable(simpleDataTable, deletedControlFlattenList);
        }

        public static void AddToAddedConstantProjectDataTable(this Project iProject, List<AddedConstantManaged> iAddedConstantList)
        {
            if (iAddedConstantList.IsNullOrEmpty())
                throw new Exception("La liste des nouvelles constantes est vide");

            var flattenList = new List<List<string>>();

            foreach (var item in iAddedConstantList)
            {
                var newList = new List<string>();

                newList.Add(item.ConstantName);
                newList.Add(item.ProjectVersion.ToString());

                flattenList.Add(newList);
            }

            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTADDEDCONSTANTDATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTADDEDCONSTANTDATATABLENAME + " est inexistante");

            var simpleDataTable = (DriveWorks.SimpleDataTable)projectTable;

            DataTableHelper.AddDataToSimpleDataTable(simpleDataTable, flattenList);
        }

        public static void AddToDeletedConstantProjectDataTable(this Project iProject, List<DeletedConstantManaged> iOldConstantList)
        {
            if (iOldConstantList.IsNullOrEmpty())
                throw new Exception("La liste des constantes à supprimer est vide");

            var flattenList = new List<List<string>>();

            foreach (var item in iOldConstantList)
            {
                var newList = new List<string>();

                newList.Add(item.ConstantName);
                newList.Add(item.ProjectVersion.ToString());
                newList.Add(item.TransfertConstantName);

                flattenList.Add(newList);
            }

            var projectTable = iProject.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTDELETEDCONSTANTDATATABLENAME);
            if (projectTable == null)
                throw new Exception("La table de projet : " + PROJECTDELETEDCONSTANTDATATABLENAME + " est inexistante");

            var simpleDataTable = (DriveWorks.SimpleDataTable)projectTable;

            DataTableHelper.AddDataToSimpleDataTable(simpleDataTable, flattenList);
        }

        public static Tuple<List<AddedControlManaged>, List<DeletedControlManaged>, List<AddedConstantManaged>, List<DeletedConstantManaged>> GetAddedDeletedControlConstant(this Project iProject)
        {
            //Control
            var addedControlList = iProject.GetAddedControlList();
            var deletedControlList = iProject.GetDeletedControlList();

            if (addedControlList.Exists2(x => x.ProjectVersion > SettingsManager.GetProjectSettings(iProject).ProjectVersion))
                throw new Exception("Il existe dans la table des controles ajoutés, des controles de version supérieur");

            if (deletedControlList.Exists2(x => x.ProjectVersion > SettingsManager.GetProjectSettings(iProject).ProjectVersion))
                throw new Exception("Il existe dans la table des controles supprimés, des controles de version supérieur");

            var controlBuildResult = iProject.BuildControlListFromVersion(new List<ControlState>(), Math.Truncate(SettingsManager.GetProjectSettings(iProject).ProjectVersion));

            var controlComparator = new ListComparator<ControlState, ControlState>(controlBuildResult.Item1, x => x.Name, GetCurrentControlStateList(iProject), x => x.Name);

            var addedControlManagedList = new List<AddedControlManaged>();
            foreach (var item in controlComparator.NewList.Enum())
                addedControlManagedList.Add(new AddedControlManaged() { ControlName = item.Name, Message = "", ProjectVersion = SettingsManager.GetProjectSettings(iProject).ProjectVersion });

            var deletedControlManagedList = new List<DeletedControlManaged>();
            foreach (var item in controlComparator.RemovedList.Enum())
                deletedControlManagedList.Add(new DeletedControlManaged() { ControlName = item.Name, Message = "", ProjectVersion = SettingsManager.GetProjectSettings(iProject).ProjectVersion });

            //Constant
            var addedConstantList = iProject.GetAddedConstantList();
            var deletedConstantList = iProject.GetDeletedConstantList();

            if (addedConstantList.Exists2(x => x.ProjectVersion > SettingsManager.GetProjectSettings(iProject).ProjectVersion))
                throw new Exception("Il existe dans la table des constantes ajoutées, des controles de version supérieur");

            if (deletedConstantList.Exists2(x => x.ProjectVersion > SettingsManager.GetProjectSettings(iProject).ProjectVersion))
                throw new Exception("Il existe dans la table des constantes supprimées, des controles de version supérieur");

            var constantBuildResult = iProject.BuildConstantListFromVersion(new List<ConstantState>(), Math.Truncate(SettingsManager.GetProjectSettings(iProject).ProjectVersion));

            var constantComparator = new ListComparator<ConstantState, ConstantState>(constantBuildResult, x => x.Name, GetCurrentConstantList(iProject), x => x.Name);

            var addedConstantManagedList = new List<AddedConstantManaged>();
            foreach (var item in constantComparator.NewList.Enum())
                addedConstantManagedList.Add(new AddedConstantManaged() { ConstantName = item.Name, ProjectVersion = SettingsManager.GetProjectSettings(iProject).ProjectVersion });

            var deletedConstantManagedList = new List<DeletedConstantManaged>();
            foreach (var item in constantComparator.NewList.Enum())
                deletedConstantManagedList.Add(new DeletedConstantManaged() { ConstantName = item.Name, ProjectVersion = SettingsManager.GetProjectSettings(iProject).ProjectVersion });

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
                if (item.Form.Tag.ToLower().Trim().Contains((string)SettingsManager.GetProjectSettings(iProject).TagIgnore))
                    continue;

                var formControlList = item.Form.Controls;
                //Bouclage sur les controls
                foreach (var controlItem in formControlList)
                {
                    //Ignore les controles si besoin
                    if (controlItem.Tag.ToLower().Trim().Contains((string)SettingsManager.GetProjectSettings(iProject).TagIgnore))
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

        public static List<string> GetControlTableNameList(this Project iProject)
        {
            var result = new List<string>();
            result.Add(PROJECTADDEDCONSTANTDATATABLENAME);
            result.Add(PROJECTDELETEDCONSTANTDATATABLENAME);
            result.Add(PROJECTDELETEDCONTROLDATATABLENAME);
            result.Add(PROJECTADDDEDCONTROLATATABLENAME);
            return result;
        }

        #endregion

        #region Private FIELDS

        private const string PROJECTADDEDCONSTANTDATATABLENAME = "PrjAddedConstant";
        private const string PROJECTDELETEDCONSTANTDATATABLENAME = "PrjDeletedConstant";
        private const string PROJECTDELETEDCONTROLDATATABLENAME = "PrjDeletedControl";
        private const string PROJECTADDDEDCONTROLATATABLENAME = "PrjAddedControl";

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

                var controlName = projectTableDataArray[rowIndex, 0];
                newAddedControl.ControlName = (controlName != null) ? controlName.ToString() : null;
                if (newAddedControl.ControlName.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' le nom du control est invalide".FormatString(PROJECTADDDEDCONTROLATATABLENAME, rowIndex + 1));

                var projectVersion = projectTableDataArray[rowIndex, 1];
                newAddedControl.ProjectVersion = (projectVersion!= null) ? ((projectVersion.ToString() != "") ? Convert.ToDecimal(projectVersion.ToString()):0) : 0;
                if (newAddedControl.ProjectVersion < 1)
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' la version de projet est invalide".FormatString(PROJECTADDDEDCONTROLATATABLENAME, rowIndex + 1));

                var message = projectTableDataArray[rowIndex, 2];
                newAddedControl.Message = (message != null) ? message.ToString() : null;

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

                var controlName = projectTableDataArray[rowIndex, 0];
                newDeletedControl.ControlName = (controlName != null) ? controlName.ToString() : null;
                if (newDeletedControl.ControlName.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' le nom du control est invalide".FormatString(PROJECTDELETEDCONTROLDATATABLENAME, rowIndex + 1));

                var controlDescription = projectTableDataArray[rowIndex, 1];
                newDeletedControl.ControlDescription = (controlDescription != null) ? controlDescription.ToString() : null;
                if (newDeletedControl.ControlDescription.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' la description du control est invalide".FormatString(PROJECTDELETEDCONTROLDATATABLENAME, rowIndex + 1));

                var projectVersion = projectTableDataArray[rowIndex, 2];
                newDeletedControl.ProjectVersion = (projectVersion != null) ? ((projectVersion.ToString() != "") ? Convert.ToDecimal(projectVersion.ToString()) : 0) : 0;
                if (newDeletedControl.ProjectVersion < 1)
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' la version de projet est invalide".FormatString(PROJECTDELETEDCONTROLDATATABLENAME, rowIndex + 1));

                var transfertControlName = projectTableDataArray[rowIndex, 3];
                newDeletedControl.TransfertControlName = (transfertControlName != null) ? transfertControlName.ToString() : null;

                var message = projectTableDataArray[rowIndex, 4];
                newDeletedControl.Message = (message != null) ? message.ToString() : null;

                if(newDeletedControl.TransfertControlName.IsNullOrEmpty() && newDeletedControl.Message.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' le transfert ou le message est manquant. Un des deux est obligatoire".FormatString(PROJECTDELETEDCONTROLDATATABLENAME, rowIndex + 1));

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

                var constantName = projectTableDataArray[rowIndex, 0];
                newAddedConstant.ConstantName = (constantName != null) ? constantName.ToString() : null;
                if (newAddedConstant.ConstantName.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' le nom du control est invalide".FormatString(PROJECTADDEDCONSTANTDATATABLENAME, rowIndex + 1));

                var projectVersion = projectTableDataArray[rowIndex, 1];
                newAddedConstant.ProjectVersion = (projectVersion != null) ? ((projectVersion.ToString() != "") ? Convert.ToDecimal(projectVersion.ToString()) : 0) : 0;
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

                var constantName = projectTableDataArray[rowIndex, 0];
                newDeletedConstant.ConstantName = (constantName != null) ? constantName.ToString() : null;
                if (newDeletedConstant.ConstantName.IsNullOrEmpty())
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' le nom du control est invalide".FormatString(PROJECTDELETEDCONSTANTDATATABLENAME, rowIndex + 1));

                var projectVersion = projectTableDataArray[rowIndex, 1];
                newDeletedConstant.ProjectVersion = (projectVersion != null) ? ((projectVersion.ToString() != "") ? Convert.ToDecimal(projectVersion.ToString()) : 0) : 0;
                if (newDeletedConstant.ProjectVersion <= 1)
                    throw new Exception("Dans la table de projet '{0}', ligne '{1}' la version majeure de projet est invalide".FormatString(PROJECTDELETEDCONSTANTDATATABLENAME, rowIndex + 1));

                var transfertConstantName = projectTableDataArray[rowIndex, 2];
                newDeletedConstant.TransfertConstantName = (transfertConstantName != null) ? transfertConstantName.ToString() : null;

                result.Add(newDeletedConstant);
            }

            return result;
        }

        #endregion
    }

    public static partial class ControlVersionManagement
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