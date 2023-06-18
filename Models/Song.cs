namespace Touhou_Songs_MVC.Models
{
	public class Song
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Origin { get; set; } = string.Empty;
	}

	public class SongRequest
	{
		public string Title { get; set; } = string.Empty;
		public string Origin { get; set; } = string.Empty;
	}

	public class SongResponse
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Origin { get; set; } = string.Empty;
		public int TitleLength { get; set; }
	}
}
