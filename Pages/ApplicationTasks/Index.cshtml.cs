using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public async Task OnGetAsync()
    {
        ApplicationTasks = await _context.ApplicationTasks.ToListAsync();
    }
}