use [master]
go


if db_id('UNotionsDB') is not null
begin
	drop database [UNotionsDB];
end
go


create database [UNotionsDB];
go


use [UNotionsDB];
go


create table [Users](
	[Id] int not null identity(1,1),
	[Username] varchar(30) unique not null,
	[Password] varchar(64) not null,
	[Email] varchar(256) not null,
	[Nickname] nvarchar(100) not null,
	[RegistrationDate] datetime not null,

	constraint PK_Users_Id primary key([Id]),
	constraint CK_Users_Username check([Username] <> ''),
	constraint CK_Users_Password check([Password] <> ''),
	constraint CK_Users_Email check([Email] <> ''),
	constraint CK_Users_Nickname check([Nickname] <> '')
);


create table [Notes](
	[Id] int not null identity(1,1),
	[Title] nvarchar(100) not null,
	[Content] nvarchar(max) not null,

	-- 0 = Active; 1 = Archive; 2 = RecycleBin;
	[Status] smallint null,

	[MediaContent] varchar(2048) null,
	[ReminderDate] datetime not null,
	[CreationDate] datetime not null,
	[EditedDate] datetime null,
	[DeletionDate] datetime null,

	constraint PK_Notes_Id primary key([Id]),
	constraint CK_Notes_Title check([Title] <> ''),
	constraint CK_Notes_Content check([Content] <> ''),
	constraint CK_Notes_MediaContent check([MediaContent] <> ''),
	constraint CK_Notes_Status check([Status] >= 0 and [Status] <= 2)
);


create table [Labels](
	[Id] int not null identity(1,1),
	[Name] nvarchar(100) not null,

	constraint PK_Labels_Id primary key([Id]),
	constraint CK_Labels_Name check([Name] <> ''),
);


create table [UsersLabels](
	[UserId] int not null,
	[LabelId] int not null,

	constraint PK_UsersLabels primary key([UserId], [LabelId]),
	constraint FK_UsersLabels_UserId foreign key([UserId]) references [Users]([Id])
	on delete cascade
	on update cascade,
	constraint FK_UsersLabels_LabelId foreign key([LabelId]) references [Labels]([Id])
	on delete cascade
	on update cascade
);


create table [UsersNotes](
	[UserId] int not null,
	[NoteId] int not null,

	constraint PK_UsersNotes primary key([UserId], [NoteId]),
	constraint FK_UsersNotes_UserId foreign key([UserId]) references [Users]([Id])
	on delete cascade
	on update cascade,
	constraint FK_UsersNotes_NoteId foreign key([NoteId]) references [Notes]([Id])
	on delete cascade
	on update cascade
);


create table [NotesLabels](
	[NoteId] int not null,
	[LabelId] int not null,

	constraint PK_NotesLabels primary key([NoteId], [LabelId]),
	constraint FK_NotesLabels_NoteId foreign key([NoteId]) references [Notes]([Id])
	on delete cascade
	on update cascade,
	constraint FK_NotesLabels_LabelId foreign key([LabelId]) references [Labels]([Id])
	on delete cascade
	on update cascade
);