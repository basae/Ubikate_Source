create procedure SP_S_GetBussinessMap
as
SELECT 
	BUSINESSID,
	NAME,
	LATITUDE,
	LONGITUDE,
	BUSINESSTYPEID
	FROM DBO.BUSINESS