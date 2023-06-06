use [UNotionsDB];
go

create function dbo.GetNotesByUserId
(
    @UserId int
)
returns table
as
return
(
    select
        n.*,
        l.[Id] as [LabelId],
        l.[Name] as [LabelName]
    from
        [UsersNotes] un
        inner join [Notes] n on un.[NoteId] = n.[Id]
        left join [NotesLabels] nl on n.[Id] = nl.[NoteId]
        left join [Labels] l on nl.[LabelId] = l.[Id]
    where
        un.[UserId] = @UserId
)