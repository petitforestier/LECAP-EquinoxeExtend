CREATE PROCEDURE [S_Pool].[P_D_CreatePoolIfNotExists]
	@PoolName nvarchar(50) 
AS
	BEGIN IF NOT EXISTS (SELECT * FROM S_Pool.T_E_Pool WHERE [PoolId] = @PoolName) BEGIN INSERT INTO S_Pool.T_E_Pool([PoolId], [Cursor]) VALUES(@PoolName,1) END END
RETURN 0
