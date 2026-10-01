using grad_manager.Data;
using grad_manager.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace grad_manager.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public int TotalApplications { get; set; }
    public int ApplicationsSubmitted { get; set; }
    public int Interviews { get; set; }
    public int Offers { get; set; }

    public List<Application> UpcomingDeadlines { get; set; } = new();

    public List<ApplicationTask> UpcomingTaskDeadlines { get; set; } = new();

    public async Task OnGetAsync()
    {
        var applications = await _context.Applications.ToListAsync();

        var applicationTasks = await _context.ApplicationTasks
            .Include(t => t.Application)
            .ToListAsync();

        TotalApplications = applications.Count;

        ApplicationsSubmitted = applications.Count(a =>
            a.Stage == "Applied" ||
            a.Stage == "Online Test" ||
            a.Stage == "Interview" ||
            a.Stage == "Assessment Centre" ||
            a.Stage == "Offer");

        Interviews = applications.Count(a =>
            a.Stage == "Interview" ||
            a.Stage == "Assessment Centre");

        Offers = applications.Count(a =>
            a.Stage == "Offer");

        UpcomingDeadlines = applications
            .Where(a => a.Deadline.HasValue &&
                        a.Deadline.Value.Date >= DateTime.Today)
            .OrderBy(a => a.Deadline)
            .Take(5)
            .ToList();

        UpcomingTaskDeadlines = applicationTasks
            .Where(a => a.Deadline.HasValue &&
                        a.Deadline.Value.Date >= DateTime.Today &&
                        a.Finished == false)
            .OrderBy(a => a.Deadline)
            .Take(5)
            .ToList();
    }

    public async Task<IActionResult> OnPostSetFinishedAsync(int id, bool finished)
    {
        var applicationTask = await _context.ApplicationTasks.FindAsync(id);

        if (applicationTask == null)
        {
            return NotFound();
        }

        applicationTask.Finished = finished;

        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

}