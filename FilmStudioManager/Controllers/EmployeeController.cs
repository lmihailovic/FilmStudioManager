using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FilmStudioManager.Models;
using Microsoft.AspNetCore.Authorization;
using FilmStudioManager.Data;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class EmployeeController : Controller
{

    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var employees = await _context.Employees.ToListAsync();

        return View(employees);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EmployeeApi()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(employee);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
        if (employee == null)
            return NotFound();

        return View(employee);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.EmployeeId == employee.EmployeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

}