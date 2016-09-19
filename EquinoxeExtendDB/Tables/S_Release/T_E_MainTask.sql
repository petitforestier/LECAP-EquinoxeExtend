CREATE TABLE [S_Release].[T_E_MainTask]
(
	[MainTaskId]			BIGINT				NOT NULL PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[Name]					NVARCHAR(300)		NOT NULL,
	[Description]			NVARCHAR(1000)		NULL,
	[Comments]				NVARCHAR(1000)		NULL,
	[StatusRef]				SMALLINT			NOT NULL,
	[TaskTypeRef]			SMALLINT			NOT NULL, 
	[ObjectifCloseDate]		DATETIME2			NULL,	
	[CreationUserGUID]		UNIQUEIDENTIFIER	NOT NULL,
	[RequestUserGUID]		UNIQUEIDENTIFIER	NOT NULL,
	[Priority]				INT					NULL,
	[PackageId]				BIGINT				NULL		CONSTRAINT[FK.T_E_MainTask.PackageId] FOREIGN KEY REFERENCES [S_Release].T_E_Package, 
	[StartDate]				DATETIME2			NULL,
		
	[CreationDate]			DATETIME2			NOT NULL,
	[OpenedDate]			DATETIME2			NULL,
	[CompletedDate]			DATETIME2			NULL,
	[CanceledDate]			DATETIME2			NULL,
	[ExternalProjectId]		BIGINT				NULL		CONSTRAINT[FK.T_E_MainTask.ExternalProjectId] FOREIGN KEY REFERENCES [S_Release].T_E_ExternalProject,
)
Go

CREATE UNIQUE NONCLUSTERED INDEX [IX.T_E_MainTask.Priority.IsUnique]
ON[S_Release].[T_E_MainTask]([Priority])
WHERE [Priority] IS NOT NULL;
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX.T_E_MainTask.ExternalProjectId.IsUnique]
ON[S_Release].[T_E_MainTask]([ExternalProjectId])
WHERE [ExternalProjectId] IS NOT NULL;
GO
