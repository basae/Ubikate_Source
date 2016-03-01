CREATE PROCEDURE SP_S_GetBusiness
@businessid int =null
AS
IF @businessid is null
BEGIN
	SELECT 
	businessid, 
	name, 
	number, 
	street, 
	city, 
	country, 
	proprietaryid, 
	businessTypeid, 
	latitude, 
	longitude
	FROM dbo.business
	WHERE ACTIVE=1
END
ELSE
BEGIN
	SELECT 
	businessid, 
	name, 
	number, 
	street, 
	city, 
	country, 
	proprietaryid, 
	businessTypeid, 
	latitude, 
	longitude
	FROM dbo.business
	WHERE businessid=@BUSINESSID AND ACTIVE=1
	
	
END