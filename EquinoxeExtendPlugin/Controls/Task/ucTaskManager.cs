using DriveWorks;
using DriveWorks.Helper;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Product;
using EquinoxeExtend.Shared.Object.Release;
using EquinoxeExtendPlugin.Controls.Task;
using Library.Control.Extensions;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquinoxeExtendPlugin.Controls.ReleaseManagement
{
    public partial class ucTaskManager : UserControl
    {
        #region Public CONSTRUCTORS

        public ucTaskManager()
        {
            InitializeComponent();
        }

        #endregion

        #region Public METHODS

        public void Initialize(Group iGroup)
        {
            if (_IsLoading.Value) return;
            using (new BoolLocker(ref _IsLoading))
            {
                _Group = iGroup;

                //init
                this.ucMainTaskManager.Initialize(_Group);
                this.ucMainTaskManager.CreateMainTask += CreateMainTask;
                this.ucMainTaskManager.UpdateMainTask += UpdateMainTask;
                this.ucMainTaskManager.NothingSelected += NoMainTaskSelected;
                this.ucMainTaskManager.MainTaskSelectionChanged += MainTaskSelected;

                this.ucMainTaskEdit.Initialize(_Group);
                this.ucMainTaskEdit.Close += MainTaskEditClosed;

                //Status
                cboMainTaskStatusSearch = cboMainTaskStatusSearch.FillByDictionary(new MainTaskStatusSearchEnum().ToDictionary("FR"));
                cboMainTaskStatusSearch.SelectedValue = MainTaskStatusSearchEnum.NotCompleted;

                //Order
                cboOrderBy = cboOrderBy.FillByDictionary(new MainTaskOrderByEnum().ToDictionary("FR"));
                cboOrderBy.SelectedValue = MainTaskOrderByEnum.Priority;

                //Project
                cboProject.DisplayMember = PropertyObserver.GetPropertyName<ProjectDetails>(x => x.Name);
                cboProject.ValueMember = PropertyObserver.GetPropertyName<ProjectDetails>(x => x.Id);
                cboProject.DataSource = _Group.Projects.GetProjects().ToList().Enum().OrderBy(x => x.Name).ToList();
                cboProject.SelectedIndex = -1;

                using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                {
                    //Gamme
                    cboProductLine.DisplayMember = PropertyObserver.GetPropertyName<ProductLine>(x => x.Name);
                    cboProductLine.ValueMember = PropertyObserver.GetPropertyName<ProductLine>(x => x.ProductLineId);
                    cboProductLine.DataSource = releaseService.GetProductLineList().Enum().OrderBy(x => x.Name).Enum().ToList();
                    cboProductLine.SelectedIndex = -1;

                    //Package
                    cboPackage.DisplayMember = PropertyObserver.GetPropertyName<Package>(x => x.PackageIdString);
                    cboPackage.ValueMember = PropertyObserver.GetPropertyName<Package>(x => x.PackageId);
                    cboPackage.DataSource = releaseService.GetPackageList(PackageStatusSearchEnum.All);
                    cboPackage.SelectedIndex = -1;
                }

                //Type
                cboMainTaskType = cboMainTaskType.FillByDictionary(new MainTaskTypeEnum().ToDictionary("FR"));
                cboMainTaskType.SelectedIndex = -1;

                //Développeur
                cboDevelopper.DisplayMember = PropertyObserver.GetPropertyName<DriveWorks.Security.UserDetails>(x => x.DisplayName);
                cboDevelopper.ValueMember = PropertyObserver.GetPropertyName<DriveWorks.Security.UserDetails>(x => x.Id);
                cboDevelopper.DataSource = _Group.GetUserAllowedCaptureList();
                cboDevelopper.SelectedIndex = -1;
              
                this.ucSubTaskManager.Initialize(_Group);
                this.ucSubTaskManager.CreateProjectTask += CreateSubTask;
                this.ucSubTaskManager.UpdateProjectTask += UpdateSubTask;
                this.ucSubTaskManager.DeletedProjectTask += SubTaskDeleted;
                this.ucSubTaskManager.NothingSelected += NoSubTaskSelected;
                this.ucSubTaskManager.SubTaskSelectionChanged += ProjectTaskSelected;

                this.ucSubTaskEdit.Initialize(_Group);
                this.ucSubTaskEdit.Close += SubTaskEditClosed;

                LoadCriteria();
            }
        }

        #endregion

        #region Private FIELDS

        private BoolLock _IsLoading = new BoolLock();
        private Group _Group;

        #endregion

        #region Private METHODS

        private void cmdSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    LoadCriteria();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void LoadCriteria()
        {
            DisplayReadMode();
            this.ucSubTaskEdit.Hide();
            this.ucMainTaskEdit.Hide();

            Guid? projectId = (cboProject.SelectedIndex != -1) ? (Guid?)cboProject.SelectedValue : null;
            long? productLineId = (cboProductLine.SelectedIndex != -1) ? (long?)cboProductLine.SelectedValue : null;
            MainTaskTypeEnum? type = (cboMainTaskType.SelectedIndex != -1) ? (MainTaskTypeEnum?)cboMainTaskType.SelectedValue : null;
            Guid? developperId = (cboDevelopper.SelectedIndex != -1) ? (Guid?)cboDevelopper.SelectedValue : null;
            long? packageId = (cboPackage.SelectedIndex != -1) ? (long?)cboPackage.SelectedValue : null;

            this.ucMainTaskManager.LoadControl((MainTaskStatusSearchEnum)cboMainTaskStatusSearch.SelectedValue, (MainTaskOrderByEnum)cboOrderBy.SelectedValue, projectId, productLineId, type, developperId, packageId);
        }

        private void LoadMainTask()
        {
            DisplayReadMode();
            this.ucSubTaskEdit.Hide();
            this.ucMainTaskEdit.Hide();

            this.ucMainTaskManager.LoadControl(txtMaintaskId.Text.ToInt64());
        }

        private void DisplayReadMode()
        {
            this.ucMainTaskEdit.EnabledPages = false;
            this.ucMainTaskManager.Enabled = true;
            this.ucSubTaskManager.Enabled = true;
            this.ucSubTaskEdit.Enabled = false;
            this.cmdCriteriaSearch.Enabled = true;
            this.cboMainTaskStatusSearch.Enabled = true;
        }

        private void CreateMainTask()
        {
            DisplayEditMainTaskMode();
            ucMainTaskEdit.Visible = true;
            this.ucMainTaskEdit.CreateMainTask();
        }

        private void UpdateMainTask(MainTask iMainTask)
        {
            DisplayEditMainTaskMode();
            this.ucMainTaskEdit.EditMainTask(iMainTask);
        }

        private void MainTaskEditClosed(object sender, System.EventArgs e)
        {
            if (e == null)
                throw new Exception("L'argument ne peux pas être null");

            DisplayReadMode();

            var arg = (EditionEventArgs)e;
            if (arg.Status == EditionEventArgs.StatusEnum.Cancel)
            {
                ucMainTaskManager.ReLoadControl(arg.Id);
            }
            else if (arg.Status == EditionEventArgs.StatusEnum.New)
            {
                ucMainTaskManager.LoadControl(arg.Id);
            }
            else if (arg.Status == EditionEventArgs.StatusEnum.Update)
            {
                ucMainTaskManager.ReLoadControl(arg.Id);
            }
            else
                throw new NotSupportedException(arg.Status.ToStringWithEnumName());
        }

        private void NoMainTaskSelected(object sender, System.EventArgs e)
        {
            ucMainTaskEdit.Hide();
            ucSubTaskManager.LoadControl(null);
            NoSubTaskSelected(null, null);
            DisplayReadMode();
        }

        private void MainTaskSelected(MainTask iMainTask)
        {
            ucMainTaskEdit.Show();
            ucMainTaskEdit.EditMainTask(iMainTask);
            ucSubTaskManager.LoadControl(iMainTask);
        }

        private void DisplayEditMainTaskMode()
        {
            this.ucMainTaskEdit.EnabledPages = true;
            this.ucMainTaskManager.Enabled = false;
            this.ucSubTaskManager.Enabled = false;
            this.ucSubTaskEdit.Enabled = false;
            this.cmdCriteriaSearch.Enabled = false;
            this.cboMainTaskStatusSearch.Enabled = false;
        }

        private void CreateSubTask(MainTask iMainTask)
        {
            DisplayEditSubTaskMode();
            ucSubTaskEdit.Visible = true;
            this.ucSubTaskEdit.CreateProjectTask(iMainTask);
        }

        private void UpdateSubTask(SubTask iSubTask)
        {
            DisplayEditSubTaskMode();
            this.ucSubTaskEdit.EditProjectTask(iSubTask);
        }

        private void SubTaskEditClosed(object sender, System.EventArgs e)
        {
            ucMainTaskManager.ReLoadControl();
            DisplayReadMode();
        }

        private void SubTaskDeleted(object sender, System.EventArgs e)
        {
            ucMainTaskManager.ReLoadControl();
        }

        private void NoSubTaskSelected(object sender, System.EventArgs e)
        {
            ucSubTaskEdit.Hide();
        }

        private void ProjectTaskSelected(SubTask iProjectTask)
        {
            ucSubTaskEdit.Show();
            ucSubTaskEdit.EditProjectTask(iProjectTask);
        }

        private void DisplayEditSubTaskMode()
        {
            this.ucMainTaskEdit.EnabledPages = false;
            this.ucMainTaskManager.Enabled = false;
            this.ucSubTaskManager.Enabled = false;
            this.ucSubTaskEdit.Enabled = true;
            this.ucSubTaskEdit.Visible = true;
            this.cmdCriteriaSearch.Enabled = false;
            this.cboMainTaskStatusSearch.Enabled = false;
        }

        private void cboProject_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                    {
                        cboProject.DroppedDown = false;
                        cboProject.SelectedIndex = -1;
                        cmdCriteriaSearch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cboProductLine_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                    {
                        cboProductLine.DroppedDown = false;
                        cboProductLine.SelectedIndex = -1;
                        cmdCriteriaSearch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cboMainTaskType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                    {
                        cboMainTaskType.DroppedDown = false;
                        cboMainTaskType.SelectedIndex = -1;
                        cmdCriteriaSearch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdMainTaskSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (txtMaintaskId.Text.IsNotNullAndNotEmpty())
                        LoadMainTask();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void txtMaintaskId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (txtMaintaskId.Text.IsNotNullAndNotEmpty())
                            LoadMainTask();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cboDevelopper_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                    {
                        cboDevelopper.DroppedDown = false;
                        cboDevelopper.SelectedIndex = -1;
                        cmdCriteriaSearch.Focus();
                    }
                        
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        #endregion

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
                        cmdCriteriaSearch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }
    }
}