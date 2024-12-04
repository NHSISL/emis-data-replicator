namespace data_replicator;

public class Session
{
    public string? AppointmentSessionGuid { get; set; }
    public string? Description { get; set; }
    public string? LocationGuid { get; set; }
    public string? SessionTypeDescription { get; set; }
    public string? SessionCategoryDisplayName { get; set; }
    public string? StartDate { get; set; }
    public string? StartTime { get; set; }
    public string? EndDate { get; set; }
    public string? EndTime { get; set; }
    public string? Private { get; set; }
    public string? OrganisationGuid { get; set; }
    public string? Deleted { get; set; }
    public string? ProcessingId { get; set; }
}