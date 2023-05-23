
CREATE PROCEDURE [dbo].[spSetRetailSales]
(
	-- Customer
	@ContactPerson		nvarchar(64) = NULL,
	@Email				nvarchar(64) = NULL,
	@Mobile				nvarchar(16) = NULL,
	@CompanyName		nvarchar(1204) = NULL,
	@CompanyType		nvarchar(64) = NULL,
	@StateCode			nvarchar(2) = null,
	@IsGSTRegistered	bit = 0,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTNumber			nvarchar(16) = NULL,
	@PANNumber			nvarchar(16) = NULL,

	-- SO
	@CustomerId				bigint = 0,
	@SalesOrderDate			datetime = NULL,
	@SalesOrderAmount			decimal(18,5) = 0.0,
	@SalesOrderTaxAmount		decimal(18,5) = 0.0,
	@SalesOrderTotalAmount		decimal(18,5) = 0.0,
	@DeliveryDate				datetime = NULL,		-- extra
	@Remarks					nvarchar(512) = NULL,
	@SalesOrderDetails			[dbo].[SalesOrderDetailType] READONLY,

	-- Ship
	@WareHouseId			bigint = 0,
	@ShipmentNumber			nvarchar(16) = NULL,
	@ShipmentDate			datetime = NULL,
	@IsDirect				bit = 0,
    @IsFullShipped			bit = 0,

    @VehicleNumber			nvarchar(16) = NULL,
    @HandOverPerson			nvarchar(64) = NULL,
    @HandOverPersonMobile	nvarchar(16) = NULL,   
    
	@ShipmentDetails		[dbo].[ShipmentDetailType] READONLY, 

	@IsInvoiceGenerated		bit = 0,
	@IsReceived				bit = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@ShipmentId		bigint,
		@NextShipmentNo bigint;

	SELECT @T1 = 'ShipmentInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				DECLARE	@SalesOrderId nvarchar(16);

				/* Customer Entry */
				EXEC [dbo].[spSetCustomer]
					@CustomerTypeId = NULL,
					@ContactPerson = @ContactPerson,
					@Email = @Email,
					@Mobile = @Mobile,
					@IsRetailCustomer = 1,
					@CompanyName = @CompanyName,
					@CompanyType = @CompanyType,
					@StateCode = @StateCode,
					@IsGSTRegistered = @IsGSTRegistered,
					@GSTStateCode = @GSTStateCode,
					@GSTNumber = @GSTNumber,
					@PANNumber = @PANNumber,					
					@RequestId = @RequestId,
					@QueryType = 'RETAILINSERT',
					@OutParam = @CustomerId OUTPUT

				/* Shipment Direct Entry */
				EXEC [dbo].[spSetShipmentDirect]
					@SalesTypeId = NULL,
					@CustomerId = @CustomerId,
					@SalesOrderDate = @SalesOrderDate,
					@SalesOrderAmount = @SalesOrderAmount,
					@SalesOrderTaxAmount = @SalesOrderTaxAmount,
					@SalesOrderTotalAmount= @SalesOrderTotalAmount,
					@DeliveryDate = @DeliveryDate,
					@Remarks = @Remarks,
					@SalesOrderDetails = @SalesOrderDetails,
					@WareHouseId = @WareHouseId,
					@ShipmentTypeId = NULL,
					@ShipmentNumber = @ShipmentNumber,
					@ShipmentDate = @ShipmentDate,
					@IsDirect = @IsDirect,
					@IsFullShipped = @IsFullShipped,					
					@VehicleNumber = @VehicleNumber,
					@HandOverPerson	= @HandOverPerson,
					@HandOverPersonMobile = @HandOverPersonMobile,
					@ShipmentDetails = @ShipmentDetails,					
					@IsInvoiceGenerated	= @IsInvoiceGenerated,
					@IsReceived	= @IsReceived,					
					@RequestId = @RequestId,					
					@QueryType = 'INSERT',	
					@OutParam = @OutParam OUTPUT,
					@Id = @SalesOrderId OUTPUT

				SET @OutParam = @SalesOrderId;
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