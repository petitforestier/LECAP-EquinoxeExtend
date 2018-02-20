CREATE TABLE [S_Record].[T_E_Specification]
(
	[SpecificationId]		BIGINT				NOT NULL PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[Name]					NVARCHAR(259)		NOT NULL UNIQUE,
	[DossierId]				BIGINT				NOT NULL CONSTRAINT[FK.T_E_Dossier.DossierId] FOREIGN KEY REFERENCES [S_Record].T_E_Dossier,
	[ProjectVersion]		DECIMAL(10,4)		NOT NULL,
	[Controls]				NVARCHAR(MAX)		NOT NULL,
	[Constants]				NVARCHAR(MAX)		NOT NULL,
	[CreationDate]			DATETIME2			NOT NULL,
	[Comments]				NVARCHAR(MAX)		NULL,
	[CreatorGUID]			UNIQUEIDENTIFIER    NOT NULL,
)

