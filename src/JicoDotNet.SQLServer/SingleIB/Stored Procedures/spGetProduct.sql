CREATE PROCEDURE [SingleIB].[spGetProduct]
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
				SELECT p.*, t.ProductTypeName
				FROM SingleIB.mProduct as p
					INNER JOIN SingleIB.mProductType as t
				ON p.ProductTypeId = t.ProductTypeId
				ORDER BY p.IsActive DESC, p.ProductId DESC;
			END
			IF (@QueryType ='INTIME')
			BEGIN
				SELECT p.*, t.ProductTypeName
				FROM SingleIB.mProduct as p
					INNER JOIN SingleIB.mProductType as t
				ON p.ProductTypeId = t.ProductTypeId					
				WHERE p.IsActive = 1
					AND p.ProductInOut IN (0, 1);
			END
			IF (@QueryType ='OUTTIME')
			BEGIN
				SELECT p.*, t.ProductTypeName
				FROM SingleIB.mProduct as p
					INNER JOIN SingleIB.mProductType as t
				ON p.ProductTypeId = t.ProductTypeId					
				WHERE p.IsActive = 1
					AND p.ProductInOut IN (0, 2);
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