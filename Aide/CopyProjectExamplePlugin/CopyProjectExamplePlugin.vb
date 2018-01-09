Imports DriveWorks
Imports DriveWorks.Applications
Imports DriveWorks.Applications.Extensibility
Imports DriveWorks.GroupMaintenance
Imports DriveWorks.Hosting
Imports DriveWorks.Security

<ApplicationPlugin("CopyProjectVBExample", "Copy Project VB Example", "An example of how to copy a project in VB")>
Public Class Plugin
    Implements IApplicationPlugin

    Private mGroupService As IGroupService

    Private WithEvents mCopyProjectCommand As ICommand

    Public Sub Initialize(application As IApplication) Implements IApplicationPlugin.Initialize

        mGroupService = application.ServiceManager.GetService(Of IGroupService)()

        Dim commandManager = application.ServiceManager.GetService(Of ICommandManager)()

        ' This creates a command that will only be displayed when a group has been loaded (due to our plugin relying on using the logged in group)
        mCopyProjectCommand = commandManager.RegisterCommand("CopyProjectCommand", StandardStates.GroupLoadedStateFilter, "Copy Project", New ManagedImageHandle(My.Resources.CopyPlain16))

        Dim commandBarManager = application.ServiceManager.GetService(Of ICommandBarManager)()

        ' Create a section for this command to appear on the command bar. The second parameter determines which view it should show on
        ' use Nothing to show the command button on all views.
        ' Hint: To pick a specific view use the AdministratorViewNames class
        Dim commandGroup = commandBarManager.AddGroup("GroupMaintenance", Nothing)

        commandGroup.AddCommandButton(mCopyProjectCommand.Name, Nothing, CommandBarDisplayHint.Large, CommandUnavailableBehavior.Disable)
    End Sub

    Private Sub mCopyProjectCommand_Invoking(sender As Object, e As CommandInvokeEventArgs) Handles mCopyProjectCommand.Invoking

        ' This example uses the currently logged in group as the one to copy a project from (source)
        Dim sourceGroup = mGroupService.ActiveGroup

        Dim options As New CopyGroupOptions() With
        {
            .TargetFolder = "<Path to target folder that you want to copy the project to>"
        }

        ' Get hold of the project to copy 
        Dim projectToCopy = sourceGroup.Projects.GetProject("<project name>")

        ' Add the projects you want to copy here
        options.Projects.Add(projectToCopy)

        ' Add the projects you want to copy the rule revisions for here
        options.ProjectRuleRevisions.Add(projectToCopy)

        Dim rootSourceFolder = "<Path to the source folder that you want to copy the project from>"

        Dim fileOptions As New FilePickingOptions(rootSourceFolder)
        options.FileOptions = fileOptions

        ' Repeat for each folder/file you don't want to copy
        ' Note: The files / folders must contain their full path And they can't be outside the root folder you specified before
        fileOptions.ExcludeFolders.Add(rootSourceFolder + "\<folder name>")
        fileOptions.ExcludeFiles.Add(rootSourceFolder + "\<file name, e.g. test.xml>")

        ' A GroupManager must be created for every group we need to open
        Dim host As New EngineHost(HostEnvironment.CreateDefaultEnvironment(False))
        Dim groupManager = host.CreateGroupManager()

        ' For the purposes of this example the target group must have the same credentials as the user currently logged into the source group
        Dim targetGroup = Me.OpenTargetGroup(groupManager, "<enter the path to the group to open here>", sourceGroup.CurrentUserCredentials)

        ' This is the main process that will handle copying the project
        ' Note: This example just shows how To copy a project, however if you wanted to copy other things from a group
        ' you can do this by changing the CopyGroupOptions above.
        Using copyProcess = CopyGroupProcess.CreateCopyGroupProcess(sourceGroup, targetGroup, options)

            Try

                If copyProcess.Start() Then

                    ' Successfully copied the project
                Else

                    ' Failed to copy the project
                End If
            Finally

                ' Close the target group so we don't get any lock files (if an individual group)
                groupManager.CloseGroup()
            End Try
        End Using
    End Sub

    Private Function OpenTargetGroup(groupManager As GroupManager, groupPath As String, credentials As IProviderCredentials) As Group

        ' NOTE This Is an example for how to copy to an invidiual group. To copy to a shared group you will need
        ' to change Provider to "RemoteGroupProvider" And instead of setting the Path property, you will need
        ' to set the Server And GroupName properties
        Dim connectionStringBuilder = New GroupConnectionStringBuilder() With
        {
            .Provider = "LocalGroupProvider",
            .Path = groupPath
        }

        ' Open the group we want to copy the project into
        groupManager.OpenGroup(connectionStringBuilder.GetConnectionString(), credentials)

        Return groupManager.Group
    End Function

End Class
