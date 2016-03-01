CREATE PROCEDURE sp_IU_SaveBusiness
@businessid int=null,
@name varchar(150),
@number varchar(20)='',
@street varchar(30)='',
@city varchar(30)='',
@country varchar(30)='',
@proprietaryid int,
@businesstypeid int,
@latitude decimal(10,6),
@longitude decimal(10,6),
@insertuser int
AS
IF @businessid is null or @businessid=0
BEGIN
	INSERT INTO dbo.business(
	name, 
	number, 
	street, 
	city, 
	country, 
	proprietaryid, 
	businessTypeid, 
	active, 
	insertuser, 
	insertdate,
	latitude,
	longitude
	)
	VALUES(
	@name,
	@number,
	@street,
	@city,
	@country,
	@proprietaryid,
	@businesstypeid,
	1,
	@insertuser,
	getdate(),
	@latitude,
	@longitude
	)
END
ELSE
BEGIN
	UPDATE dbo.business SET
	name=@name, 
	number=@number, 
	street=@street, 
	city=@city, 
	country=@country, 
	proprietaryid=@proprietaryid, 
	businessTypeid=@businesstypeid, 
	updateuser=@insertuser, 
	updatedate=getdate(),
	latitude=@latitude,
	longitude=@longitude
	WHERE businessid=@businessid
END