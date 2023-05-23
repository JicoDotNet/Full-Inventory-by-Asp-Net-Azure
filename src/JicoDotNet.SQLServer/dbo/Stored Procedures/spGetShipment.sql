
CREATE PROCEDURE [dbo].[spGetShipment]
(
	@SalesOrderId	bigint = 0,
	@ShipmentId		bigint = 0,

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
				SELECT sph.*, who.*, spt.ShipmentTypeName,
				CAST(
					(CASE 
						WHEN sph.IsDirect = 1
							THEN 
							(CASE 
								WHEN EXISTS (SELECT * FROM [dbo].[mInvoiceHeader] AS inv WHERE inv.SalesOrderId = sph.SalesOrderId
											AND inv.IsFullInvoiced = 1)
									THEN 1
								WHEN EXISTS (SELECT * FROM [dbo].[mInvoiceHeader] AS inv WHERE inv.SalesOrderId = sph.SalesOrderId
										AND inv.IsFullInvoiced <> 1)
								THEN 0
								ELSE null
							END) 
						ELSE null
					END) 
				 AS BIT) AS InvoicedStatus
				FROM dbo.mShipmentHeader as sph
					INNER JOIN dbo.mWareHouse as who
					ON sph.WareHouseId = who.WareHouseId
					INNER JOIN dbo.mShipmentType as spt
					ON sph.ShipmentTypeId = spt.ShipmentTypeId
				ORDER BY sph.ShipmentDate DESC, sph.ShipmentId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT
					0 AS ShipmentDetailId,
					NULL AS ShipmentId,
					NULL AS ShipmentNumber,
					shpd.SalesOrderDetailId,
					0 as ProductId,
					SUM(shpd.ShippedQuantity) AS ShippedQuantity,
					NULL AS Description,
					CAST(1 AS BIT) AS IsActive,
					NULL AS TransactionDate,
					NULL AS RequestId
				FROM dbo.mShipmentDetail as shpd
				WHERE shpd.ShipmentId IN (
					SELECT shph.ShipmentId
					FROM dbo.mShipmentHeader as shph
					WHERE shph.SalesOrderId = @SalesOrderId
				)
				GROUP BY shpd.SalesOrderDetailId;
			END
			ELSE IF (@QueryType = 'DETAIL')
			BEGIN
				SELECT sph.*, who.*
				FROM dbo.mShipmentHeader as sph
					INNER JOIN dbo.mWareHouse as who
				ON sph.WareHouseId = who.WareHouseId
				WHERE sph.ShipmentId = @ShipmentId;

				SELECT spd.*		
				FROM dbo.mShipmentDetail as spd					
				WHERE spd.ShipmentId = @ShipmentId;
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