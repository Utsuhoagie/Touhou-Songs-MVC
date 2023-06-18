using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs_MVC.Data;
using Touhou_Songs_MVC.Models;

namespace Touhou_Songs_MVC.Controllers
{
	public class SongsController : Controller
	{
		private readonly Touhou_Songs_MVC_Context _context;

		public SongsController(Touhou_Songs_MVC_Context context)
		{
			_context = context;
		}

		// GET: Songs
		public async Task<IActionResult> Index()
		{
			var songResponses = await _context.Songs
				.Select(s => new SongResponse
				{
					Id = s.Id,
					Title = s.Title,
					Origin = s.Origin,
					TitleLength = s.Title.Length,
				})
				.ToListAsync();

			return View(songResponses);

		}

		// GET: Songs/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			var song = await _context.Songs.FindAsync(id);

			if (song == null)
			{
				return NotFound();
			}

			var songResponse = new SongResponse
			{
				Id = song.Id,
				Title = song.Title,
				Origin = song.Origin,
				TitleLength = song.Title.Length,
			};

			return View(songResponse);
		}


		// GET: Songs/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Songs/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SongRequest req)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(Index));
			}

			var song = new Song
			{
				Title = req.Title,
				Origin = req.Origin,
			};

			_context.Songs.Add(song);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}


		// GET: Songs/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			var song = await _context.Songs.FindAsync(id);

			if (song == null)
			{
				return NotFound();
			}

			var baseReq = new SongRequest
			{
				Title = song.Title,
				Origin = song.Origin,
			};

			return View(baseReq);
		}

		// POST: Songs/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, SongRequest req)
		{
			//if (id != req.Id)
			//{
			//	return NotFound();
			//}

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				var song = await _context.Songs.FindAsync(id);

				if (song == null)
				{
					return BadRequest();
				}

				song.Title = req.Title;
				song.Origin = req.Origin;

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!SongExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
		}


		// GET: Songs/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			var song = await _context.Songs.FindAsync(id);

			if (song == null)
			{
				return NotFound();
			}

			var songResponse = new SongResponse
			{
				Title = song.Title,
				Origin = song.Origin,
				TitleLength = song.Title.Length,
			};

			return View(songResponse);
		}

		// POST: Songs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var song = await _context.Songs.FindAsync(id);

			if (song != null)
			{
				_context.Songs.Remove(song);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}


		private bool SongExists(int id)
		{
			return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
