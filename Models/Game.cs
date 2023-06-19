namespace Touhou_Songs_MVC.Models
{
	public class Game
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;
		public string NameCode { get; set; } = string.Empty;
		public decimal NumberCode { get; set; }
		public int YearReleased { get; set; }
	}

	public class GameRequest
	{
		public string Name { get; set; } = string.Empty;
		public string NameCode { get; set; } = string.Empty;
		public decimal NumberCode { get; set; }
		public int YearReleased { get; set; }
	}

	public class GameResponse
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;
		public string NameCode { get; set; } = string.Empty;
		public string NumberCodeDisplay { get; set; } = string.Empty;
		public int YearReleased { get; set; }

		public static string GetNumberCodeDisplay(decimal numberCode)
		{
			if (decimal.IsInteger(numberCode))
			{
				return decimal.Truncate(numberCode).ToString();
			}
			else
			{
				return decimal.Round(numberCode, 1).ToString();
			}
		}
	}
}
