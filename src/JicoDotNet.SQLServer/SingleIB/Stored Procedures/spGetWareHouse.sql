CREATE PROCEDURE [SingleIB].[spGetWareHouse]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT who.*, bra.*
				FROM SingleIB.mWareHouse as who
					INNER JOIN SingleIB.mBranch as bra
					ON who.BranchId = bra.BranchId
				ORDER BY who.IsActive DESC, who.WareHouseId DESC;
			END
		COMMIT TRANSACTION @T1;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END