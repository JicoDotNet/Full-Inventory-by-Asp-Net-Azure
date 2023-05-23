CREATE PROCEDURE [dbo].[spSetQuotation]
(	
	@QuotationId		bigint = 0,

	@ComapnyIsGSTRegistered		bit = 0,

	@CustomerId			bigint = 0,	

	@QuotationDate			datetime = NULL,
	@QuotationNumber		nvarchar(16) = NULL,

	@QuotationAmount		decimal(18,5) = 0.0,
	@QuotationTaxAmount		decimal(18,5) = 0.0,
	@QuotationTotalAmount	decimal(18,5) = 0.0,


	@TandC		text = NULL,
	@Remarks	nvarchar(512) = NULL,

	@QuotationDetails		[dbo].[QuotationDetailType] READONLY,
	
	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(512) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Quotation';
	DECLARE @IsGstAllowed bit;
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ENTRY')
			BEGIN
				DECLARE @NextQoNo bigint;
				SELECT @NextQoNo = (COUNT(*) + 1) FROM dbo.mQuotationHeader; 

				SET @QuotationNumber = @QuotationNumber + RIGHT('000000' + CAST(@NextQoNo as varchar(16)), 6);
				
				/* Get GST Eligibility*/
				SET @IsGstAllowed = @ComapnyIsGSTRegistered;

				INSERT INTO dbo.mQuotationHeader
					(CustomerId,
					IsGstAllowed,
					QuotationDate,
					QuotationNumber,
					QuotationAmount,
					QuotationTaxAmount,
					QuotationTotalAmount,
					TandC,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@CustomerId, 
					@IsGstAllowed,
					@QuotationDate, 
					@QuotationNumber, 
					@QuotationAmount, 
					@QuotationTaxAmount, 
					@QuotationTotalAmount, 
					@TandC, 
					@Remarks, 
					1, 
					dbo.ISTNow(), 
					@RequestId);
				SET @QuotationId = CAST(SCOPE_IDENTITY() AS bigint);
								
				INSERT INTO dbo.mQuotationDetail
					(QuotationId,
					QuotationNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT @QuotationId,
					@QuotationNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					1 AS IsActive,
					dbo.ISTNow() AS TransactionDate,
					@RequestId
				FROM @QuotationDetails 
				SET @OutParam = '{ "QuotationId" : ' +
							CAST(@QuotationId as VARCHAR) +
							', "QuotationNumber" : "' +
							CAST(@QuotationNumber AS VARCHAR) +
							'" }';
			END
			ELSE IF (@QueryType ='DELETE')
			BEGIN
				IF (NOT EXISTS(SELECT * FROM [dbo].[mSalesOrderHeader] 
								WHERE QuotationId = @QuotationId AND IsActive = 1))
				BEGIN
					UPDATE dbo.mQuotationHeader
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE QuotationId = @QuotationId;

					UPDATE dbo.mQuotationDetail
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE QuotationId = @QuotationId;
					SET @OutParam = @QuotationId;
				END
				ELSE
				BEGIN
					SET @OutParam = '-1';
				END
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