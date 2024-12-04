namespace data_replicator;

public class Observation
{
    public string? ObservationGuid { get; set; }
    public string? PatientGuid { get; set; }
    public string? OrganisationGuid { get; set; }
    public string? EffectiveDate { get; set; }
    public string? EffectiveDatePrecision { get; set; }
    public string? EnteredDate { get; set; }
    public string? EnteredTime { get; set; }
    public string? ClinicianUserInRoleGuid { get; set; }
    public string? EnteredByUserInRoleGuid { get; set; }
    public string? ParentObservationGuid { get; set; }
    public string? CodeId { get; set; }
    public string? ProblemGuid { get; set; }
    public string? ConsultationGuid { get; set; }
    public string? AssociatedText { get; set; }
    public string? Value { get; set; }
    public string? NumericUnit { get; set; }
    public string? ObservationType { get; set; }
    public string? NumericRangeLow { get; set; }
    public string? NumericRangeHigh { get; set; }
    public string? DocumentGuid { get; set; }
    public string? Qualifiers { get; set; }
    public string? Abnormal { get; set; }
    public string? AbnormalReason { get; set; }
    public string? Episode { get; set; }
    public string? Deleted { get; set; }
    public string? IsConfidential { get; set; }
    public string? NumericOperator { get; set; }
    public string? ProcessingId { get; set; }
}