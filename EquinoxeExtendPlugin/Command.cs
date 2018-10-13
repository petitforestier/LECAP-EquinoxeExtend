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
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DriveWorks.Helper.Manager;
using System.Xml.Serialization;
using System.Xml;
using DriveWorks.Helper;

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

                    if (!Consts.Consts.DontShowCheckTaskOnStartup.Value)
                    {
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
                                if (ucCheckTaskOnStartupControl.RunCheckup())
                                    checkTaskOnStartupForm.ShowDialog();

                                if (ucCheckTaskOnStartupControl.DialogResult == ucCheckTaskOnStartup.OpenModeEnum.NotAllowed)
                                    projectService.CloseProject();
                                else if (ucCheckTaskOnStartupControl.DialogResult == ucCheckTaskOnStartup.OpenModeEnum.ReadOnly)
                                    File.SetAttributes(activeProject.ProjectFilePath, FileAttributes.ReadOnly);
                                else if (ucCheckTaskOnStartupControl.DialogResult == ucCheckTaskOnStartup.OpenModeEnum.Writable)
                                    File.SetAttributes(activeProject.ProjectFilePath, File.GetAttributes(activeProject.ProjectFilePath) & ~FileAttributes.ReadOnly);
                            }
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

                    //Cas d'emploi des tables
                    var tableWhereUsedCommand = settingsEnvironment.CommandManager.RegisterCommand("Tables groupe", StateFilter.Empty, "Cas d'emploi Groupe", null);
                    var tableWhereUsedButtonUi = groupTablesEnvironmentUIGroup.AddCommandButton(tableWhereUsedCommand.Name, null, CommandBarDisplayHint.LargeAndText, CommandUnavailableBehavior.Disable);
                    tableWhereUsedCommand.Invoking += WhereUsedTableGroup_Invoking;
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

                //Gestion des release
                var groupService = _Application.ServiceManager.GetService<IGroupService>();
                if (groupService.ActiveGroup.Name == EnvironmentEnum.Developpement.GetName("FR") ||
                    groupService.ActiveGroup.Name == EnvironmentEnum.Sandbox.GetName("FR"))
                {
                    var releaseCommand = settingsEnvironment.CommandManager.RegisterCommand("ReleaseManagement", StateFilter.Empty, "Gestion des releases", null);
                    var releaseButtonUi = groupTablesEnvironmentUIGroup.AddCommandButton(releaseCommand.Name, null, CommandBarDisplayHint.LargeAndText, CommandUnavailableBehavior.Disable);
                    releaseCommand.Invoking += Release_Invoking;
                }

                //Cas d'emploi des tables
                var tableWhereUsedCommand = settingsEnvironment.CommandManager.RegisterCommand("Tables projet", StateFilter.Empty, "Cas d'emploi Projet", null);
                var tableWhereUsedButtonUi = groupTablesEnvironmentUIGroup.AddCommandButton(tableWhereUsedCommand.Name, null, CommandBarDisplayHint.LargeAndText, CommandUnavailableBehavior.Disable);
                tableWhereUsedCommand.Invoking += WhereUsedTableProject_Invoking;

                IView formNavigationView = viewManager.GetViewByName(AdministratorViewNames.FormNavigation, true);
                IViewEnvironment formNavigationEnvironment = formNavigationView.ViewEnvironment;
                var formNavigationEnvironmentUIGroup = formNavigationEnvironment.CommandBarManager.AddGroup("Utilisation tables");

                //Gestion control de version
                var controlVersionCommand = formNavigationEnvironment.CommandManager.RegisterCommand("ControlVersion", StateFilter.Empty, "Gestion control de version", null);
                var controlVersionButtonUi = formNavigationEnvironmentUIGroup.AddCommandButton(controlVersionCommand.Name, null, CommandBarDisplayHint.LargeAndText, CommandUnavailableBehavior.Disable);
                controlVersionCommand.Invoking += ControlVersion_Invoking;

                //Gestion des versions de PDM
                var pdmVersionTableCommand = settingsEnvironment.CommandManager.RegisterCommand("SetPDMVersionTable", StateFilter.Empty, "Définition des versions PDM", null);
                var pdmVersionTableUi = groupTablesEnvironmentUIGroup.AddCommandButton(pdmVersionTableCommand.Name, null, CommandBarDisplayHint.LargeAndText, CommandUnavailableBehavior.Disable);
                pdmVersionTableCommand.Invoking += SetPDMVersionTable_Invoking;
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void WhereUsedTableGroup_Invoking(object sender, CommandInvokeEventArgs e)
        {
            try
            {
                var groupService = _Application.ServiceManager.GetService<IGroupService>();
                var activeGroup = groupService.ActiveGroup;

                if (!Tools.Tools.IsAdminCurrentUser(activeGroup))
                    throw new Exception("Seul l'admin peut executer cette fonction");

                var usedTableUserControl = new ucWhereUsedGroup(_Application, activeGroup);
                using (var usedTableForm = new frmUserControl(usedTableUserControl, "Cas d'emploi des tables", true, false))
                {
                    usedTableUserControl.Close += (s, d) => usedTableForm.Close();
                    usedTableForm.StartPosition = FormStartPosition.CenterParent;
                    usedTableForm.Width = 1000;
                    usedTableForm.Height = 800;

                    usedTableForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void WhereUsedTableProject_Invoking(object sender, CommandInvokeEventArgs e)
        {
            try
            {
                var groupService = _Application.ServiceManager.GetService<IGroupService>();
                var activeGroup = groupService.ActiveGroup;

                var usedTableUserControl = new ucWhereUsedProject(_Application, activeGroup);
                using (var usedTableForm = new frmUserControl(usedTableUserControl, "Cas d'emploi des tables", true, false))
                {
                    usedTableUserControl.Close += (s, d) => usedTableForm.Close();
                    usedTableForm.StartPosition = FormStartPosition.CenterParent;
                    usedTableForm.Width = 1000;
                    usedTableForm.Height = 800;

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
                var OpenedProject = projectService.ActiveProject;

                var ucControlVersionControl = new ucControlVersion(_Application);
                using (var controlVersionForm = new frmUserControl(ucControlVersionControl, "Gestion des versions de controls", true, false))
                {
                    ucControlVersionControl.Close += (s, d) => controlVersionForm.Close();
                    controlVersionForm.StartPosition = FormStartPosition.CenterParent;
                    controlVersionForm.Width = 1200;
                    controlVersionForm.Height = 800;

                    controlVersionForm.ShowDialog();

                    if (ucControlVersionControl.SaveNeeded)
                        MessageBox.Show("Veuillez sauvegarder le projet pour conserver les modifications sur les contrôles");
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
                var releaseForm = new frmUserControl(releaseUserControl, "Gestion des releases", true, false);

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
                }

                releaseForm.Show();
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void SetPDMVersionTable_Invoking(object sender, CommandInvokeEventArgs e)
        {
            try
            {
                var inProgressUserControl = new ucMessageBox("Traitement en cours");
                using (var inProgressForm = new frmUserControl(inProgressUserControl, "MAJ du versionning PDM", false, false))
                {
                    inProgressForm.TopMost = true;
                    inProgressForm.Show();
                    inProgressForm.Refresh();

                    //ProjectTable
                    inProgressUserControl.SetMessage("Démarrage...");
                    inProgressForm.Refresh();

                    var groupService = _Application.ServiceManager.GetService<IGroupService>();
                    var activeGroup = groupService.ActiveGroup;

                    var projectService = _Application.ServiceManager.GetService<IProjectService>();
                    var activeProject = projectService.ActiveProject;

                    //Récupération des settings
                    var groupSettings = DriveWorks.Helper.Manager.SettingsManager.GetGroupSettings(activeGroup);
                    if (groupSettings.EPDMVaultName.IsNullOrEmpty())
                        throw new Exception("Le nom du coffre PDM n'est pas renseigné dans les settings");
                    if (groupSettings.EPDMMasterVersionPrefixe.IsNullOrEmpty())
                        throw new Exception("Le préfixe de table de versionning n'est pas renseigné dans les settings");
                    var tablePrefixeName = groupSettings.EPDMMasterVersionPrefixe;

                    inProgressUserControl.SetMessage("Récupération des components set.");
                    inProgressForm.Refresh();

                    //Récupération des chemins de component Sets du projet complet
                    var dwComponentSetsPathList = activeProject.GetComponentsFilePathList();

                    //Récupération des références
                    var pdmComponentSetsList = new List<List<EPDM.Helper.Object.FileResult>>();

                    inProgressUserControl.SetMessage("Récupération des références PDM");
                    inProgressForm.Refresh();

                    var epdmService = new EPDM.Helper.EPDMAPIService(groupSettings.EPDMVaultName, 0, Library.Tools.Enums.DebugModeEnum.Minimal);
                    foreach (var dwComponentPathItem in dwComponentSetsPathList.Enum())
                    {
                        //Suppression des doublons
                        var dwComponentPathItemWithoutDuplicate = dwComponentPathItem.GroupBy(x => x).Select(x => x.First()).ToList();

                        inProgressUserControl.SetMessage("Récupération du components set '{0}'".FormatString(dwComponentPathItemWithoutDuplicate.First()));
                        inProgressForm.Refresh();

                        var pdmComponentSet = epdmService.GetReferenceListFromFile(dwComponentPathItemWithoutDuplicate.First(), 0);

                        //Suppression des doublons
                        pdmComponentSet = pdmComponentSet.GroupBy(x => x.FileName + x.Version).Select(x => x.First()).ToList();

                        //Vérification que les versions de fichiers identiques sont bien identiques
                        var pdmComponentSetGroup = pdmComponentSet.GroupBy(x => x.Path);
                        var versionDifference = new List<string>();
                        foreach (var groupItem in pdmComponentSetGroup.Enum())
                        {
                            if (groupItem.Exists2(x => x.Version != groupItem.First().Version))
                                versionDifference.Add(groupItem.Select(x => x.ParentReferencePath + "\\" + x.FileName + "=>" + x.Version).Concat(Environment.NewLine));
                        }

                        if (versionDifference.IsNotNullAndNotEmpty())
                            throw new Exception("Des fichiers identiques ont des versions différentes : " + Environment.NewLine + versionDifference.Concat(Environment.NewLine + Environment.NewLine));

                        //Comparaison des chemins de fichier
                        var comparator = new Library.Tools.Comparator.ListComparator<string, EPDM.Helper.Object.FileResult>(dwComponentPathItemWithoutDuplicate, x => x, pdmComponentSet, y => y.Path);

                        if (comparator.RemovedList.IsNotNullAndNotEmpty())
                            throw new Exception("Incohérence sur le component set '{0}' et le modèle 3d".FormatString(dwComponentPathItemWithoutDuplicate.First()));

                        pdmComponentSetsList.Add(pdmComponentSet);
                    }

                    //Création ou écrasement de la table
                    foreach (var componentItem in pdmComponentSetsList.Enum())
                    {
                        inProgressUserControl.SetMessage("MAJ des tables '{0}'".FormatString(componentItem.First().FileName));
                        inProgressForm.Refresh();

                        var fileGroupList = componentItem.GroupBy(x => x.DocumentCode);

                        //création table formaté
                        var epdmVersionList = new List<DriveWorks.Helper.Object.EPDMVersion>();
                        foreach (var itemGroup in fileGroupList.Enum())
                        {
                            //Groupement des fichiers du même nom pour ranger 2d, 3d
                            var newRow = new DriveWorks.Helper.Object.EPDMVersion();
                            if (itemGroup.Count() > 2)
                                throw new Exception("Plus de 2 fichiers sont nommés identiquement {0}".FormatString(itemGroup.First().FileName));

                            newRow.CodeDocument = itemGroup.First().DocumentCode;
                            foreach (var itemFile in itemGroup)
                            {
                                if (itemFile.TypeDocument == "SLDPRT" || itemFile.TypeDocument == "SLDASM")
                                    newRow.Version3D = itemFile.Version;
                                else if (itemFile.TypeDocument == "SLDDRW")
                                    newRow.Version2D = itemFile.Version;
                                else
                                    throw new Exception("Type de fichier non supporté {0}".FormatString(itemFile.TypeDocument));
                            }
                            epdmVersionList.Add(newRow);
                        }
                        DriveWorks.Helper.Manager.EPDMVersionManager.UpdateOrCreateEPDMVersionDataTable(activeProject, tablePrefixeName,componentItem.First().DocumentCode, epdmVersionList);
                    } 
                }

                MessageBox.Show("MAJ table de versionning PDM terminée");

            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        #endregion
    }
}