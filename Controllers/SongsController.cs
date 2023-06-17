﻿using Microsoft.AspNetCore.Mvc;
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
			return _context.Songs != null ?
						View(await _context.Songs.ToListAsync()) :
						Problem("Entity set 'Touhou_Songs_MVC_Context.Songs'  is null.");
		}

		// GET: Songs/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Songs == null)
			{
				return NotFound();
			}

			var song = await _context.Songs
				.FirstOrDefaultAsync(m => m.Id == id);
			if (song == null)
			{
				return NotFound();
			}

			return View(song);
		}

		// GET: Songs/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Songs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Origin")] Song song)
		{
			if (ModelState.IsValid)
			{
				_context.Add(song);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(song);
		}

		// GET: Songs/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Songs == null)
			{
				return NotFound();
			}

			var song = await _context.Songs.FindAsync(id);
			if (song == null)
			{
				return NotFound();
			}
			return View(song);
		}

		// POST: Songs/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Origin")] Song song)
		{
			if (id != song.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(song);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SongExists(song.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(song);
		}

		// GET: Songs/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Songs == null)
			{
				return NotFound();
			}

			var song = await _context.Songs
				.FirstOrDefaultAsync(m => m.Id == id);
			if (song == null)
			{
				return NotFound();
			}

			return View(song);
		}

		// POST: Songs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Songs == null)
			{
				return Problem("Entity set 'Touhou_Songs_MVC_Context.Songs'  is null.");
			}
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