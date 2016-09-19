CREATE TABLE [S_Release].[T_E_SubTask]
(
	[SubTaskId]				BIGINT					NOT NULL	PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[MainTaskId]			BIGINT				NOT NULL	CONSTRAINT[FK.T_E_ProjectTask.MainTaskId] FOREIGN KEY REFERENCES [S_Release].T_E_MainTask,
	[Designation]			NVARCHAR(150)		NULL,
	[ProjectGUID]			UNIQUEIDENTIFIER	NULL,
	[Progression]			INT					NOT NULL	CHECK ([Progression] >=0 AND [Progression] <=100),	
	[Duration]				INT					NULL,
	[Start]					INT					NULL, -- Days for MainTaskStart		
	[DevelopperGUID]		UNIQUEIDENTIFIER    NULL,
	[Comments]				NVARCHAR(MAX)		NULL,	
)
