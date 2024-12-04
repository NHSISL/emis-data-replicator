namespace data_replicator;

public class Problem
{
    public string? ObservationGuid { get; set; }
    public string? PatientGuid { get; set; }
    public string? OrganisationGuid { get; set; }
    public string? ParentProblemObservationGuid { get; set; }
    public string? Deleted { get; set; }
    public string? Comment { get; set; }
    public string? EndDate { get; set; }
    public string? EndDatePrecision { get; set; }
    public string? ExpectedDuration { get; set; }
    public string? LastReviewDate { get; set; }
    public string? LastReviewDatePrecision { get; set; }
    public string? LastReviewUserInRoleGuid { get; set; }
    public string? ParentProblemRelationship { get; set; }
    public string? ProblemStatusDescription { get; set; }
    public string? SignificanceDescription { get; set; }
    public string? ProcessingId { get; set; }
}