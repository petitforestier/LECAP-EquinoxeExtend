CREATE TABLE [S_Release].[T_E_ExternalProject]
(
	[ExternalProjectId]		BIGINT				NOT NULL	PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[ProjectNumber]			NVARCHAR(10)		NOT NULL	UNIQUE,
	[ProjectName]			NVARCHAR(100)		NOT NULL,
	[Description]			NVARCHAR(MAX)		NULL,
	[Pilote]				NVARCHAR(100)		NOT NULL,
	[DateObjectiveEnd]		DATETIME2			NULL,
	[Priority]				INT					NULL,
	[StatusRef]				SMALLINT			NOT NULL,
	[TypeRef]				SMALLINT			NOT NULL,
	[IsProcessed]			BIT					NOT NULL,
	[BEImpacted]			BIT 				NOT NULL,
)
