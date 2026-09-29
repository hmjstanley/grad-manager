namespace grad_manager.Models;

public class ApplicationTask
{
    public int Id { get; set; }

    public int? ApplicationId { get; set; }

    public Application? Application { get; set; }

    public DateTime? Deadline { get; set; }

    public String? Task { get; set; }

    public Boolean Finished { get; set; } = false;
}