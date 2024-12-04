namespace data_replicator;

public class Diary
{
    public string? DiaryGuid { get; set; }
    public string? PatientGuid { get; set; }
    public string? OrganisationGuid { get; set; }
    public string? EffectiveDate { get; set; }
    public string? EffectiveDatePrecision { get; set; }
    public string? EnteredDate { get; set; }
    public string? EnteredTime { get; set; }
    public string? ClinicianUserInRoleGuid { get; set; }
    public string? EnteredByUserInRoleGuid { get; set; }
    public string? CodeId { get; set; }
    public string? OriginalTerm { get; set; }
    public string? AssociatedText { get; set; }
    public string? DurationTerm { get; set; }
    public string? LocationTypeDescription { get; set; }
    public string? Deleted { get; set; }
    public string? IsConfidential { get; set; }
    public string? IsActive { get; set; }
    public string? IsComplete { get; set; }
    public string? ConsultationGuid { get; set; }
    public string? ProcessingId { get; set; }
}