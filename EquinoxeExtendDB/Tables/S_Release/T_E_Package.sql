CREATE TABLE [S_Release].[T_E_Package]
(
	[PackageId]					BIGINT			NOT NULL PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[ReleaseNumber]				INT				NULL,  --Comprendre comme version du groupe complet (peut importe les versions de configurateur commercial)
	[StatusRef]					SMALLINT		NOT NULL,
	[IsLocked]					BIT				NOT NULL,
	[Priority]					INT				NULL,
	[DeployementObjectifDate]	DATETIME2		NULL,
)
GO
 