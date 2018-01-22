using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper.Manager;
using DriveWorks.Helper.Object;
using Library.Control.Datagridview;
using Library.Control.UserControls;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquinoxeExtendPlugin.Controls.ControlVersion
{
    public partial class ucControlVersion : UserControl, IUcUserControl
    {
        #region Public EVENTS

        public event EventHandler Close;

        #endregion

        #region Public PROPERTIES

        public bool SaveNeeded { get; private set; }

        public decimal NewVersionNumber { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        public ucControlVersion(IApplication iApplication)
        {
            InitializeComponent();

            _Application = iApplication;

            var projectService = _Application.ServiceManager.GetService<IProjectService>();
            _Project = projectService.ActiveProject;

            //incrémentation de la version de projet.
            NewVersionNumber = DriveWorks.Helper.Manager.SettingsManager.GetProjectSettings(_Project).ProjectVersion + 0.0001m;

            //Chargement controles
            dgvNewConstant.MultiSelect = false;
            dgvNewConstant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNewConstant.ReadOnly = false;
            dgvNewConstant.AllowUserToAddRows = false;
            dgvNewConstant.RowHeadersVisible = false;
            dgvNewConstant.AllowUserToResizeRows = false;
            dgvNewConstant.AllowUserToResizeColumns = true;
            dgvNewConstant.AllowUserToOrderColumns = false;

            dgvNewControl.MultiSelect = false;
            dgvNewControl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNewControl.ReadOnly = false;
            dgvNewControl.AllowUserToAddRows = false;
            dgvNewControl.RowHeadersVisible = false;
            dgvNewControl.AllowUserToResizeRows = false;
            dgvNewControl.AllowUserToResizeColumns = true;
            dgvNewControl.AllowUserToOrderColumns = false;

            dgvOldConstant.MultiSelect = false;
            dgvOldConstant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOldConstant.ReadOnly = false;
            dgvOldConstant.AllowUserToAddRows = false;
            dgvOldConstant.RowHeadersVisible = false;
            dgvOldConstant.AllowUserToResizeRows = false;
            dgvOldConstant.AllowUserToResizeColumns = true;
            dgvOldConstant.AllowUserToOrderColumns = false;

            dgvOldControl.MultiSelect = false;
            dgvOldControl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOldControl.ReadOnly = false;
            dgvOldControl.AllowUserToAddRows = false;
            dgvOldControl.RowHeadersVisible = false;
            dgvOldControl.AllowUserToResizeRows = false;
            dgvOldControl.AllowUserToResizeColumns = true;
            dgvOldControl.AllowUserToOrderColumns = false;

            LoadData();
        }

        #endregion

        #region Protected CLASSES

        protected class AddedControlView
        {
            #region Public PROPERTIES

            [Visible]
            [ReadOnly]
            [Name("FR", "Nom contrôle")]
            [WidthColumn(150)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string ControlName { get; set; }

            [Visible]
            [ReadOnly]
            [Name("FR", "Version")]
            [WidthColumn(60)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public decimal ProjectVersion { get; set; }

            [Visible]
            [Name("FR", "Information d'ouverture")]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            [WidthColumn(250)]
            public string Message { get; set; }

            public AddedControlManaged Object { get; set; }

            #endregion

            #region Public METHODS

            public static AddedControlView ConvertTo(AddedControlManaged iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new AddedControlView();
                newView.Object = iObj;

                newView.ControlName = iObj.ControlName;
                newView.Message = iObj.Message;
                newView.ProjectVersion = iObj.ProjectVersion;

                return newView;
            }

            public static AddedControlManaged GetFromRow(DataGridViewRow iRow)
            {
                var addedControl = new AddedControlManaged();
                var viewRow = (AddedControlView)iRow.DataBoundItem;

                addedControl.ControlName = viewRow.Object.ControlName;
                addedControl.ProjectVersion = viewRow.Object.ProjectVersion;
                addedControl.Message = viewRow.Message;

                return addedControl;
            }

            #endregion
        }

        protected class DeletedControlView
        {
            #region Public PROPERTIES

            [Visible]
            [ReadOnly]
            [Name("FR", "Nom contrôle")]
            [WidthColumn(150)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string ControlName { get; set; }

            [Visible]
            [Name("FR", "Description contrôle")]
            [WidthColumn(200)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string ControlDescription { get; set; }

            [Visible]
            [ReadOnly]
            [Name("FR", "Version")]
            [WidthColumn(60)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public decimal ProjectVersion { get; set; }

            [Visible]
            [Name("FR", "Contrôle à transférer")]
            [WidthColumn(300)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string TransfertControlName { get; set; }

            [Visible]
            [Name("FR", "Information d'ouverture")]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            [WidthColumn(300)]
            public string Message { get; set; }

            public DeletedControlManaged Object { get; set; }

            #endregion

            #region Public METHODS

            public static DeletedControlView ConvertTo(DeletedControlManaged iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new DeletedControlView();
                newView.Object = iObj;

                newView.ControlName = iObj.ControlName;
                newView.ControlDescription = iObj.ControlDescription;
                newView.TransfertControlName = iObj.TransfertControlName;
                newView.Message = iObj.Message;
                newView.ProjectVersion = iObj.ProjectVersion;

                return newView;
            }

            public static DeletedControlManaged GetFromRow(DataGridViewRow iRow)
            {
                var control = new DeletedControlManaged();
                var viewRow = (DeletedControlView)iRow.DataBoundItem;

                control.ControlName = viewRow.Object.ControlName;
                control.ControlDescription = viewRow.ControlDescription;
                control.TransfertControlName = viewRow.TransfertControlName;
                control.ProjectVersion = viewRow.Object.ProjectVersion;
                control.Message = viewRow.Message;

                return control;
            }

            #endregion
        }

        protected class AddedConstantView
        {
            #region Public PROPERTIES

            [Visible]
            [ReadOnly]
            [Name("FR", "Nom constante")]
            [WidthColumn(150)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string ConstantName { get; set; }

            [Visible]
            [ReadOnly]
            [Name("FR", "Version")]
            [WidthColumn(60)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public decimal ProjectVersion { get; set; }

            public AddedConstantManaged Object { get; set; }

            #endregion

            #region Public METHODS

            public static AddedConstantView ConvertTo(AddedConstantManaged iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new AddedConstantView();
                newView.Object = iObj;

                newView.ConstantName = iObj.ConstantName;
                newView.ProjectVersion = iObj.ProjectVersion;

                return newView;
            }

            public static AddedConstantManaged GetFromRow(DataGridViewRow iRow)
            {
                var control = new AddedConstantManaged();
                var viewRow = (AddedConstantView)iRow.DataBoundItem;

                control.ConstantName = viewRow.Object.ConstantName;
                control.ProjectVersion = viewRow.Object.ProjectVersion;

                return control;
            }

            #endregion
        }

        protected class DeletedConstantView
        {
            #region Public PROPERTIES

            [Visible]
            [ReadOnly]
            [Name("FR", "Nom constante")]
            [WidthColumn(150)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string ConstantName { get; set; }

            [Visible]
            [ReadOnly]
            [Name("FR", "Version")]
            [WidthColumn(60)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public decimal ProjectVersion { get; set; }

            [Visible]
            [ReadOnly]
            [Name("FR", "Constante à transférer")]
            [WidthColumn(60)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string TransfertConstantName { get; set; }

            public DeletedConstantManaged Object { get; set; }

            #endregion

            #region Public METHODS

            public static DeletedConstantView ConvertTo(DeletedConstantManaged iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new DeletedConstantView();
                newView.Object = iObj;

                newView.ConstantName = iObj.ConstantName;
                newView.ProjectVersion = iObj.ProjectVersion;
                newView.TransfertConstantName = iObj.TransfertConstantName;

                return newView;
            }

            public static DeletedConstantManaged GetFromRow(DataGridViewRow iRow)
            {
                var control = new DeletedConstantManaged();
                var viewRow = (DeletedConstantView)iRow.DataBoundItem;

                control.ConstantName = viewRow.Object.ConstantName;
                control.ProjectVersion = viewRow.Object.ProjectVersion;
                control.TransfertConstantName = viewRow.TransfertConstantName;

                return control;
            }

            #endregion
        }

        #endregion

        #region Private FIELDS

        private IApplication _Application;

        private Project _Project;

        private BoolLock _IsLoading = new BoolLock();

        private List<AddedControlManaged> _AddedControlManaged;

        private List<DeletedControlManaged> _DeletedControlManaged;

        private List<AddedConstantManaged> _AddedConstantManaged;

        private List<DeletedConstantManaged> _DeletedConstantManaged;

        #endregion

        #region Private METHODS

        private void LoadData()
        {
            var groupService = _Application.ServiceManager.GetService<IGroupService>();

            var tupleValues = _Project.GetAddedDeletedControlConstant();

            _AddedControlManaged = tupleValues.Item1;
            //bouclage pour définir la bonne version
            foreach (var item in _AddedControlManaged.Enum())
                item.ProjectVersion = NewVersionNumber;

            _DeletedControlManaged = tupleValues.Item2;
            //bouclage pour définir la bonne version
            foreach (var item in _DeletedControlManaged.Enum())
                item.ProjectVersion = NewVersionNumber;

            _AddedConstantManaged = tupleValues.Item3;
            //bouclage pour définir la bonne version
            foreach (var item in _AddedConstantManaged.Enum())
                item.ProjectVersion = NewVersionNumber;

            _DeletedConstantManaged = tupleValues.Item4;
            //bouclage pour définir la bonne version
            foreach (var item in _DeletedConstantManaged.Enum())
                item.ProjectVersion = NewVersionNumber;

            dgvNewControl.DataSource = _AddedControlManaged.Enum().Select(x => AddedControlView.ConvertTo(x)).ToList();
            dgvNewControl.FormatColumns<AddedControlView>("FR");
            if (!_AddedControlManaged.Any())
                cmdNewControlCopy.Enabled = false;

            dgvOldControl.DataSource = _DeletedControlManaged.Enum().Select(x => DeletedControlView.ConvertTo(x)).ToList();
            dgvOldControl.FormatColumns<DeletedControlView>("FR");
            if (!_DeletedControlManaged.Any())
                cmdOldControlCopy.Enabled = false;

            dgvNewConstant.DataSource = _AddedConstantManaged.Enum().Select(x => AddedConstantView.ConvertTo(x)).ToList();
            dgvNewConstant.FormatColumns<AddedConstantView>("FR");
            if (!_AddedConstantManaged.Any())
                cmdNewConstantCopy.Enabled = false;

            dgvOldConstant.DataSource = _DeletedConstantManaged.Enum().Select(x => DeletedConstantView.ConvertTo(x)).ToList();
            dgvOldConstant.FormatColumns<DeletedConstantView>("FR");
            if (!_DeletedConstantManaged.Any())
                cmdOldConstantCopy.Enabled = false;

            lblNewConstantCount.Text = _AddedConstantManaged.Count.ToString();
            lblOldConstantCount.Text = _DeletedConstantManaged.Count.ToString();
            lblNewControlCount.Text = _AddedControlManaged.Count.ToString();
            lblOldControlCount.Text = _DeletedControlManaged.Count.ToString();
        }

        private void CloseFake()
        {
            Close(null, null);
        }

        private void cmdNewControlCopy_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (_AddedControlManaged.Any())
                    {
                        var newControls = new List<AddedControlManaged>();

                        //Ajout des nouvelles données
                        foreach (DataGridViewRow item in dgvNewControl.Rows)
                            newControls.Add(AddedControlView.GetFromRow(item));

                        _Project.AddToAddedControlProjectDataTable(newControls);

                        //modification de la version du projet
                        DriveWorks.Helper.Manager.SettingsManager.UpdateProjectVersionNumber(_Project, NewVersionNumber);

                        LoadData();

                        SaveNeeded = true;

                        MessageBox.Show("Les données sont maintenant ajoutées");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdOldControlCopy_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (_DeletedControlManaged.Any())
                    {
                        var oldControls = new List<DeletedControlManaged>();

                        //Récupère la liste des controles géré (Différent des controls actuel, aux ajouts pas en fait près)
                        var managedControlList = _Project.GetManagedControls();

                        //Ajout des nouvelles données
                        foreach (DataGridViewRow item in dgvOldControl.Rows)
                        {
                            var newDeletedControlManaged = DeletedControlView.GetFromRow(item);

                            //Validation du transfert
                            if (newDeletedControlManaged.TransfertControlName.IsNotNullAndNotEmpty())
                            {
                                if (managedControlList.Any(x => x.ControlName == newDeletedControlManaged.TransfertControlName) == false)
                                    throw new Exception("Le transfert de {0} vers {1} est impossible car {1} est introuvable".FormatString(newDeletedControlManaged.ControlName, newDeletedControlManaged.TransfertControlName));
                            }

                            //Validation control description
                            if (newDeletedControlManaged.ControlDescription.IsNullOrEmpty())
                                throw new Exception("La description est manquante pour {0}".FormatString(newDeletedControlManaged.ControlName));

                            //Validation
                            if (newDeletedControlManaged.TransfertControlName.IsNullOrEmpty() && newDeletedControlManaged.Message.IsNullOrEmpty())
                                throw new Exception("Le controle de transfert ou le message est manquant. Il faut au moins un des deux");

                            oldControls.Add(newDeletedControlManaged);
                        }

                        _Project.AddToDeletedControlProjectDataTable(oldControls);

                        //modification de la version du projet
                        DriveWorks.Helper.Manager.SettingsManager.UpdateProjectVersionNumber(_Project, NewVersionNumber);

                        LoadData();
                        SaveNeeded = true;

                        MessageBox.Show("Les données sont maintenant ajoutées");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdNewConstantCopy_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (_AddedConstantManaged.Any())
                    {
                        var newConstants = new List<AddedConstantManaged>();

                        //Ajout des nouvelles données
                        foreach (DataGridViewRow item in dgvNewConstant.Rows)
                            newConstants.Add(AddedConstantView.GetFromRow(item));

                        _Project.AddToAddedConstantProjectDataTable(newConstants);

                        //modification de la version du projet
                        DriveWorks.Helper.Manager.SettingsManager.UpdateProjectVersionNumber(_Project, NewVersionNumber);

                        LoadData();
                        SaveNeeded = true;

                        MessageBox.Show("Les données sont maintenant ajoutées");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdOldConstantCopy_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (_DeletedConstantManaged.Any())
                    {
                        var oldConstants = new List<DeletedConstantManaged>();

                        var currentConstantList = _Project.GetCurrentConstantList();

                        //Ajout des nouvelles données
                        foreach (DataGridViewRow item in dgvOldConstant.Rows)
                        {
                            var newDeletedConstantManaged = DeletedConstantView.GetFromRow(item);

                            if (newDeletedConstantManaged.TransfertConstantName.IsNotNullAndNotEmpty())
                            {
                                if (currentConstantList.Any(x => x.Name == newDeletedConstantManaged.TransfertConstantName) == false)
                                    throw new Exception("Le transfert de {0} vers {1} est impossible car {1} est introuvable".FormatString(newDeletedConstantManaged.ConstantName, newDeletedConstantManaged.TransfertConstantName));
                            }
                            oldConstants.Add(newDeletedConstantManaged);
                        }

                        _Project.AddToDeletedConstantProjectDataTable(oldConstants);

                        //modification de la version du projet
                        DriveWorks.Helper.Manager.SettingsManager.UpdateProjectVersionNumber(_Project, NewVersionNumber);

                        LoadData();
                        SaveNeeded = true;

                        MessageBox.Show("Les données sont maintenant ajoutées");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void dgvOldControl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > -1)
                {
                    //Chargement de la combo dans la cellule
                    var columnName = Library.Tools.Misc.PropertyObserver.GetPropertyName<DeletedControlView>(x => x.TransfertControlName);

                    // Bind grid cell with combobox and than bind combobox with datasource.
                    var l_objGridDropbox = new DataGridViewComboBoxCell();

                    // Check the column  cell, in which it click.
                    if (dgvOldControl.Columns[e.ColumnIndex].Name.Contains(columnName))
                    {
                        //Création de la liste des controls possibles
                        var controlManagedList = _Project.GetManagedControls();
                        var comboList = new List<string>();
                        comboList.Add(string.Empty);
                        comboList.AddRange(controlManagedList.Enum().Select(x => x.ControlName).ToList());

                        // On click of datagridview cell, attched combobox with this click cell of datagridview
                        dgvOldControl[e.ColumnIndex, e.RowIndex] = l_objGridDropbox;
                        l_objGridDropbox.DataSource = comboList.Enum().OrderBy(x=>x).ToList();
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