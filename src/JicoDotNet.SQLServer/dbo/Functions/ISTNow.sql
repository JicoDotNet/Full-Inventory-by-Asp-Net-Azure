CREATE FUNCTION [dbo].[ISTNow]()  
RETURNS DATETIME   
AS   
-- Returns the stock level for the product.  
BEGIN 
    RETURN DATEADD(MINUTE, 330, SYSUTCDATETIME());
END;