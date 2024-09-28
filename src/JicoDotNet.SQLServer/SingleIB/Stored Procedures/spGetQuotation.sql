
CREATE PROCEDURE [SingleIB].[spGetQuotation]
(
	@QuotationId int = 0,

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
				SELECT cmr.CompanyName,
					cmr.IsGSTRegistered, 
					cmr.GSTStateCode,
					cmr.GSTNumber, 
					cmr.PANNumber, 
					qh.*,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mSalesOrderHeader] AS sph 
										WHERE sph.QuotationId = qh.QuotationId AND sph.IsActive = 1)
							   THEN 1
							ELSE 0
						END) AS BIT) AS SOGenerated
				FROM SingleIB.mQuotationHeader AS qh					
					INNER JOIN SingleIB.mCustomer AS cmr
				ON qh.CustomerId = cmr.CustomerId
				WHERE qh.IsActive = 1
				ORDER BY qh.QuotationDate DESC, qh.QuotationId DESC;
			END
			ELSE IF (@QueryType ='DETAIL')
			BEGIN
				-- Table 0
				SELECT 					
					soh.*, 
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mSalesOrderHeader] AS sph 
										WHERE sph.QuotationId = soh.QuotationId AND sph.IsActive = 1)
							   THEN 1
							ELSE null
						END) AS BIT) AS SOGenerated,
					cus.CompanyName, cus.CompanyType, cus.ContactPerson, cus.StateCode,
						cus.GSTNumber, cus.GSTStateCode, cus.IsGSTRegistered, cus.PANNumber
				FROM SingleIB.mQuotationHeader AS soh
				INNER JOIN SingleIB.mCustomer as cus
					ON soh.CustomerId = cus.CustomerId
				WHERE soh.QuotationId = @QuotationId
					AND soh.IsActive = 1; 

				-- Table 1
				SELECT pro.Brand, pro.ProductName, pro.ProductCode, 
					pro.ProductId, 
					pro.ProductImageUrl, 
					pro.ProductInOut,					
					pro.UnitOfMeasureName,
					pro.IsGoods,
					sod.*
				FROM SingleIB.mQuotationDetail as sod
					INNER JOIN SingleIB.mProduct as pro
					ON sod.ProductId = pro.ProductId
				WHERE sod.QuotationId = @QuotationId;
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