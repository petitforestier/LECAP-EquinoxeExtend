CREATE TABLE [S_Record].[T_E_Specification]
(
	[SpecificationName]		NVARCHAR(259)	NOT NULL CONSTRAINT[PK.T_E_Specification.SpecificationName] PRIMARY KEY CLUSTERED,
	[Description]			NVARCHAR(1000)	NULL,
	[SpecificationVersion]	INT				NOT NULL,
	[ProjectVersion]		DECIMAL(10,4)	NOT NULL,
	[Controls]				NVARCHAR(MAX)	NOT NULL,
	[Constants]				NVARCHAR(MAX)   NOT NULL,
	[IsTemplate]			BIT				NOT NULL,
	[DisplayName]			NVARCHAR(259)	NULL,

	CONSTRAINT[CK.T_E_Specificationt.IsTemplate.DisplayName.Coherence] CHECK (([IsTemplate] = 0 AND [DisplayName] IS NULL) OR ([IsTemplate]=1 AND [DisplayName] IS NOT NULL)),
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX.T_E_Specification.DisplayName.IsUnique]
ON [S_Record].[T_E_Specification](DisplayName)
WHERE DisplayName IS NOT NULL;