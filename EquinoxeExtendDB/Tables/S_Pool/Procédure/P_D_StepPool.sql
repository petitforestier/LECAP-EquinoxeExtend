CREATE PROCEDURE [S_Pool].[P_D_StepPool]
	@PoolName nvarchar(50)
AS
	UPDATE [S_Pool].[T_E_Pool] SET [Cursor] = [Cursor] + 1 OUTPUT inserted.[Cursor] WHERE[PoolId] = @PoolName
RETURN 0
