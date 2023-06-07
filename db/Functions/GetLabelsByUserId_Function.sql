use [UNotionsDB];
go

create function dbo.GetLabelsByUserId
(
	@UserId int
)
returns table
as
return
(
    select
		l.[Id], l.[Name]
    from
		[Labels] l
		inner join [UsersLabels] ul ON l.[Id] = ul.[LabelId]
    where
		ul.[UserId] = @UserId
)