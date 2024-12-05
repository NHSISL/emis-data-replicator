[![.NET](https://github.com/NHSISL/emis-data-replicator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/NHSISL/emis-data-replicator/actions/workflows/dotnet.yml)

# Emis Data Replicator

## Intended use

This command line tool is used to generate loads of test data for the EMIS GP supplier.

## Quick start

`data-replicator.exe [required patient count] [path to sample data directory] [path to output directory]`

## Methodology

This tool generates synthetic EMIS data with realistic proportions to EMIS production data. Values are selected for each cell by sampling values from EMIS supplied test data, so that all values should be realistic.

Rendered results are written directly to storage, limiting the tool's memory consumption to around 0.5GB.

EMIS production data count sample.
```
Average counts:

Admin_Patient - 34964
Admin_PatientHistory - 141599
Appointment_Session - 75133
Appointment_SessionUser - 74298
Appointment_Slot - 1274771
Audit_PatientAudit - Can't evaluate, not in EXA
Audit_RegistrationAudit - Can't evaluate, not in EXA
CareRecord_Consultation - 4705095
CareRecord_Diary - 223137
CareRecord_Observation - 19274940
CareRecord_ObservationReferral - I think you mean CareRecord_Referral. 99826
CareRecord_Problem - 743378
Prescribing_DrugRecord - 1470517
Prescribing_IssueRecord - 5613386
```