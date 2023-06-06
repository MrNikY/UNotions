using System.ComponentModel;

namespace UNotions_Lib.Models
{
	public class Label : INotifyPropertyChanged
	{
		private int _id;
		private string? _name;

		public int Id
		{
			get { return _id; }
			set { SetProperty(ref _id, value, nameof(Id)); }
		}

		public string? Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value, nameof(Name)); }
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
