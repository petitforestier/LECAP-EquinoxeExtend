using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper;
using DriveWorks.Helper.Object;
using Library.Control.UserControls;
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

        #region Public CONSTRUCTORS

        public ucControlVersion(IApplication iApplication)
        {
            InitializeComponent();
            _Application = iApplication;

            dgvNewConstant.MultiSelect = false;
            dgvNewConstant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNewConstant.ReadOnly = true;
            dgvNewConstant.AllowUserToAddRows = false;
            dgvNewConstant.RowHeadersVisible = false;
            dgvNewConstant.AllowUserToResizeRows = false;
            dgvNewConstant.AllowUserToResizeColumns = true;
            dgvNewConstant.AllowUserToOrderColumns = false;

            dgvNewControl.MultiSelect = false;
            dgvNewControl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNewControl.ReadOnly = true;
            dgvNewControl.AllowUserToAddRows = false;
            dgvNewControl.RowHeadersVisible = false;
            dgvNewControl.AllowUserToResizeRows = false;
            dgvNewControl.AllowUserToResizeColumns = true;
            dgvNewControl.AllowUserToOrderColumns = false;

            dgvOldConstant.MultiSelect = false;
            dgvOldConstant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOldConstant.ReadOnly = true;
            dgvOldConstant.AllowUserToAddRows = false;
            dgvOldConstant.RowHeadersVisible = false;
            dgvOldConstant.AllowUserToResizeRows = false;
            dgvOldConstant.AllowUserToResizeColumns = true;
            dgvOldConstant.AllowUserToOrderColumns = false;

            dgvOldControl.MultiSelect = false;
            dgvOldControl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOldControl.ReadOnly = true;
            dgvOldControl.AllowUserToAddRows = false;
            dgvOldControl.RowHeadersVisible = false;
            dgvOldControl.AllowUserToResizeRows = false;
            dgvOldControl.AllowUserToResizeColumns = true;
            dgvOldControl.AllowUserToOrderColumns = false;

            var groupService = _Application.ServiceManager.GetService<IGroupService>();

            var projectService = _Application.ServiceManager.GetService<IProjectService>();
            _Project = projectService.ActiveProject;
            var tupleValues = _Project.GetAddedDeletedControlConstant();

            _AddedControlManaged = tupleValues.Item1;
            _DeletedControlManaged = tupleValues.Item2;
            _AddedConstantManaged = tupleValues.Item3;
            _DeletedConstantManaged = tupleValues.Item4;

            dgvNewControl.DataSource = _AddedControlManaged;
            if (!_AddedControlManaged.Any())
                cmdNewControlCopy.Enabled = false;

            dgvOldControl.DataSource = _DeletedControlManaged;
            if (!_DeletedControlManaged.Any())
                cmdOldControlCopy.Enabled = false;

            dgvNewConstant.DataSource = _AddedConstantManaged;
            if (!_AddedConstantManaged.Any())
                cmdNewConstantCopy.Enabled = false;

            dgvOldConstant.DataSource = _DeletedConstantManaged;
            if (!_DeletedConstantManaged.Any())
                cmdOldConstantCopy.Enabled = false;

            lblNewConstantCount.Text = _AddedConstantManaged.Count.ToString();
            lblOldConstantCount.Text = _DeletedConstantManaged.Count.ToString();
            lblNewControlCount.Text = _AddedControlManaged.Count.ToString();
            lblOldControlCount.Text = _DeletedControlManaged.Count.ToString();
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
                        var result = string.Empty;

                        //Ajout des nouvelles données
                        foreach (var item in _AddedControlManaged.Enum())
                        {
                            if (result.IsNotNullAndNotEmpty())
                                result += Environment.NewLine;
                            result += "{0}	{1}	{2}".FormatString(item.ControlName, item.ProjectVersion, "");
                        }

                        Clipboard.SetText(result);
                        MessageBox.Show("Les données sont maintenant ajoutées aux presse-papier. Ouvrir la table concernée et coller dans la dernière ligne");
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
                        var result = string.Empty;
                        //Ajout des nouvelles données
                        foreach (var item in _DeletedControlManaged.Enum())
                        {
                            if (result.IsNotNullAndNotEmpty())
                                result += Environment.NewLine;
                            result += "{0}	{1}	{2}	{3}	{4}".FormatString(item.ControlName, "", item.ProjectVersion, "", "");
                        }

                        Clipboard.SetText(result);
                        MessageBox.Show("Les données sont maintenant ajoutées aux presse-papier. Ouvrir la table concernée et coller dans la dernière ligne");
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
                        var result = string.Empty;
                        //Ajout des nouvelles données
                        foreach (var item in _AddedConstantManaged.Enum())
                        {
                            if (result.IsNotNullAndNotEmpty())
                                result += Environment.NewLine;
                            result += "{0}	{1}".FormatString(item.ConstantName, item.ProjectVersion);
                        }

                        Clipboard.SetText(result);
                        MessageBox.Show("Les données sont maintenant ajoutées aux presse-papier. Ouvrir la table concernée et coller dans la dernière ligne");
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
                        var result = string.Empty;
                        //Ajout des nouvelles données
                        foreach (var item in _DeletedConstantManaged.Enum())
                        {
                            if (result.IsNotNullAndNotEmpty())
                                result += Environment.NewLine;
                            result += "{0}	{1}	{2}".FormatString(item.ConstantName, item.ProjectVersion, "");
                        }

                        Clipboard.SetText(result);
                        MessageBox.Show("Les données sont maintenant ajoutées aux presse-papier. Ouvrir la table concernée et coller dans la dernière ligne");
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