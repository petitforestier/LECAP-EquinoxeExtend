﻿CREATE TABLE [S_Record].[T_E_Lock]
(
	[LockId]				BIGINT				NOT NULL PRIMARY KEY CLUSTERED IDENTITY (1, 1),
	[DossierId]				BIGINT				NOT NULL Unique CONSTRAINT[FK.T_E_Lock.DossierId] FOREIGN KEY REFERENCES [S_Record].T_E_Dossier,
	[SessionGUID]			NVARCHAR(100)		NOT NULL Unique,
	[LockDate]				DATETIME2			NOT NULL,
	[UserId]				UNIQUEIDENTIFIER	NOT NULL,
)


