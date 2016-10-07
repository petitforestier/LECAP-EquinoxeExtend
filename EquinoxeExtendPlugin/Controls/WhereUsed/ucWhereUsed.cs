using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper;
using EquinoxeExtend.Shared.Enum;
using Library.Control.Datagridview;
using Library.Control.UserControls;
using Library.Excel;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using Service.Specification.Front;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquinoxeExtendPlugin.Controls.WhereUsedTable
{
    public partial class ucWhereUsed : UserControl, IUcUserControl
    {
        #region Public EVENTS

        public event EventHandler Close;

        #endregion

        #region Public CONSTRUCTORS

        public ucWhereUsed(IApplication iApplication)
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

        #endregion

        #region Private METHODS

        private void LoadTreeView()
        {
            var inProgressUserControl = new ucMessageBox("Traitement en cours");
            using (var inProgressForm = new frmUserControl(inProgressUserControl, "Cas d'emploi des fichiers tables excel", false, false))
            {
                inProgressForm.TopMost = true;
                inProgressForm.Show();
                inProgressForm.Refresh();

                //GroupTable
                inProgressUserControl.SetMessage("Traitement des tables de groupes");
                inProgressForm.Refresh();
                trvGroupTable.Nodes.Clear();

                foreach (var item in GenerateGroupTableTreeNode().Enum())
                    trvGroupTable.Nodes.Add(item);

                //ProjectTable
                inProgressUserControl.SetMessage("Traitement des tables de projet");
                inProgressForm.Refresh();
                trvProjectTable.Nodes.Clear();
                foreach (var item in GenerateProjetTableTreeNode().Enum())
                    trvProjectTable.Nodes.Add(item);
            }

        }

        private List<TreeNode> GenerateProjetTableTreeNode()
        {
            var groupService = _Application.ServiceManager.GetService<IGroupService>();
            var projects = groupService.ActiveGroup.Projects.GetProjects();

            //Génération d'une liste facilement groupable
            var tupleTables = new List<Tuple<ProjectDetails, ImportedDataTable>>();

            //Bouclage sur les projets
            foreach (var projectItem in projects.Enum())
            {
                var projectService = _Application.ServiceManager.GetService<IProjectService>();
                projectService.OpenProject(projectItem.Name);

                var project = projectService.ActiveProject;
                var importedDataTables = project.GetImportedDataTableList();
                foreach (var tableItem in importedDataTables.Enum())
                    tupleTables.Add(new Tuple<ProjectDetails, ImportedDataTable>(projectItem, tableItem));
            }

            var treeNodeCollection = new List<TreeNode>();

            //Fichier excel
            foreach (var excelFileLocationItem in tupleTables.GroupBy(x => x.Item2.FileLocation).Enum())
            {
                var theLocationNode = new TreeNode();
                theLocationNode.Name = excelFileLocationItem.First().Item2.FileLocation;
                theLocationNode.Text = "Fichier : " + theLocationNode.Name;
                theLocationNode.ForeColor = (File.Exists(theLocationNode.Name)) ? theLocationNode.ForeColor : System.Drawing.Color.Red;

                //Feuille excel
                foreach (var sheetItem in excelFileLocationItem.GroupBy(x => x.Item2.SheetName).Enum())
                {
                    var theSheetNode = new TreeNode();
                    theSheetNode.Name = sheetItem.First().Item2.SheetName;
                    theSheetNode.Text = "Feuille : " + theSheetNode.Name;

                    //Projet
                    foreach (var projectItem in sheetItem.GroupBy(x => x.Item1.Name).Enum())
                    {
                        var theProjectNode = new TreeNode();
                        theProjectNode.Name = projectItem.First().Item1.Name;
                        theProjectNode.Text = "Projet : " + theProjectNode.Name;

                        //Table
                        foreach (var tableItem in projectItem.GroupBy(x => x.Item2.DisplayName).Enum())
                        {
                            var theTableNode = new TreeNode();
                            theTableNode.Name = tableItem.First().Item2.DisplayName;
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

        private void cmdRunWhereUsedAnalyse_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    using (var projectOpenLocker = new BoolLocker(ref Consts.Consts.DontShowCheckTaskOnStartup))
                    {
                        //Confirmation
                        if (MessageBox.Show("Le recensement des tables va débuter, et peut prendre quelques minutes. Etes-vous sûr de vouloir lancer le traitement ?", "Cas d'emploi", MessageBoxButtons.YesNo) != DialogResult.Yes)
                            return;

                        //Création des services
                        var projectService = _Application.ServiceManager.GetService<IProjectService>();
                        var OpenedProjectName = projectService.ActiveProject.Name;
                        projectService.CloseProject();

                        //Vérification que tous les projets sont fermés
                        var groupService = _Application.ServiceManager.GetService<IGroupService>();
                        var openedProjectList = groupService.ActiveGroup.GetOpenedProjectList();

                        if (openedProjectList.IsNotNullAndNotEmpty())
                        {
                            MessageBox.Show("Certains projets du groupe sont ouverts. L'analyse n'est donc pas possible." + Environment.NewLine
                                + Environment.NewLine + openedProjectList.Select(x => x.Name).Concat(Environment.NewLine), "Projet ouvert", MessageBoxButtons.OK);
                        }
                        else
                        {
                            LoadTreeView();
                        }
                        projectService.OpenProject(OpenedProjectName);

                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
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