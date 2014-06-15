-- 14-jun-2014 delete proprietary

create procedure DeleteProprietary
(
@proprietaryid int
)
as
begin
	delete from proprietary where proprietaryid=@proprietaryid
end
