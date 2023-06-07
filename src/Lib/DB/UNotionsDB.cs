using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using UNotions_Lib.Models;

namespace UNotions_Lib.DB
{
	public class UNotionsDB
	{
		#region Fields
		private readonly string? CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["UNotionsDB_ConnectionString"].ConnectionString;
		private const string MANAGE_USER = "ManageUser";
		private const string MANAGE_NOTE = "ManageNote";
		private const string MANAGE_LABEL = "ManageLabel";
		private const string MANAGE_NOTES_LABELS = "ManageNotesLabels";
		private IDbConnection? _connection;
		public bool IsConnected { get; protected set; }
		#endregion


		#region Events and Delegates
		public delegate void DatabaseMessageDelegate(string message);
		public event DatabaseMessageDelegate? DatabaseMessage;
		#endregion


		#region Connect/Disonnect
		public void ConnectToDB()
		{
			_connection?.Dispose();

			try
			{
				_connection = new SqlConnection(CONNECTION_STRING);
				_connection.Open();
			}
			catch (Exception ex)
			{
				DatabaseMessage?.Invoke(ex.Message);
				return;
			}

			IsConnected = true;
			DatabaseMessage?.Invoke("The connection to the Database was successfully established.");
		}


		public void DisconnectBase()
		{
			if (_connection is null) return;

			_connection.Dispose();
			IsConnected = false;
			DatabaseMessage?.Invoke("The connection to the Database was interrupted.");
		}
		#endregion


		#region Editing Data (Users)
		public async Task<User?> AddUser(string username, string password, string email, string nickname, DateTime registrationDate)
		{
			User user = new()
			{
				Username = username,
				Password = password,
				Email = email,
				Nickname = nickname,
				RegistrationDate = registrationDate
			};

			int id = await _connection.QuerySingleOrDefaultAsync<int>(MANAGE_USER, new
			{
				Action = "insert",
				user.Username, 
				user.Password, 
				user.Email, 
				user.Nickname, 
				user.RegistrationDate
			},
			commandType: CommandType.StoredProcedure);

			user.Id = id;

			return user;
		}


		public async Task UpdateUser(User user)
		{
			await _connection.ExecuteAsync(MANAGE_USER, new
			{
				Action = "update",
				user.Id,
				user.Username,
				user.Password,
				user.Email,
				user.Nickname,
				user.RegistrationDate
			},
			commandType: CommandType.StoredProcedure);
		}


		public async Task DeleteUser(int id)
		{
			await _connection.ExecuteAsync(MANAGE_USER, new
			{
				Action = "delete",
				Id = id
			},
			commandType: CommandType.StoredProcedure);
		}
		#endregion


		#region Editing Data (Notes)
		public async Task<Note?> AddNote(string title, string content, Status status, string mediaContent, DateTime reminderDate, DateTime creationDate, DateTime editedDate, DateTime deletionDate, int userId)
		{
			Note note = new()
			{
				Title = title,
				Content = content,
				Status = status,
				ReminderDate = reminderDate,
				MediaContent = mediaContent,
				CreationDate = creationDate,
				EditedDate = editedDate,
				DeletionDate = deletionDate
			};

			int id = await _connection.QuerySingleOrDefaultAsync<int>(MANAGE_NOTE, new
			{
				Action = "insert",
				note.Title,
				note.Content,
				note.Status,
				note.ReminderDate,
				note.MediaContent,
				note.CreationDate, 
				note.EditedDate,
				note.DeletionDate,
				UserId = userId
			},
			commandType: CommandType.StoredProcedure);

			note.Id = id;

			return note;
		}


		public async Task UpdateNote(Note note)
		{
			await _connection.ExecuteAsync(MANAGE_NOTE, new
			{
				Action = "update",
				note.Id,
				note.Title,
				note.Content,
				note.Status,
				note.ReminderDate,
				note.MediaContent,
				note.CreationDate,
				note.EditedDate,
				note.DeletionDate,
			},
			commandType: CommandType.StoredProcedure);
		}


		public async Task DeleteNote(int id)
		{
			await _connection.ExecuteAsync(MANAGE_NOTE, new
			{
				Action = "delete",
				Id = id
			},
			commandType: CommandType.StoredProcedure);
		}
		#endregion


		#region Editing Data (Labels)
		public async Task<Label?> AddLabel(string name, int userId)
		{
			Label label = new()
			{
				Name = name
			};

			int id = await _connection.QuerySingleOrDefaultAsync<int>(MANAGE_LABEL, new
			{
				Action = "insert",
				label.Name,
				UserId = userId
			},
			commandType: CommandType.StoredProcedure);

			label.Id = id;

			return label;
		}


		public async Task UpdateLabel(Label label)
		{
			await _connection.ExecuteAsync(MANAGE_LABEL, new
			{
				Action = "update",
				label.Id,
				label.Name
			});
		}


		public async Task DeleteLabel(int id)
		{
			await _connection.ExecuteAsync(MANAGE_LABEL, new
			{
				Action = "delete",
				Id = id
			},
			commandType: CommandType.StoredProcedure);
		}
		#endregion


		#region Editing Data (Notes & Labels)
		public async Task AttachLabelToNote(int noteId, int labelId)
		{
			await _connection.ExecuteAsync(MANAGE_NOTES_LABELS, new
			{
				Action = "insert",
				NoteId = noteId,
				LabelId = labelId
			},
			commandType: CommandType.StoredProcedure);
		}


		public async Task DetachLabelFromNote(int noteId, int labelId)
		{
			await _connection.ExecuteAsync(MANAGE_NOTES_LABELS, new
			{
				Action = "delete",
				NoteId = noteId,
				LabelId = labelId
			},
			commandType: CommandType.StoredProcedure);
		}
		#endregion


		#region Data Requests
		public async Task<User> GetUserByLoginAndPassword(string username, string password)
		{
			string query = "select * from dbo.GetUserByLoginAndPassword(@Username, @Password)";

			User user = await _connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username, Password = password });

			return user;
		}


		public async Task<ObservableCollection<Note>> GetNotesByUserId(int userId)
		{
			string query = "select * from dbo.GetNotesByUserId(@UserId)";

			var lookup = new Dictionary<int, Note>();

			await _connection.QueryAsync<Note, Label, Note>(query, (note, label) =>
			{
				if (!lookup.TryGetValue(note.Id, out Note? noteEntry))
				{
					noteEntry = note;
					noteEntry.Labels = new ObservableCollection<Label>();
					lookup.Add(noteEntry.Id, noteEntry);
				}

				noteEntry.Labels?.Add(label);
				return noteEntry;
			}, new { UserId = userId }, splitOn: "LabelId");

			return new ObservableCollection<Note>(lookup.Values);
		}


		public async Task<ObservableCollection<Label>> GetLabelsByUserId(int userId)
		{
			string query = "select * from dbo.GetLabelsByUserId(@UserId)";

			var labels = await _connection.QueryAsync<Label>(query, new { UserId = userId });

			return new ObservableCollection<Label>(labels);
		}
		#endregion
	}
}