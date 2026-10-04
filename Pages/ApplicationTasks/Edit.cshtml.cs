using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace grad_manager.Pages.ApplicationTasks;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ApplicationTask ApplicationTask { get; set; } = new();

    public SelectList Applications { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var applicationTask = await _context.ApplicationTasks.FindAsync(id);

        if (applicationTask == null)
        {
            return NotFound();
        }

        ApplicationTask = applicationTask;

        await LoadApplicationsAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadApplicationsAsync();
            return Page();
        }

        var taskToUpdate = await _context.ApplicationTasks
            .FindAsync(ApplicationTask.Id);

        if (taskToUpdate == null)
        {
            return NotFound();
        }

        taskToUpdate.Deadline = ApplicationTask.Deadline;
        taskToUpdate.Task = ApplicationTask.Task;

        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }

    private async Task LoadApplicationsAsync()
    {
        var applications = await _context.Applications
            .OrderBy(a => a.Company)
            .ThenBy(a => a.Role)
            .Select(a => new
            {
                a.Id,
                DisplayName = a.Company + " — " + a.Role
            })
            .ToListAsync();

        Applications = new SelectList(
            applications,
            "Id",
            "DisplayName",
            ApplicationTask.ApplicationId
        );
    }
}