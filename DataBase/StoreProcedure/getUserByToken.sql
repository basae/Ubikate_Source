create procedure getUserByToken(
@username varchar(50),
@password varchar(50)
)
as
select userid,username,[password],firstname,lastname from [user]
where username=@username and [password]=@password
