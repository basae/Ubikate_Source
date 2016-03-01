--14-jun-2014 .- insert new register for propietary

create procedure SavePropietary
(
@proprietaryid int =null output,
@rfc varchar(15),
@lastname varchar(60),
@firstname varchar(60),
@subscribed bit,
@insertuser int
)
as
begin
if(@proprietaryid is null or @proprietaryid<=0)
	begin
		insert into proprietary
		(rfc,lastname,firstname,subscribed,insertuser,inserdate,active)
		values(@rfc,@lastname,@firstname,@subscribed,@insertuser,getdate(),1);
		set @proprietaryid=scope_identity();
	end
else
	begin
		update proprietary set
		rfc=@rfc,
		lastname=@lastname,
		firstname=@firstname,
		subscribed=@subscribed,
		updateuser=@insertuser,
		updatedate=getdate()
		where proprietaryid=@proprietaryid;

		set @proprietaryid=0;
	end
end 