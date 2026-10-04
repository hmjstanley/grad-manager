using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace grad_manager.Pages.ApplicationTasks;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ApplicationTask ApplicationTask { get; set; } = new();

    public SelectList Applications { get; set; } = default!;

    public async Task OnGetAsync()
    {
        await LoadApplicationsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadApplicationsAsync();
            return Page();
        }

        _context.ApplicationTasks.Add(ApplicationTask);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
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
