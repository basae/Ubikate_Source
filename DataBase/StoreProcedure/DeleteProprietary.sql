-- 14-jun-2014 delete proprietary

create procedure DeleteProprietary
(
@proprietaryid int
)
as
begin
	update proprietary 
	set
	active=0
	where proprietaryid=@proprietaryid
end
