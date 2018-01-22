using DriveWorks;
using DriveWorks.Helper.Manager;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
using Library.Control.Datagridview;
using Library.Control.UserControls;
using Library.Tools.Attributes;
using Library.Tools.Enums;
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
    public partial class ucMainTaskManager : UserControl
    {
        #region Public DELEGATES

        public delegate void MainTaskDelegate(MainTask iMainTask);

        public delegate void EmptyDelegate();

        #endregion

        #region Public EVENTS

        public event MainTaskDelegate MainTaskSelectionChanged = delegate { };
        public event EmptyDelegate CreateMainTask = delegate { };
        public event MainTaskDelegate UpdateMainTask = delegate { };
        public event EventHandler NothingSelected = delegate { };

        #endregion

        #region Public PROPERTIES

        public MainTask MainTaskSelected
        {
            get
            {
                return GetSelectedMainTask();
            }
        }

        #endregion

        #region Public CONSTRUCTORS

        public ucMainTaskManager()
        {
            InitializeComponent();
        }

        #endregion

        #region Public METHODS

        public void LoadControl(long iMainTaskId)
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                _LoadingType = LoadingType.MainTaskId;
                _MainTaskId = iMainTaskId;
                LoadDataGridViewMainTask(true, null);
                DisplaySelectionMode();
            }
        }

        public void LoadControl(MainTaskStatusSearchEnum iMainTaskStatusSearch, MainTaskOrderByEnum iMainTaskOrderBy, Guid? iProjectId, long? iProductLineId, MainTaskTypeEnum? iMainTaskType, Guid? iDevelopperId, long? iPackageId)
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                _LoadingType = LoadingType.Criteria;
                _MainTaskStatusSearchEnum = iMainTaskStatusSearch;
                _MainTaskOrderBy = iMainTaskOrderBy;
                _ProjectId = iProjectId;
                _ProductLineId = iProductLineId;
                _MainTaskType = iMainTaskType;
                _DevelopperId = iDevelopperId;
                _PackageId = iPackageId;

                LoadDataGridViewMainTask(true, null);
                DisplaySelectionMode();
            }
        }

        public void ReLoadControl(long? iMainTaskId = null)
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                InternalReLoadControl(iMainTaskId);
            }
        }

        public void Initialize(Group iGroup)
        {
            if (_IsLoading.Value) return;
            using (new BoolLocker(ref _IsLoading))
            {
                _Group = iGroup;

                //Init
                this.ucNavigator.Initialize(50, 1);
                ucNavigator.LoadRequested += ucNavigator_LoadRequested;

                dgvMain.MultiSelect = false;
                dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMain.ReadOnly = true;
                dgvMain.RowTemplate.Height = DATAGRIDVIEWROWHEIGTH;
                dgvMain.AllowUserToAddRows = false;
                dgvMain.RowHeadersVisible = false;
                dgvMain.AllowUserToResizeRows = false;
                dgvMain.AllowUserToResizeColumns = true;
                dgvMain.AllowUserToOrderColumns = false;

                _MainTaskBindingSource.DataSource = new List<MainTaskView>();
                dgvMain.DataSource = _MainTaskBindingSource;
                dgvMain.FormatColumns<MainTaskView>("FR");
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

        protected MainTask GetSelectedMainTask()
        {
            if (dgvMain.SelectedRows.Count == 1)
                return ((MainTaskView)dgvMain.SelectedRows[0].DataBoundItem).Object;
            return null;
        }

        protected void DisplayEditMode()
        {
            cmdAddMainTask.Enabled = false;
            ucNavigator.Enabled = false;
            dgvMain.Enabled = false;
        }

        protected void DisplaySelectionMode()
        {
            cmdAddMainTask.Enabled = true;
            ucNavigator.Enabled = true;
            dgvMain.Enabled = true;
        }

        protected void CommandEnableManagement()
        {
            var theMainTask = GetSelectedMainTask();

            if (theMainTask == null)
            {
                cmdUpdateMainTask.Enabled = false;
                cmdCancelTask.Enabled = false;
                cmdUpPriority.Enabled = false;
                cmdDownPriority.Enabled = false;
                cmdAcceptRequestMainTask.Enabled = false;
            }
            else
            {
                //UpdateMainTask
                if (theMainTask.Status == MainTaskStatusEnum.Dev
                    || theMainTask.Status == MainTaskStatusEnum.Requested
                    || theMainTask.Status == MainTaskStatusEnum.Waiting)
                    cmdUpdateMainTask.Enabled = true;
                else
                    cmdUpdateMainTask.Enabled = false;

                //cmdCancelTask
                if (theMainTask.Status == MainTaskStatusEnum.Dev
                    || theMainTask.Status == MainTaskStatusEnum.Requested
                    || theMainTask.Status == MainTaskStatusEnum.Waiting)
                    cmdCancelTask.Enabled = true;
                else
                    cmdCancelTask.Enabled = false;

                //cmdUpPriority cmdDownPriority
                if (theMainTask.Status == MainTaskStatusEnum.Dev
                     || theMainTask.Status == MainTaskStatusEnum.Requested
                     || theMainTask.Status == MainTaskStatusEnum.Staging
                     || theMainTask.Status == MainTaskStatusEnum.Waiting)
                {
                    cmdUpPriority.Enabled = true;
                    cmdDownPriority.Enabled = true;
                }
                else
                {
                    cmdUpPriority.Enabled = true;
                    cmdDownPriority.Enabled = true;
                }

                //cmdAcceptRequestMainTask
                if (theMainTask.Status == MainTaskStatusEnum.Requested)
                    cmdAcceptRequestMainTask.Enabled = true;
                else
                    cmdAcceptRequestMainTask.Enabled = false;
            }
        }

        #endregion

        #region Private ENUMS

        private enum LoadingType
        {
            Criteria,
            MainTaskId,
        }

        #endregion

        #region Protected CLASSES

        protected class MainTaskView
        {
            #region Public PROPERTIES

            [Visible]
            [Frozen]
            [Name("FR", "Stt")]
            [WidthColumn(30)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public Image Status { get; set; }

            [Visible]
            [Name("FR", "N° Tâche")]
            [WidthColumn(75)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string MainTaskId { get; set; }

            [Visible]
            [Name("FR", "Priorité")]
            [WidthColumn(50)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public int? Priority { get; set; }

            [Visible]
            [Name("FR", "Objectif")]
            [WidthColumn(70)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Objectif { get; set; }

            [Visible]
            [Name("FR", "N° Projet")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string ProjectNumber { get; set; }

            [Visible]
            [Name("FR", "Nom")]
            [WidthColumn(250)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string Name { get; set; }

            [Visible]
            [Name("FR", "Gammes")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string ProductLine { get; set; }

            [Visible]
            [Name("FR", "Avancement")]
            [WidthColumn(75)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public Image Progression { get; set; }

            [Visible]
            [Name("FR", "Charge ( jrs ) ")]
            [WidthColumn(75)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Duration { get; set; }

            [Visible]
            [Name("FR", "Sous tâches")]
            [WidthColumn(75)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string SubTasks { get; set; }

            [Visible]
            [Name("FR", "Package")]
            [WidthColumn(75)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Package { get; set; }

            public MainTask Object { get; set; }

            #endregion

            #region Public METHODS

            public static MainTaskView ConvertTo(MainTask iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new MainTaskView();
                newView.Object = iObj;

                newView.MainTaskId = iObj.MainTaskIdString;

                if (iObj.Status == MainTaskStatusEnum.Canceled)
                    newView.Status = Properties.Resources.delete_icone;
                else if (iObj.Status == MainTaskStatusEnum.Requested)
                    newView.Status = Properties.Resources.Button_Help_icon24;
                else if (iObj.Status == MainTaskStatusEnum.Completed)
                    newView.Status = Properties.Resources.accept;
                else if (iObj.Status == MainTaskStatusEnum.Dev)
                    newView.Status = Properties.Resources.Gear_icon24;
                else if (iObj.Status == MainTaskStatusEnum.Waiting)
                    newView.Status = Properties.Resources.hourglass_icon24;
                else if (iObj.Status == MainTaskStatusEnum.Staging)
                    newView.Status = Properties.Resources.Science_Test_Tube_icon_24;
                else
                    throw new NotSupportedException(iObj.Status.ToStringWithEnumName());

                newView.Name = iObj.Name;

                //Progression
                int progressionAverage = iObj.SubTasks.IsNotNullAndNotEmpty() ? (int)(Math.Truncate(iObj.SubTasks.Enum().Average(x => x.Progression))) : 0;
                var imageWidth = (int)typeof(MainTaskView).GetWidthColumn(Library.Tools.Misc.PropertyObserver.GetPropertyName<MainTaskView>(x => x.Progression));
                newView.Progression = Library.Control.Datagridview.ImageHelper.GetProgressionBarImage(progressionAverage, DATAGRIDVIEWROWHEIGTH, imageWidth, true);

                newView.Priority = iObj.Priority;
                if (iObj.ObjectifCloseDate != null)
                    newView.Objectif = ((DateTime)iObj.ObjectifCloseDate).ToShortDateString();
                else
                    newView.Objectif = null;

                //Duration
                int durationSum = iObj.DurationSum;
                int doneDuration = iObj.DoneDuration;
                if (durationSum != 0 || doneDuration != 0)
                    newView.Duration = doneDuration + "/" + durationSum;
                else
                    newView.Duration = null;

                //SubTasks
                if (iObj.SubTasks.Any())
                {
                    var completSubTaskCount = iObj.SubTasks.Count(x => x.Progression == 100);
                    newView.SubTasks = completSubTaskCount + "/" + iObj.SubTasks.Count();
                }
                else
                    newView.SubTasks = null;

                //Package
                if (iObj.Package != null)
                    newView.Package = iObj.Package.PackageIdString;

                //ProductLine
                if (iObj.ProductLines.IsNotNullAndNotEmpty())
                    newView.ProductLine = iObj.ProductLines.Select(x => x.Name).Concat("--");

                if (iObj.ExternalProject != null)
                    newView.ProjectNumber = iObj.ExternalProject.ProjectNumber;

                return newView;
            }

            #endregion
        }

        #endregion

        #region Private FIELDS

        private const int DATAGRIDVIEWROWHEIGTH = 28;

        private BoolLock _IsLoading = new BoolLock();

        private Group _Group;

        private LoadingType _LoadingType;
        private MainTaskStatusSearchEnum _MainTaskStatusSearchEnum;
        private MainTaskOrderByEnum _MainTaskOrderBy;
        private Guid? _ProjectId;
        private long? _ProductLineId;
        private MainTaskTypeEnum? _MainTaskType;
        private long _MainTaskId;
        private Guid? _DevelopperId;
        private long? _PackageId;

        private BindingSource _MainTaskBindingSource = new BindingSource();

        #endregion

        #region Private METHODS

        private void InternalReLoadControl(long? iSelectedMainTaskId = null)
        {
            LoadDataGridViewMainTask(false, iSelectedMainTaskId);
            DisplaySelectionMode();
            MainTaskSelectionChange();
        }

        private void LoadDataGridViewMainTask(bool iShowFirstPage, long? iSelectedMainTaskId = null)
        {
            var firstDisplayIndex = dgvMain.FirstDisplayedScrollingRowIndex;

            using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                var skip = (iShowFirstPage) ? 0 : ucNavigator.Skip;

                if (iShowFirstPage)
                    ucNavigator.PageNumber = 1;

                Tuple<List<MainTask>, int> maintaskTuple;

                if (_LoadingType == LoadingType.Criteria)
                {
                    maintaskTuple = releaseService.GetMainTaskList(_MainTaskStatusSearchEnum, _MainTaskOrderBy, _ProjectId, _ProductLineId, _MainTaskType, _DevelopperId, _PackageId, skip, ucNavigator.Take, GranularityEnum.Full);
                }
                else if (_LoadingType == LoadingType.MainTaskId)
                {
                    var mainTask = releaseService.GetMainTaskById(_MainTaskId, GranularityEnum.Full);
                    if (mainTask != null)
                        maintaskTuple = new Tuple<List<MainTask>, int>(new List<MainTask>() { mainTask }, 1);
                    else
                        maintaskTuple = new Tuple<List<MainTask>, int>(null, 0);
                }
                else
                    throw new NotSupportedException(_LoadingType.ToStringWithEnumName());

                ucNavigator.Count = (maintaskTuple != null) ? maintaskTuple.Item2 : 0;
                var mainTaskList = (maintaskTuple != null) ? maintaskTuple.Item1 : null;

                if (mainTaskList.IsNotNullAndNotEmpty())
                {
                    lblMessage.Text = "{0} tâche(s) sur {1}".FormatString(maintaskTuple.Item1.Count(), maintaskTuple.Item2);

                    //commande
                    var list = mainTaskList.Enum().Select(x => MainTaskView.ConvertTo(x)).ToList();
                    _MainTaskBindingSource.DataSource = list;
                    dgvMain.Refresh();
                }
                else
                {
                    lblMessage.Text = "Aucune tâche.";
                    _MainTaskBindingSource.Clear();
                }
            }
            //dgvMain.SetFirstDisplayedScrollingRowIndex(firstDisplayIndex);

            if (dgvMain.RowCount != 0)
            {
                if (iSelectedMainTaskId != null)
                {
                    dgvMain.Refresh();
                    if (!dgvMain.SelectRowByPropertyValue<MainTaskView>(x => x.Object.MainTaskId.ToString(), iSelectedMainTaskId.ToString()))
                        MessageBox.Show("La nouvelle tâche n'a pas pu être sélectionnée car absente. Veuillez changer de page ou modifier les filtres");
                }

                var selectedTransaction = GetSelectedMainTask();
                if (selectedTransaction != null)
                    MainTaskSelectionChanged(selectedTransaction);
                else
                    MainTaskSelectionChanged(null);
            }
            else
            {
                NothingSelected(null, null);
            }
            CommandEnableManagement();
            dgvMain.PerformLayout();
        }

        private void ucNavigator_LoadRequested(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    LoadDataGridViewMainTask(false);
                    dgvMain.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdAddMainTask_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    //Enabled
                    CreateMainTask();
                    DisplayEditMode();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdUpdateMainTask_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedMainTask = GetSelectedMainTask();
                    if (selectedMainTask == null)
                        return;

                    UpdateMainTask(GetSelectedMainTask());
                    DisplayEditMode();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void dgvMain_SelectionChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    MainTaskSelectionChange();
                    CommandEnableManagement();
                    dgvMain.Select();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void MainTaskSelectionChange()
        {
            var selectedTask = GetSelectedMainTask();
            if (selectedTask != null)
                MainTaskSelectionChanged(selectedTask);
            else
                NothingSelected(null, null);
        }

        private void dgvMain_DoubleClick(object sender, System.EventArgs e)
        {
            cmdUpdateMainTask_Click(sender, e);
        }

        private void cmdValidateMainTask_Click(object sender, System.EventArgs e)
        {
            try
            {
                Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedMainTask = GetSelectedMainTask();
                    if (selectedMainTask == null)
                        return;
                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetSQLExtendConnectionString()))
                    {
                        releaseService.AcceptMainTaskRequest(selectedMainTask.MainTaskId);
                        InternalReLoadControl();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdCancelTask_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    var selectedMainTask = GetSelectedMainTask();
                    if (selectedMainTask == null)
                        return;

                    if (MessageBox.Show("Etes-vous sûr de vouloir annuler cette tâche ?", "Annulation tâche", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetSQLExtendConnectionString()))
                    {
                        releaseService.CancelMainTask(selectedMainTask.MainTaskId);
                        InternalReLoadControl();
                    }

                    //Applications des droits sur dev
                    Tools.Tools.ReleaseProjectsRights(_Group);
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdUpPriority_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    var selectedMainTask = GetSelectedMainTask();
                    if (selectedMainTask == null)
                        return;

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetSQLExtendConnectionString()))
                    {
                        if (selectedMainTask.Priority == null)
                            if (MessageBox.Show("Etes-vous sûr de vouloir placer cette tâche en priorité 1 et de décaler tous les autres ?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                return;
                        releaseService.MoveUpMainTaskPriority(selectedMainTask);
                        InternalReLoadControl();
                        dgvMain.Refresh();
                        dgvMain.SelectRowByPropertyValue<MainTaskView>(x => x.MainTaskId.ToString(), selectedMainTask.MainTaskIdString);

                        var reSelectedMainTask = GetSelectedMainTask();
                        if (reSelectedMainTask != null)
                            MainTaskSelectionChanged(reSelectedMainTask);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdDownPriority_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    var selectedMainTask = GetSelectedMainTask();
                    if (selectedMainTask == null)
                        return;

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetSQLExtendConnectionString()))
                    {
                        releaseService.MoveDownMainTaskPriority(selectedMainTask);
                        InternalReLoadControl();
                        dgvMain.Refresh();
                        dgvMain.SelectRowByPropertyValue<MainTaskView>(x => x.MainTaskId.ToString(), selectedMainTask.MainTaskIdString);

                        var reSelectedMainTask = GetSelectedMainTask();
                        if (reSelectedMainTask != null)
                            MainTaskSelectionChanged(reSelectedMainTask);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdImportFromProjectExcelFile_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    var ucImportTaskControl = new ucImportTaskFromPDC(_Group);
                    using (var ImportTaskForm = new frmUserControl(ucImportTaskControl, "Importation Tâche", true, false))
                    {
                        ucImportTaskControl.Close += (s, d) => ImportTaskForm.Close();
                        ImportTaskForm.StartPosition = FormStartPosition.CenterParent;
                        ucImportTaskControl.Initialize();
                        ImportTaskForm.ShowDialog();

                        InternalReLoadControl();
                        dgvMain.Refresh();
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