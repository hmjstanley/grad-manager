using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace grad_manager.Pages.Applications;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Application> Applications { get; set; } = new();

    public async Task OnGetAsync()
    {
        Applications = await _context.Applications.ToListAsync();
    }
}