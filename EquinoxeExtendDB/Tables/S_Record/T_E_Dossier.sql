CREATE TABLE [S_Record].[T_E_Dossier]
(
	[DossierId]						BIGINT			NOT NULL PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[Name]							NVARCHAR(259)	NOT NULL Unique,
	[ProjectName]					NVARCHAR(259)	NOT NULL,
	[IsTemplate]					BIT				NOT NULL,
	[TemplateDescription]			NVARCHAR(1000)	NULL,	
	[TemplateName]					NVARCHAR(259)	NULL,
	[StateRef]						INT				NOT NULL,
	[IsCreateVersionOnGeneration]	BIT				NOT NULL,

	CONSTRAINT[CK.T_E_Dossier.IsTemplate.DisplayName.Coherence] CHECK (([IsTemplate] = 0 AND [TemplateName] IS NULL) OR ([IsTemplate]=1 AND [TemplateName] IS NOT NULL))
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX.T_E_Dossier.DisplayName.IsUnique]
ON [S_Record].[T_E_Dossier](Name)
WHERE Name IS NOT NULL;
