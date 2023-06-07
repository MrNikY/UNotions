using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UNotions_Lib.Models
{
	public enum Status
	{
		Active,
		Archive,
		RecycleBin
	}

	public class Note : INotifyPropertyChanged
	{
		private int _id;
		private string? _title;
		private string? _content;
		private string? _mediaContent;
		private Status _status;
		private DateTime _reminderDate;
		private DateTime _creationDate;
		private DateTime _editedDate;
		private DateTime _deletionDate;
		private ObservableCollection<Label>? _labels;


		public int Id
		{
			get { return _id; }
			set { SetProperty(ref _id, value, nameof(Id)); }
		}

		public string? Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value, nameof(Title)); }
		}

		public string? Content
		{
			get { return _content; }
			set { SetProperty(ref _content, value, nameof(Content)); }
		}

		public string? MediaContent
		{
			get { return _mediaContent; }
			set { SetProperty(ref _mediaContent, value, nameof(MediaContent)); }
		}

		public Status Status
		{
			get { return _status; }
			set { SetProperty(ref _status, value, nameof(Status)); }
		}

		public DateTime ReminderDate
		{
			get { return _reminderDate; }
			set { SetProperty(ref _reminderDate, value, nameof(ReminderDate)); }
		}

		public DateTime CreationDate
		{
			get { return _creationDate; }
			set { SetProperty(ref _creationDate, value, nameof(CreationDate)); }
		}

		public DateTime EditedDate
		{
			get { return _editedDate; }
			set { SetProperty(ref _editedDate, value, nameof(EditedDate)); }
		}

		public DateTime DeletionDate
		{
			get { return _deletionDate; }
			set { SetProperty(ref _deletionDate, value, nameof(DeletionDate)); }
		}

		public ObservableCollection<Label>? Labels
		{
			get { return _labels; }
			set { SetProperty(ref _labels, value, nameof(Labels)); }
		}


		public event PropertyChangedEventHandler? PropertyChanged;
		private void SetProperty<T>(ref T field, T value, string name)
		{
			if (!EqualityComparer<T>.Default.Equals(field, value))
			{
				field = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
			}
		}
	}
}
