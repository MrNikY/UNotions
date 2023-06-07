use [UNotionsDB];
go

create function dbo.GetUserByLoginAndPassword
(
    @Username varchar(30),
    @Password varchar(64)
)
returns table
as
return
(
    select 
		*
	from
		[Users] 
	where
		[Username] = @Username and [Password] = @Password
)