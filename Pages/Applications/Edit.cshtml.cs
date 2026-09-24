using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace grad_manager.Pages.Applications;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Application Application { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var application = await _context.Applications.FindAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        Application = application;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var applicationToUpdate = await _context.Applications.FindAsync(Application.Id);

        if (applicationToUpdate == null)
        {
            return NotFound();
        }

        applicationToUpdate.Company = Application.Company;
        applicationToUpdate.Role = Application.Role;
        applicationToUpdate.Location = Application.Location;
        applicationToUpdate.Deadline = Application.Deadline;
        applicationToUpdate.DateApplied = Application.DateApplied;
        applicationToUpdate.Status = Application.Status;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}