using Microsoft.AspNetCore.Mvc;
using FilmStudioManager.Models;
using FilmStudioManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

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

    public IActionResult Create()
    {
        ViewData["IDGenre"] = new SelectList(_context.Genres, "ID", "GenreName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FilmName,IDGenre,DateOfRelease")] Film film)
    {
        if (ModelState.IsValid)
        {
            _context.Add(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["IDGenre"] = new SelectList(_context.Genres, "ID", "GenreName", film.IDGenre);
        return View(film);
    }
}