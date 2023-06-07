use [UNotionsDB];
go

create procedure [dbo].[ManageUser]
	@Action varchar(6),
	@Id int = null,
	@Username varchar(30) = null,
	@Password varchar(64) = null,
	@Email varchar(256) = null,
	@Nickname nvarchar(100) = null,
	@RegistrationDate datetime = null
as
begin
	if(@Action = 'insert')
		begin
			insert into 
			[Users] ([Username],
					 [Password], 
					 [Email], 
					 [Nickname], 
					 [RegistrationDate])

			values (@Username, 
					@Password, 
					@Email, 
					@Nickname,
					@RegistrationDate);

			select SCOPE_IDENTITY() as [Id]
		end;

	else if(@Action = 'update')
		begin
			update [Users]
			set [Username] = @Username,
				[Password] = @Password, 
				[Email] = @Email, 
				[Nickname] = @Nickname, 
				[RegistrationDate] = @RegistrationDate
			where [Id] = @Id;
		end;

	else if(@Action = 'delete')
		begin
			delete from [Users]
			where [Id] = @Id;
		end;
end;