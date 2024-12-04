namespace data_replicator;

public class IssueRecord
{
    public string? IssueRecordGuid { get; set; }
    public string? PatientGuid { get; set; }
    public string? OrganisationGuid { get; set; }
    public string? DrugRecordGuid { get; set; }
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
    public string? CourseDurationInDays { get; set; }
    public string? EstimatedNhsCost { get; set; }
    public string? IsConfidential { get; set; }
    public string? EmisCode { get; set; }
    public string? PatientMessage { get; set; }
    public string? ScriptPharmacyStamp { get; set; }
    public string? Compliance { get; set; }
    public string? AverageCompliance { get; set; }
    public string? IsPrescribedAsContraceptive { get; set; }
    public string? IsPrivatelyPrescribed { get; set; }
    public string? PharmacyMessage { get; set; }
    public string? PharmacyText { get; set; }
    public string? ConsultationGuid { get; set; }
    public string? ExpiryDate { get; set; }
    public string? ReviewDate { get; set; }
    public string? Deleted { get; set; }
    public string? ProcessingId { get; set; }
}