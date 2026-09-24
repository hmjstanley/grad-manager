using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace grad_manager.Pages.Applications;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
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
        if (Application.Id == 0)
        {
            return NotFound();
        }

        var application = await _context.Applications.FindAsync(Application.Id);

        if (application == null)
        {
            return NotFound();
        }

        _context.Applications.Remove(application);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}