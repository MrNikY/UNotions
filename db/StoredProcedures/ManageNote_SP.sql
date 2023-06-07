use [UNotionsDB];
go

create procedure [dbo].[ManageNote]
	@Action varchar(6),
	@Id int = null,
	@Title nvarchar(100) = null,
	@Content nvarchar(max) = null,
	@Status smallint = null,
	@MediaContent varchar(2048) = null,
	@ReminderDate datetime = null,
	@CreationDate datetime = null,
	@EditedDate datetime = null,
	@DeletionDate datetime = null,
	@UserId int = null
as
begin
	if(@Action = 'insert')
		begin
			insert into 
			[Notes] ([Title],
					 [Content], 
					 [Status], 
					 [MediaContent],
					 [ReminderDate],
					 [CreationDate],
					 [EditedDate],
					 [DeletionDate])

			values (@Title, 
					@Content, 
					@Status, 
					@MediaContent,
					@ReminderDate,
					@CreationDate,
					@EditedDate,
					@DeletionDate);


			declare @NoteId int;
			set @NoteId = SCOPE_IDENTITY();

			if(@UserId is not null)
				begin
					insert into [UsersNotes] ([UserId], [NoteId])
					values (@UserId, @NoteId);
				end;

				select @NoteId as [Id]
		end;

	else if(@Action = 'update')
		begin
			update [Notes]
			set [Title] = @Title,
				[Content] = @Content, 
				[Status] = @Status, 
				[MediaContent] = @MediaContent,
				[ReminderDate] = @ReminderDate,
				[CreationDate] = @CreationDate,
				[EditedDate] = @EditedDate,
				[DeletionDate] = @DeletionDate
			where [Id] = @Id;
		end;

	else if(@Action = 'delete')
		begin
			delete from [Notes]
			where [Id] = @Id;
		end;

end;