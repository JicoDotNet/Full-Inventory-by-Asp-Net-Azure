CREATE PROCEDURE [dbo].[spSetSmsTrack]
(
	@UserEmail			nvarchar(64) = NULL,

	@ComponentIdentity	nvarchar(64) = NULL,	
	@UrlLink			nvarchar(128) = NULL,
	@SmsContent			nvarchar(512) = NULL,
	@MobileNo			nvarchar(16) = NULL,
	@IsMobileNoChanged	bit = 0,
	@IsResend			bit = 0,
	@SmsFor				varchar(64) = NULL,

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(16),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'sms';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				UPDATE dbo.tSmsTrack
				SET IsActive = 0
				WHERE ComponentIdentity = @ComponentIdentity
					AND SmsFor = @SmsFor
					AND IsActive = 1;

				INSERT INTO dbo.tSmsTrack
					(UserName,
					ComponentIdentity,
					SendTime,
					UrlLink,
					SmsContent,
					MobileNo,
					IsMobileNoChanged,
					IsResend,
					SmsFor,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@UserEmail, 
					@ComponentIdentity, 
					dbo.IstNow(), 
					@UrlLink, 
					@SmsContent, 
					@MobileNo, 
					@IsMobileNoChanged, 
					@IsResend, 
					@SmsFor, 
					1, 
					dbo.IstNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END