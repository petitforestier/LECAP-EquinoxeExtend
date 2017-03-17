using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper;
using DriveWorks.Helper.Manager;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
using Library.Control.Datagridview;
using Library.Control.Extensions;
using Library.Control.UserControls;
using Library.Tools.Attributes;
using Library.Tools.Comparator;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquinoxeExtendPlugin.Controls.Task
{
    public partial class ucPackageManagement : UserControl
    {
        #region Public CONSTRUCTORS

        public ucPackageManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region Public METHODS

        public void Initialize(IApplication iApplication, Group iGroup)
        {
            if (_IsLoading.Value) return;
            using (new BoolLocker(ref _IsLoading))
            {
                _Group = iGroup;
                _Application = iApplication;

                ucPackageEdit.Close += UcPackageEdit_Close;

                //Status
                cboStatus = cboStatus.FillByDictionary(new PackageStatusSearchEnum().ToDictionary("FR"));
                cboStatus.SelectedValue = PackageStatusSearchEnum.NotCompleted;

                //Order
                cboOrderBy = cboOrderBy.FillByDictionary(new PackageOrderByEnum().ToDictionary("FR"));
                cboOrderBy.SelectedValue = PackageOrderByEnum.Priority;

                //Main task
                dgvMainTask.MultiSelect = false;
                dgvMainTask.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMainTask.ReadOnly = true;
                dgvMainTask.RowTemplate.Height = DATAGRIDVIEWROWHEIGTH;
                dgvMainTask.AllowUserToAddRows = false;
                dgvMainTask.RowHeadersVisible = false;
                dgvMainTask.AllowUserToResizeRows = false;
                dgvMainTask.AllowUserToResizeColumns = true;
                dgvMainTask.AllowUserToOrderColumns = false;

                bdsMainTask.DataSource = new List<MainTaskView>();
                dgvMainTask.DataSource = bdsMainTask;
                dgvMainTask.FormatColumns<MainTaskView>("FR");

                //Package
                dgvPackage.MultiSelect = false;
                dgvPackage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPackage.ReadOnly = true;
                dgvPackage.RowTemplate.Height = 35;
                dgvPackage.AllowUserToAddRows = false;
                dgvPackage.RowHeadersVisible = false;
                dgvPackage.AllowUserToResizeRows = false;
                dgvPackage.AllowUserToResizeColumns = true;
                dgvPackage.AllowUserToOrderColumns = false;

                bdsPackage.DataSource = new List<PackageView>();
                dgvPackage.DataSource = bdsPackage;
                dgvPackage.FormatColumns<PackageView>("FR");

                //SubTask
                dgvSubTask.MultiSelect = false;
                dgvSubTask.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvSubTask.ReadOnly = true;
                dgvSubTask.RowTemplate.Height = 35;
                dgvSubTask.AllowUserToAddRows = false;
                dgvSubTask.RowHeadersVisible = false;
                dgvSubTask.AllowUserToResizeRows = false;
                dgvSubTask.AllowUserToResizeColumns = true;
                dgvSubTask.AllowUserToOrderColumns = false;

                bdsSubTask.DataSource = new List<SubTaskView>();
                dgvSubTask.DataSource = bdsSubTask;
                dgvSubTask.FormatColumns<SubTaskView>("FR");

                //Deployement
                dgvDeployement.MultiSelect = false;
                dgvDeployement.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDeployement.ReadOnly = true;
                dgvDeployement.RowTemplate.Height = 35;
                dgvDeployement.AllowUserToAddRows = false;
                dgvDeployement.RowHeadersVisible = false;
                dgvDeployement.AllowUserToResizeRows = false;
                dgvDeployement.AllowUserToResizeColumns = true;
                dgvDeployement.AllowUserToOrderColumns = false;

                bdsDeployement.DataSource = new List<DeployementView>();
                dgvDeployement.DataSource = bdsDeployement;
                dgvDeployement.FormatColumns<DeployementView>("FR");

                DisplaySelectionMode();

                LoadPackageDataGridView(null);
            }
        }

        private void UcPackageEdit_Close(object sender, EventArgs e)
        {
            DisplaySelectionMode();

            if (ucPackageEdit.DialogResult == DialogResult.OK)
                LoadPackageDataGridView(GetSelectedPackage().PackageId);
        }

        #endregion

        #region Protected METHODS

        protected EquinoxeExtend.Shared.Object.Release.Package GetSelectedPackage()
        {
            if (dgvPackage.SelectedRows.Count == 1)
                return ((PackageView)dgvPackage.SelectedRows[0].DataBoundItem).Object;
            return null;
        }

        protected void CommandEnableManagement()
        {
            var thePackage = GetSelectedPackage();

            if (thePackage == null)
            {
                cmdDeletePackage.Enabled = false;
                cmdDeployToDev.Enabled = false;
                cmdDeployToStaging.Enabled = false;
                cmdDeployToProduction.Enabled = false;
                cmdLockUnlock.Enabled = false;
            }
            else
            {
                //DeletePackage
                if ((thePackage.Status == PackageStatusEnum.Waiting || thePackage.Status == PackageStatusEnum.Developpement))
                    cmdDeletePackage.Enabled = true;
                else
                    cmdDeletePackage.Enabled = false;

                //OpenPackage
                if (thePackage.Status == PackageStatusEnum.Waiting || thePackage.Status == PackageStatusEnum.Staging)
                    cmdDeployToDev.Enabled = true;
                else
                    cmdDeployToDev.Enabled = false;

                //DeployToStaging
                if (thePackage.Status == PackageStatusEnum.Developpement)
                    cmdDeployToStaging.Enabled = true;
                else
                    cmdDeployToStaging.Enabled = false;

                //DeployToProduction
                if (thePackage.Status == PackageStatusEnum.Staging)
                    cmdDeployToProduction.Enabled = true;
                else
                    cmdDeployToProduction.Enabled = false;

                //LockUnlock
                if (thePackage.Status == PackageStatusEnum.Waiting || thePackage.Status == PackageStatusEnum.Developpement)
                    cmdLockUnlock.Enabled = true;
                else
                    cmdLockUnlock.Enabled = false;
            }
        }

        protected void DisplayEditMode()
        {
            tlsPackage.Enabled = false;
            dgvPackage.Enabled = false;
            ucPackageEdit.Enabled = true;
        }

        protected void DisplaySelectionMode()
        {
            tlsPackage.Enabled = true;
            dgvPackage.Enabled = true;
            ucPackageEdit.Enabled = false;
        }

        #endregion

        #region Protected CLASSES

        protected class PackageView
        {
            #region Public PROPERTIES

            [Visible]
            [Frozen]
            [Name("FR", "Stt")]
            [WidthColumn(30)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public Image Status { get; set; }

            [Visible]
            [Name("FR", "Ver")]
            [WidthColumn(30)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public Image Lock { get; set; }

            [Visible]
            [Name("FR", "Priorité")]
            [WidthColumn(50)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public int? Priority { get; set; }

            [Visible]
            [Name("FR", "Package")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string PackageId { get; set; }

            [Visible]
            [Name("FR", "Release")]
            [WidthColumn(65)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public int? ReleaseNumber { get; set; }

            [Visible]
            [Name("FR", "Avancement")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public Image Progression { get; set; }

            [Visible]
            [Name("FR", "Objectif")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Objectif { get; set; }

            [Visible]
            [Name("FR", "Charge ( jrs ) ")]
            [WidthColumn(75)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Duration { get; set; }

            public EquinoxeExtend.Shared.Object.Release.Package Object { get; set; }

            #endregion

            #region Public METHODS

            public static PackageView ConvertTo(EquinoxeExtend.Shared.Object.Release.Package iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new PackageView();
                newView.Object = iObj;

                //Lock
                if (iObj.IsLocked)
                    newView.Lock = Properties.Resources._lock;
                else
                    newView.Lock = Properties.Resources.lock_open;

                //Status
                if (iObj.Status == PackageStatusEnum.Waiting)
                    newView.Status = Properties.Resources.hourglass_icon24;
                else if (iObj.Status == PackageStatusEnum.Developpement)
                    newView.Status = Properties.Resources.Gear_icon24;
                else if (iObj.Status == PackageStatusEnum.Staging)
                    newView.Status = Properties.Resources.Science_Test_Tube_icon_24;
                else if (iObj.Status == PackageStatusEnum.Production)
                    newView.Status = Properties.Resources.accept;
                else
                    throw new Exception(iObj.Status.ToStringWithEnumName());

                newView.PackageId = iObj.PackageIdString;
                newView.ReleaseNumber = iObj.ReleaseNumber;

                //Progression
                int progressionAverage = iObj.MainTasks.IsNotNullAndNotEmpty() ? (int)(Math.Truncate(iObj.MainTasks.Average(x => x.SubTasks.Any() ? x.SubTasks.Average(y => y.Progression) : 0))) : 0;

                var imageWidth = (int)typeof(MainTaskView).GetWidthColumn(Library.Tools.Misc.PropertyObserver.GetPropertyName<MainTaskView>(x => x.Progression));
                newView.Progression = Library.Control.Datagridview.ImageHelper.GetProgressionBarImage(progressionAverage, DATAGRIDVIEWROWHEIGTH, imageWidth, true);

                //Priority
                newView.Priority = iObj.Priority;

                //Objectif
                if (iObj.DeployementDateObjectif != null)
                    newView.Objectif = ((DateTime)iObj.DeployementDateObjectif).ToShortDateString();
                else
                    newView.Objectif = null;

                //Duration
                newView.Duration = iObj.DoneDuration.ToString() + "/" + iObj.DurationSum.ToString();

                return newView;
            }

            #endregion
        }

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
            [Name("FR", "Avancement")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public Image Progression { get; set; }

            [Visible]
            [Name("FR", "N° Tâche")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string MainTaskId { get; set; }

            [Visible]
            [Name("FR", "Nom")]
            [WidthColumn(250)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string Name { get; set; }

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

                //Status
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
                    throw new Exception(iObj.Status.ToStringWithEnumName());

                newView.Name = iObj.Name;

                //Progression
                int progressionAverage = iObj.SubTasks.IsNotNullAndNotEmpty() ? (int)(Math.Truncate(iObj.SubTasks.Average(x => x.Progression))) : 0;
                var imageWidth = (int)typeof(MainTaskView).GetWidthColumn(Library.Tools.Misc.PropertyObserver.GetPropertyName<MainTaskView>(x => x.Progression));
                newView.Progression = Library.Control.Datagridview.ImageHelper.GetProgressionBarImage(progressionAverage, DATAGRIDVIEWROWHEIGTH, imageWidth, true);

                return newView;
            }

            #endregion
        }

        protected class SubTaskView
        {
            #region Public PROPERTIES

            [Visible]
            [Name("FR", "Action")]
            [WidthColumn(300)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string Action { get; set; }

            [Visible]
            [Name("FR", "Avancement")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public Image Progression { get; set; }

            public SubTask Object { get; set; }

            #endregion

            #region Public METHODS

            public static SubTaskView ConvertTo(SubTask iObj, Group iGroup)
            {
                if (iObj == null)
                    return null;

                var newView = new SubTaskView();
                newView.Object = iObj;

                //ProjectGUID
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
                newView.Progression = Library.Control.Datagridview.ImageHelper.GetProgressionBarImage(iObj.Progression, DATAGRIDVIEWROWHEIGTH, imageWidth, true);

                return newView;
            }

            #endregion
        }

        protected class DeployementView
        {
            #region Public PROPERTIES

            [Visible]
            [Name("FR", "Date")]
            [WidthColumn(130)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Date { get; set; }

            [Visible]
            [Name("FR", "Vers l'environnement")]
            [WidthColumn(150)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string Environment { get; set; }

            public EquinoxeExtend.Shared.Object.Release.Deployement Object { get; set; }

            #endregion

            #region Public METHODS

            public static DeployementView ConvertTo(EquinoxeExtend.Shared.Object.Release.Deployement iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new DeployementView();

                newView.Date = iObj.DeployementDate.ToStringDMYHMS();
                newView.Environment = iObj.EnvironmentDestination.GetName("FR");

                newView.Object = iObj;

                return newView;
            }

            #endregion
        }

        #endregion

        #region Private FIELDS

        private const int DATAGRIDVIEWROWHEIGTH = 35;
        private BindingSource bdsPackage = new BindingSource();
        private BindingSource bdsMainTask = new BindingSource();
        private BindingSource bdsSubTask = new BindingSource();
        private BindingSource bdsDeployement = new BindingSource();
        private IApplication _Application;
        private BoolLock _IsLoading = new BoolLock();
        private Group _Group;

        #endregion

        #region Private METHODS

        private void cmdPackageSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    LoadPackageDataGridView(null);
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void LoadPackageDataGridView(long? iPackageId)
        {
            using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
            {
                var packages = releaseService.GetPackageList((PackageStatusSearchEnum)cboStatus.SelectedValue, (PackageOrderByEnum)cboOrderBy.SelectedValue);
                bdsPackage.DataSource = packages.Enum().Select(x => PackageView.ConvertTo(x)).Enum().ToList();
            }

            if (iPackageId != null)
            {
                dgvPackage.Refresh();
                dgvPackage.SelectRowByPropertyValue<PackageView>(x => x.Object.PackageId.ToString(), iPackageId.ToString());
            }

            LoadMainTaskProjectTaskDatagridview();
            DisplaySelectionMode();
            CommandEnableManagement();
        }

        private void dgvPackage_SelectionChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    LoadMainTaskProjectTaskDatagridview();
                    ucPackageEdit.Initialize(_Group);

                    dgvPackage.Select();
                    var selectedPackage = GetSelectedPackage();
                    ucPackageEdit.EditPackage(selectedPackage);
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void LoadMainTaskProjectTaskDatagridview()
        {
            var seletedPackage = GetSelectedPackage();
            if (seletedPackage != null)
            {
                bdsMainTask.DataSource = seletedPackage.MainTasks.Enum().Select(x => MainTaskView.ConvertTo(x)).Enum().ToList();
                var projectTaskGroup = seletedPackage.SubTasks.Enum().GroupBy(x => x.ProjectGUID);

                var projectList = new List<SubTask>();
                foreach (var groupItem in projectTaskGroup.Enum())
                {
                    if (groupItem.First().ProjectGUID != null)
                    {
                        var newProjectTask = new SubTask();
                        newProjectTask.Progression = (int)Math.Round(groupItem.Average(x => x.Progression));
                        newProjectTask.ProjectGUID = groupItem.First().ProjectGUID;
                        projectList.Add(newProjectTask);
                    }
                }
                bdsSubTask.DataSource = projectList.Select(x => SubTaskView.ConvertTo(x, _Group)).Enum().ToList();
                bdsDeployement.DataSource = seletedPackage.Deployements.Enum().Select(x => DeployementView.ConvertTo(x)).Enum().ToList();
                dgvDeployement.Focus();
            }
            else
            {
                bdsMainTask.Clear();
                bdsSubTask.Clear();
            }

            CommandEnableManagement();
        }

        private void cmdAddPackage_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        releaseService.AddPackage();
                        LoadPackageDataGridView(null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdDeletePackage_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    //Admin obligatoire
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    var selectedPackage = GetSelectedPackage();
                    if (selectedPackage == null)
                        return;

                    //Confirmation
                    if (MessageBox.Show("Etes-sûr de vouloir supprimer le package {0} ?".FormatString(selectedPackage.PackageIdString), "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        releaseService.DeletePackage(selectedPackage);
                        LoadPackageDataGridView(null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdLockUnlock_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedPackage = GetSelectedPackage();
                    if (selectedPackage == null)
                        return;

                    //Admin obligatoire
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        var thePackage = releaseService.GetPackageById(selectedPackage.PackageId, Library.Tools.Enums.GranularityEnum.Full);
                        if (thePackage.IsLocked != selectedPackage.IsLocked)
                            throw new Exception("L'état de verrouillage du package à changé depuis le chargement. Veuillez recharger les packages");

                        if (thePackage.Status != PackageStatusEnum.Developpement
                            && thePackage.Status != PackageStatusEnum.Waiting)
                            throw new Exception("Le package doit être en 'Developpement' ou en 'Attente' pour pouvoir modifier le verrouillage");

                        thePackage.IsLocked = !thePackage.IsLocked;
                        releaseService.UpdatePackage(thePackage);
                        LoadPackageDataGridView(null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdDeployToDev_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedPackage = GetSelectedPackage();
                    if (selectedPackage == null)
                        return;

                    if (MessageBox.Show("Etes-vous sûr de remettre le package '{0}' en developpement ?".FormatString(selectedPackage.PackageIdString), "Changement d'environnement", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;

                    //Admin obligatoire
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        var thePackage = releaseService.GetPackageById(selectedPackage.PackageId, Library.Tools.Enums.GranularityEnum.Full);

                        if (thePackage.Status == PackageStatusEnum.Developpement)
                            throw new Exception("Le package est déjà en développement");

                        if (thePackage.Status != PackageStatusEnum.Waiting && thePackage.Status != PackageStatusEnum.Staging)
                            throw new Exception("Le status du package ne permet pas le changement d'environnement");

                        releaseService.MovePackageToDev(thePackage);
                    }

                    //Applications des droits sur dev
                    Tools.Tools.ReleaseProjectsRights(_Group);

                    //Rechargement de l'ensemble
                    LoadPackageDataGridView(null);
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdDeployToStaging_Click(object sender, System.EventArgs e)
        {
            var groupService = _Application.ServiceManager.GetService<IGroupService>();
            var activeEnvironment = groupService.ActiveGroup.GetEnvironment();

            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    using (var projectOpenLocker = new BoolLocker(ref Consts.Consts.DontShowCheckTaskOnStartup))
                    {
                        var loadingControl = new ucMessageBox("Démarrage déploiement vers PréProd...");
                        using (var loadingForm = new frmUserControl(loadingControl, "Déploiement PréProd", false, false))
                        {
                            loadingForm.Show();
                            loadingForm.Refresh();

                            var selectedPackage = GetSelectedPackage();
                            if (selectedPackage == null)
                                return;

                            //Admin obligatoire
                            Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                            using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                            {
                                loadingControl.SetMessage("Vérifications en cours...");

                                var packageToDeployInQuality = releaseService.GetPackageById(selectedPackage.PackageId, Library.Tools.Enums.GranularityEnum.Full);
                                var devgroup = groupService.ActiveGroup;
                                DriveWorks.Group qualityGroup = null;

                                //vérification du bon groupe
                                if (devgroup.Name != EnvironmentEnum.Developpement.GetName("FR"))
                                    throw new Exception("Le groupe actuel doit être celui de développement");

                                //Vérification que le package contient des tâches et des sous tâches
                                if (packageToDeployInQuality.MainTasks.IsNullOrEmpty())
                                    throw new Exception("Le package ne contient aucune tâche");

                                if (packageToDeployInQuality.SubTasks.IsNullOrEmpty())
                                    throw new Exception("Le package ne contient aucune sous tâche");

                                if (!packageToDeployInQuality.MainTasks.Any(x => x.Status != MainTaskStatusEnum.Staging))
                                    throw new Exception("Ce package est déjà en 'PréProd");

                                //Vérification du statut de toutes les tâches
                                if (packageToDeployInQuality.MainTasks.Any(x => x.Status != MainTaskStatusEnum.Dev))
                                    throw new Exception("Toutes les tâches doivent être en cours");

                                //Vérification que toutes les sous tâches soient à 100%
                                if (packageToDeployInQuality.SubTasks.Any(x => x.Progression != 100))
                                    throw new Exception("Toutes les sous tâches doivent être à 100% d'avancement");

                                //Confirmation
                                if (MessageBox.Show("Etes-sûr de vouloir déployer le package '{0}' vers PréProd ?".FormatString(selectedPackage.PackageIdString), "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                    return;

                                //Vérification que l'autopilot ou le web ne fonctionne pas sur le group préprod
                                //Mettre message pour demander la fermeture de l'autopilot et de solidworks.
                                MessageBox.Show("Merci de fermer complètement les applications connectées au groupe PréProd (Solidworks, DW Administrator, DW DataManagement, Autopilot) et cliquer sur OK une fois terminé", "Fermeture des connexions", MessageBoxButtons.OK);

                                //Vérification qu'aucun projet à copier dans le groupe dev est ouvert
                                var openedDevProjectlist = devgroup.GetOpenedProjectList();

                                var packageDistinctProjectGUIDList = packageToDeployInQuality.SubTasks.Where(x => x.ProjectGUID != null).GroupBy(x => x.ProjectGUID).Select(x => (Guid?)x.First().ProjectGUID).ToList();
                                var packageDistinctProjectDetailsList = new List<ProjectDetails>();
                                foreach (var item in packageDistinctProjectGUIDList)
                                {
                                    if (item != null)
                                        packageDistinctProjectDetailsList.Add(devgroup.Projects.GetProject((Guid)item));
                                }

                                var projectDevComparator = new ListComparator<ProjectDetails, ProjectDetails>(openedDevProjectlist, x => x.Id, packageDistinctProjectDetailsList, x => x.Id);
                                if (projectDevComparator.CommonList.IsNotNullAndNotEmpty())
                                    throw new Exception("Certains projets du groupe '{0}' sont ouverts. L'analyse n'est donc pas possible.".FormatString(devgroup.Name) + Environment.NewLine + Environment.NewLine + projectDevComparator.CommonPairList.Select(x => x.Key.Name).Concat(Environment.NewLine));

                                //PLUGING
                                if (MessageBox.Show("Voulez-vous effectuer les différentes vérifications (Projet ouvert, cohérence des tables, ...) ?", "Vérifications", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    loadingControl.SetMessage("Vérification des projets ouverts en cours...");

                                    //Vérification que les tables à source commune et entre différent projets sont bien identique
                                    var tupleTables = new List<Tuple<string, ProjectDetails, ImportedDataTable>>();

                                    loadingControl.SetMessage("Vérification des différences entre tables de projets en cours...");

                                    tupleTables = Tools.Tools.GetImportedDataTableFromPackage(_Application, packageToDeployInQuality);

                                    var invalideDataTables = new List<string>();
                                    //Bouclage sur les fichiers
                                    foreach (var excelFileLocationItem in tupleTables.GroupBy(x => x.Item3.FileLocation).Enum())
                                    {
                                        //Bouclage sur les feuilles excels
                                        foreach (var sheetItem in excelFileLocationItem.GroupBy(x => x.Item3.SheetName).Enum())
                                        {
                                            var projectList = sheetItem.ToList();
                                            if (projectList.Count > 1)
                                            {
                                                var differenceList = DriveWorks.Helper.DataTableHelper.GetProjectDataTableDifference(projectList);
                                                if (differenceList.IsNotNullAndNotEmpty())
                                                    invalideDataTables.AddRange(differenceList);
                                            }
                                        }
                                    }

                                    if (invalideDataTables.IsNotNullAndNotEmpty())
                                        throw new Exception("Des tables de projet excel suivantes utilisent le même fichier source et ne sont pas identiques." + Environment.NewLine + invalideDataTables.Concat(Environment.NewLine));

                                    //Vérification qu'aucun projet de destination dans le groupe préprod est ouvert
                                    qualityGroup = groupService.OpenGroup(EnvironmentEnum.Staging);
                                    var openedQualityProjectlist = qualityGroup.GetOpenedProjectList();
                                    var projectQualityComparator = new ListComparator<ProjectDetails, SubTask>(openedQualityProjectlist, x => x.Id, packageToDeployInQuality.SubTasks, x => x.ProjectGUID);
                                    if (projectQualityComparator.CommonList.IsNotNullAndNotEmpty())
                                        throw new Exception("Certains projets du groupe '{0}' sont ouverts. L'analyse n'est donc pas possible.".FormatString(qualityGroup.Name) + Environment.NewLine + Environment.NewLine + projectQualityComparator.CommonPairList.Select(x => x.Key.Name).Concat(Environment.NewLine));
                                }

                                devgroup = groupService.OpenGroup(EnvironmentEnum.Developpement);

                                //Enleve les droits
                                loadingControl.SetMessage("Modification des droits...");
                                devgroup.RemoveProjectPermissionsToTeam(_Group.Security.GetTeams().Single(x => x.DisplayName == EnvironmentEnum.Developpement.GetDevelopperTeam()), packageDistinctProjectGUIDList.Select(x => (Guid)x).ToList());

                                //PLUGING
                                if (MessageBox.Show("Voulez-vous appliquer les  plugins ?", "Pluging", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    loadingControl.SetMessage("Application des plugings...");
                                    //Vérifier qu'il n'y a pas de nouveau dossier de pluging
                                    var stagingPluginDirectory = new DirectoryInfo(EnvironmentEnum.Staging.GetPluginDirectory());
                                    var devPluginDirectory = new DirectoryInfo(EnvironmentEnum.Developpement.GetPluginDirectory());
                                    var directoryComparator = new ListComparator<DirectoryInfo, DirectoryInfo>(devPluginDirectory.GetDirectories().Enum().ToList(), x => x.Name, stagingPluginDirectory.GetDirectories().Enum().ToList(), y => y.Name);

                                    var newPluging = directoryComparator.NewList;
                                    var removePluging = directoryComparator.RemovedList;

                                    //Importation de la totalité des plugins
                                    if (!Library.Tools.IO.MyDirectory.IsDirectoryEmpty(EnvironmentEnum.Staging.GetPluginDirectory()))
                                    {
                                        var directoryName = DateTime.Now.ToStringYMDHMS();
                                        //Archive le dossier actuel de plugin de préprod sans prendre le dossier archives
                                        foreach (var directoryItem in stagingPluginDirectory.GetDirectories().Enum())
                                        {
                                            if (directoryItem.FullName + "\\" != EnvironmentEnum.Staging.GetPluginDirectoryArchive())
                                            {
                                                var archivePackageDirectoryName = EnvironmentEnum.Staging.GetPluginDirectoryArchive() + packageToDeployInQuality.PackageIdString + "_" + directoryName;
                                                Library.Tools.IO.MyDirectory.Cut(directoryItem.FullName, archivePackageDirectoryName + "\\" + directoryItem.Name);
                                            }
                                        }

                                        //Copy de dev vers préprod sans prendre le dossier archives
                                        foreach (var directoryItem in devPluginDirectory.GetDirectories().Enum())
                                        {
                                            if (directoryItem.FullName + "\\" != EnvironmentEnum.Developpement.GetPluginDirectoryArchive())
                                                Library.Tools.IO.MyDirectory.Copy(directoryItem.FullName, EnvironmentEnum.Staging.GetPluginDirectory() + directoryItem.Name);
                                        }
                                    }

                                    //Affichage du message des plugings à installer
                                    if (newPluging.Any())
                                        MessageBox.Show("Attention, des nouveaux plugings ont été ajoutés {0}. Il est nécessaire installer ces plugings sur les machines préprod".FormatString(newPluging.Select(x => x.Name).Concat(",")));

                                    //Affichage du message des plugings à supprimer
                                    if (removePluging.Any())
                                        MessageBox.Show("Attention, des plugings ont été supprimés {0}. Il est nécessaire de désintaller ces plugings sur les machines préprod".FormatString(newPluging.Select(x => x.Name).Concat(",")));
                                }

                                //IMPORTATION DES PROJECTS
                                loadingControl.SetMessage("Importation des projets...");
                                qualityGroup = groupService.OpenGroup(EnvironmentEnum.Staging);
                                foreach (var projectItem in packageDistinctProjectDetailsList)
                                {
                                    var answer = MessageBox.Show("Voulez-vous importer le projet '{0}' ? Cliquer sur annuler pour annuler complètement le déploiement".FormatString(projectItem.Name), "Confirmation importation", MessageBoxButtons.YesNoCancel);
                                    if (answer == DialogResult.Cancel)
                                    {
                                        MessageBox.Show("Annulation du déploiement en l'état");
                                        return;
                                    }
                                    else if (answer == DialogResult.Yes)
                                    {
                                        var qualityProjectDirectory = EnvironmentEnum.Staging.GetProjectDirectory() + projectItem.Name;
                                        var projectService = _Application.ServiceManager.GetService<IProjectService>();
                                        projectService.CloseProject();

                                        //Archivage du dossier de destination si existant
                                        if (qualityGroup.Projects.GetProjects().Any(x => x.Name == projectItem.Name))
                                        {
                                            projectService.OpenProject(projectItem.Name);
                                            var activeProject = projectService.ActiveProject;
                                            var activeProjectId = activeProject.Id;
                                            if (activeProject == null)
                                                throw new Exception("Le projet n'est pas ouvert");
                                            var activeProjectDirectory = activeProject.BaseDirectory;
                                            projectService.CloseProject();
                                            Directory.Move(activeProjectDirectory, EnvironmentEnum.Staging.GetProjectDirectoryArchive() + packageToDeployInQuality.PackageIdString + "_" + projectItem.Name + DateTime.Now.ToStringYMDHMS());

                                            //Suppression du projet qui n'existe plus dans Data management
                                            qualityGroup.Projects.DeleteProjectById(activeProjectId);
                                        }

                                        //Importation du nouveau projet
                                        MessageBox.Show("Importer manuellement le projet '{0}' de Equinoxe_Dev vers Equinoxe_PréProd".FormatString(projectItem.Name), "Opération manuelle", MessageBoxButtons.OK);

                                        //Reconstruction forcée
                                        MessageBox.Show("Forcer la reconstruction de l'assemblage de tête");
                                    }
                                }
                                devgroup = groupService.OpenGroup(EnvironmentEnum.Developpement);

                                releaseService.MovePackageToStaging(selectedPackage);

                                MessageBox.Show("Le Package '{0}' a été déployé avec succès dans l'environnement '{1}'".FormatString(selectedPackage.PackageIdString, EnvironmentEnum.Staging.GetName("FR")));

                                //Applications des droits sur dev
                                Tools.Tools.ReleaseProjectsRights(devgroup);
                            }

                            //Rechargement de l'ensemble
                            LoadPackageDataGridView(null);
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
                groupService.OpenGroup(activeEnvironment);
            }
        }

        private void cmdDeployToProduction_Click(object sender, System.EventArgs e)
        {
            var groupService = _Application.ServiceManager.GetService<IGroupService>();
            var activeEnvironment = groupService.ActiveGroup.GetEnvironment();

            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedPackage = GetSelectedPackage();
                    if (selectedPackage == null)
                        return;

                    //Admin obligatoire
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    //Confirmation
                    if (MessageBox.Show("Etes-sûr de vouloir déployer ce package vers l'environnement de production ?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        var devgroup = groupService.OpenGroup(EnvironmentEnum.Developpement);

                        releaseService.MovePackageToProduction(selectedPackage);

                        MessageBox.Show("Le Package '{0}' a été déployé avec succès dans l'environnement '{1}'".FormatString(selectedPackage.PackageIdString, EnvironmentEnum.Production.GetName("FR")));

                        //Applications des droits sur dev
                        Tools.Tools.ReleaseProjectsRights(devgroup);
                    }

                    //Rechargement de l'ensemble
                    LoadPackageDataGridView(null);
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
            finally
            {
                groupService.OpenGroup(activeEnvironment);
            }
        }

        private void cmdRestoreFromBackup_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedPackage = GetSelectedPackage();
                    if (selectedPackage == null)
                        return;

                    //Admin obligatoire
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    ////Confirmation
                    //if (MessageBox.Show("Confirmation", "Etes-sûr de vouloir déployer ce package vers l'environnement de production ?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    //    return;

                    ////todo droits
                    //using (var releaseService = new Service.Release.Front.ReleaseService(_Project.Group.GetGroupSettings().ExtendDataBaseConnectionString))
                    //{
                    //}
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdUpPackagePriority_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedPackage = GetSelectedPackage();
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    if (selectedPackage == null)
                        return;

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        if (selectedPackage.Priority == null)
                            if (MessageBox.Show("Etes-vous sûr de vouloir placer ce package en priorité 1 et de décaler tous les autres ?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                return;
                        releaseService.MoveUpPackagePriority(selectedPackage);
                    }
                }
                LoadPackageDataGridView(selectedPackage.PackageId);
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdDownPackagePriority_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedPackage = GetSelectedPackage();

                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    Tools.Tools.ThrowExceptionIfCurrentUserIsNotAdmin(_Group);

                    if (selectedPackage == null)
                        return;

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        releaseService.MoveDownPackagePriority(selectedPackage);
                    }
                }
                LoadPackageDataGridView(selectedPackage.PackageId);
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedPackage = GetSelectedPackage();
                    if (selectedPackage == null)
                        return;

                    ucPackageEdit.Initialize(_Group);
                    ucPackageEdit.EditPackage(selectedPackage);
                    DisplayEditMode();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void dgvPackage_DoubleClick(object sender, EventArgs e)
        {
            cmdUpdate_Click(sender, e);
        }

        #endregion
    }
}