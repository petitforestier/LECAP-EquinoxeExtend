﻿using DriveWorks;
using DriveWorks.Helper;
using EquinoxeExtend.Shared.Enum;
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
using DriveWorks.Helper.Manager;

namespace EquinoxeExtendPlugin.Controls.ReleaseManagement
{
    public partial class ucCheckTaskOnStartup : UserControl, IUcUserControl
    {
        public enum OpenModeEnum
        {
            ReadOnly,
            NotAllowed,
            Writable,         
        }

        #region Public EVENTS

        public event EventHandler Close;

        #endregion

        #region Public PROPERTIES

        public OpenModeEnum DialogResult { get; private set; }

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
            _MainBindingSource.DataSource = new List<MainTaskView>();
            dgvMain.DataSource = _MainBindingSource;
            dgvMain.FormatColumns<MainTaskView>("FR");
        }

        #endregion

        #region Public METHODS

        public bool RunCheckup()
        {
            DialogResult = OpenModeEnum.NotAllowed;

            using (var releaseService = new Service.Release.Front.ReleaseService(_Project.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                var openedTask = releaseService.GetDevMainTasks(Library.Tools.Enums.GranularityEnum.Full);
                var concerneTaskList = openedTask.Enum().Where(x => x.SubTasks.Enum().Any(y => y.ProjectGUID == _Project.Id)).Enum().ToList();

                if (concerneTaskList.Any())
                {
                    _MainBindingSource.DataSource = concerneTaskList.Enum().Select(x => MainTaskView.ConvertTo(x)).Enum().ToList();
                    return true;
                }
                else
                {
                    var answer = MessageBox.Show("Ce projet n'est dans aucune tâche. Une sous-tâche est nécessaire. L'ouverture en modification n'est pas possible. Voulez-vous ouvrir un lecture seule le projet ?","Information",MessageBoxButtons.YesNo);

                    if (answer == System.Windows.Forms.DialogResult.Yes)
                    {
                        DialogResult = OpenModeEnum.ReadOnly;
                        return false;
                    } 
                    else
                    {
                        DialogResult = OpenModeEnum.NotAllowed;
                        return false;
                    }
                }
            }
        }

        #endregion

        #region Protected CLASSES

        protected class MainTaskView
        {
            #region Public PROPERTIES

            [Visible]
            [Name("FR", "N° Tâche")]
            [WidthColumn(120)]
            [ContentAlignment(DataGridViewContentAlignment.MiddleCenter)]
            public string MainTaskId { get; set; }

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
                newView.MainTaskId = iObj.MainTaskIdString;

                return newView;
            }

            #endregion
        }

        #endregion

        #region Private FIELDS

        private Project _Project;
        private BindingSource _MainBindingSource = new BindingSource();

        #endregion

        #region Private METHODS

        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            DialogResult = OpenModeEnum.Writable;
            Close(null, null);
        }

        private void cmdNo_Click(object sender, System.EventArgs e)
        {
            DialogResult = OpenModeEnum.NotAllowed;
            Close(null, null);
        }

        #endregion
    }
}