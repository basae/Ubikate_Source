--14-jun-2014 delete register iof businessType
create procedure DeleteBusinessType
(
@businessTypeid int
)
as
begin
	delete from businessType where businessTypeid=@businessTypeid;
end