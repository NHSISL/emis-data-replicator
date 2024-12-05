using CsvHelper;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ShellProgressBar;

namespace data_replicator;

internal class Program
{
    public static dynamic[] GetSampleRecords(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<dynamic>().Take(2000).ToArray();
    }

    static Random random = new Random();

    static string GetRandomValue(dynamic[] array, Func<dynamic, string> selector)
    {
        int next = random.Next(0, array.Length - 1);

        dynamic selection = array[next];

        return selector(selection);
    }

    static string? GetRandomValue<T>(T[] array, Func<T, string> selector)
    {
        int next = random.Next(0, array.Length - 1);

        dynamic selection = array[next];

        return selector(selection);
    }

    static T GetRandomRecord<T>(T[] array)
    {
        int next = random.Next(0, array.Length - 1);

        return array[next];
    }

    private static string FindFilePath(string path, string type)
    {
        var files = Directory.GetFiles(path, "*.csv");

        var file = files.SingleOrDefault(f => Path.GetFileName(f).Contains(type));

        if (file == null)
        {
            Console.Error.WriteLine($"File type {type} not found. Quitting.");

            throw new InvalidOperationException("File not found");
        }

        return file;
    }

    private static dynamic[] Admin_Organisation;
    private static dynamic[] Admin_Patient;
    private static dynamic[] Admin_UserInRole;
    private static dynamic[] Appointment_Session;
    private static dynamic[] Appointment_Slot;
    private static dynamic[] CareRecord_Consultation;
    private static dynamic[] CareRecord_Diary;
    private static dynamic[] CareRecord_Observation;
    private static dynamic[] CareRecord_Problem;
    private static dynamic[] Prescribing_DrugRecord;
    private static dynamic[] Prescribing_IssueRecord;

    private static IEnumerable<object> GetEmisObjects(ProgressBar progressBar, int orgNumber, int targetPatientCount)
    {
        var childOptions = new ProgressBarOptions
        {
            ForegroundColor = ConsoleColor.Green,
            BackgroundColor = ConsoleColor.DarkGreen,
            ProgressCharacter = '─'
        };

        using var childProgressBar = progressBar.Spawn(targetPatientCount, $"Organsiation #{orgNumber}", childOptions);

        string orgGuid = Guid.NewGuid().ToString();

        yield return
            new Organisation
            {
                OrganisationGuid = orgGuid,
                CDB = GetRandomValue(Admin_Organisation, o => o.CDB),
                OrganisationName = GetRandomValue(Admin_Organisation, o => o.OrganisationName),
                ODSCode = GetRandomValue(Admin_Organisation, o => o.ODSCode),
                ParentOrganisationGuid = GetRandomValue(Admin_Organisation, o => o.ParentOrganisationGuid),
                CCGOrganisationGuid = GetRandomValue(Admin_Organisation, o => o.CCGOrganisationGuid),
                OrganisationType = GetRandomValue(Admin_Organisation, o => o.OrganisationType),
                OpenDate = GetRandomValue(Admin_Organisation, o => o.OpenDate),
                CloseDate = GetRandomValue(Admin_Organisation, o => o.CloseDate),
                MainLocationGuid = GetRandomValue(Admin_Organisation, o => o.MainLocationGuid),
                ProcessingId = GetRandomValue(Admin_Organisation, o => o.ProcessingId)
            };

        var userInRoles = new List<UserInRole>();

        for (int userInRoleIndex = 0; userInRoleIndex < 200; userInRoleIndex++)
        {
            var userInRole =
                new UserInRole
                {
                    UserInRoleGuid = GetRandomValue(Admin_UserInRole, o => o.UserInRoleGuid),
                    OrganisationGuid = orgGuid,
                    Title = GetRandomValue(Admin_UserInRole, o => o.Title),
                    GivenName = GetRandomValue(Admin_UserInRole, o => o.GivenName),
                    Surname = GetRandomValue(Admin_UserInRole, o => o.Surname),
                    JobCategoryCode = GetRandomValue(Admin_UserInRole, o => o.JobCategoryCode),
                    JobCategoryName = GetRandomValue(Admin_UserInRole, o => o.JobCategoryName),
                    ContractStartDate = GetRandomValue(Admin_UserInRole, o => o.ContractStartDate),
                    ContractEndDate = GetRandomValue(Admin_UserInRole, o => o.ContractEndDate),
                    RegistrationNumber = GetRandomValue(Admin_UserInRole, o => o.RegistrationNumber),
                    ProcessingId = GetRandomValue(Admin_UserInRole, o => o.ProcessingId),
                };

            yield return userInRole;

            userInRoles.Add(userInRole);
        }

        var userInRoleArray = userInRoles.ToArray();

        var patientList = new List<Patient>();

        for (int patientIndex = 0; patientIndex < targetPatientCount; patientIndex++)
        {
            childProgressBar.Tick($"Organsiation #{orgNumber}. Patient {patientIndex} of {targetPatientCount}.");

            string patientGuid = Guid.NewGuid().ToString();

            var patient =
                new Patient
                {
                    PatientGuid = patientGuid,
                    OrganisationGuid = orgGuid,
                    UsualGpUserInRoleGuid = GetRandomValue<UserInRole>(userInRoleArray, o => o.UserInRoleGuid),
                    Sex = GetRandomValue(Admin_Patient, o => o.Sex),
                    DateOfBirth = GetRandomValue(Admin_Patient, o => o.DateOfBirth),
                    DateOfDeath = GetRandomValue(Admin_Patient, o => o.DateOfDeath),
                    Title = GetRandomValue(Admin_Patient, o => o.Title),
                    GivenName = GetRandomValue(Admin_Patient, o => o.GivenName),
                    MiddleNames = GetRandomValue(Admin_Patient, o => o.MiddleNames),
                    Surname = GetRandomValue(Admin_Patient, o => o.Surname),
                    DateOfRegistration = GetRandomValue(Admin_Patient, o => o.DateOfRegistration),
                    NhsNumber = NHSNumberGenerator.GenerateNHSNumber(),
                    PatientNumber = patientIndex.ToString(),
                    PatientTypeDescription = GetRandomValue(Admin_Patient, o => o.PatientTypeDescription),
                    DummyType = GetRandomValue(Admin_Patient, o => o.DummyType),
                    HouseNameFlatNumber = GetRandomValue(Admin_Patient, o => o.HouseNameFlatNumber),
                    NumberAndStreet = GetRandomValue(Admin_Patient, o => o.NumberAndStreet),
                    Village = GetRandomValue(Admin_Patient, o => o.Village),
                    Town = GetRandomValue(Admin_Patient, o => o.Town),
                    County = GetRandomValue(Admin_Patient, o => o.County),
                    Postcode = GetRandomValue(Admin_Patient, o => o.Postcode),
                    ResidentialInstituteCode = GetRandomValue(Admin_Patient, o => o.ResidentialInstituteCode),
                    NHSNumberStatus = GetRandomValue(Admin_Patient, o => o.NHSNumberStatus),
                    CarerName = GetRandomValue(Admin_Patient, o => o.CarerName),
                    CarerRelation = GetRandomValue(Admin_Patient, o => o.CarerRelation),
                    PersonGuid = Guid.NewGuid().ToString(),
                    DateOfDeactivation = GetRandomValue(Admin_Patient, o => o.DateOfDeactivation),
                    Deleted = GetRandomValue(Admin_Patient, o => o.Deleted),
                    SpineSensitive = GetRandomValue(Admin_Patient, o => o.SpineSensitive),
                    IsConfidential = GetRandomValue(Admin_Patient, o => o.IsConfidential),
                    EmailAddress = GetRandomValue(Admin_Patient, o => o.EmailAddress),
                    HomePhone = GetRandomValue(Admin_Patient, o => o.HomePhone),
                    MobilePhone = GetRandomValue(Admin_Patient, o => o.MobilePhone),
                    ExternalUsualGPGuid = GetRandomValue(Admin_Patient, o => o.ExternalUsualGPGuid),
                    ExternalUsualGP = GetRandomValue(Admin_Patient, o => o.ExternalUsualGP),
                    ExternalUsualGPOrganisation = GetRandomValue(Admin_Patient, o => o.ExternalUsualGPOrganisation),
                    ContactComments = GetRandomValue(Admin_Patient, o => o.ContactComments),
                    ProcessingId = GetRandomValue(Admin_Patient, o => o.ProcessingId),

                };

            yield return patient;

            patientList.Add(patient);

            var patientArray = patientList.ToArray();

            var slotsList = new List<Slot>();

            for (int sessionIndex = 0; sessionIndex < 2; sessionIndex++)
            {
                string sessionGuid = Guid.NewGuid().ToString();

                var session =
                    new Session
                    {
                        AppointmentSessionGuid = sessionGuid,
                        Description = GetRandomValue(Appointment_Session, o => o.Description),
                        LocationGuid = GetRandomValue(Appointment_Session, o => o.LocationGuid),
                        SessionTypeDescription = GetRandomValue(Appointment_Session, o => o.SessionTypeDescription),
                        SessionCategoryDisplayName = GetRandomValue(Appointment_Session, o => o.SessionCategoryDisplayName),
                        StartDate = GetRandomValue(Appointment_Session, o => o.StartDate),
                        StartTime = GetRandomValue(Appointment_Session, o => o.StartTime),
                        EndDate = GetRandomValue(Appointment_Session, o => o.EndDate),
                        EndTime = GetRandomValue(Appointment_Session, o => o.EndTime),
                        Private = GetRandomValue(Appointment_Session, o => o.Private),
                        OrganisationGuid = orgGuid,
                        Deleted = GetRandomValue(Appointment_Session, o => o.Deleted),
                        ProcessingId = GetRandomValue(Appointment_Session, o => o.ProcessingId)
                    };

                yield return session;

                for (int slotIndex = 0; slotIndex < 17; slotIndex++)
                {
                    var slot =
                        new Slot
                        {
                            SlotGuid = Guid.NewGuid().ToString(),
                            AppointmentDate = GetRandomValue(Appointment_Slot, o => o.AppointmentDate),
                            AppointmentStartTime = GetRandomValue(Appointment_Slot, o => o.AppointmentStartTime),
                            PlannedDurationInMinutes = GetRandomValue(Appointment_Slot, o => o.PlannedDurationInMinutes),
                            PatientGuid = GetRandomValue<Patient>(patientArray, o => o.PatientGuid),
                            SendInTime = GetRandomValue(Appointment_Slot, o => o.SendInTime),
                            LeftTime = GetRandomValue(Appointment_Slot, o => o.LeftTime),
                            DidNotAttend = GetRandomValue(Appointment_Slot, o => o.DidNotAttend),
                            PatientWaitInMin = GetRandomValue(Appointment_Slot, o => o.PatientWaitInMin),
                            AppointmentDelayInMin = GetRandomValue(Appointment_Slot, o => o.AppointmentDelayInMin),
                            ActualDurationInMinutes = GetRandomValue(Appointment_Slot, o => o.ActualDurationInMinutes),
                            OrganisationGuid = orgGuid,
                            SessionGuid = sessionGuid,
                            DnaReasonCodeId = GetRandomValue(Appointment_Slot, o => o.DnaReasonCodeId),
                            BookedDate = GetRandomValue(Appointment_Slot, o => o.BookedDate),
                            BookedTime = GetRandomValue(Appointment_Slot, o => o.BookedTime),
                            SlotStatus = GetRandomValue(Appointment_Slot, o => o.SlotStatus),
                            SlotType = GetRandomValue(Appointment_Slot, o => o.SlotType),
                            IsBookableOnline = GetRandomValue(Appointment_Slot, o => o.IsBookableOnline),
                            BookingMethod = GetRandomValue(Appointment_Slot, o => o.BookingMethod),
                            ExternalPatientGuid = null,
                            ExternalPatientOrganisation = null,
                            ModeOfContact = GetRandomValue(Appointment_Slot, o => o.ModeOfContact),
                            Deleted = GetRandomValue(Appointment_Slot, o => o.Deleted),
                            ProcessingId = GetRandomValue(Appointment_Slot, o => o.ProcessingId),
                        };

                    yield return slot;

                    slotsList.Add(slot);
                }
            }

            var slotArray = slotsList.ToArray();

            var consultationList = new List<Consultation>();
            var observationList = new List<Observation>();
            var drugRecords = new List<DrugRecord>();

            for (int consultationIndex = 0; consultationIndex < 134; consultationIndex++)
            {
                var slot = GetRandomRecord(slotArray);

                string consultationGuid = Guid.NewGuid().ToString();

                var consultation =
                    new Consultation
                    {
                        ConsultationGuid = Guid.NewGuid().ToString(),
                        PatientGuid = slot.PatientGuid,
                        OrganisationGuid = slot.OrganisationGuid,
                        EffectiveDate = GetRandomValue(CareRecord_Consultation, o => o.EffectiveDate),
                        EffectiveDatePrecision = GetRandomValue(CareRecord_Consultation, o => o.EffectiveDatePrecision),
                        EnteredDate = GetRandomValue(CareRecord_Consultation, o => o.EnteredDate),
                        EnteredTime = GetRandomValue(CareRecord_Consultation, o => o.EnteredTime),
                        ClinicianUserInRoleGuid = GetRandomValue(userInRoleArray, o => o.UserInRoleGuid),
                        EnteredByUserInRoleGuid = GetRandomValue(userInRoleArray, o => o.UserInRoleGuid),
                        AppointmentSlotGuid = slot.SlotGuid,
                        ConsultationSourceTerm = GetRandomValue(CareRecord_Consultation, o => o.ConsultationSourceTerm),
                        ConsultationSourceCodeId = GetRandomValue(CareRecord_Consultation, o => o.ConsultationSourceCodeId),
                        Complete = GetRandomValue(CareRecord_Consultation, o => o.Complete),
                        ConsultationType = GetRandomValue(CareRecord_Consultation, o => o.ConsultationType),
                        Deleted = GetRandomValue(CareRecord_Consultation, o => o.Deleted),
                        IsConfidential = GetRandomValue(CareRecord_Consultation, o => o.IsConfidential),
                        ProcessingId = GetRandomValue(CareRecord_Consultation, o => o.ProcessingId),
                    };

                yield return consultation;

                consultationList.Add(consultation);

                for (int observationIndex = 0; observationIndex < 4; observationIndex++)
                {
                    string observationGuid = Guid.NewGuid().ToString();
                    string problemGuid = null;

                    if (consultationIndex % 6 == 0 && patientIndex % 4 == 0)
                    {
                        problemGuid = observationGuid;

                        var problem =
                            new Problem
                            {
                                ObservationGuid = observationGuid,
                                PatientGuid = patientGuid,
                                OrganisationGuid = orgGuid,
                                ParentProblemObservationGuid = null,
                                Deleted = GetRandomValue(CareRecord_Problem, o => o.Deleted),
                                Comment = GetRandomValue(CareRecord_Problem, o => o.Comment),
                                EndDate = GetRandomValue(CareRecord_Problem, o => o.EndDate),
                                EndDatePrecision = GetRandomValue(CareRecord_Problem, o => o.EndDatePrecision),
                                ExpectedDuration = GetRandomValue(CareRecord_Problem, o => o.ExpectedDuration),
                                LastReviewDate = GetRandomValue(CareRecord_Problem, o => o.LastReviewDate),
                                LastReviewDatePrecision = GetRandomValue(CareRecord_Problem, o => o.LastReviewDatePrecision),
                                LastReviewUserInRoleGuid = GetRandomValue(userInRoleArray, o => o.UserInRoleGuid),
                                ParentProblemRelationship = GetRandomValue(CareRecord_Problem, o => o.ParentProblemRelationship),
                                ProblemStatusDescription = GetRandomValue(CareRecord_Problem, o => o.ProblemStatusDescription),
                                SignificanceDescription = GetRandomValue(CareRecord_Problem, o => o.SignificanceDescription),
                                ProcessingId = GetRandomValue(CareRecord_Problem, o => o.ProcessingId)
                            };

                        yield return problem;
                    }

                    var observation =
                        new Observation
                        {
                            ObservationGuid = observationGuid,
                            PatientGuid = patientGuid,
                            OrganisationGuid = orgGuid,
                            EffectiveDate = GetRandomValue(CareRecord_Observation, o => o.EffectiveDate),
                            EffectiveDatePrecision = GetRandomValue(CareRecord_Observation, o => o.EffectiveDatePrecision),
                            EnteredDate = GetRandomValue(CareRecord_Observation, o => o.EnteredDate),
                            EnteredTime = GetRandomValue(CareRecord_Observation, o => o.EnteredTime),
                            ClinicianUserInRoleGuid = GetRandomValue<UserInRole>(userInRoleArray, o => o.UserInRoleGuid),
                            EnteredByUserInRoleGuid = GetRandomValue<UserInRole>(userInRoleArray, o => o.UserInRoleGuid),
                            ParentObservationGuid = null,
                            CodeId = GetRandomValue(CareRecord_Observation, o => o.CodeId),
                            ProblemGuid = problemGuid,
                            ConsultationGuid = consultationGuid,
                            AssociatedText = GetRandomValue(CareRecord_Observation, o => o.AssociatedText),
                            Value = GetRandomValue(CareRecord_Observation, o => o.Value),
                            NumericUnit = GetRandomValue(CareRecord_Observation, o => o.NumericUnit),
                            ObservationType = GetRandomValue(CareRecord_Observation, o => o.ObservationType),
                            NumericRangeLow = GetRandomValue(CareRecord_Observation, o => o.NumericRangeLow),
                            NumericRangeHigh = GetRandomValue(CareRecord_Observation, o => o.NumericRangeHigh),
                            DocumentGuid = GetRandomValue(CareRecord_Observation, o => o.DocumentGuid),
                            Qualifiers = GetRandomValue(CareRecord_Observation, o => o.Qualifiers),
                            Abnormal = GetRandomValue(CareRecord_Observation, o => o.Abnormal),
                            AbnormalReason = GetRandomValue(CareRecord_Observation, o => o.AbnormalReason),
                            Episode = GetRandomValue(CareRecord_Observation, o => o.Episode),
                            Deleted = GetRandomValue(CareRecord_Observation, o => o.Deleted),
                            IsConfidential = GetRandomValue(CareRecord_Observation, o => o.IsConfidential),
                            NumericOperator = GetRandomValue(CareRecord_Observation, o => o.NumericOperator),
                            ProcessingId = GetRandomValue(CareRecord_Observation, o => o.ProcessingId),
                        };

                    yield return observation;

                    observationList.Add(observation);

                    if (consultationIndex % 12 == 0)
                    {
                        var drugRecord =
                            new DrugRecord
                            {
                                DrugRecordGuid = Guid.NewGuid().ToString(),
                                PatientGuid = patientGuid,
                                OrganisationGuid = orgGuid,
                                EffectiveDate = GetRandomValue(Prescribing_DrugRecord, o => o.EffectiveDate),
                                EffectiveDatePrecision = GetRandomValue(Prescribing_DrugRecord, o => o.EffectiveDatePrecision),
                                EnteredDate = GetRandomValue(Prescribing_DrugRecord, o => o.EnteredDate),
                                EnteredTime = GetRandomValue(Prescribing_DrugRecord, o => o.EnteredTime),
                                ClinicianUserInRoleGuid = GetRandomValue(userInRoleArray, o => o.UserInRoleGuid),
                                EnteredByUserInRoleGuid = GetRandomValue(userInRoleArray, o => o.UserInRoleGuid),
                                CodeId = GetRandomValue(Prescribing_DrugRecord, o => o.CodeId),
                                Dosage = GetRandomValue(Prescribing_DrugRecord, o => o.Dosage),
                                Quantity = GetRandomValue(Prescribing_DrugRecord, o => o.Quantity),
                                QuantityUnit = GetRandomValue(Prescribing_DrugRecord, o => o.QuantityUnit),
                                ProblemObservationGuid = problemGuid,
                                PrescriptionType = GetRandomValue(Prescribing_DrugRecord, o => o.PrescriptionType),
                                IsActive = GetRandomValue(Prescribing_DrugRecord, o => o.IsActive),
                                CancellationDate = GetRandomValue(Prescribing_DrugRecord, o => o.CancellationDate),
                                NumberOfIssues = GetRandomValue(Prescribing_DrugRecord, o => o.NumberOfIssues),
                                NumberOfIssuesAuthorised = GetRandomValue(Prescribing_DrugRecord, o => o.NumberOfIssuesAuthorised),
                                IsConfidential = GetRandomValue(Prescribing_DrugRecord, o => o.IsConfidential),
                                Deleted = GetRandomValue(Prescribing_DrugRecord, o => o.Deleted),
                                ProcessingId = GetRandomValue(Prescribing_DrugRecord, o => o.ProcessingId),
                            };

                        yield return drugRecord;

                        drugRecords.Add(drugRecord);
                    }
                }
            }


            var consultationArray = consultationList.ToArray();

            for (int diaryIndex = 0; diaryIndex < 6; diaryIndex++)
            {
                var consultation = GetRandomRecord(consultationArray);

                var diary =
                    new Diary
                    {
                        DiaryGuid = Guid.NewGuid().ToString(),
                        PatientGuid = consultation.PatientGuid,
                        OrganisationGuid = consultation.OrganisationGuid,
                        EffectiveDate = GetRandomValue(CareRecord_Diary, o => o.EffectiveDate),
                        EffectiveDatePrecision = GetRandomValue(CareRecord_Diary, o => o.EffectiveDatePrecision),
                        EnteredDate = GetRandomValue(CareRecord_Diary, o => o.EnteredDate),
                        EnteredTime = GetRandomValue(CareRecord_Diary, o => o.EnteredTime),
                        ClinicianUserInRoleGuid = GetRandomValue<UserInRole>(userInRoleArray, o => o.UserInRoleGuid),
                        EnteredByUserInRoleGuid = GetRandomValue<UserInRole>(userInRoleArray, o => o.UserInRoleGuid),
                        CodeId = GetRandomValue(CareRecord_Diary, o => o.CodeId),
                        OriginalTerm = GetRandomValue(CareRecord_Diary, o => o.OriginalTerm),
                        AssociatedText = GetRandomValue(CareRecord_Diary, o => o.AssociatedText),
                        DurationTerm = GetRandomValue(CareRecord_Diary, o => o.DurationTerm),
                        LocationTypeDescription = GetRandomValue(CareRecord_Diary, o => o.LocationTypeDescription),
                        Deleted = GetRandomValue(CareRecord_Diary, o => o.Deleted),
                        IsConfidential = GetRandomValue(CareRecord_Diary, o => o.IsConfidential),
                        IsActive = GetRandomValue(CareRecord_Diary, o => o.IsActive),
                        IsComplete = GetRandomValue(CareRecord_Diary, o => o.IsComplete),
                        ConsultationGuid = consultation.ConsultationGuid,
                        ProcessingId = GetRandomValue(CareRecord_Diary, o => o.ProcessingId)
                    };

                yield return diary;
            }

            var filteredDrugRecords = drugRecords.Where(dr => dr.ProblemObservationGuid != null).ToArray();

            for (int i = 0; i < 160 * 4; i++)
            {
                if (drugRecords.Count == 0)
                    continue;

                if (filteredDrugRecords.Length == 0)
                    continue;

                var drugRecord = GetRandomRecord(filteredDrugRecords.ToArray());

                yield return
                    new IssueRecord
                    {
                        IssueRecordGuid = Guid.NewGuid().ToString(),
                        PatientGuid = patientGuid,
                        OrganisationGuid = orgGuid,
                        DrugRecordGuid = drugRecord.DrugRecordGuid, //GetRandomValue(Prescribing_IssueRecord, o => o.DrugRecordGuid),
                        EffectiveDate = GetRandomValue(Prescribing_IssueRecord, o => o.EffectiveDate),
                        EffectiveDatePrecision = GetRandomValue(Prescribing_IssueRecord, o => o.EffectiveDatePrecision),
                        EnteredDate = GetRandomValue(Prescribing_IssueRecord, o => o.EnteredDate),
                        EnteredTime = GetRandomValue(Prescribing_IssueRecord, o => o.EnteredTime),
                        ClinicianUserInRoleGuid = GetRandomValue<UserInRole>(userInRoleArray, o => o.UserInRoleGuid),
                        EnteredByUserInRoleGuid = GetRandomValue<UserInRole>(userInRoleArray, o => o.UserInRoleGuid),
                        CodeId = GetRandomValue(Prescribing_IssueRecord, o => o.CodeId),
                        Dosage = GetRandomValue(Prescribing_IssueRecord, o => o.Dosage),
                        Quantity = GetRandomValue(Prescribing_IssueRecord, o => o.Quantity),
                        QuantityUnit = GetRandomValue(Prescribing_IssueRecord, o => o.QuantityUnit),
                        ProblemObservationGuid = drugRecord.ProblemObservationGuid, //GetRandomValue(Prescribing_IssueRecord, o => o.ProblemObservationGuid),
                        CourseDurationInDays = GetRandomValue(Prescribing_IssueRecord, o => o.CourseDurationInDays),
                        EstimatedNhsCost = GetRandomValue(Prescribing_IssueRecord, o => o.EstimatedNhsCost),
                        IsConfidential = GetRandomValue(Prescribing_IssueRecord, o => o.IsConfidential),
                        EmisCode = GetRandomValue(Prescribing_IssueRecord, o => o.EmisCode),
                        PatientMessage = GetRandomValue(Prescribing_IssueRecord, o => o.PatientMessage),
                        ScriptPharmacyStamp = GetRandomValue(Prescribing_IssueRecord, o => o.ScriptPharmacyStamp),
                        Compliance = GetRandomValue(Prescribing_IssueRecord, o => o.Compliance),
                        AverageCompliance = GetRandomValue(Prescribing_IssueRecord, o => o.AverageCompliance),
                        IsPrescribedAsContraceptive = GetRandomValue(Prescribing_IssueRecord, o => o.IsPrescribedAsContraceptive),
                        IsPrivatelyPrescribed = GetRandomValue(Prescribing_IssueRecord, o => o.IsPrivatelyPrescribed),
                        PharmacyMessage = GetRandomValue(Prescribing_IssueRecord, o => o.PharmacyMessage),
                        PharmacyText = GetRandomValue(Prescribing_IssueRecord, o => o.PharmacyText),
                        ConsultationGuid = observationList.Single(o => o.ObservationGuid == drugRecord.ProblemObservationGuid).ConsultationGuid,
                        ExpiryDate = GetRandomValue(Prescribing_IssueRecord, o => o.ExpiryDate),
                        ReviewDate = GetRandomValue(Prescribing_IssueRecord, o => o.ReviewDate),
                        Deleted = GetRandomValue(Prescribing_IssueRecord, o => o.Deleted),
                        ProcessingId = GetRandomValue(Prescribing_IssueRecord, o => o.ProcessingId),
                    };
            }
        }
    }

    private static void WriteRecords(string path, IEnumerable<object> records, string prefix, string postfix)
    {
        using var Admin_Organisation_writer = new StreamWriter(Path.Combine(path, $"{prefix}Admin_Organisation{postfix}"));
        using var Admin_Organisation_csv = new CsvWriter(Admin_Organisation_writer, CultureInfo.InvariantCulture);
        using var Admin_Patient_writer = new StreamWriter(Path.Combine(path, $"{prefix}Admin_Patient{postfix}"));
        using var Admin_Patient_csv = new CsvWriter(Admin_Patient_writer, CultureInfo.InvariantCulture);
        using var Admin_UserInRole_writer = new StreamWriter(Path.Combine(path, $"{prefix}Admin_UserInRole{postfix}"));
        using var Admin_UserInRole_csv = new CsvWriter(Admin_UserInRole_writer, CultureInfo.InvariantCulture);
        using var Appointment_Session_writer = new StreamWriter(Path.Combine(path, $"{prefix}Appointment_Session{postfix}"));
        using var Appointment_Session_csv = new CsvWriter(Appointment_Session_writer, CultureInfo.InvariantCulture);
        using var Appointment_Slot_writer = new StreamWriter(Path.Combine(path, $"{prefix}Appointment_Slot{postfix}"));
        using var Appointment_Slot_csv = new CsvWriter(Appointment_Slot_writer, CultureInfo.InvariantCulture);
        using var CareRecord_Consultation_writer = new StreamWriter(Path.Combine(path, $"{prefix}CareRecord_Consultation{postfix}"));
        using var CareRecord_Consultation_csv = new CsvWriter(CareRecord_Consultation_writer, CultureInfo.InvariantCulture);
        using var CareRecord_Diary_writer = new StreamWriter(Path.Combine(path, $"{prefix}CareRecord_Diary{postfix}"));
        using var CareRecord_Diary_csv = new CsvWriter(CareRecord_Diary_writer, CultureInfo.InvariantCulture);
        using var CareRecord_Observation_writer = new StreamWriter(Path.Combine(path, $"{prefix}CareRecord_Observation{postfix}"));
        using var CareRecord_Observation_csv = new CsvWriter(CareRecord_Observation_writer, CultureInfo.InvariantCulture);
        using var CareRecord_Problem_writer = new StreamWriter(Path.Combine(path, $"{prefix}CareRecord_Problem{postfix}"));
        using var CareRecord_Problem_csv = new CsvWriter(CareRecord_Problem_writer, CultureInfo.InvariantCulture);
        using var Prescribing_DrugRecord_writer = new StreamWriter(Path.Combine(path, $"{prefix}Prescribing_DrugRecord{postfix}"));
        using var Prescribing_DrugRecord_csv = new CsvWriter(Prescribing_DrugRecord_writer, CultureInfo.InvariantCulture);
        using var Prescribing_IssueRecord_writer = new StreamWriter(Path.Combine(path, $"{prefix}Prescribing_IssueRecord{postfix}"));
        using var Prescribing_IssueRecord_csv = new CsvWriter(Prescribing_IssueRecord_writer, CultureInfo.InvariantCulture);

        Admin_Organisation_csv.WriteHeader(typeof(Organisation));
        Admin_Organisation_csv.NextRecord();
        Admin_Patient_csv.WriteHeader(typeof(Patient));
        Admin_Patient_csv.NextRecord();
        Admin_UserInRole_csv.WriteHeader(typeof(UserInRole));
        Admin_UserInRole_csv.NextRecord();
        Appointment_Session_csv.WriteHeader(typeof(Session));
        Appointment_Session_csv.NextRecord();
        Appointment_Slot_csv.WriteHeader(typeof(Slot));
        Appointment_Slot_csv.NextRecord();
        CareRecord_Consultation_csv.WriteHeader(typeof(Consultation));
        CareRecord_Consultation_csv.NextRecord();
        CareRecord_Diary_csv.WriteHeader(typeof(Diary));
        CareRecord_Diary_csv.NextRecord();
        CareRecord_Observation_csv.WriteHeader(typeof(Observation));
        CareRecord_Observation_csv.NextRecord();
        CareRecord_Problem_csv.WriteHeader(typeof(Problem));
        CareRecord_Problem_csv.NextRecord();
        Prescribing_DrugRecord_csv.WriteHeader(typeof(DrugRecord));
        Prescribing_DrugRecord_csv.NextRecord();
        Prescribing_IssueRecord_csv.WriteHeader(typeof(IssueRecord));
        Prescribing_IssueRecord_csv.NextRecord();

        int rowCounter = 0;

        foreach (var record in records)
        {
            switch (record)
            {
                case Organisation:
                    Admin_Organisation_csv.WriteRecord(record);
                    Admin_Organisation_csv.NextRecord();
                    break;
                case Patient:
                    Admin_Patient_csv.WriteRecord(record);
                    Admin_Patient_csv.NextRecord();
                    break;
                case UserInRole:
                    Admin_UserInRole_csv.WriteRecord(record);
                    Admin_UserInRole_csv.NextRecord();
                    break;
                case Session:
                    Appointment_Session_csv.WriteRecord(record);
                    Appointment_Session_csv.NextRecord();
                    break;
                case Slot:
                    Appointment_Slot_csv.WriteRecord(record);
                    Appointment_Slot_csv.NextRecord();
                    break;
                case Consultation:
                    CareRecord_Consultation_csv.WriteRecord(record);
                    CareRecord_Consultation_csv.NextRecord();
                    break;
                case Diary:
                    CareRecord_Diary_csv.WriteRecord(record);
                    CareRecord_Diary_csv.NextRecord();
                    break;
                case Observation:
                    CareRecord_Observation_csv.WriteRecord(record);
                    CareRecord_Observation_csv.NextRecord();
                    break;
                case Problem:
                    CareRecord_Problem_csv.WriteRecord(record);
                    CareRecord_Problem_csv.NextRecord();
                    break;
                case DrugRecord:
                    Prescribing_DrugRecord_csv.WriteRecord(record);
                    Prescribing_DrugRecord_csv.NextRecord();
                    break;
                case IssueRecord:
                    Prescribing_IssueRecord_csv.WriteRecord(record);
                    Prescribing_IssueRecord_csv.NextRecord();
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Expected usage [required patient count] [path to sample data directory] [path to output directory]");
            return;
        }

        int patientsNeeded = int.Parse(args[0]);

        const int patientsPerPractice = 35000;

        decimal orgsRequired = (decimal)patientsNeeded / patientsPerPractice;

        var options = new ProgressBarOptions
        {
            ForegroundColor = ConsoleColor.Yellow,
            BackgroundColor = ConsoleColor.DarkYellow,
            ProgressCharacter = '─'
        };

        string pathDirectory = args[1];

        Admin_Organisation = GetSampleRecords(FindFilePath(pathDirectory, @"Admin_Organisation_"));
        Admin_Patient = GetSampleRecords(FindFilePath(pathDirectory, @"Admin_Patient_"));
        Admin_UserInRole = GetSampleRecords(FindFilePath(pathDirectory, @"Admin_UserInRole_"));
        Appointment_Session = GetSampleRecords(FindFilePath(pathDirectory, @"Appointment_Session_"));
        Appointment_Slot = GetSampleRecords(FindFilePath(pathDirectory, @"Appointment_Slot_"));
        CareRecord_Consultation = GetSampleRecords(FindFilePath(pathDirectory, @"CareRecord_Consultation_"));
        CareRecord_Diary = GetSampleRecords(FindFilePath(pathDirectory, @"CareRecord_Diary_"));
        CareRecord_Observation = GetSampleRecords(FindFilePath(pathDirectory, @"CareRecord_Observation_"));
        CareRecord_Problem = GetSampleRecords(FindFilePath(pathDirectory, @"CareRecord_Problem_"));
        Prescribing_DrugRecord = GetSampleRecords(FindFilePath(pathDirectory, @"Prescribing_DrugRecord_"));
        Prescribing_IssueRecord = GetSampleRecords(FindFilePath(pathDirectory, @"Prescribing_IssueRecord_"));

        using var progressBar = new ProgressBar((int)Math.Ceiling(orgsRequired), $"Generating EMIS data. Target population approximately {patientsNeeded}.", options);

        var records =
            ParallelEnumerable
                .Range(0, count: (int)Math.Ceiling(orgsRequired))
                .SelectMany(orgNumber => GetEmisObjects(progressBar, orgNumber, orgsRequired < 1 ? patientsNeeded : patientsPerPractice));


        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string guid = Guid.NewGuid().ToString();
        string number = random.Next(100000, 999999).ToString();

        string prefix = $"bulk_{number}_";
        string postfix = $"_{timestamp}_{guid}.csv";

        string outputDirectory = Path.Combine(args[2], guid, timestamp);

        Directory.CreateDirectory(outputDirectory);

        File.Copy(
            FindFilePath(pathDirectory, "Coding_ClinicalCode_"),
            Path.Combine(outputDirectory, $"{prefix}Coding_ClinicalCode{postfix}"));

        File.Copy(
            FindFilePath(pathDirectory, "Coding_ClinicalCode_"),
            Path.Combine(outputDirectory, $"{prefix}Coding_DrugCode{postfix}"));

        File.WriteAllLines(
            Path.Combine(outputDirectory, $"{prefix}Admin_Location{postfix}"),
            new[]
            {
                @"""LocationGuid"",""LocationName"",""LocationTypeDescription"",""ParentLocationGuid"",""OpenDate"",""CloseDate"",""MainContactName"",""FaxNumber"",""EmailAddress"",""PhoneNumber"",""HouseNameFlatNumber"",""NumberAndStreet"",""Village"",""Town"",""County"",""Postcode"",""Deleted"",""ProcessingId"""
            });

        File.WriteAllLines(
            Path.Combine(outputDirectory, $"{prefix}Admin_OrganisationLocation{postfix}"),
            new[]
            {
                @"""OrganisationGuid"",""LocationGuid"",""IsMainLocation"",""Deleted"",""ProcessingId"""
            });

        File.WriteAllLines(
            Path.Combine(outputDirectory, $"{prefix}Admin_PatientHistory{postfix}"),
            new[]
            {
                @"""PatientGuid"",""OrganisationGuid"",""HistoryDate"",""HistoryTime"",""StatusDescription"",""ProcessingId"""
            });

        File.WriteAllLines(
            Path.Combine(outputDirectory, $"{prefix}Agreements_SharingOrganisation{postfix}"),
            new[]
            {
                @"""OrganisationGuid"",""IsActivated"",""LastModifiedDate"",""Disabled"",""Deleted"""
            });

        File.WriteAllLines(
            Path.Combine(outputDirectory, $"{prefix}Audit_PatientAudit{postfix}"),
            new[]
            {
                @"""ItemGuid"",""PatientGuid"",""OrganisationGuid"",""ModifiedDate"",""ModifiedTime"",""UserInRoleGuid"",""ItemType"",""ModeType"",""ProcessingId"""
            });

        File.WriteAllLines(
            Path.Combine(outputDirectory, $"{prefix}Audit_RegistrationAudit{postfix}"),
            new[]
            {
                @"""PatientGuid"",""OrganisationGuid"",""ModifiedDate"",""ModifiedTime"",""UserInRoleGuid"",""ModeType"",""ProcessingId"""
            });

        File.WriteAllLines(
            Path.Combine(outputDirectory, $"{prefix}CareRecord_ObservationReferral{postfix}"),
            new[]
            {
                @"""ObservationGuid"",""PatientGuid"",""OrganisationGuid"",""ReferralTargetOrganisationGuid"",""ReferralUrgency"",""ReferralServiceType"",""ReferralMode"",""ReferralReceivedDate"",""ReferralReceivedTime"",""ReferralEndDate"",""ReferralSourceId"",""ReferralSourceOrganisationGuid"",""ReferralUBRN"",""ReferralReasonCodeId"",""ReferringCareProfessionalStaffGroupCodeId"",""ReferralEpisodeRTTMeasurementTypeId"",""ReferralEpisodeClosureDate"",""ReferralEpisodeDischargeLetterIssuedDate"",""ReferralClosureReasonCodeId"",""TransportRequired"",""ProcessingId"""
            });

        WriteRecords(outputDirectory, records, prefix, postfix);
    }
}