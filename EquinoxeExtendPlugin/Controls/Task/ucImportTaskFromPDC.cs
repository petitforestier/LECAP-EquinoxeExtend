using DriveWorks;
using DriveWorks.Helper;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
using Library.Control.Datagridview;
using Library.Control.Extensions;
using Library.Control.UserControls;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using Service.Release.Front;
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
    public partial class ucImportTaskFromPDC : UserControl, IUcUserControl
    {
        #region Public EVENTS

        public event EventHandler Close;

        #endregion

        #region Public CONSTRUCTORS

        public ucImportTaskFromPDC(Group iGroup)
        {
            InitializeComponent();
            _Group = iGroup;
        }

        #endregion

        #region Public METHODS

        public void Initialize()
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                cboExternalProjectSearch = cboExternalProjectSearch.FillByDictionary(new ExternalProjectStatusSearchEnum().ToDictionary("FR"));
                cboExternalProjectSearch.SelectedValue = ExternalProjectStatusSearchEnum.UnProcessed;

                LoadDatagridview();

                dgvProject.MultiSelect = false;
                dgvProject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProject.ReadOnly = true;
                dgvProject.AllowUserToAddRows = false;
                dgvProject.RowHeadersVisible = false;
                dgvProject.AllowUserToResizeRows = false;
                dgvProject.AllowUserToResizeColumns = true;
                dgvProject.AllowUserToOrderColumns = false;
            }
        }

        #endregion

        #region Protected METHODS

        protected ExternalProject GetSelectedExternalProject()
        {
            if (dgvProject.SelectedRows.Count == 1)
                return ((ExternalProjectView)dgvProject.SelectedRows[0].DataBoundItem).Object;
            return null;
        }

        #endregion

        #region Protected CLASSES

        protected class ExternalProjectView
        {
            #region Public PROPERTIES

            [Visible]
            [Name("FR", "N° Projet")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string ProjectNumber { get; set; }

            [Visible]
            [Name("FR", "Statut")]
            [WidthColumn(50)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Status { get; set; }

            [Visible]
            [Name("FR", "Priorité")]
            [WidthColumn(50)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string Priority { get; set; }

            [Visible]
            [Name("FR", "Nom")]
            [WidthColumn(250)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string ProjectName { get; set; }

            [Visible]
            [Name("FR", "Description")]
            [WidthColumn(200)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string Description { get; set; }

            [Visible]
            [Name("FR", "Date cloture objectif")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string DateObjectiveEnd { get; set; }

            [Visible]
            [Name("FR", "Pilote")]
            [WidthColumn(80)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public string Pilote { get; set; }

            [Visible]
            [Name("FR", "Equinoxe")]
            [WidthColumn(60)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleLeft)]
            public bool BEImpacted { get; set; }

            public ExternalProject Object { get; set; }

            #endregion

            #region Public METHODS

            public static ExternalProjectView ConvertTo(ExternalProject iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new ExternalProjectView();
                newView.Object = iObj;

                newView.DateObjectiveEnd = iObj.DateObjectiveEnd.ToShortDateString();
                newView.Description = iObj.Description;
                newView.Pilote = iObj.Pilote;
                newView.Priority = (iObj.Priority != null) ? iObj.Priority.ToString() : null;
                newView.ProjectName = iObj.ProjectName;
                newView.ProjectNumber = iObj.ProjectNumber;
                newView.Status = iObj.Status.GetName("FR");
                newView.BEImpacted = iObj.BEImpacted;

                return newView;
            }

            #endregion
        }

        #endregion

        #region Private FIELDS

        private Group _Group;
        private BoolLock _IsLoading = new BoolLock();

        #endregion

        #region Private METHODS

        private void cmdRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var ucMessageBox = new ucMessageBox("Traitement en cours...");
                    using (var messageBoxForm = new frmUserControl(ucMessageBox, "Traitement", false, false))
                    {
                        messageBoxForm.Show();
                        using (var releaseService = new ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                        {
                            releaseService.AddAndUpdateExternalProjectFromFile();
                            LoadDatagridview();
                        }
                        messageBoxForm.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void LoadDatagridview()
        {
            using (var releaseService = new ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
            {
                var statusSearch = (ExternalProjectStatusSearchEnum)cboExternalProjectSearch.SelectedValue;
                var list = releaseService.GetExternalProjectList(statusSearch).Enum().Select(x => ExternalProjectView.ConvertTo(x)).Enum().ToList();
                dgvProject.DataSource = list;
                dgvProject.FormatColumns<ExternalProjectView>("FR");

                lblMessage.Text = "{0} projet(s)".FormatString(list.Count());
            }
        }

        private void cmdSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    LoadDatagridview();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdHideProjet_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedExternalProject = GetSelectedExternalProject();
                    if (selectedExternalProject != null)
                    {
                        using (var releaseService = new ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                        {
                            releaseService.ProcessedExternalProject(selectedExternalProject);
                            LoadDatagridview();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdAddTask_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    var selectedExternalProject = GetSelectedExternalProject();

                    if (selectedExternalProject != null)
                    {
                        var ucMainTaskEditControl = new ucMainTaskEdit();
                        using (var mainTaskForm = new frmUserControl(ucMainTaskEditControl, "Importation Tâche", false, false))
                        {
                            ucMainTaskEditControl.Close += (s, d) => mainTaskForm.Close();
                            mainTaskForm.StartPosition = FormStartPosition.CenterParent;
                            ucMainTaskEditControl.Initialize(_Group);
                            ucMainTaskEditControl.LoadFromExternalProject(selectedExternalProject);
                            mainTaskForm.ShowDialog();

                            if (ucMainTaskEditControl.DialogResult == DialogResult.OK)
                            {     
                                //traite le projet pour ne plus qu'il soit visible
                                using (var releaseService = new ReleaseService(_Group.GetEnvironment().GetExtendConnectionString()))
                                    releaseService.ProcessedExternalProject(selectedExternalProject);

                                LoadDatagridview();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void FakeProdedure()
        {
            Close(null, null);
        }

        #endregion
    }
}