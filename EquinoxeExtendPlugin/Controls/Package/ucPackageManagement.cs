using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
using Library.Control.Datagridview;
using Library.Control.Extensions;
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

                cboPackageSearch = cboPackageSearch.FillByDictionary(new PackageStatusSearchEnum().ToDictionary("FR"));
                cboPackageSearch.SelectedValue = PackageStatusSearchEnum.NotCompleted;

                dgvMainTask.MultiSelect = false;
                dgvMainTask.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMainTask.ReadOnly = true;
                dgvMainTask.RowTemplate.Height = DATAGRIDVIEWROWHEIGTH;
                dgvMainTask.AllowUserToAddRows = false;
                dgvMainTask.RowHeadersVisible = false;
                dgvMainTask.AllowUserToResizeRows = false;
                dgvMainTask.AllowUserToResizeColumns = true;
                dgvMainTask.AllowUserToOrderColumns = false;
                dgvMainTask.DataSource = bdsMainTask;

                dgvPackage.MultiSelect = false;
                dgvPackage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPackage.ReadOnly = true;
                dgvPackage.RowTemplate.Height = 35;
                dgvPackage.AllowUserToAddRows = false;
                dgvPackage.RowHeadersVisible = false;
                dgvPackage.AllowUserToResizeRows = false;
                dgvPackage.AllowUserToResizeColumns = true;
                dgvPackage.AllowUserToOrderColumns = false;
                dgvPackage.DataSource = bdsPackage;              

                dgvProjectTask.MultiSelect = false;
                dgvProjectTask.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProjectTask.ReadOnly = true;
                dgvProjectTask.RowTemplate.Height = 35;
                dgvProjectTask.AllowUserToAddRows = false;
                dgvProjectTask.RowHeadersVisible = false;
                dgvProjectTask.AllowUserToResizeRows = false;
                dgvProjectTask.AllowUserToResizeColumns = true;
                dgvProjectTask.AllowUserToOrderColumns = false;
                dgvProjectTask.DataSource = bdsProjectTask;

                dgvDeployement.MultiSelect = false;
                dgvDeployement.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDeployement.ReadOnly = true;
                dgvDeployement.RowTemplate.Height = 35;
                dgvDeployement.AllowUserToAddRows = false;
                dgvDeployement.RowHeadersVisible = false;
                dgvDeployement.AllowUserToResizeRows = false;
                dgvDeployement.AllowUserToResizeColumns = true;
                dgvDeployement.AllowUserToOrderColumns = false;
                dgvDeployement.DataSource = bdsDeployement;

                LoadPackageDataGridView();
            }
        }

        private BindingSource bdsPackage = new BindingSource();
        private BindingSource bdsMainTask = new BindingSource();
        private BindingSource bdsProjectTask = new BindingSource();
        private BindingSource bdsDeployement = new BindingSource();

        #endregion

        #region Protected METHODS

        protected Package GetSelectedPackage()
        {
            if (dgvPackage.SelectedRows.Count == 1)
                return ((PackageView)dgvPackage.SelectedRows[0].DataBoundItem).Object;
            return null;
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

            public Package Object { get; set; }

            #endregion

            #region Public METHODS

            public static PackageView ConvertTo(Package iObj)
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
                else if (iObj.Status == PackageStatusEnum.Test)
                    newView.Status = Properties.Resources.Science_Test_Tube_icon_32;
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
                else if (iObj.Status == MainTaskStatusEnum.InProgress)
                    newView.Status = Properties.Resources.Gear_icon24;
                else if (iObj.Status == MainTaskStatusEnum.Waiting)
                    newView.Status = Properties.Resources.hourglass_icon24;
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

        protected class ProjectTaskView
        {
            //[Visible]
            //[Name("FR", "N° sous tâche")]
            //[WidthColumn(90)]
            //[ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            //public string ProjectTask { get; set; }

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

            public static ProjectTaskView ConvertTo(SubTask iObj, Group iGroup)
            {
                if (iObj == null)
                    return null;

                var newView = new ProjectTaskView();
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
                var imageWidth = (int)typeof(ProjectTaskView).GetWidthColumn(Library.Tools.Misc.PropertyObserver.GetPropertyName<ProjectTaskView>(x => x.Progression));
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
                    LoadPackageDataGridView();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void LoadPackageDataGridView()
        {
            using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
            {
                var packages = releaseService.GetPackageList((PackageStatusSearchEnum)cboPackageSearch.SelectedValue);
                bdsPackage.DataSource = packages.Enum().Select(x => PackageView.ConvertTo(x)).Enum().ToList();
                dgvPackage.FormatColumns<PackageView>("FR");
            }
            LoadMainTaskProjectTaskDatagridview();
        }

        private void dgvPackage_SelectionChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    LoadMainTaskProjectTaskDatagridview();
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
                dgvMainTask.FormatColumns<MainTaskView>("FR");

                var projectTaskGroup = seletedPackage.SubTasks.Enum().GroupBy(x => x.ProjectGUID);

                var projectList = new List<SubTask>();
                foreach (var groupItem in projectTaskGroup.Enum())
                {
                    var newProjectTask = new SubTask();
                    newProjectTask.Progression = (int)Math.Round(groupItem.Average(x => x.Progression));
                    newProjectTask.ProjectGUID = groupItem.First().ProjectGUID;
                    projectList.Add(newProjectTask);
                }
                bdsProjectTask.DataSource = projectList.Select(x => ProjectTaskView.ConvertTo(x, _Group)).Enum().ToList();
                dgvProjectTask.FormatColumns<ProjectTaskView>("FR");
                bdsDeployement.DataSource = seletedPackage.Deployements.Enum().Select(x => DeployementView.ConvertTo(x)).Enum().ToList();
                dgvDeployement.FormatColumns<DeployementView>("FR");
            }
            else
            {
                bdsMainTask.DataSource = null;
                bdsProjectTask.DataSource = null;
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
                    ThrowExceptionIfCurrentUserIsNotAdmin();

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
                        LoadPackageDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
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
                        LoadPackageDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdOpenRelease_Click(object sender, System.EventArgs e)
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
                    ThrowExceptionIfCurrentUserIsNotAdmin();

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        var thePackage = releaseService.GetPackageById(selectedPackage.PackageId, Library.Tools.Enums.GranularityEnum.Full);

                        if (thePackage.Status == PackageStatusEnum.Developpement)
                            throw new Exception("Le package est déjà en développement");

                        if (thePackage.Status != PackageStatusEnum.Waiting)
                            throw new Exception("Le status du package n'est pas 'En attente'");

                        releaseService.OpenPackage(thePackage);
                    }

                    //Rechargement de l'ensemble
                    LoadPackageDataGridView();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdDeployToProduction_Click(object sender, System.EventArgs e)
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
                    ThrowExceptionIfCurrentUserIsNotAdmin();

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

        private void cmdDeployToStaging_Click(object sender, System.EventArgs e)
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
                    ThrowExceptionIfCurrentUserIsNotAdmin();

                    

                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                    {
                        var packageToDeployInQuality = releaseService.GetPackageById(selectedPackage.PackageId, Library.Tools.Enums.GranularityEnum.Full);
                        var groupService = _Application.ServiceManager.GetService<IGroupService>();
                        var devgroup = groupService.ActiveGroup;

                        //vérification du bon groupe
                        if (devgroup.Name != EnvironmentEnum.Developpement.GetName("FR"))
                            throw new Exception("Le groupe actuel doit être celui de développement");

                        //Vérification que le package contient des projets et des sous projet
                        if (packageToDeployInQuality.MainTasks.IsNullOrEmpty())
                            throw new Exception("Le package ne contient aucune tâche");

                        if (packageToDeployInQuality.SubTasks.IsNullOrEmpty())
                            throw new Exception("Le package ne contient aucune sous tâche");

                        //todo remettre
                        //if (!packageToDeployInQuality.MainTasks.Any(x => x.Status != MainTaskStatusEnum.Test))
                        //    throw new Exception("Ce package est déjà en 'PréProd");

                        ////Vérification du statut de toutes les tâches
                        //if (packageToDeployInQuality.MainTasks.Any(x => x.Status != MainTaskStatusEnum.InProgress))
                        //    throw new Exception("Toutes les tâches doivent être en cours");

                        //Vérification que toutes les sous tâches soient à 100%
                        if (packageToDeployInQuality.SubTasks.Any(x => x.Progression != 100))
                            throw new Exception("Toutes les sous tâches doivent être à 100% d'avancement");

                        //Confirmation
                        if (MessageBox.Show("Etes-sûr de vouloir déployer ce package vers PréProd ?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                            return;

                        //Vérification que l'autopilot ou le web ne fonctionne pas sur le group préprod
                        //Mettre message pour demander la fermeture de l'autopilot et de solidworks.
                        MessageBox.Show("Merci de fermer complètement les applications connectées au groupe PréProd (Solidworks, DW Administrator, DW DataManagement, Autopilot) et cliquer sur OK une fois terminé", "Fermeture des connextion", MessageBoxButtons.OK);

                        //Vérification qu'aucun projet à copier dans le groupe dev est ouvert
                        var openedDevProjectlist = devgroup.GetOpenedProjectList();
                        var projectDevComparator = new ListComparator<ProjectDetails, SubTask>(openedDevProjectlist, x => x.Id, packageToDeployInQuality.SubTasks, x => x.ProjectGUID);
                        if (projectDevComparator.CommonList.IsNotNullAndNotEmpty())
                            throw new Exception("Certains projets du groupe '{0}' sont ouverts. L'analyse n'est donc pas possible." + Environment.NewLine + Environment.NewLine + projectDevComparator.CommonPairList.Select(x => x.Key.Name).Concat(Environment.NewLine).FormatString(devgroup.Name));

                        var packageDistinctProjectGUIDList = packageToDeployInQuality.SubTasks.GroupBy(x => x.ProjectGUID).Select(x => (Guid?)x.First().ProjectGUID).ToList();
                        var packageDisctinctProjectDetailsList = new List<ProjectDetails>();
                        foreach (var item in packageDistinctProjectGUIDList)
                            packageDisctinctProjectDetailsList.Add(devgroup.Projects.GetProject((Guid)item));

                        //TODO à vérifier et tester
                        ////Vérification que les tables à source commune et entre différent projets sont bien identique

                        //var tupleTables = new List<Tuple<ProjectDetails, ImportedDataTable>>();

                        ////Bouclage sur les projets
                        //foreach (var projectItem in packageProjectDetailsList.Enum())
                        //{
                        //    var projectService = _Application.ServiceManager.GetService<IProjectService>();
                        //    projectService.OpenProject(projectItem.Name);

                        //    var project = projectService.ActiveProject;
                        //    var importedDataTables = project.GetImportedDataTableList();
                        //    foreach (var tableItem in importedDataTables.Enum())
                        //        tupleTables.Add(new Tuple<ProjectDetails, ImportedDataTable>(projectItem, tableItem));
                        //}

                        //var invalideDataTables = new List<string>();
                        ////Bouclage sur les fichiers
                        //foreach (var excelFileLocationItem in tupleTables.GroupBy(x => x.Item2.FileLocation).Enum())
                        //{
                        //    //Bouclage sur les feuilles excels
                        //    foreach (var sheetItem in excelFileLocationItem.GroupBy(x => x.Item2.SheetName).Enum())
                        //    {
                        //        var projectList = sheetItem.ToList();
                        //        if (projectList.Count > 1)
                        //        {
                        //            var notEqualsTable = sheetItem.ToList().Select(x => x.Item2).ToList().GetProjectDataTableDifferent();
                        //            if (notEqualsTable.IsNotNullAndNotEmpty())
                        //                throw new Exception("Les tables de projet excel suivantes utilisent le même fichier source et ne sont pas identique." + notEqualsTable.Select(x => x.Item1.DisplayName).Concat(Environment.NewLine));
                        //        }
                        //    }
                        //}

                        //Vérification que les zones nommées dans les documents sont identiques entre différents projets de tout le groupe de destination.

                        //Vérification qu'aucun projet de destination dans le groupe préprod est ouvert
                        var qualityGroup = groupService.OpenGroup(EnvironmentEnum.Staging);
                        var openedQualityProjectlist = qualityGroup.GetOpenedProjectList();
                        var projectQualityComparator = new ListComparator<ProjectDetails, SubTask>(openedQualityProjectlist, x => x.Id, packageToDeployInQuality.SubTasks, x => x.ProjectGUID);
                        if (projectQualityComparator.CommonList.IsNotNullAndNotEmpty())
                            throw new Exception("Certains projets du groupe '{0}' sont ouverts. L'analyse n'est donc pas possible." + Environment.NewLine + Environment.NewLine + projectQualityComparator.CommonPairList.Select(x => x.Key.Name).Concat(Environment.NewLine).FormatString(devgroup.Name));

                        devgroup = groupService.OpenGroup(EnvironmentEnum.Developpement);

                        //Todo remettre
                        //Enleve les droits
                        //var openedDistinctProjectGUIDList = releaseService.GetOpenedProjectTask().GroupBy(x => x.ProjectGUID).Select(x => (Guid?)x.First().ProjectGUID).ToList();

                        //var taskComparator = new ListComparator<Guid?, Guid?>(openedDistinctProjectGUIDList, x => x, packageDistinctProjectGUIDList, x => x);

                        //if (taskComparator.NewList.IsNotNullAndNotEmpty())
                        //    throw new Exception("Erreur, la liste devrait être null, Contacter l'administrateur");

                        //if (taskComparator.CommonList.IsNullOrEmpty())
                        //    throw new Exception("Erreur, la liste ne devrait pas être null, contacter l'administrateur");
                        //devgroup.SetExclusitivelyPermissionToTeam(_Group.Security.GetTeams().Single(x => x.DisplayName == EnvironmentEnum.Developpement.GetDevelopperTeam()), taskComparator.CommonList.Select(x => (Guid)x).ToList());

                        //Importation de la totalité des plugins
                        if (!Library.Tools.IO.MyDirectory.IsDirectoryEmpty(EnvironmentEnum.Staging.GetPluginDirectory()))
                        {
                            //Archive le dossier actuel de plugin de préprod sans prendre le dossier archives
                            var stagingPluginDirectory = new DirectoryInfo(EnvironmentEnum.Staging.GetPluginDirectory());
                            foreach (var directoryItem in stagingPluginDirectory.GetDirectories().Enum())
                            {
                                if (directoryItem.FullName != EnvironmentEnum.Staging.GetPluginDirectoryArchive())
                                {
                                    var archivePackageDirectoryName = EnvironmentEnum.Staging.GetPluginDirectoryArchive() + "\\" + packageToDeployInQuality.PackageIdString + "_" + DateTime.Now.ToStringYMDHMS();
                                    Library.Tools.IO.MyDirectory.Cut(directoryItem.FullName, archivePackageDirectoryName + "\\" + directoryItem.Name);
                                }
                            }

                            //Copy de dev vers préprod sans prendre le dossier archives
                            var devPluginDirectory = new DirectoryInfo(EnvironmentEnum.Developpement.GetPluginDirectory());
                            foreach (var directoryItem in devPluginDirectory.GetDirectories().Enum())
                            {
                                if (directoryItem.FullName != EnvironmentEnum.Developpement.GetPluginDirectoryArchive())
                                    Library.Tools.IO.MyDirectory.Copy(directoryItem.FullName, EnvironmentEnum.Staging.GetPluginDirectory() + "\\" + directoryItem.Name);
                            }
                        }

                        //IMPORTATION DES PROJECTS
                        qualityGroup = groupService.OpenGroup(EnvironmentEnum.Staging);
                        foreach (var projectItem in packageDisctinctProjectDetailsList)
                        {
                            var qualityProjectDirectory = EnvironmentEnum.Staging.GetProjectDirectory() + projectItem.Name;
                            var projectService = _Application.ServiceManager.GetService<IProjectService>();
                            projectService.CloseProject();

                            //Archivage du dossier de destination si existant
                            if (qualityGroup.Projects.GetProjects().Any(x => x.Name == projectItem.Name))
                            {
                                projectService.OpenProject(projectItem.Name);
                                var activeProject = projectService.ActiveProject;
                                if (activeProject == null)
                                    throw new Exception("Le projet n'est pas ouvert");
                                var activeProjectDirectory = activeProject.BaseDirectory;
                                projectService.CloseProject();
                                Directory.Move(activeProjectDirectory, EnvironmentEnum.Staging.GetProjectDirectoryArchive() + packageToDeployInQuality.PackageIdString + "_" + projectItem.Name + DateTime.Now.ToStringYMDHMS());

                                //Suppression du projet qui n'existe plus dans Data management
                                qualityGroup.Projects.DeleteProjectById(activeProject.Id);
                            }

                            //Importation du nouveau projet
                            MessageBox.Show("Importer manuellement le projet '{0}' de Equinoxe_Dev vers Equinoxe_PréProd".FormatString(projectItem.Name), "Opération manuelle", MessageBoxButtons.OK);

                            //Ajout du droit de run du nouveau projet
                            //todo

                            //Reconstruction forcée
                            MessageBox.Show("Forcer la reconstruction de l'assemblage de tête");
                        }
                        devgroup = groupService.OpenGroup(EnvironmentEnum.Developpement);

                        //Modification package
                        releaseService.UpdatePackage(selectedPackage);

                        //Passage en test des maintask
                        //todo
                        //foreach (var mainTaskItem in packageToDeployInQuality.MainTasks)
                        //    releaseService.UpdateMainTaskStatus(mainTaskItem, MainTaskStatusEnum.Test);

                        //Création de la trace de déploiement
                        var newDeployement = new EquinoxeExtend.Shared.Object.Release.Deployement();
                        newDeployement.DeployementDate = DateTime.Now;
                        newDeployement.DeployementId = -1;
                        newDeployement.EnvironmentDestination = EnvironmentEnum.Staging;
                        newDeployement.PackageId = selectedPackage.PackageId;
                        releaseService.AddDeployement(newDeployement);

                        MessageBox.Show("Le Package '{0}' a été déployé avec succès avec l'environnement '{1}'".FormatString(selectedPackage.PackageIdString, EnvironmentEnum.Staging.GetName("FR")));

                        //Rechargement de l'ensemble
                        LoadPackageDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
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
                    ThrowExceptionIfCurrentUserIsNotAdmin();

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

        private void ThrowExceptionIfCurrentUserIsNotAdmin()
        {
            if (_Group.CurrentUser.LoginName != _Group.GetEnvironment().GetLoginPassword().Item1)
                throw new Exception("Seul le login Admin peut effectuer cette action");
        }

        #endregion


    }
}