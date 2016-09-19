CREATE TABLE [S_Release].[T_E_ProductLineTask]
(
	[ProductLineTaskId]				BIGINT		NOT NULL	PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[ProductLineId]					BIGINT		NOT NULL	CONSTRAINT[FK.T_E_ProductLineTask.ProductLineId] FOREIGN KEY REFERENCES [S_Product].T_E_ProductLine,
	[MainTaskId]					BIGINT		NOT NULL	CONSTRAINT[FK.T_E_ProductLineTask.MainTaskId] FOREIGN KEY REFERENCES [S_Release].T_E_MainTask,
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX.T_E_ProductLineTask.ProductLineId.MainTaskId.IsUnique]
ON[S_Release].[T_E_ProductLineTask]([ProductLineId], [MainTaskId])
GO
