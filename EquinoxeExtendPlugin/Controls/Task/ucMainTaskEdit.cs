using DriveWorks.Helper;
using DriveWorks.Security;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Product;
using EquinoxeExtend.Shared.Object.Release;
using Library.Control.Extensions;
using Library.Control.UserControls;
using Library.Tools.Attributes;
using Library.Tools.Enums;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriveWorks.Helper.Manager;


namespace EquinoxeExtendPlugin.Controls.Task
{
    
    public partial class ucMainTaskEdit : UserControl, IUcUserControl
    {
        #region Public EVENTS

        public event EventHandler Close = delegate { };

        #endregion

        #region Public PROPERTIES

        public DialogResult DialogResult { get; private set; }

        public bool EnabledPages
        {
            set
            {
                tlpDescription.Enabled = value;
                tlpMain.Enabled = value;
                tlpFooter.Enabled = value;
                tlpDates.Enabled = value;
            }
        }

        #endregion

        #region Public CONSTRUCTORS

        public ucMainTaskEdit()
        {
            InitializeComponent();

            txtName.Validating += ValidatorNeeded;
            cboTaskType.Validating += ValidatorNeeded;
            numPriority.Validating += ValidatorNeeded;
            cboPackage.Validating += ValidatorNeeded;
            txtDescription.Validating += ValidatorNeeded;
            txtComments.Validating += ValidatorNeeded;
            cboProjectNumber.Validating += ValidatorNeeded;
            cboRequestUser.Validating += ValidatorNeeded;
        }

        #endregion

        #region Public METHODS

        public void Initialize(DriveWorks.Group iGroup)
        {
            _Group = iGroup;
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                cboTaskType = cboTaskType.FillByDictionary(new MainTaskTypeEnum().ToDictionary("FR"));

                using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetSQLExtendConnectionString()))
                {
                    _ProductLineList = releaseService.GetProductLineList().Enum().OrderBy(x => x.Name).Enum().ToList();
                    foreach (var item in _ProductLineList.Enum())
                        chlProductLines.Items.Add(item.Name, false);

                    cboProjectNumber.DisplayMember = PropertyObserver.GetPropertyName<ExternalProject>(x => x.ProjectNumber);
                    cboProjectNumber.ValueMember = PropertyObserver.GetPropertyName<ExternalProject>(x => x.ExternalProjectId);
                    cboProjectNumber.DataSource = releaseService.GetExternalProjectList(ExternalProjectStatusSearchEnum.All).Enum().OrderBy(x => x.ProjectNumber).Enum().ToList();
                }

                var userList = _Group.GetUserList().Enum().OrderBy(x => x.DisplayName).Enum().ToList();
                var userList2 = _Group.GetUserList().Enum().OrderBy(x => x.DisplayName).Enum().ToList();

                cboRequestUser.DisplayMember = PropertyObserver.GetPropertyName<UserDetails>(x => x.DisplayName);
                cboRequestUser.ValueMember = PropertyObserver.GetPropertyName<UserDetails>(x => x.Id);
                cboRequestUser.DataSource = userList;

                cboCreationUser.DisplayMember = PropertyObserver.GetPropertyName<UserDetails>(x => x.DisplayName);
                cboCreationUser.ValueMember = PropertyObserver.GetPropertyName<UserDetails>(x => x.Id);
                cboCreationUser.DataSource = userList2;
            }
        }

        public void CreateMainTask()
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {              
                _StatusEnum = StatusEnum.New;
                _MainTask = null;
                LoadPackageCombo();
                FillControl();

                cboCreationUser.SelectedValue = _Group.CurrentUser.Id;
                txtCreationDate.Text = DateTime.Now.ToShortDateString();

                tabMain.SelectedTab = pagGeneral;

                ControlValidator();
                this.Focus();
            }
        }

        public void EditMainTask(MainTask iMainTask)
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                LoadPackageCombo();
                _StatusEnum = StatusEnum.Modified;
                _MainTask = iMainTask;
                FillControl();
                ControlValidator();
                this.Focus();
            }
        }

        public void LoadFromExternalProject(ExternalProject iExternalProject)
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                LoadPackageCombo();
                _StatusEnum = StatusEnum.New;
                _MainTask = null;
                FillControl();

                cboCreationUser.SelectedValue = _Group.CurrentUser.Id;
                txtCreationDate.Text = DateTime.Now.ToShortDateString();
                txtDescription.Text = iExternalProject.Description;
                txtName.Text = iExternalProject.ProjectName;

                cboProjectNumber.Text = iExternalProject.ProjectNumber;
                cboRequestUser.Text = iExternalProject.Pilote;
                cboTaskType.SelectedValue = MainTaskTypeEnum.ProjectDeveloppement;

                if (iExternalProject.DateObjectiveEnd != null)
                {
                    dtpObjectifDate.Checked = true;
                    dtpObjectifDate.Value = (DateTime)iExternalProject.DateObjectiveEnd;
                }

                ControlValidator();
                this.Focus();
            }
        }

        #endregion

        #region Protected METHODS

        protected void FillControl()
        {
            txtMainTaskId.Text = null;
            txtName.Text = null;
            cboTaskType.SelectedIndex = -1;
            numPriority.Value = 0;
            cboPackage.SelectedIndex = -1;
            cboCreationUser.SelectedIndex = -1;
            txtCreationDate.Text = null;
            txtStatus.Text = null;
            txtCompletedDate.Text = null;
            txtOpenedDate.Text = null;
            cboRequestUser.SelectedIndex = -1;
            txtDescription.Text = null;
            txtComments.Text = null;
            cboProjectNumber.SelectedIndex = -1;
            dtpObjectifDate.Checked = false;
            chlProductLines.ClearSelected();
            for (int a = 0; a <= chlProductLines.Items.Count - 1; a++)
                chlProductLines.SetItemChecked(a, false);

            if (_MainTask != null)
            {
                txtStatus.Text = _MainTask.Status.GetName("FR");
                txtMainTaskId.Text = _MainTask.MainTaskIdString;
                txtName.Text = _MainTask.Name;
                cboTaskType.SelectedValue = _MainTask.TaskType;
                numPriority.Value = _MainTask.Priority ?? 0;
                if (_MainTask.PackageId != null)
                    cboPackage.SelectedValue = _MainTask.PackageId;
                cboCreationUser.SelectedValue = _MainTask.CreationUserGUID;
                txtCreationDate.Text = _MainTask.CreationDate.ToShortDateString();

                if (_MainTask.CompletedDate != null)
                    txtCompletedDate.Text = _MainTask.CompletedDate.ToShortDateString();
                if (_MainTask.OpenedDate != null)
                    txtOpenedDate.Text = _MainTask.OpenedDate.ToShortDateString();

                if (_MainTask.ExternalProjectId != null)
                    cboProjectNumber.SelectedValue = _MainTask.ExternalProjectId;

                cboRequestUser.SelectedValue = _MainTask.RequestUserGUID;
                txtDescription.Text = _MainTask.Description;
                txtComments.Text = _MainTask.Comments;
                if (_MainTask.ObjectifCloseDate != null)
                {
                    dtpObjectifDate.Checked = true;
                    dtpObjectifDate.Value = (DateTime)_MainTask.ObjectifCloseDate;
                }

                //productline
                for (int a = 0; a <= chlProductLines.Items.Count - 1; a++)
                {
                    if (_MainTask.ProductLines.Exists(x => x.Name == (string)chlProductLines.Items[a]))
                    {
                        chlProductLines.SetItemChecked(a, true);
                        continue;
                    }
                }   
            }
            //ProjectNumber
            EnableActionProjectNumber();
        }

        protected MainTask FillFromControl()
        {
            var mainTask = new MainTask();

            if (_StatusEnum == StatusEnum.New)
            {
                mainTask.MainTaskId = -1;
                mainTask.CreationDate = DateTime.Now;
                mainTask.CreationUserGUID = _Group.CurrentUser.Id;
                mainTask.Status = MainTaskStatusEnum.Waiting;
            }
            else if (_StatusEnum == StatusEnum.Modified)
            {
                mainTask.CreationDate = Convert.ToDateTime(txtCreationDate.Text);
                mainTask.MainTaskId = _MainTask.MainTaskId;
                mainTask.CreationUserGUID = _MainTask.CreationUserGUID;
                mainTask.CompletedDate = _MainTask.CompletedDate;
                mainTask.CreationDate = _MainTask.CreationDate;
                mainTask.OpenedDate = _MainTask.OpenedDate;
                mainTask.StartDate = _MainTask.StartDate;
                mainTask.Status = _MainTask.Status;
            }
            else
                throw new NotSupportedException(_StatusEnum.ToStringWithEnumName());

            mainTask.Comments = txtComments.Text;
            mainTask.Description = txtDescription.Text;
            mainTask.Name = txtName.Text;
            mainTask.PackageId = (cboPackage.SelectedIndex != -1) ? (long?)cboPackage.SelectedValue : null;
            mainTask.Priority = (numPriority.Value != 0) ? (int?)numPriority.Value : null;
            mainTask.RequestUserGUID = (Guid)cboRequestUser.SelectedValue;
            mainTask.TaskType = (EquinoxeExtend.Shared.Enum.MainTaskTypeEnum)cboTaskType.SelectedValue;

            mainTask.ExternalProjectId = (mainTask.TaskType == MainTaskTypeEnum.ProjectDeveloppement) ? (long?)cboProjectNumber.SelectedValue : null;

            mainTask.ObjectifCloseDate = dtpObjectifDate.Checked ? (DateTime?)dtpObjectifDate.Value : null;

            mainTask.ProductLines = new List<ProductLine>();

            if (chlProductLines.CheckedItems.Count != 0)
            {
                foreach (var item in chlProductLines.CheckedItems)
                    mainTask.ProductLines.Add(_ProductLineList.Single(x => x.Name == (string)item));
            }

            return mainTask;
        }

        protected bool ControlValidator()
        {
            var result = true;

            //name
            if (txtName.Text.IsNullOrEmpty())
            {
                this.errorProvider.SetError(txtName, "Le nom est requis");
                result = false;
            }
            else if (txtName.Text.Length > 300)
            {
                this.errorProvider.SetError(txtName, "Le nom est limité à 300 caractères");
                result = false;
            }
            else
            {
                this.errorProvider.SetError(txtName, null);
            }

            //Type
            if (cboTaskType.SelectedIndex == -1)
            {
                this.errorProvider.SetError(cboTaskType, "Le type de tâche est requis");
                this.errorProvider.SetError(cboProjectNumber, null);
                result = false;
            }
            else
            {
                if (((EquinoxeExtend.Shared.Enum.MainTaskTypeEnum)cboTaskType.SelectedValue) == MainTaskTypeEnum.ProjectDeveloppement && cboProjectNumber.SelectedIndex == -1)
                {
                    this.errorProvider.SetError(cboProjectNumber, "Le type de tâche est requis");
                    result = false;
                }
                else
                {
                    this.errorProvider.SetError(cboProjectNumber, null);
                }
                this.errorProvider.SetError(cboTaskType, null);
            }

            //Demandeur
            if (cboRequestUser.SelectedIndex == -1)
            {
                this.errorProvider.SetError(cboRequestUser, "Le demandeur est requis");
                result = false;
            }
            else
            {
                this.errorProvider.SetError(cboRequestUser, null);
            }

            return result;
        }

        #endregion

        #region Private FIELDS

        private List<ProductLine> _ProductLineList;
        private BoolLock _IsLoading = new BoolLock();

        private StatusEnum _StatusEnum;

        private MainTask _MainTask;

        private DriveWorks.Group _Group;

        #endregion

        #region Private METHODS

        private void LoadPackageCombo()
        {
            using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                cboPackage.DisplayMember = PropertyObserver.GetPropertyName<EquinoxeExtend.Shared.Object.Release.Package>(x => x.PackageIdStatusString);
                cboPackage.ValueMember = PropertyObserver.GetPropertyName<EquinoxeExtend.Shared.Object.Release.Package>(x => x.PackageId);
                cboPackage.DataSource = releaseService.GetAllowedPackagesForMainTask(_MainTask);
            }
        }

        private void ValidatorNeeded(object sender, CancelEventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    ControlValidator();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            try
            {
                long mainTaskId = 0;

                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetSQLExtendConnectionString()))
                    {
                        if (!ControlValidator())
                        {
                            MessageBox.Show("Données invalides");
                            return;
                        }

                        if (_StatusEnum == StatusEnum.New)
                        {
                            var newMaintask = FillFromControl();
                            mainTaskId = releaseService.AddMainTask(newMaintask);
                        }
                        else if (_StatusEnum == StatusEnum.Modified)
                        {
                            var theMainTask = FillFromControl();
                            mainTaskId = theMainTask.MainTaskId;
                            releaseService.UpdateMainTask(theMainTask);
                        }
                        else
                            throw new NotSupportedException(_StatusEnum.ToStringWithEnumName());

                        DialogResult = System.Windows.Forms.DialogResult.OK;                      
                    }

                    //Applications des droits sur dev
                    Tools.Tools.ReleaseProjectsRights(_Group);
                }
                EditionEventArgs.StatusEnum status;
                if (_StatusEnum == StatusEnum.New)
                    status = EditionEventArgs.StatusEnum.New;
                else if (_StatusEnum == StatusEnum.Modified)
                    status = EditionEventArgs.StatusEnum.Update;
                else
                    throw new NotSupportedException(_StatusEnum.ToStringWithEnumName());

               Close(this, new EditionEventArgs(mainTaskId, status));
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }

                long id = (_MainTask != null) ? _MainTask.MainTaskId : -1;

                Close(this, new EditionEventArgs(id, EditionEventArgs.StatusEnum.Cancel));
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cboTaskType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    EnableActionProjectNumber();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdDeletePackage_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    cboPackage.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void EnableActionProjectNumber()
        {
            if (cboTaskType.SelectedIndex != -1)
            {
                if (((EquinoxeExtend.Shared.Enum.MainTaskTypeEnum)cboTaskType.SelectedValue) == MainTaskTypeEnum.ProjectDeveloppement)
                {
                    lblProjectNumber.Enabled = true;
                    cboProjectNumber.Enabled = true;
                }
                else
                {
                    lblProjectNumber.Enabled = false;
                    cboProjectNumber.Enabled = false;
                    cboProjectNumber.SelectedIndex = -1;
                }
            }
            else
            {
                lblProjectNumber.Enabled = false;
                cboProjectNumber.Enabled = false;
                cboProjectNumber.SelectedIndex = -1;
            }
        }

        #endregion

        
    }
}