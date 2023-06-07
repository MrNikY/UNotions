use [UNotionsDB];
go

create procedure [dbo].[ManageLabel]
    @Action varchar(6) = null,
    @Id int = null,
    @Name nvarchar(100) = null,
	@UserId int = null
as
begin
    if (@Action = 'insert')
		begin
			insert into [Labels] ([Name])
			values (@Name)

			declare @LabelId int;
			set @LabelId = SCOPE_IDENTITY();

			if(@LabelId is not null)
				begin
					insert into [UsersLabels] ([UserId], [LabelId])
					values (@UserId, @LabelId);
				end;

				select @LabelId as [Id]
		end;

    else if (@Action = 'update')
		begin
			update [Labels]
			set [Name] = @Name
			where [ID] = @Id
		end;

    else if (@Action = 'delete')
		begin
			delete from [Labels]
			where [ID] = @Id
		end;
end;