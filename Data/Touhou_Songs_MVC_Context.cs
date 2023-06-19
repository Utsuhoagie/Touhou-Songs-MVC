using Microsoft.EntityFrameworkCore;
using Touhou_Songs_MVC.Models;

namespace Touhou_Songs_MVC.Data
{
	public class Touhou_Songs_MVC_Context : DbContext
	{
		public Touhou_Songs_MVC_Context(DbContextOptions<Touhou_Songs_MVC_Context> options)
			: base(options)
		{
		}

		public DbSet<Song> Songs { get; set; } = default!;
		public DbSet<Game> Games { get; set; } = default!;
	}
}
