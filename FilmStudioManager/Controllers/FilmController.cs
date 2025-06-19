using Microsoft.AspNetCore.Mvc;
using FilmStudioManager.Models;
using FilmStudioManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

public class FilmController : Controller
{

    private readonly ApplicationDbContext _context;

    public FilmController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var films = await _context.Films.Include(f => f.Genre).ToListAsync();

        return View(films);
    }

    [Authorize]
    public IActionResult Create()
    {
        ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName");
        return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Film film)
    {
        if (ModelState.IsValid)
        {
            _context.Add(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", film.GenreId);
        return View(film);
    }

    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var film = _context.Films.FirstOrDefault(f => f.FilmId == id);
        if (film == null)
            return NotFound();

        ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", film.GenreId);
        return View(film);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Film film)
    {
        if (id != film.FilmId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Films.Any(e => e.FilmId == film.FilmId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", film.GenreId);
        return View(film);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var film = await _context.Films.FindAsync(id);
        if (film == null)
        {
            return NotFound();
        }

        _context.Films.Remove(film);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

}