CREATE PROCEDURE [SingleIB].[spGetSmsTrack]
(
	@ComponentIdentity	nvarchar(64) = NULL,
	@SmsFor				varchar(64) = NULL,

	@QueryType		varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='LINKWISE')
			BEGIN
				SELECT TOP 1 *
				FROM SingleIB.tSmsTrack 
				WHERE ComponentIdentity = @ComponentIdentity
					AND SmsFor = @SmsFor
					AND IsActive = 1;
			END
			ELSE IF (@QueryType ='SINGLE')
			BEGIN
				SELECT TOP 1 *
				FROM SingleIB.tSmsTrack 
				WHERE ComponentIdentity = @ComponentIdentity
					AND SmsFor = @SmsFor
					AND IsActive = 1;
			END
			ELSE IF (@QueryType ='ALL')
			BEGIN
				SELECT st.* 
				FROM SingleIB.tSmsTrack as st
				Order By st.SmsSendId DESC;
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