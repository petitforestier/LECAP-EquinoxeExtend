using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
using Library.Control.Datagridview;
using Library.Control.UserControls;
using Library.Excel;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriveWorks.Helper.Manager;

namespace EquinoxeExtendPlugin.Controls.WhereUsedTable
{
    public partial class ucWhereUsedProject : UserControl, IUcUserControl
    {
        #region Public EVENTS

        public event EventHandler Close;

        #endregion

        #region Public CONSTRUCTORS

        public ucWhereUsedProject(IApplication iApplication, Group iGroup)
        {
            InitializeComponent();
            _Application = iApplication;

            dgvUnusedTable.MultiSelect = false;
            dgvUnusedTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUnusedTable.ReadOnly = true;
            dgvUnusedTable.AllowUserToAddRows = false;
            dgvUnusedTable.RowHeadersVisible = false;
            dgvUnusedTable.AllowUserToResizeRows = false;
            dgvUnusedTable.AllowUserToResizeColumns = true;
            dgvUnusedTable.AllowUserToOrderColumns = false;

        }

        #endregion

        #region Protected CLASSES

        protected class TableView
        {
            #region Public PROPERTIES

            [Visible]
            [Frozen]
            [Name("FR", "Nom de la table")]
            [WidthColumn(500)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string TableName { get; set; }

            #endregion

            #region Public METHODS

            public static TableView ConvertTo(string iTableName)
            {
                var newView = new TableView();
                newView.TableName = iTableName;

                return newView;
            }

            #endregion
        }

        protected class VariableView
        {
            #region Public PROPERTIES

            [Visible]
            [Frozen]
            [Name("FR", "Nom de la variable")]
            [WidthColumn(500)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string VariableName { get; set; }

            #endregion

            #region Public METHODS

            public static VariableView ConvertTo(string iVariableName)
            {
                var newView = new VariableView();
                newView.VariableName = iVariableName;

                return newView;
            }

            #endregion
        }

        #endregion

        #region Private FIELDS

        private IApplication _Application;

        private BoolLock _IsLoading = new BoolLock();

        private ImageList _ImageList = new ImageList();

        #endregion

        #region Private METHODS

        private List<TreeNode> GenerateProjetTableTreeNode(EquinoxeExtend.Shared.Object.Release.Package iPackage)
        {
            var groupService = _Application.ServiceManager.GetService<IGroupService>();
            var projects = groupService.ActiveGroup.Projects.GetProjects();

            //Génération d'une liste facilement groupable
            var tupleTables = new List<Tuple<string, ProjectDetails, ImportedDataTable>>();

            if (iPackage == null)
            {
                //Bouclage sur les projets
                foreach (var projectItem in projects.Enum())
                {
                    var projectService = _Application.ServiceManager.GetService<IProjectService>();
                    projectService.OpenProject(projectItem.Name);

                    var project = projectService.ActiveProject;
                    var importedDataTables = project.GetImportedDataTableList();
                    foreach (var tableItem in importedDataTables.Enum())
                        tupleTables.Add(new Tuple<string, ProjectDetails, ImportedDataTable>("", projectItem, tableItem));
                    projectService.CloseProject();
                }
            }
            else
            {
                tupleTables = Tools.Tools.GetImportedDataTableFromPackage(_Application, iPackage);
            }

            var treeNodeCollection = new List<TreeNode>();

            //Fichier excel
            foreach (var excelFileLocationItem in tupleTables.GroupBy(x => x.Item3.FileLocation).Enum().OrderBy(x => x.First().Item3.FileLocation).Enum())
            {
                var theLocationNode = new TreeNode();
                theLocationNode.Name = excelFileLocationItem.First().Item3.FileLocation;
                theLocationNode.Text = "Fichier : " + theLocationNode.Name;

                if (!File.Exists(theLocationNode.Name))
                {
                    theLocationNode.ForeColor = System.Drawing.Color.Red;
                    theLocationNode.ToolTipText = "Le fichier est introuvable." + Environment.NewLine;
                }

                //Feuille excel
                foreach (var sheetItem in excelFileLocationItem.GroupBy(x => x.Item3.SheetName).Enum())
                {
                    var theSheetNode = new TreeNode();
                    theSheetNode.Name = sheetItem.First().Item3.SheetName;
                    theSheetNode.Text = "Feuille : " + theSheetNode.Name;

                    if (sheetItem.Count() >= 2)
                    {
                        var tableList = sheetItem.Select(x => new Tuple<string, ProjectDetails, ImportedDataTable>(groupService.ActiveGroup.GetEnvironment().GetName("FR"), x.Item2, x.Item3)).ToList();
                        var tableDifferences = DriveWorks.Helper.DataTableHelper.GetProjectDataTableDifference(tableList);

                        if (tableDifferences.IsNotNullAndNotEmpty())
                        {
                            theLocationNode.ImageIndex = 1;
                            theLocationNode.SelectedImageIndex = 1;
                            theLocationNode.ToolTipText += tableDifferences.Concat(Environment.NewLine);

                            theSheetNode.ImageIndex = 1;
                            theSheetNode.SelectedImageIndex = 1;
                            theSheetNode.ToolTipText += tableDifferences.Concat(Environment.NewLine);
                        }
                    }

                    //Projet
                    foreach (var projectItem in sheetItem.GroupBy(x => x.Item2.Name).Enum())
                    {
                        var theProjectNode = new TreeNode();
                        theProjectNode.Name = projectItem.First().Item2.Name;
                        theProjectNode.Text = "Projet : " + theProjectNode.Name;

                        //Table
                        foreach (var tableItem in projectItem.GroupBy(x => x.Item3.DisplayName).Enum())
                        {
                            var theTableNode = new TreeNode();
                            theTableNode.Name = tableItem.First().Item3.DisplayName;
                            theTableNode.Text = "Table : " + theTableNode.Name;
                            theProjectNode.Nodes.Add(theTableNode);
                        }
                        theSheetNode.Nodes.Add(theProjectNode);
                    }
                    theLocationNode.Nodes.Add(theSheetNode);
                }

                treeNodeCollection.Add(theLocationNode);
            }

            return treeNodeCollection;
        }

        private List<TreeNode> GenerateGroupTableTreeNode()
        {
            var groupService = _Application.ServiceManager.GetService<IGroupService>();
            var projects = groupService.ActiveGroup.Projects.GetProjects();

            //Génération d'une liste facilement groupable
            var tupleTables = new List<Tuple<ProjectDetails, GroupDataTable>>();

            //Bouclage sur les projets
            foreach (var projectItem in projects.Enum())
            {
                var projectService = _Application.ServiceManager.GetService<IProjectService>();
                projectService.OpenProject(projectItem.Name);

                var groupDataTables = groupService.ActiveGroup.DataTables.ToList();

                foreach (var tableItem in projectService.ActiveProject.GetUsedGroupTableList(groupDataTables).Enum())
                    tupleTables.Add(new Tuple<ProjectDetails, GroupDataTable>(projectItem, tableItem));
                projectService.CloseProject();
            }

            var treeNodeCollection = new List<TreeNode>();

            //Fichier excel
            foreach (var groupDataTableItem in tupleTables.GroupBy(x => x.Item2.Name).Enum())
            {
                var theTableNode = new TreeNode();
                theTableNode.Name = groupDataTableItem.First().Item2.Name;
                theTableNode.Text = "Table : " + theTableNode.Name;

                //Feuille excel
                foreach (var sheetItem in groupDataTableItem.GroupBy(x => x.Item1.Name).Enum())
                {
                    var theSheetNode = new TreeNode();
                    theSheetNode.Name = sheetItem.First().Item1.Name;
                    theSheetNode.Text = "Projet : " + theSheetNode.Name;
                    theTableNode.Nodes.Add(theSheetNode);
                }
                treeNodeCollection.Add(theTableNode);
            }

            return treeNodeCollection;
        }

        private void cmdClose_Click(object sender, System.EventArgs e)
        {
            Close(sender, e);
        }

       
        private void cmdRunUnusedAnalyse_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    using (var projectOpenLocker = new BoolLocker(ref Consts.Consts.DontShowCheckTaskOnStartup))
                    {
                        var inProgressUserControl = new ucMessageBox("Traitement en cours");
                        using (var inProgressForm = new frmUserControl(inProgressUserControl, "Recherche des tables inutilisées", false, false))
                        {
                            inProgressForm.TopMost = true;
                            inProgressForm.Show();
                            inProgressForm.Refresh();

                            var projectService = _Application.ServiceManager.GetService<IProjectService>();
                            var activeProject = projectService.ActiveProject;

                            var result = new List<string>();

                            //Récupération de la liste des tables settings
                            var settingsDatatable = activeProject.GetTableSettingsList();
                            settingsDatatable.AddRange(activeProject.GetControlTableNameList());

                            //récupération de la liste des tables
                            var searchProcess = new SearchRuleProcess(activeProject);

                            foreach (var tableItem in activeProject.DataTables.Enum())
                            {
                                //ignore les tables de settings
                                if (settingsDatatable.Exists(x => x == tableItem.InvariantName))
                                    continue;

                                if (!searchProcess.GetSearchResult("DwLookup" + tableItem.InvariantName).IsNotNullAndNotEmpty())
                                    result.Add(tableItem.DisplayName);
                            }

                            dgvUnusedTable.DataSource = result.Enum().Select(x => TableView.ConvertTo(x)).Enum().ToList();
                            dgvUnusedTable.FormatColumns<TableView>("FR");

                            MessageBox.Show("{0} tables sont inutilisées sur {1} tables".FormatString(result.Count, activeProject.DataTables.Count()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdRunUnusedVariable_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    using (var projectOpenLocker = new BoolLocker(ref Consts.Consts.DontShowCheckTaskOnStartup))
                    {
                        var projectService = _Application.ServiceManager.GetService<IProjectService>();
                        var activeProject = projectService.ActiveProject;

                        var inProgressUserControl = new ucMessageBox("Traitement en cours");
                        using (var inProgressForm = new frmUserControl(inProgressUserControl, "Recherches des variables inutilisés", false, false))
                        {
                            inProgressForm.TopMost = true;
                            inProgressForm.Show();
                            inProgressForm.Refresh();

                            var result = new List<string>();

                            //récupération de la liste des tables
                            var searchProcess = new SearchRuleProcess(activeProject);

                            foreach (var variableItem in activeProject.Variables.GetVariables().Enum())
                            {
                                if (!searchProcess.GetSearchResult(variableItem.Name).IsNotNullAndNotEmpty())
                                    result.Add(variableItem.Name);
                            }
                            dgvUnusedVariables.DataSource = result.Enum().Select(x => VariableView.ConvertTo(x)).Enum().ToList();
                            dgvUnusedVariables.FormatColumns<VariableView>("FR");

                            MessageBox.Show("{0} paramètres sont inutilisés sur {1} paramètres".FormatString(result.Count, activeProject.Variables.GetVariables().Enum().Count()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdExportProjectVariablesToExcel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (dgvUnusedVariables.RowCount >= 1)
                    {
                        //Ajout de la liste dans une liste d'object
                        var dataList = new List<object>();
                        foreach (var item in (List<VariableView>)dgvUnusedVariables.DataSource)
                            dataList.Add(item);

                        var excelService = new ExcelTools(new System.Threading.CancellationTokenSource());
                        var excelList = new List<Library.Excel.Object.ExcelSheet<object>>();
                        excelList.Add(new Library.Excel.Object.ExcelSheet<object>() { DataList = dataList, Lang = "FR" });

                        var path = excelService.SendListToNewExcelFile(excelList);
                        Process.Start(path);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdExportProjectTableToExcel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (dgvUnusedTable.RowCount >= 1)
                    {
                        //Ajout de la liste dans une liste d'object
                        var dataList = new List<object>();
                        foreach (var item in (List<TableView>)dgvUnusedTable.DataSource)
                            dataList.Add(item);

                        var excelService = new ExcelTools(new System.Threading.CancellationTokenSource());
                        var excelList = new List<Library.Excel.Object.ExcelSheet<object>>();
                        excelList.Add(new Library.Excel.Object.ExcelSheet<object>() { DataList = dataList, Lang = "FR" });

                        var path = excelService.SendListToNewExcelFile(excelList);
                        Process.Start(path);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }
      
        
        #endregion
    }
}