using DriveWorks.Helper;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
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
    public partial class ucSubTaskEdit : UserControl
    {
        #region Public EVENTS

        public event EventHandler Close = delegate { };

        #endregion

        #region Public PROPERTIES

        public DialogResult DialogResult { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        public ucSubTaskEdit()
        {
            InitializeComponent();

            cboDevelopper.Validating += ValidatorNeeded;
            cboProject.Validating += ValidatorNeeded;
            numDuration.Validating += ValidatorNeeded;
            numProgression.Validating += ValidatorNeeded;
            txtComments.Validating += ValidatorNeeded;
            txtDesignation.Validating += ValidatorNeeded;
        }

        #endregion

        #region Public METHODS

        public void Initialize(DriveWorks.Group iGroup)
        {
            _Group = iGroup;
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                cboProject.DisplayMember = PropertyObserver.GetPropertyName<DriveWorks.ProjectDetails>(x => x.Name);
                cboProject.ValueMember = PropertyObserver.GetPropertyName<DriveWorks.ProjectDetails>(x => x.Id);
                cboProject.DataSource = _Group.Projects.GetProjects().ToList().Enum().OrderBy(x => x.Name).ToList();

                cboDevelopper.DisplayMember = PropertyObserver.GetPropertyName<DriveWorks.Security.UserDetails>(x => x.DisplayName);
                cboDevelopper.ValueMember = PropertyObserver.GetPropertyName<DriveWorks.Security.UserDetails>(x => x.Id);
                cboDevelopper.DataSource = _Group.GetUserAllowedCaptureList();
            }
        }

        public void CreateProjectTask(MainTask iMainTask)
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                _StatusEnum = StatusEnum.New;
                cboProject.Enabled = true;
                cmdDeleteProject.Enabled = true;
                _MainTask = iMainTask;
                _SubTask = null;
                FillControl();

                ControlValidator();
                cmdOk.Focus();
            }
        }

        public void EditProjectTask(SubTask iSubTask)
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                _StatusEnum = StatusEnum.Modified;
                cboProject.Enabled = false;
                cmdDeleteProject.Enabled = false;
                _SubTask = iSubTask;
                FillControl();
                ControlValidator();
                cmdOk.Focus();
            }
        }

        #endregion

        #region Protected METHODS

        protected void FillControl()
        {
            cboProject.SelectedIndex = -1;
            cboDevelopper.SelectedIndex = -1;
            numProgression.Value = 0;
            numDuration.Value = 0;
            txtComments.Text = null;
            txtDesignation.Text = null;

            if (_SubTask != null)
            {
                if (_SubTask.ProjectGUID != null)
                    cboProject.SelectedValue = _SubTask.ProjectGUID;

                txtDesignation.Text = _SubTask.Designation;

                if (_SubTask.DevelopperGUID != null)
                    cboDevelopper.SelectedValue = _SubTask.DevelopperGUID;
                numProgression.Value = _SubTask.Progression;
                if (_SubTask.Duration != null)
                    numDuration.Value = (int)_SubTask.Duration;
                txtComments.Text = _SubTask.Comments;
            }
        }

        protected SubTask FillFromControl()
        {
            var subTaks = new SubTask();

            if (_StatusEnum == StatusEnum.New)
            {
                subTaks.SubTaskId = -1;
                subTaks.MainTaskId = _MainTask.MainTaskId;
            }
            else if (_StatusEnum == StatusEnum.Modified)
            {
                subTaks.SubTaskId = _SubTask.SubTaskId;
                subTaks.MainTaskId = _SubTask.MainTaskId;
            }
            else
                throw new NotSupportedException(_StatusEnum.ToStringWithEnumName());

            subTaks.Designation = txtDesignation.Text;

            if (cboProject.SelectedIndex != -1)
                subTaks.ProjectGUID = (Guid)cboProject.SelectedValue;

            if (cboDevelopper.SelectedIndex != -1)
                subTaks.DevelopperGUID = (Guid)cboDevelopper.SelectedValue;
            subTaks.Progression = (int)numProgression.Value;
            subTaks.Duration = (int)numDuration.Value;
            subTaks.Comments = txtComments.Text;
            return subTaks;
        }

        protected bool ControlValidator()
        {
            var result = true;

            //Project / Désignation
            if (cboProject.SelectedIndex == -1 && txtDesignation.Text.IsNullOrEmpty())
            {
                this.errorProvider.SetError(cboProject, "Le projet ou la désignation est requis");
                this.errorProvider.SetError(txtDesignation, "Le projet ou la désignation est requis");
                result = false;
            }
            else
            {
                this.errorProvider.SetError(cboProject, null);
                this.errorProvider.SetError(txtDesignation, null);
            }

            return result;
        }

        protected void CommandEnableManagement()
        {


        }

        protected void ValidateEdit()
        {
            long projectTaskId = 0;

            using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
            {
                if (!ControlValidator())
                {
                    MessageBox.Show("Données invalides");
                    return;
                }

                if (_StatusEnum == StatusEnum.New)
                {
                    var newProjectTask = FillFromControl();
                    projectTaskId = releaseService.AddSubTask(newProjectTask);

                    //Applications des droits sur dev
                    Tools.Tools.ReleaseProjectsRights(_Group);
                }
                else if (_StatusEnum == StatusEnum.Modified)
                {
                    var theProjectTask = FillFromControl();
                    theProjectTask.SubTaskId = _SubTask.SubTaskId;
                    releaseService.UpdateSubTask(theProjectTask);
                }
                else
                    throw new NotSupportedException(_StatusEnum.ToStringWithEnumName());

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }

            Close(null, null);
        }

        #endregion

        #region Private FIELDS

        private BoolLock _IsLoading = new BoolLock();

        private StatusEnum _StatusEnum;

        private SubTask _SubTask;
        private MainTask _MainTask;

        private DriveWorks.Group _Group;

        #endregion

        #region Private METHODS

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
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    ValidateEdit();
                }
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
                Close(null, e);
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdDeleteProject_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    cboProject.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void numProgression_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (e.KeyCode == Keys.Enter)
                        ValidateEdit();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void numDuration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (e.KeyCode == Keys.Enter)
                        ValidateEdit();
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