using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace grad_manager.Pages.ApplicationTasks;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public ApplicationTask ApplicationTask { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var applicationTask = await _context.ApplicationTasks
            .Include(t => t.Application)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (applicationTask == null)
        {
            return NotFound();
        }

        ApplicationTask = applicationTask;

        return Page();
    }
}