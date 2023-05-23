
CREATE PROCEDURE [dbo].[spSetStockAdjust]
(
	@StockAdjustNumber		nvarchar(16) = NULL,
	@StockAdjustDate		datetime = NULL,
	@Remarks				nvarchar(512) = NULL,
	@WareHouseId			bigint = 0,
	@IsStockIncrease		bit = null, -- Can't set 0, here 0 means Stock Decrease
	@AdjustReasonId			bigint =  NULL,
	@AdjustReason			nvarchar(64) = null,
	        
	@STDetail [dbo].[StockAdjustDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@StockAdjustId		bigint,
		@NextSTNo				bigint;

	SELECT @T1 = 'StockAdjust';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** SKA Number Generate **/
				SELECT @NextSTNo = (COUNT(*) + 1) FROM dbo.tStockAdjustHeader;

				SET @StockAdjustNumber = @StockAdjustNumber + RIGHT('000000' + CAST(@NextSTNo as varchar(16)), 6);

				-- AdjustReason Logic
				IF (@AdjustReasonId IS NULL OR @AdjustReasonId = 0)
				BEGIN
					-- Validation in SP, if AdjustReason is null set a value
					IF (@AdjustReason IS NULL OR @AdjustReason = '')
					BEGIN
						SET @AdjustReason = 'Other reason by user';
					END	

					-- INSERT ROW to sStockAdjustReason
					EXEC [dbo].[spSetStockAdjustReason]
						@AdjustReason = @AdjustReason,
						@RequestId = @RequestId,
						@QueryType = 'INSERT',
						@OutParam = @AdjustReasonId OUTPUT
				END
				ELSE
				BEGIN
					SELECT @AdjustReason = ssar.AdjustReason FROM dbo.sStockAdjustReason AS ssar
					WHERE ssar.AdjustReasonId = @AdjustReasonId;
				END

				/** INSERT INTO tStockAdjustHeader **/
				INSERT INTO dbo.tStockAdjustHeader
					(WareHouseId,
					StockAdjustNumber,
					StockAdjustDate,					
					IsStockIncrease,
					AdjustReasonId,
					AdjustReason,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@WareHouseId, 
					@StockAdjustNumber, 
					@StockAdjustDate, 
					@IsStockIncrease,
					@AdjustReasonId,
					@AdjustReason,
					@Remarks, 
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @StockAdjustId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO tStockAdjustDetail **/
				INSERT INTO dbo.tStockAdjustDetail
					(StockAdjustId,
					StockAdjustNumber,
					ProductId,
					IsStockIncrease,
					AvailableQuantity,
					AdjustQuantity,
					StockDetailId,
					GRNDate,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@StockAdjustId,
					@StockAdjustNumber,
					ProductId,
					@IsStockIncrease,
					AvailableQuantity,
					AdjustQuantity,
					StockDetailId,
					GRNDate,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					1,
					dbo.ISTNow(),
					@RequestId
				FROM @STDetail;

				/** UPDATE STOCK **/
				DECLARE @Counter INT, 
					@MaxId INT,
					@StockId bigint, 				
					@ProductId bigint, 
					@AvailableQuantity decimal(18, 5),
					@AdjustQuantity decimal(18, 5), 
					@Description nvarchar(512),
					@StockDetailId bigint,
					@GRNDate datetime,
					@IsPerishable bit,
					@ExpiryDate datetime,
					@BatchNo nvarchar(256),
					@SetStockParam nvarchar(16);

				SELECT @Counter = min(Id), @MaxId = max(Id) 
				FROM @STDetail;

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
					SELECT @ProductId = ProductId, 
						@AvailableQuantity = AvailableQuantity, 
						@AdjustQuantity = AdjustQuantity,
						@Description = Description,
						@StockDetailId = StockDetailId,
						@GRNDate = GRNDate,
						@IsPerishable = IsPerishable,
						@ExpiryDate = ExpiryDate,
						@BatchNo = BatchNo						
					FROM @STDetail WHERE Id = @Counter;

					-- @OutParam Set depends on Stock Increase or Decrease
					SELECT @SetStockParam = IIF(@IsStockIncrease = 1, 'ADJUSTADD', 'ADJUSTSUB');

					-- Stock
					EXEC dbo.spSetStock						
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Quantity = @AdjustQuantity,
						@Remarks = @Description,
						@GRNOrShipmentId = @StockAdjustId,
						@GRNOrShipmentDate = @StockAdjustDate,
						@RequestId = @RequestId,
						@QueryType = @SetStockParam,
						@OutParam = @StockId OUTPUT;

					-- @OutParam Set depends on Stock Increase or Decrease
					SELECT @SetStockParam = IIF(@IsStockIncrease = 1, 'ADD', 'SUB');
						
					-- Stock Detail
					EXEC [dbo].[spSetStockDetail]
						@StockDetailId = @StockDetailId,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @AdjustQuantity,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Description,
						@GRNDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = @SetStockParam,
						@OutParam = @OutParam OUTPUT;

				   SET @Counter  = @Counter  + 1        
				END
				SET @OutParam = '{ "StockAdjustId" : ' +
							CAST(@StockAdjustId as VARCHAR) +
							', "StockAdjustNumber" : "' +
							CAST(@StockAdjustNumber AS VARCHAR) +
							'" }';
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