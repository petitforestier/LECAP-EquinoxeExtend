CREATE TABLE [S_Release].[T_E_Deployement]
(
	[DeployementId]					BIGINT		NOT NULL	PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[PackageId]						BIGINT		NOT NULL	CONSTRAINT[FK.T_E_Deployement.PackageId] FOREIGN KEY REFERENCES [S_Release].T_E_Package,
	[DeployementDate]				DATETIME2	NOT NULL,
	[EnvironmentDestinationRef]		SMALLINT	NOT NULL,  
)
