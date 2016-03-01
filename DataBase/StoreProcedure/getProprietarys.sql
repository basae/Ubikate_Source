create procedure getProprietarys
@id bigint = null
as
if @id is null
	BEGIN
		select 
		proprietaryid,
		rfc,
		lastname,
		firstname,
		subscribed,
		inserdate
		from proprietary where active=1
	END
ELSE
	BEGIN
		select 
		proprietaryid,
		rfc,
		lastname,
		firstname,
		subscribed,
		inserdate
		from proprietary where active=1 and proprietaryid=@id
	END