use [UNotionsDB];
go

create procedure [dbo].[ManageNotesLabels]
    @Action varchar(6),
    @NoteId int,
    @LabelId int
as
begin
    if (@Action = 'insert')
		begin
			insert into [NotesLabels] ([NoteId], [LabelId])
			values (@NoteId, @LabelId);
		end;

    else if (@Action = 'delete')
		begin
			delete from [NotesLabels]
			where [NoteId] = @NoteId and [LabelId] = @LabelId;
		end;
end;