namespace data_replicator;

public class Consultation
{
    public string? ConsultationGuid { get; set; }
    public string? PatientGuid { get; set; }
    public string? OrganisationGuid { get; set; }
    public string? EffectiveDate { get; set; }
    public string? EffectiveDatePrecision { get; set; }
    public string? EnteredDate { get; set; }
    public string? EnteredTime { get; set; }
    public string? ClinicianUserInRoleGuid { get; set; }
    public string? EnteredByUserInRoleGuid { get; set; }
    public string? AppointmentSlotGuid { get; set; }
    public string? ConsultationSourceTerm { get; set; }
    public string? ConsultationSourceCodeId { get; set; }
    public string? Complete { get; set; }
    public string? ConsultationType { get; set; }
    public string? Deleted { get; set; }
    public string? IsConfidential { get; set; }
    public string? ProcessingId { get; set; }
}