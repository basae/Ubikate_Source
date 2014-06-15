-- insert or modify register of businessType

create procedure SaveBusinessType
(
@businessTypeid int output,
@name varchar(50),
@urlIcon varchar(200)
)
as
	begin
		if(@businessTypeid=null or @businessTypeid<=0)
		begin
			insert into businessType(name,urlIcon)
			values(@name,@urlIcon);
			set @businessTypeid=scope_identity();
		end
		else
		begin
			update businessType set
			name=@name,
			urlIcon=@urlIcon
			where businessTypeid=@businessTypeid;

			set @businessTypeid=0;
		end
	end