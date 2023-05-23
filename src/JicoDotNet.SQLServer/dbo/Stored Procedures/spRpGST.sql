CREATE PROCEDURE [dbo].[spRpGST]
(
	@StartDate			datetime = null,
	@EndDate			datetime = null,

	@PaymentStatus		bit = null,

	@QueryType			varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'PurchaseReport';

	DECLARE @SQL VARCHAR(8000) = NULL,
	        @WhereClause	varchar(8000) = null
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='OUTPUTGST')
			BEGIN
				SET @SQL = 'SELECT inv.* 
						FROM dbo.mInvoiceHeader as inv
						WHERE (inv.InvoiceDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
															+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
															+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
								AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
										+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
										+ CAST(DAY(@EndDate) as VARCHAR) + '''))';
				
				IF @PaymentStatus IS NOT NULL
				BEGIN
					SET @WhereClause = ' (EXISTS (SELECT * FROM [dbo].[mPaymentInDetail] AS pod 
								WHERE  inv.InvoiceId = pod.InvoiceId
									AND pod.IsFullReceived = ' + CAST(@PaymentStatus as VARCHAR) + ')) ' ;
				END
				IF @WhereClause IS NOT NULL
				BEGIN
					SET @SQL = @SQL + ' AND ' + @WhereClause 
				END

				--PRINT(@WhereClause);
				--PRINT (@SQL);
				EXECUTE(@SQL);
			END

			ELSE IF (@QueryType ='INPUTGST')
			BEGIN
				SET @SQL = 'SELECT bil.* 
						FROM dbo.mBillHeader as bil
						WHERE (bil.BillDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
															+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
															+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
								AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
										+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
										+ CAST(DAY(@EndDate) as VARCHAR) + '''))';
				
				IF @PaymentStatus IS NOT NULL
				BEGIN
					SET @WhereClause = ' (EXISTS (SELECT * FROM [dbo].[mPaymentOutDetail] AS pod 
									WHERE  bil. BillId = pod.BillId
									AND pod.IsFullPaid = ' + CAST(@PaymentStatus as VARCHAR) + ')) ' ;
				END
				IF @WhereClause IS NOT NULL
				BEGIN
					SET @SQL = @SQL + ' AND ' + @WhereClause 
				END

				--PRINT(@WhereClause);
				PRINT (@SQL);
				EXECUTE(@SQL);
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