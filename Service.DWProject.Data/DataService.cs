using DriveWorks;
using DriveWorks.Forms;
using DriveWorks.Reporting;
using DriveWorks.Specification;
using EquinoxeExtend.Shared.Object.ConstantManagement;
using EquinoxeExtend.Shared.Object.ConstrolManagement;
using EquinoxeExtend.Shared.Object.Settings;
using EquinoxeExtend.Shared.Object.Specification;
using Library.Tools.Comparator;
using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Service.DWProject.Data
{
    public partial class DataService
    {
        #region Public CONSTRUCTORS

        public DataService(Project iProject)
        {
            if (iProject == null)
                throw new Exception("Le projet ne peut pas être null");
            _Project = iProject;
        }

        #endregion

        #region Public METHODS

        public ProjectSettings GetProjectSettings()
        {
            if (_ProjectSettings == null)
            {
                var tableDic = new Dictionary<string, string>();
                var projectTable = _Project.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTSETTINGSDATATABLENAME);
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

                _ProjectSettings = new ProjectSettings();
                _ProjectSettings.ProjectVersion = Convert.ToDecimal(tableDic[PROJECTVERSION]);
                _ProjectSettings.TagIgnore = tableDic[TAGIGNORE];
                _ProjectSettings.ErrorColorName = tableDic[ERRORCOLORNAME];
                _ProjectSettings.NoErrorColorName = tableDic[NOERRORCOLORNAME];
                _ProjectSettings.UserDebugControlName = tableDic[USERDEBUGCONTROLNAME];
                _ProjectSettings.ErrorConstantName = tableDic[ERRORCONSTANTNAME];
                _ProjectSettings.ProjectName = _Project.Name;
            }
            return _ProjectSettings;
        }

        public string GetProjectMetadataFilePath(string iProjectId)
        {
            var theMasterProject = _Project.Group.Projects.GetProject(new Guid(iProjectId));

            if (theMasterProject == null)
                throw new Exception("Le projet est inexistante");

            return theMasterProject.Directory + "\\" + theMasterProject.Name + theMasterProject.Extension;
        }

        public string GetEditingSpecificationName(SpecificationContext iContext)
        {
            return Path.GetFileNameWithoutExtension(iContext.SpecificationFilePath);
        }

        public List<string> GetErrorMessageList()
        {
            var result = new List<string>();
            var formList = _Project.Navigation.GetForms(true, true);

            //Bouclage sur les forms
            foreach (var item in formList)
            {
                //Ignore les formes si besoin
                if (item.Form.Tag.ToLower().Trim().Contains(GetProjectSettings().TagIgnore))
                    continue;

                var formControlList = item.Form.Controls;
                //Bouclage sur les controls
                foreach (var controlItem in formControlList)
                {
                    if (controlItem.Enabled == false)
                        continue;

                    var errorMessage = GetErrorMessageFromControlBase(controlItem, GetProjectSettings().ErrorColorName);
                    if (errorMessage.IsNotNullAndNotEmpty())
                        result.Add(errorMessage);
                }
            }
            return result;
        }

        public Tuple<string, string, string, string> GetAddedDeletedControlConstant()
        {
            //Control
            var addedControlList = GetAddedControlList();
            var deletedControlList = GetDeletedControlList();

            if (addedControlList.Exists2(x => x.ProjectVersion > GetProjectSettings().ProjectVersion))
                throw new Exception("Il existe dans la table des controles ajoutés, des controles de version supérieur");

            if (deletedControlList.Exists2(x => x.ProjectVersion > GetProjectSettings().ProjectVersion))
                throw new Exception("Il existe dans la table des controles supprimés, des controles de version supérieur");

            var controlBuildResult = BuildControlListFromVersion(new List<ControlState>(), Math.Truncate(GetProjectSettings().ProjectVersion));

            var controlComparator = new ListComparator<ControlState, ControlState>(controlBuildResult.Item1, x => x.Name, GetCurrentControlStateList(), x => x.Name);

            var addedControlString = GetAddedControlString(controlComparator.NewList, GetProjectSettings().ProjectVersion);
            var deletedControlString = GetDeletedControlString(controlComparator.RemovedList, GetProjectSettings().ProjectVersion);

            //Constant
            var addedConstantList = GetAddedConstantList();
            var deletedConstantList = GetDeletedConstantList();

            if (addedConstantList.Exists2(x => x.ProjectVersion > GetProjectSettings().ProjectVersion))
                throw new Exception("Il existe dans la table des constantes ajoutées, des controles de version supérieur");

            if (deletedConstantList.Exists2(x => x.ProjectVersion > GetProjectSettings().ProjectVersion))
                throw new Exception("Il existe dans la table des constantes supprimées, des controles de version supérieur");

            var constantBuildResult = BuildConstantListFromVersion(new List<ConstantState>(), Math.Truncate(GetProjectSettings().ProjectVersion));

            var constantComparator = new ListComparator<ConstantState, ConstantState>(constantBuildResult, x => x.Name, GetCurrentConstantList(), x => x.Name);

            var addedConstantString = GetAddedConstantString(constantComparator.NewList, GetProjectSettings().ProjectVersion);
            var deletedConstantString = GetDeletedConstantString(constantComparator.RemovedList, GetProjectSettings().ProjectVersion);

            return new Tuple<string, string, string, string>(addedControlString, deletedControlString, addedConstantString, deletedConstantString);
        }

        /// <summary>
        /// Récupération des controls du projet ouvert en ignorant les form et controls à ignorer
        /// </summary>
        /// <returns></returns>
        public List<ControlState> GetCurrentControlStateList()
        {
            var specificationControlStateList = new List<ControlState>();
            var formList = _Project.Navigation.GetForms(true, true);

            //Bouclage sur les forms
            foreach (var item in formList)
            {
                //Ignore les formes si besoin
                if (item.Form.Tag.ToLower().Trim().Contains((string)GetProjectSettings().TagIgnore))
                    continue;

                var formControlList = item.Form.Controls;
                //Bouclage sur les controls
                foreach (var controlItem in formControlList)
                {
                    //Ignore les controles si besoin
                    if (controlItem.Tag.ToLower().Trim().Contains((string)GetProjectSettings().TagIgnore))
                        continue;

                    var controlState = GetControlStateFormControlBase(controlItem);

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
        public List<ConstantState> GetCurrentConstantList()
        {
            var constants = _Project.Constants;
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

        public void LoadSpecification(Specification iSpecification)
        {
            var errorList = new List<string>();

            //Controle de Version
            if (iSpecification.ProjectVersion > GetProjectSettings().ProjectVersion)
                throw new Exception("La version du projet de la sauvegarde '{0}', ne peut pas être supérieur à la version du projet actuellement ouvert '{1}'".FormatString(iSpecification.ProjectVersion, GetProjectSettings().ProjectVersion));

            //Constant
            var specificationConstantStateList = iSpecification.Constants.DeserializeList<ConstantState>();
            var constantBuildResult = BuildConstantListFromVersion(specificationConstantStateList, iSpecification.ProjectVersion);

            //Constantes
            foreach (var item in constantBuildResult.Enum())
            {
                var theConstant = _Project.Constants.GetConstant(item.Name);
                if (theConstant != null)
                    theConstant.Value = item.Value;
                else
                    errorList.Add("La constant '{0}' est inexistante".FormatString(item.Name));
            }

            //Control
            var specificationControlStateList = iSpecification.Controls.DeserializeList<ControlState>();
            var controlBuildResult = BuildControlListFromVersion(specificationControlStateList, iSpecification.ProjectVersion);

            specificationControlStateList = controlBuildResult.Item1;
            var deletedMessageList = controlBuildResult.Item2;

            //Bouclage sur les Etats de controles pour Application de la synthèse
            foreach (var controlStateItem in specificationControlStateList.Enum())
            {
                try
                {
                    var theControl = _Project.Navigation.GetControl(controlStateItem.Name);
                    try
                    {
                        if (controlStateItem.Value != null)
                            SetControlBaseFromControlState(theControl, controlStateItem);
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    throw new Exception("Le controle nommé '" + controlStateItem.Name + "' n'existe pas. Sa suppression n'est pas gérée", ex);
                }
            }

            //Bouclage de vérification car une règle du configurateur pourrait faire une modification après écriture
            foreach (var controlStateItem in specificationControlStateList.Enum())
            {
                try
                {
                    var theControl = _Project.Navigation.GetControl(controlStateItem.Name);
                    var controlState = GetControlStateFormControlBase(theControl);

                    if (controlStateItem.Value != null)
                    {
                        if (controlState.Value != controlStateItem.Value)
                            errorList.Add("Controle '{0}' la valeur est de '{1}' au lieu de '{2}', contacter l'administrateur.".FormatString(theControl.Name, controlState.Value, controlStateItem.Value));

                        if (controlState.Value2 != controlStateItem.Value2)
                            errorList.Add("Controle '{0}' la valeur 2 est de '{1}' au lieu de '{2}', contacter l'administrateur.".FormatString(theControl.Name, controlState.Value2, controlStateItem.Value2));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Le controle nommé '" + controlStateItem.Name + "' n'existe pas. Sa suppression n'est pas gérée", ex);
                }
            }

            if (errorList.IsNotNullAndNotEmpty())
                throw new Exception(errorList.Concat(Environment.NewLine));
        }

        public void CheckControlConstantManagement()
        {
            var check = GetAddedDeletedControlConstant();
            if (check.Item1.IsNotNullAndNotEmpty() || check.Item2.IsNotNullAndNotEmpty() || check.Item3.IsNotNullAndNotEmpty() || check.Item4.IsNotNullAndNotEmpty())
                throw new Exception("Le projet actuel possède des controles non managé. Contacter l'administrateur");
        }

        public void SetTableControlItems(string iControlName, ITableValue iTableValue)
        {
            var tableControl = (DataTableControl)_Project.Navigation.GetControl(iControlName);

            if (tableControl == null)
                throw new Exception("Le controle datatable nommé '{0}' est introuvable");

            tableControl.Items = iTableValue;
        }

        public List<GroupDataTable> GetUsedGroupTableList(List<GroupDataTable> iGroupTableList)
        {
            if (_Project.Name == "test_JM")
                MyDebug.BreakForDebug();

            var result = new List<GroupDataTable>();

            //Tables de groupes
            foreach (var groupTableItem in iGroupTableList.Enum())
            {
                var searchProcess = new SearchRuleProcess(_Project);
                if (searchProcess.GetSearchResult("DWGroupTable" + groupTableItem.Name).IsNotNullAndNotEmpty())
                    result.Add(groupTableItem);
            }

            return result;
        }

        public List<ImportedDataTable> GetImportedDataTableList()
        {
            var result = new List<ImportedDataTable>();

            //Bouclage sur les tables de projets
            foreach (var tableItem in _Project.DataTables.Enum())
            {
                if (tableItem.GetType() == typeof(DriveWorks.ImportedDataTable))
                {
                    var importedDataTable = (DriveWorks.ImportedDataTable)tableItem;
                    result.Add(importedDataTable);
                }
            }

            return result;
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

        private Project _Project;

        private ProjectSettings _ProjectSettings;

        #endregion

        #region Private METHODS

        private Tuple<List<ControlState>, List<string>> BuildControlListFromVersion(List<ControlState> iOriginalList, decimal iSpecificationVersion)
        {
            if (Math.Truncate(iSpecificationVersion) != Math.Truncate(GetProjectSettings().ProjectVersion))
                throw new Exception("La version majeure de début et de fin ne peuvent pas être différent");

            var messageList = new List<string>();

            var deletedList = GetDeletedControlList().Enum().Where(x => x.ProjectVersion <= GetProjectSettings().ProjectVersion).Enum().ToList().OrderBy(x => x.ProjectVersion).Enum().ToList();
            var addedList = GetAddedControlList().Enum().Where(x => x.ProjectVersion <= GetProjectSettings().ProjectVersion).Enum().ToList().OrderBy(x => x.ProjectVersion).Enum().ToList();

            var groupAdded = addedList.Enum().Where(x => x.ProjectVersion > iSpecificationVersion && x.ProjectVersion <= GetProjectSettings().ProjectVersion).Enum().OrderBy(x => x.ProjectVersion).Enum().GroupBy(x => x.ProjectVersion);
            var groupDeleted = deletedList.Enum().Where(x => x.ProjectVersion > iSpecificationVersion && x.ProjectVersion <= GetProjectSettings().ProjectVersion).Enum().OrderBy(x => x.ProjectVersion).Enum().GroupBy(x => x.ProjectVersion);

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

        private List<ConstantState> BuildConstantListFromVersion(List<ConstantState> iOriginalList, decimal iSpecificationVersion)
        {
            if (Math.Truncate(iSpecificationVersion) != Math.Truncate(GetProjectSettings().ProjectVersion))
                throw new Exception("La version majeure de début et de fin ne peuvent pas être différent");

            var deletedList = GetDeletedConstantList().Enum().Where(x => x.ProjectVersion <= GetProjectSettings().ProjectVersion).Enum().ToList().OrderBy(x => x.ProjectVersion).Enum().ToList();
            var addedList = GetAddedConstantList().Enum().Where(x => x.ProjectVersion <= GetProjectSettings().ProjectVersion).Enum().ToList().OrderBy(x => x.ProjectVersion).Enum().ToList();

            var messageList = new List<string>();

            var groupAdded = addedList.Enum().Where(x => x.ProjectVersion > iSpecificationVersion && x.ProjectVersion <= GetProjectSettings().ProjectVersion).Enum().OrderBy(x => x.ProjectVersion).Enum().GroupBy(x => x.ProjectVersion);
            var groupDeleted = deletedList.Enum().Where(x => x.ProjectVersion > iSpecificationVersion && x.ProjectVersion <= GetProjectSettings().ProjectVersion).Enum().OrderBy(x => x.ProjectVersion).Enum().GroupBy(x => x.ProjectVersion);

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

        private List<AddedControlManaged> GetAddedControlList()
        {
            var result = new List<AddedControlManaged>();
            var projectTable = _Project.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTADDDEDCONTROLATATABLENAME);
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

        private List<DeletedControlManaged> GetDeletedControlList()
        {
            var result = new List<DeletedControlManaged>();
            var projectTable = _Project.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTDELETEDCONTROLDATATABLENAME);
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

        private List<AddedConstantManaged> GetAddedConstantList()
        {
            var result = new List<AddedConstantManaged>();
            var projectTable = _Project.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTADDEDCONSTANTDATATABLENAME);
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

        private List<DeletedConstantManaged> GetDeletedConstantList()
        {
            var result = new List<DeletedConstantManaged>();
            var projectTable = _Project.DataTables.SingleOrDefault(x => x.DisplayName == PROJECTDELETEDCONSTANTDATATABLENAME);
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

        private string GetDeletedControlString(List<ControlState> iList, decimal iProjectVersion)
        {
            var result = string.Empty;

            //Ajout des nouvelles données
            foreach (var item in iList.Enum())
            {
                if (result.IsNotNullAndNotEmpty())
                    result += Environment.NewLine;
                result += "{0}	{1}	{2}	{3}	{4}".FormatString(item.Name, "", iProjectVersion, "", "");
            }
            return result;
        }

        private string GetAddedControlString(List<ControlState> iList, decimal iProjectVersion)
        {
            var result = string.Empty;

            //Ajout des nouvelles données
            foreach (var item in iList.Enum())
            {
                if (result.IsNotNullAndNotEmpty())
                    result += Environment.NewLine;
                result += "{0}	{1}	{2}".FormatString(item.Name, iProjectVersion, "");
            }
            return result;
        }

        private string GetDeletedConstantString(List<ConstantState> iList, decimal iProjectVersion)
        {
            var result = string.Empty;

            //Ajout des nouvelles données
            foreach (var item in iList.Enum())
            {
                if (result.IsNotNullAndNotEmpty())
                    result += Environment.NewLine;
                result += "{0}	{1}	{2}".FormatString(item.Name, iProjectVersion, "");
            }
            return result;
        }

        private string GetAddedConstantString(List<ConstantState> iList, decimal iProjectVersion)
        {
            var result = string.Empty;

            //Ajout des nouvelles données
            foreach (var item in iList.Enum())
            {
                if (result.IsNotNullAndNotEmpty())
                    result += Environment.NewLine;
                result += "{0}	{2}".FormatString(item.Name, iProjectVersion);
            }
            return result;
        }

        #endregion
    }

    /// <summary>
    /// Gestion message
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public void ClearMessages()
        {
            var resultControl = _Project.Navigation.GetControl(GetProjectSettings().UserDebugControlName);
            resultControl.SetInputValue("");
        }

        public void AddErrorMessage(string iMessage, Exception iException)
        {
            AddMessage(iMessage);
            AddMessage(iException.Message);
            _Project.SpecificationContext.Report.WriteEntry(ReportingLevel.Minimal, ReportEntryType.Error, "Specification Management", iMessage, iException.Message, null);

            SetErrorConstant(true);
        }

        public void AddMessage(string iMessage)
        {
            TextBox theControl = null;
            _Project.Navigation.TryGetControl<TextBox>(GetProjectSettings().UserDebugControlName, ref theControl);
            var message = theControl.Text;
            if (message.IsNotNullAndNotEmpty())
                message += Environment.NewLine;
            message += iMessage;
            theControl.Text = message;
        }

        public void SetErrorConstant(bool iIsError)
        {
            ProjectConstant theConstant = null;
            _Project.Constants.TryGetConstant(GetProjectSettings().ErrorConstantName, ref theConstant);
            theConstant.Value = iIsError;
        }

        #endregion
    }

    /// <summary>
    /// Gestion control
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public static ControlState GetControlStateFormControlBase(ControlBase iControlBase)
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

        public static void SetControlBaseFromControlState(ControlBase iControlBase, ControlState iControlState)
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

        public static string GetErrorMessageFromControlBase(ControlBase iControlBase, string iErrorColorName)
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