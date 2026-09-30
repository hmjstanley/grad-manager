using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace grad_manager.Pages.ApplicationTasks;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<ApplicationTask> ApplicationTasks { get; set; } = new();

    public List<ApplicationTask> ToDoTasks { get; set; } = new();

    public List<ApplicationTask> CompletedTasks { get; set; } = new();

    public async Task OnGetAsync()
    {
        ApplicationTasks = await _context.ApplicationTasks
            .Include(t => t.Application)
            .ToListAsync();

        ToDoTasks = ApplicationTasks
            .Where(t => !t.Finished)
            .ToList();

        CompletedTasks = ApplicationTasks
            .Where(t => t.Finished)
            .ToList();
    }

    public async Task<IActionResult> OnPostSetFinishedAsync(int id, bool finished)
    {
        var task = await _context.ApplicationTasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        task.Finished = finished;
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}