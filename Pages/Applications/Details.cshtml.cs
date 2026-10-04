using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace grad_manager.Pages.Applications;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Application Application { get; set; } = new();

    public List<ApplicationTask> UpcomingTaskDeadlines { get; set; } = new();

    public List<ApplicationTask> FinishedTasks { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var application = await _context.Applications
            .FirstOrDefaultAsync(a => a.Id == id);

        if (application == null)
        {
            return NotFound();
        }

        Application = application;

        // Upcoming unfinished tasks
        UpcomingTaskDeadlines = await _context.ApplicationTasks
            .Where(t =>
                t.ApplicationId == id &&
                !t.Finished &&
                t.Deadline.HasValue &&
                t.Deadline.Value.Date >= DateTime.Today)
            .OrderBy(t => t.Deadline)
            .Take(5)
            .ToListAsync();

        // Finished tasks
        FinishedTasks = await _context.ApplicationTasks
            .Where(t =>
                t.ApplicationId == id &&
                t.Finished)
            .OrderByDescending(t => t.Deadline)
            .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostSetFinishedAsync(int id)
    {
        var applicationTask = await _context.ApplicationTasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (applicationTask == null)
        {
            return NotFound();
        }

        // Toggle the task's finished state
        applicationTask.Finished = !applicationTask.Finished;

        await _context.SaveChangesAsync();

        // Return to the application details page
        return RedirectToPage(new
        {
            id = applicationTask.ApplicationId
        });
    }
}