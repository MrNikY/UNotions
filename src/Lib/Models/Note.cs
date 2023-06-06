namespace UNotions_Lib.Models
{
	public enum Status
	{
		Active,
		Archive,
		RecycleBin
	}

	public class Note
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Content { get; set; }
		public Status Status { get; set; }
		public string? MediaContent { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime EditedDate { get; set; }
		public DateTime DeletionDate { get; set; }
	}
}
