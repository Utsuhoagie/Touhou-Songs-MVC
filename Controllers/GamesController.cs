using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs_MVC.Data;
using Touhou_Songs_MVC.Models;
using Touhou_Songs_MVC.Views.Games;

namespace Touhou_Songs_MVC.Controllers
{
	public class GamesController : Controller
	{
		private readonly Touhou_Songs_MVC_Context _context;

		public GamesController(Touhou_Songs_MVC_Context context)
		{
			_context = context;
		}

		// GET: Games
		public async Task<IActionResult> Index([FromQuery] string? search)
		{
			var gameReses = await _context.Games
				.Where(g => search == null || g.Name.ToLower().Contains(search.ToLower()))
				.OrderBy(g => g.NumberCode)
				.Select(g => new GameResponse
				{
					Id = g.Id,
					Name = g.Name,
					NameCode = g.NameCode,
					NumberCodeDisplay = GameResponse.GetNumberCodeDisplay(g.NumberCode),
					YearReleased = g.YearReleased,
				})
				.ToListAsync();

			return View(gameReses);
		}

		// GET: Games/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			var gameRes = await _context.Games
				.Where(g => g.Id == id)
				.Select(g => new GameResponse
				{
					Id = g.Id,
					Name = g.Name,
					NameCode = g.NameCode,
					NumberCodeDisplay = GameResponse.GetNumberCodeDisplay(g.NumberCode),
					YearReleased = g.YearReleased,
				})
				.SingleOrDefaultAsync();

			if (gameRes == null)
			{
				return NotFound();
			}

			return View(gameRes);
		}


		// GET: Games/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Games/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateGameViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(Index));
			}

			var req = viewModel.GameRequest;
			var game = new Game
			{
				Name = req.Name,
				NameCode = req.NameCode,
				NumberCode = req.NumberCode,
				YearReleased = req.YearReleased,
			};
			_context.Games.Add(game);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}


		// GET: Games/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			var game = await _context.Games.FindAsync(id);

			if (game == null)
			{
				return NotFound();
			}

			var gameRes = new GameResponse
			{
				Id = game.Id,
				Name = game.Name,
				NameCode = game.NameCode,
				NumberCodeDisplay = GameResponse.GetNumberCodeDisplay(game.NumberCode),
				YearReleased = game.YearReleased,
			};

			var req = new GameRequest
			{
				Name = game.Name,
				NameCode = game.NameCode,
				NumberCode = game.NumberCode,
				YearReleased = game.YearReleased,
			};
			var viewModel = new EditGameViewModel
			{
				GameResponse = gameRes,
				GameRequest = req,
			};

			return View(viewModel);
		}

		// POST: Games/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, EditGameViewModel viewModel)
		{
			//if (id != req.Id)
			//{
			//	return NotFound();
			//}

			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(Index));
			}

			try
			{
				var req = viewModel.GameRequest;

				var game = await _context.Games.FindAsync(id);

				if (game == null)
				{
					return NotFound();
				}

				game.Name = req.Name;
				game.NameCode = req.NameCode;
				game.NumberCode = req.NumberCode;
				game.YearReleased = req.YearReleased;

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GameExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
		}

		// GET: Games/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var gameRes = await _context.Games
				.Where(g => g.Id == id)
				.Select(g => new GameResponse
				{
					Id = g.Id,
					Name = g.Name,
					NameCode = g.NameCode,
					NumberCodeDisplay = GameResponse.GetNumberCodeDisplay(g.NumberCode),
					YearReleased = g.YearReleased,
				})
				.SingleOrDefaultAsync();

			if (gameRes == null)
			{
				return NotFound();
			}

			return View(gameRes);
		}

		// POST: Games/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var game = await _context.Games.FindAsync(id);

			if (game == null)
			{
				return BadRequest();
			}

			_context.Games.Remove(game);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

		private bool GameExists(int id)
		{
			return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
