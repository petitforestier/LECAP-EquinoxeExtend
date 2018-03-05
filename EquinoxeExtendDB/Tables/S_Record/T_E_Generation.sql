CREATE TABLE [S_Record].[T_E_Generation]
(
	[GenerationId]			BIGINT				NOT NULL PRIMARY KEY CLUSTERED,
	[SpecificationId]		BIGINT				NOT NULL CONSTRAINT[FK.T_E_Generation.SpecificationId] FOREIGN KEY REFERENCES [S_Record].T_E_Specification,
	[StateRef]				INT					NOT NULL,
	[TypeRef]				INT					NOT NULL,
	[ProjectName]			NVARCHAR(259)		NOT NULL,
	[CreatorGUID]			UNIQUEIDENTIFIER    NOT NULL,
	[CreationDate]			DATETIME2			NOT NULL,
	[History]				NVARCHAR(2000)		NULL,
	[Comments]				NVARCHAR(2000)		NULL,
)

