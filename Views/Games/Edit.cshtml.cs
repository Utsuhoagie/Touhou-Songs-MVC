﻿using Touhou_Songs_MVC.Models;

namespace Touhou_Songs_MVC.Views.Games
{
	public class EditGameViewModel
	{
		public GameResponse GameResponse { get; set; } = new();
		public GameRequest GameRequest { get; set; } = new();
	}
}
