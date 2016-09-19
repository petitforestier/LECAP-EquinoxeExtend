CREATE TABLE [S_Pool].[T_E_Pool]
(
	[PoolId]	NVARCHAR(50)	NOT NULL PRIMARY KEY,
	[Cursor]	BIGINT			NOT NUll CHECK ([Cursor]>0)
)
