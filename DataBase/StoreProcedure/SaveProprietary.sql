--14-jun-2014 .- insert new register for propietary

create procedure SavePropietary
(
@proprietaryid int output,
@rfc varchar(15),
@lastname varchar(60),
@firstname varchar(60),
@username varchar(30),
@password varchar(40),
@subscribed bit
)
as
begin
if(@proprietaryid=null or @proprietaryid<=0)
	begin
		insert into proprietary
		(rfc,lastname,firstname,username,password,subscribed)
		values(@rfc,@lastname,@firstname,@username,@password,@subscribed);
		set @proprietaryid=scope_identity();
	end
else
	begin
		update proprietary set
		rfc=@rfc,
		lastname=@lastname,
		firstname=@firstname,
		username=@username,
		password=@password,
		subscribed=@subscribed
		where proprietaryid=@proprietaryid;

		set @proprietaryid=0;
	end
end 