using DriveWorks;
using DriveWorks.Helper;
using EquinoxeExtend.Shared.Object.Release;
using Library.Control.Datagridview;
using Library.Control.UserControls;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquinoxeExtend.Shared.Enum;

namespace EquinoxeExtendPlugin.Controls.ReleaseManagement
{
    public partial class ucCheckTaskOnStartup : UserControl, IUcUserControl
    {
        #region Public EVENTS

        public event EventHandler Close;
        public DialogResult DialogResult { get; private set; }
        private Project _Project;
        #endregion

        #region Public CONSTRUCTORS

        public ucCheckTaskOnStartup(Project iProject)
        {           
            InitializeComponent();

            _Project = iProject;

            dgvMain.MultiSelect = false;
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.ReadOnly = true;
            dgvMain.AllowUserToAddRows = false;
            dgvMain.RowHeadersVisible = false;
            dgvMain.AllowUserToResizeRows = false;
            dgvMain.AllowUserToOrderColumns = false;
        }

        #endregion

        #region Public METHODS

        public void RunCheckup()
        {
            using (var releaseService = new Service.Release.Front.ReleaseService(_Project.Group.GetEnvironment().GetExtendConnectionString()))
            {
                var openedTask = releaseService.GetOpenedMainTask();
                var concerneTaskList = openedTask.Enum().Where(x => x.SubTasks.Enum().Any(y => y.ProjectGUID == _Project.Id)).Enum().ToList();

                if (concerneTaskList.Any())
                {
                    dgvMain.DataSource = concerneTaskList;
                    dgvMain.FormatColumns<MainTaskView>("FR");
                }
                else
                {
                    MessageBox.Show("Ce projet n'est dans aucune tâche. Une sous-tâche est nécessaire. L'ouverture du projet est annulé");
                    DialogResult = DialogResult.No;
                    Close(null, null);
                }
            }
        }

        #endregion

        #region Protected CLASSES

        protected class MainTaskView
        {
            #region Public PROPERTIES

            [Visible]
            [Name("FR", "Tâches")]
            [WidthColumn(0)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string TaskName { get; set; }

            public MainTask Object { get; set; }

            #endregion

            #region Public METHODS

            public static MainTaskView ConvertTo(MainTask iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new MainTaskView();
                newView.Object = iObj;
                newView.TaskName = iObj.Name;

                return newView;
            }

            #endregion
        }

        #endregion

        #region Private METHODS

        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close(null, null);
        }

        #endregion

        private void cmdNo_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close(null, null);
        }
    }
}