using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace grad_manager.Pages.Applications;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

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
}