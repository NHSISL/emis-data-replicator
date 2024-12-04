namespace data_replicator;

public class Slot
{
    public string? SlotGuid { get; set; }
    public string? AppointmentDate { get; set; }
    public string? AppointmentStartTime { get; set; }
    public string? PlannedDurationInMinutes { get; set; }
    public string? PatientGuid { get; set; }
    public string? SendInTime { get; set; }
    public string? LeftTime { get; set; }
    public string? DidNotAttend { get; set; }
    public string? PatientWaitInMin { get; set; }
    public string? AppointmentDelayInMin { get; set; }
    public string? ActualDurationInMinutes { get; set; }
    public string? OrganisationGuid { get; set; }
    public string? SessionGuid { get; set; }
    public string? DnaReasonCodeId { get; set; }
    public string? BookedDate { get; set; }
    public string? BookedTime { get; set; }
    public string? SlotStatus { get; set; }
    public string? SlotType { get; set; }
    public string? IsBookableOnline { get; set; }
    public string? BookingMethod { get; set; }
    public string? ExternalPatientGuid { get; set; }
    public string? ExternalPatientOrganisation { get; set; }
    public string? ModeOfContact { get; set; }
    public string? Deleted { get; set; }
    public string? ProcessingId { get; set; }
}