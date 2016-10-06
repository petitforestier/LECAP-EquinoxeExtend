using DriveWorks.Applications;
using DriveWorks.Applications.Administrator.Extensibility;
using DriveWorks.Applications.Extensibility;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtendPlugin.Controls.ControlVersion;
using EquinoxeExtendPlugin.Controls.ReleaseManagement;
using EquinoxeExtendPlugin.Controls.WhereUsedTable;
using Library.Control.UserControls;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquinoxeExtendPlugin
{
    [ApplicationPlugin("EquinoxeExtendPlugin", "EquinoxeExtendPlugin", "Plugin d'extension pour driveworks")]
    public class Commands : IApplicationPlugin
    {
        #region Public METHODS

        public void Initialize(IApplication iApplication)
        {
            _Application = iApplication;
            _Application.StateChanged += mApplication_StateChanged;
        }

        #endregion

        #region Private FIELDS

        private IApplication _Application;

        #endregion

        #region Private METHODS

        private void mApplication_StateChanged(object sender, System.EventArgs e)
        {
            try
            {
                //Seulement pour driveworks administrator
                if (_Application.Name != "DriveWorks Administrator")
                    return;

                if (_Application.State.Contains(StandardStates.ProjectLoaded))
                {
                    //Chargement des boutons
                    this.RegisterProjectCommandButton();

                    //Affichage des tâches ouvertes sur ce projet
                    var groupService = _Application.ServiceManager.GetService<IGroupService>();
                    var activeGroupe = groupService.ActiveGroup;

                    var projectService = _Application.ServiceManager.GetService<IProjectService>();
                    var activeProject = projectService.ActiveProject;

                    if (groupService.ActiveGroup.Name == EnvironmentEnum.Developpement.GetName("FR"))
                    {
                        var ucCheckTaskOnStartupControl = new ucCheckTaskOnStartup(activeProject);
                        using (var checkTaskOnStartupForm = new frmUserControl(ucCheckTaskOnStartupControl, "Vérification des tâches", false, false))
                        {
                            ucCheckTaskOnStartupControl.Close += (s, d) => checkTaskOnStartupForm.Close();
                            checkTaskOnStartupForm.StartPosition = FormStartPosition.CenterParent;
                            checkTaskOnStartupForm.Width = 500;
                            checkTaskOnStartupForm.Height = 500;
                            ucCheckTaskOnStartupControl.RunCheckup();

                            checkTaskOnStartupForm.ShowDialog();

                            if (ucCheckTaskOnStartupControl.DialogResult != DialogResult.Yes)
                                projectService.CloseProject();
                        }
                    }
                }
                else if (_Application.State.Contains(StandardStates.GroupLoaded))
                {
                    //Chargement des boutons
                    this.RegisterGroupCommandButton();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void RegisterGroupCommandButton()
        {
            try
            {
                IViewManager viewManager = _Application.ServiceManager.GetService<IViewManager>();

                if (viewManager == null)
                    return;

                IView settingsView = viewManager.GetViewByName(AdministratorViewNames.Security, true);
                IViewEnvironment settingsEnvironment = settingsView.ViewEnvironment;
                var groupTablesEnvironmentUIGroup = settingsEnvironment.CommandBarManager.AddGroup("Utilisation tables");

                //Gestion des release
                var groupService = _Application.ServiceManager.GetService<IGroupService>();
                if (groupService.ActiveGroup.Name == EnvironmentEnum.Developpement.GetName("FR") ||
                    groupService.ActiveGroup.Name == EnvironmentEnum.Sandbox.GetName("FR"))
                {
                    var releaseCommand = settingsEnvironment.CommandManager.RegisterCommand("ReleaseManagement", StateFilter.Empty, "Gestion des releases", null);
                    var releaseButtonUi = groupTablesEnvironmentUIGroup.AddCommandButton(releaseCommand.Name, null, CommandBarDisplayHint.LargeAndText, CommandUnavailableBehavior.Disable);
                    releaseCommand.Invoking += Release_Invoking;
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void RegisterProjectCommandButton()
        {
            try
            {
                IViewManager viewManager = _Application.ServiceManager.GetService<IViewManager>();

                if (viewManager == null)
                    return;

                IView settingsView = viewManager.GetViewByName(AdministratorViewNames.Security, true);
                IViewEnvironment settingsEnvironment = settingsView.ViewEnvironment;
                var groupTablesEnvironmentUIGroup = settingsEnvironment.CommandBarManager.AddGroup("Utilisation tables");

                //Cas d'emploi des tables
                var tableWhereUsedCommand = settingsEnvironment.CommandManager.RegisterCommand("Tables", StateFilter.Empty, "Cas d'emploi", null);
                var tableWhereUsedButtonUi = groupTablesEnvironmentUIGroup.AddCommandButton(tableWhereUsedCommand.Name, null, CommandBarDisplayHint.LargeAndText, CommandUnavailableBehavior.Disable);
                tableWhereUsedCommand.Invoking += WhereUsedTable_Invoking;

                IView formNavigationView = viewManager.GetViewByName(AdministratorViewNames.FormNavigation, true);
                IViewEnvironment formNavigationEnvironment = formNavigationView.ViewEnvironment;
                var formNavigationEnvironmentUIGroup = formNavigationEnvironment.CommandBarManager.AddGroup("Utilisation tables");

                //Gestion control de version
                var controlVersionCommand = formNavigationEnvironment.CommandManager.RegisterCommand("ControlVersion", StateFilter.Empty, "Gestion control de version", null);
                var controlVersionButtonUi = formNavigationEnvironmentUIGroup.AddCommandButton(controlVersionCommand.Name, null, CommandBarDisplayHint.LargeAndText, CommandUnavailableBehavior.Disable);
                controlVersionCommand.Invoking += ControlVersion_Invoking;
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void WhereUsedTable_Invoking(object sender, CommandInvokeEventArgs e)
        {
            try
            {
                var usedTableUserControl = new ucWhereUsed(_Application);
                using (var usedTableForm = new frmUserControl(usedTableUserControl, "Cas d'emploi des tables", true, false))
                {
                    usedTableUserControl.Close += (s, d) => usedTableForm.Close();
                    usedTableForm.StartPosition = FormStartPosition.CenterParent;
                    usedTableForm.Width = 500;
                    usedTableForm.Height = 500;

                    usedTableForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void ControlVersion_Invoking(object sender, CommandInvokeEventArgs e)
        {
            try
            {
                //Création des services
                var projectService = _Application.ServiceManager.GetService<IProjectService>();
                var OpenedProjectName = projectService.ActiveProject;

                var ucControlVersionControl = new ucControlVersion(_Application);
                using (var controlVersionForm = new frmUserControl(ucControlVersionControl, "Gestion des versions de controls", true, false))
                {
                    ucControlVersionControl.Close += (s, d) => controlVersionForm.Close();
                    controlVersionForm.StartPosition = FormStartPosition.CenterParent;
                    controlVersionForm.Width = 800;
                    controlVersionForm.Height = 800;

                    controlVersionForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void Release_Invoking(object sender, CommandInvokeEventArgs e)
        {
            try
            {
                var groupService = _Application.ServiceManager.GetService<IGroupService>();
                var activeGroup = groupService.ActiveGroup;

                var releaseUserControl = new ucRelease();
                using (var releaseForm = new frmUserControl(releaseUserControl, "Gestion des releases", true, false))
                {
                    var loadingControl = new ucMessageBox("Chargement en cours...");
                    using (var loadingForm = new frmUserControl(loadingControl, "Gestion des releases", true, false))
                    {
                        loadingForm.Show();
                        loadingForm.Refresh();

                        //Init
                        releaseUserControl.Initialize(_Application, activeGroup);
                        releaseUserControl.Close += (s, d) => releaseForm.Close();
                        releaseForm.Parent = Control.FromHandle(_Application.MainWindowHandle);
                        releaseForm.StartPosition = FormStartPosition.CenterParent;
                        releaseForm.WindowState = FormWindowState.Normal;
                        releaseForm.Width = 1550;
                        releaseForm.Height = 950;

                        loadingForm.Hide();

                        releaseForm.ShowDialog();
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