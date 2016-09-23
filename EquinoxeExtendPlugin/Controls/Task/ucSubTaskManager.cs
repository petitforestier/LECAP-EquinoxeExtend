using DriveWorks;
using DriveWorks.Helper;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
using Library.Control.Datagridview;
using Library.Control.Extensions;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquinoxeExtendPlugin.Controls.Task
{
    public partial class ucSubTaskManager : UserControl
    {
        #region Public DELEGATES

        public delegate void SubTaskDelegate(SubTask iSubTask);

        public delegate void MainTaskDelegate(MainTask iMainTask);

        public delegate void EmptyDelegate();

        #endregion

        #region Public EVENTS

        public event SubTaskDelegate SubTaskSelectionChanged = delegate { };
        public event MainTaskDelegate CreateProjectTask = delegate { };
        public event SubTaskDelegate UpdateProjectTask = delegate { };
        public event EventHandler DeletedProjectTask = delegate { };
        public event EventHandler NothingSelected = delegate { };

        #endregion

        #region Public CONSTRUCTORS

        public ucSubTaskManager()
        {
            InitializeComponent();
        }

        #endregion

        #region Public METHODS

        public void LoadControl(MainTask iMainTask)
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                _MainTask = iMainTask;
                LoadDataGridViewSubTask();
                DisplaySelectionMode();
            }
        }

        public void Initialize(Group iGroup)
        {
            if (_IsLoading.Value) return;
            using (new BoolLocker(ref _IsLoading))
            {
                _Group = iGroup;

                //Init
                dgvSubTasks.MultiSelect = false;
                dgvSubTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvSubTasks.ReadOnly = true;
                dgvSubTasks.RowTemplate.Height = DATAGRIDVIEWROWHEIGHT;
                dgvSubTasks.AllowUserToAddRows = false;

                dgvSubTasks.RowHeadersVisible = false;
                dgvSubTasks.AllowUserToResizeRows = false;
                dgvSubTasks.AllowUserToResizeColumns = true;
                dgvSubTasks.AllowUserToOrderColumns = false;

                _SubTaskBindingSource.DataSource = new List<SubTaskView>();
                dgvSubTasks.DataSource = _SubTaskBindingSource;
                dgvSubTasks.FormatColumns<SubTaskView>("FR");
            }
        }

        public void SelectionMode()
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                DisplaySelectionMode();
            }
        }

        #endregion

        #region Protected METHODS

        protected SubTask GetSelectedSubTask()
        {
            if (dgvSubTasks.SelectedRows.Count == 1)
                return ((SubTaskView)dgvSubTasks.SelectedRows[0].DataBoundItem).Object;
            return null;
        }

        protected void DisplayEditMode()
        {
            cmdAddSubTask.Enabled = false;
            cmdDeleteSubTask.Enabled = false;
            dgvSubTasks.Enabled = false;
        }

        protected void DisplaySelectionMode()
        {
            cmdAddSubTask.Enabled = true;
            cmdDeleteSubTask.Enabled = true;
            dgvSubTasks.Enabled = true;
        }

        #endregion

        #region Protected CLASSES

        protected class SubTaskView
        {
            //[Visible]
            //[Name("FR", "N° sous tâche")]
            //[WidthColumn(90)]
            //[ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            //public string ProjectTask { get; set; }

            #region Public PROPERTIES

            [Visible]
            [Name("FR", "Action")]
            [WidthColumn(250)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string Action { get; set; }

            [Visible]
            [Name("FR", "Développeur")]
            [WidthColumn(150)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Developper { get; set; }

            [Visible]
            [Name("FR", "Avancement")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public Image Progression { get; set; }

            [Visible]
            [Name("FR", "Charge")]
            [WidthColumn(60)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Duration { get; set; }

            public SubTask Object { get; set; }

            #endregion

            #region Public METHODS

            public static SubTaskView ConvertTo(SubTask iObj, Group iGroup)
            {
                if (iObj == null)
                    return null;

                var newView = new SubTaskView();

                newView.Object = iObj;
                if (iObj.DevelopperGUID != null)
                    newView.Developper = iGroup.GetUserById((Guid)iObj.DevelopperGUID).DisplayName;

                if (iObj.ProjectGUID != null)
                {
                    var theProjectDetails = iGroup.GetProjectFromGUID((Guid)iObj.ProjectGUID);
                    if (theProjectDetails == null)
                        newView.Action = "!!! Le projet a été supprimé !!!";
                    else
                        newView.Action = "Modifier le projet : " + theProjectDetails.Name;
                }
                else
                    newView.Action = iObj.Designation;

                //Progression
                var imageWidth = (int)typeof(SubTaskView).GetWidthColumn(Library.Tools.Misc.PropertyObserver.GetPropertyName<SubTaskView>(x => x.Progression));
                newView.Progression = Library.Control.Datagridview.ImageHelper.GetProgressionBarImage(iObj.Progression, DATAGRIDVIEWROWHEIGHT, imageWidth, true);

                newView.Duration = iObj.Duration + " j";
                return newView;
            }

            #endregion
        }

        #endregion

        #region Private FIELDS

        private const int DATAGRIDVIEWROWHEIGHT = 28;
        private BoolLock _IsLoading = new BoolLock();

        private Group _Group;

        private MainTask _MainTask;

        private BindingSource _SubTaskBindingSource = new BindingSource();

        #endregion

        #region Private METHODS

        private void LoadDataGridViewSubTask(long? iSelectedSubTaskId = null)
        {
            var firstDisplayIndex = dgvSubTasks.FirstDisplayedScrollingRowIndex;

            var subTaskList = (_MainTask != null) ? _MainTask.SubTasks : null;

            if (subTaskList.IsNotNullAndNotEmpty())
            {
                var list = subTaskList.Enum().OrderByDescending(x => x.MainTaskId).Select(x => SubTaskView.ConvertTo(x, _Group)).ToList();
                _SubTaskBindingSource.DataSource = list;
                dgvSubTasks.Refresh();
            }
            else
                _SubTaskBindingSource.Clear();

            dgvSubTasks.SetFirstDisplayedScrollingRowIndex(firstDisplayIndex);

            lblMessage.Text = "{0} sous tâche(s)".FormatString(subTaskList.Enum().Count());

            if (dgvSubTasks.RowCount != 0)
            {
                if (iSelectedSubTaskId != null)
                {
                    dgvSubTasks.Refresh();
                    dgvSubTasks.SelectRowByPropertyValue<SubTaskView>(x => x.Object.SubTaskId.ToString(), iSelectedSubTaskId.ToString());
                }

                var selectedTransaction = GetSelectedSubTask();
                if (selectedTransaction != null)
                    SubTaskSelectionChanged(selectedTransaction);
                else
                    SubTaskSelectionChanged(null);
            }
            else
            {
                NothingSelected(null, null);
            }
        }

        private void dgvProjectTasks_SelectionChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    SubTaskSelectionChanged(GetSelectedSubTask());
                    dgvSubTasks.Select();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdAddProjectTask_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    if (_MainTask != null)
                    {
                        CreateProjectTask(_MainTask);
                        DisplayEditMode();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdDeleteProjectTask_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        var selectedProjectTask = GetSelectedSubTask();

                        if (selectedProjectTask == null)
                            return;

                        //suppression project task
                        releaseService.DeleteProjectTask(selectedProjectTask);
                    }
                }

                //Doit être à l'extérieur du boollocker pour permettre le reload.
                DeletedProjectTask(sender, e);
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdEditProjectTask_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedProjectTask = GetSelectedSubTask();

                    if (selectedProjectTask == null)
                        return;
                    UpdateProjectTask(selectedProjectTask);
                    DisplayEditMode();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void dgvProjectTasks_DoubleClick(object sender, System.EventArgs e)
        {
            cmdEditProjectTask_Click(sender, e);
        }


        #endregion
    }
}