CREATE TABLE [S_Record].[T_E_Generation]
(
	[GenerationId]			BIGINT				NOT NULL PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[SpecificationId]		BIGINT				NOT NULL CONSTRAINT[FK.T_E_Generation.SpecificationId] FOREIGN KEY REFERENCES [S_Record].T_E_Specification,
	[StateRef]				INT					NOT NULL,
	[TypeRef]				INT					NOT NULL,
	[ProjectName]			NVARCHAR(259)		NOT NULL,
	[CreatorGUID]			UNIQUEIDENTIFIER    NOT NULL,
	[CreationDate]			DATETIME2			NOT NULL,
	[Comments]				NVARCHAR(2000)		NULL,
)

