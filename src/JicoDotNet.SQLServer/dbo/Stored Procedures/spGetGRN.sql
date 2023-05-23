CREATE PROCEDURE [dbo].[spGetGRN]
(
	@PurchaseOrderId	bigint = 0,
	@GRNId				bigint = 0,

	@QueryType	varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='LIST')
			BEGIN
				SELECT grh.*, who.*,
				CAST(
					(CASE 
						WHEN grh.IsDirect = 1
							THEN 
							(CASE 
								WHEN EXISTS (SELECT * FROM [dbo].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = grh.PurchaseOrderId
											AND bil.IsFullBilled = 1)
									THEN 1
								WHEN EXISTS (SELECT * FROM [dbo].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = grh.PurchaseOrderId
										AND bil.IsFullBilled <> 1)
								THEN 0
								ELSE null
							END) 
						ELSE null
					END) 
				 AS BIT) AS BilledStatus
				FROM dbo.mGRNHeader as grh
					INNER JOIN dbo.mWareHouse as who
					ON grh.WareHouseId = who.WareHouseId
				ORDER BY grh.GRNDate DESC, grh.GRNId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT
					0 AS GRNDetailId,
					0 AS GRNId,
					NULL AS GRNNumber,
					grnd.PurchaseOrderDetailId,
					0 AS ProductId,	
					SUM(grnd.ReceivedQuantity) AS ReceivedQuantity,
					NULL AS Description,
					CAST(1 AS BIT) AS IsActive,
					NULL AS TransactionDate,
					NULL AS RequestId
				FROM dbo.mGRNDetail as grnd
				WHERE grnd.GRNId IN 
						(SELECT grnh.GRNId
						FROM dbo.mGRNHeader as grnh
						WHERE grnh.PurchaseOrderId = @PurchaseOrderId)
				GROUP BY grnd.PurchaseOrderDetailId;
			END
			ELSE IF (@QueryType ='ENTRY')
			BEGIN
				SELECT poh.*
				FROM dbo.mPurchaseOrderHeader poh
				WHERE poh.IsActive = 1
					AND poh.PurchaseOrderId NOT IN (SELECT PurchaseOrderId
						FROM dbo.mGRNHeader
						WHERE IsFullReceived = 1);
			END
			ELSE IF (@QueryType = 'ENTRYSINGLE')
			BEGIN
				IF NOT EXISTS (
					SELECT PurchaseOrderId
						FROM dbo.mGRNHeader
						WHERE IsFullReceived = 1 
							AND PurchaseOrderId = @PurchaseOrderId)
				BEGIN
					SELECT @PurchaseOrderId as PurchaseOrderId
				END
			END
			ELSE IF (@QueryType = 'DETAIL')
			BEGIN
				SELECT grh.*, who.*
				FROM dbo.mGRNHeader as grh
					INNER JOIN dbo.mWareHouse as who
				ON grh.WareHouseId = who.WareHouseId
				WHERE grh.GRNId = @GRNId;

				SELECT grd.*		
				FROM dbo.mGRNDetail as grd					
				WHERE grd.GRNId = @GRNId;
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