using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper;
using DriveWorks.Helper.Manager;
using EquinoxeExtend.Shared.Enum;
using Library.Control.Datagridview;
using Library.Control.UserControls;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using Library.Tools.Tasks;

namespace EquinoxeExtendPlugin.Controls.WhereUsedTable
{
    public partial class ucWhereUsedGroup : UserControl, IUcUserControl
    {
        #region Public EVENTS

        public event EventHandler Close;

        #endregion

        #region Public CONSTRUCTORS

        public ucWhereUsedGroup(IApplication iApplication, Group iGroup)
        {
            InitializeComponent();
            _Application = iApplication;

            //image list
            trvProjectTable.ShowNodeToolTips = true;
            trvProjectTable.ImageList = _ImageList;
            _ImageList.Images.Add(Properties.Resources.blank);
            _ImageList.Images.Add(Properties.Resources.Warning16);

            using (var releaseService = new Service.Release.Front.ReleaseService(iGroup.GetEnvironment().GetSQLExtendConnectionString()))
            {
                //Package
                cboPackage.DisplayMember = PropertyObserver.GetPropertyName<EquinoxeExtend.Shared.Object.Release.Package>(x => x.PackageIdStatusString);
                cboPackage.ValueMember = PropertyObserver.GetPropertyName<EquinoxeExtend.Shared.Object.Release.Package>(x => x.PackageId);
                cboPackage.DataSource = releaseService.GetPackageList(PackageStatusSearchEnum.All, PackageOrderByEnum.PackageId);
                cboPackage.SelectedIndex = -1;
            }
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

        private List<TreeNode> GenerateProjetTableTreeNode(EnvironmentEnum iSourceEnvironment, EnvironmentEnum iDestinationEnvironment, EquinoxeExtend.Shared.Object.Release.Package iPackage)
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

                    Library.Tools.Debug.MyDebug.PrintInformation(projectItem.Name);

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
                var tools = new Tools.Tools();
                tupleTables = tools.GetImportedDataTableFromPackage(iSourceEnvironment, iDestinationEnvironment, iPackage);
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

        private void cmdRunWhereUsedAnalyse_Click(object sender, System.EventArgs e)
        {
            //Création des services
            var projectService = _Application.ServiceManager.GetService<IProjectService>();

            string openedProjectName = null;
            if (projectService.ActiveProject != null)
            {
                if (projectService.ActiveProject.IsOpen)
                {
                    openedProjectName = projectService.ActiveProject.Name;
                    projectService.CloseProject();
                }
            }

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
                            var inProgressUserControl = new ucMessageBox("Traitement en cours");
                            using (var inProgressForm = new frmUserControl(inProgressUserControl, "Cas d'emploi des tables de groupe", false, false))
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
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
            finally
            {
                if (openedProjectName != null)
                    projectService.OpenProject(openedProjectName);
            }
        }

        private void cmdRunWhereUsedProjectTableAnalyse_Click(object sender, EventArgs e)
        {
            var groupService = _Application.ServiceManager.GetService<IGroupService>();
            var activeEnvironment = groupService.ActiveGroup.GetEnvironment();

            EnvironmentEnum destinationEnvironment;
            if (activeEnvironment == EnvironmentEnum.Developpement)
                destinationEnvironment = EnvironmentEnum.Staging;
            else if (activeEnvironment == EnvironmentEnum.Staging)
                destinationEnvironment = EnvironmentEnum.Production;
            else
                throw new Exception("Cette function ne support pas l'environnement actuel.");

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

                        string openedProjectName = null;
                        if (projectService.ActiveProject != null)
                        {
                            if (projectService.ActiveProject.IsOpen)
                            {
                                openedProjectName = projectService.ActiveProject.Name;
                                projectService.CloseProject();
                            }
                        }
                        //Vérification que tous les projets sont fermés
                        var openedProjectList = groupService.ActiveGroup.GetOpenedProjectList();

                        if (openedProjectList.IsNotNullAndNotEmpty())
                        {
                            MessageBox.Show("Certains projets du groupe sont ouverts. L'analyse n'est donc pas possible." + Environment.NewLine
                                + Environment.NewLine + openedProjectList.Select(x => x.Name).Concat(Environment.NewLine), "Projet ouvert", MessageBoxButtons.OK);
                        }
                        else
                        {
                            var inProgressUserControl = new ucMessageBox("Traitement en cours");
                            using (var inProgressForm = new frmUserControl(inProgressUserControl, "Cas d'emploi des fichiers tables excel", false, false))
                            {
                                inProgressForm.TopMost = true;
                                inProgressForm.Show();
                                inProgressForm.Refresh();

                                //ProjectTable
                                inProgressUserControl.SetMessage("Traitement des tables de projet");
                                inProgressForm.Refresh();
                                trvProjectTable.Nodes.Clear();

                                var selectedPackage = (cboPackage.SelectedIndex != -1) ? (EquinoxeExtend.Shared.Object.Release.Package)cboPackage.SelectedItem : null;
                                foreach (var item in GenerateProjetTableTreeNode(activeEnvironment, destinationEnvironment, selectedPackage).Enum())
                                    trvProjectTable.Nodes.Add(item);
                            }
                        }
                        if (openedProjectName != null)
                            projectService.OpenProject(openedProjectName);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cboPackage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                    {
                        cboPackage.DroppedDown = false;
                        cboPackage.SelectedIndex = -1;
                        cmdRunWhereUsedProjectTableAnalyse.Focus();
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