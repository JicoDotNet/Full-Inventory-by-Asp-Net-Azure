
CREATE PROCEDURE [dbo].[spGetCustomer]
(
	@CustomerId		bigint = 0,

	@QueryType		varchar(16)
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
				SELECT *
				FROM dbo.mCustomer c
					LEFT JOIN dbo.mCustomerType as ct
				ON c.CustomerTypeId = ct.CustomerTypeId
				ORDER BY c.IsActive DESC, c.CustomerId DESC;
			END
			ELSE IF (@QueryType ='SINGLE')
			BEGIN
				SELECT *
				FROM dbo.mCustomer c
					LEFT JOIN dbo.mCustomerType as ct
				ON c.CustomerTypeId = ct.CustomerTypeId
				WHERE c.CustomerId = @CustomerId;
			END
			ELSE IF (@QueryType ='NONRETAILALL')
			BEGIN
				SELECT *
				FROM dbo.mCustomer c
					LEFT JOIN dbo.mCustomerType as ct
				ON c.CustomerTypeId = ct.CustomerTypeId
				WHERE c.IsRetailCustomer = 0					
				ORDER BY c.IsActive DESC, c.CustomerId DESC;
			END
			ELSE IF (@QueryType ='RETAILALL')
			BEGIN
				SELECT *
				FROM dbo.mCustomer c
					LEFT JOIN dbo.mCustomerType as ct
				ON c.CustomerTypeId = ct.CustomerTypeId
				WHERE c.IsRetailCustomer = 1
				ORDER BY c.IsActive DESC, c.CustomerId DESC;
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