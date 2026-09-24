namespace grad_manager.Models;

public class Application
{
    public int Id { get; set; }

    public string Company { get; set; } = "";

    public string Role { get; set; } = "";

    public string Location { get; set; } = "";

    public DateTime? Deadline { get; set; }

    public DateTime? DateApplied { get; set; }

    public string Stage { get; set; } = "Interested";

    public string Url { get; set; } = "";

    public string? Notes { get; set; }
}