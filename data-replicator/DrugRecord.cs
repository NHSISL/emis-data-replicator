namespace data_replicator;

public class DrugRecord
{
    public string? DrugRecordGuid { get; set; }
    public string? PatientGuid { get; set; }
    public string? OrganisationGuid { get; set; }
    public string? EffectiveDate { get; set; }
    public string? EffectiveDatePrecision { get; set; }
    public string? EnteredDate { get; set; }
    public string? EnteredTime { get; set; }
    public string? ClinicianUserInRoleGuid { get; set; }
    public string? EnteredByUserInRoleGuid { get; set; }
    public string? CodeId { get; set; }
    public string? Dosage { get; set; }
    public string? Quantity { get; set; }
    public string? QuantityUnit { get; set; }
    public string? ProblemObservationGuid { get; set; }
    public string? PrescriptionType { get; set; }
    public string? IsActive { get; set; }
    public string? CancellationDate { get; set; }
    public string? NumberOfIssues { get; set; }
    public string? NumberOfIssuesAuthorised { get; set; }
    public string? IsConfidential { get; set; }
    public string? Deleted { get; set; }
    public string? ProcessingId { get; set; }
}