CREATE procedure SP_S_GetBussinessMapID
@ID INT
as
SELECT 
	NAME,
	NUMBER,
	STREET,
	CITY,
	COUNTRY
	FROM DBO.BUSINESS
	WHERE BUSINESSID=@ID