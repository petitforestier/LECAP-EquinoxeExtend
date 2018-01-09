CREATE TABLE [S_Record].[T_E_Lock]
(
	[LockId]				BIGINT				NOT NULL PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[DossierId]				BIGINT				NOT NULL Unique,
	[SessionGUID]			NVARCHAR(30)		NOT NULL Unique,
	[LockDate]				DATETIME2			NOT NULL,
	[UserId]				UniqueIdentifier	NOT NULL,
)


