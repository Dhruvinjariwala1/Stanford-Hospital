using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StanfordHospital.Data;
using StanfordHospital.Models;
using StanfordHospital.Models.Dtos;

namespace StanfordHospital.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly ILogger <ReportController> _logger;
        private readonly ApplicationDbContext _context;

        public ReportController(ILogger<ReportController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.isreport = "active";

            var model = new List<Ipd>();
            model = _context.Ipd.Select(a => new Ipd
            {
                Ipdid = a.Ipdid,
                Patient = a.Patient,
                User = a.User,
                AdmitDate = a.AdmitDate,
                DischargeDate = a.DischargeDate,
                DiagnosisCharges = a.DiagnosisCharges,
                ExtraCharges = a.ExtraCharges,
                RoomType = a.RoomType,
                RoomCharges = a.RoomCharges,
                PerDayRoom = a.PerDayRoom,
                TotalRoomPrice = a.TotalRoomPrice,
                MediclaimName = a.MediclaimName,
                CashLess = a.CashLess,
            }).ToList();

            return View("Report",model);
        }

        public IActionResult IndexAppointmentReport()
        {
            ViewBag.isappointmentreport = "active";

            var model = new List<Appointment>();
            model = _context.Appointment.Select(a => new Appointment
            {
                Appointmentid = a.Appointmentid,
                Patient = a.Patient,
                User = a.User,
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                AppointmentStatus = a.AppointmentStatus,
                Diagnosis = a.Diagnosis,
                Prescription = a.Prescription,
                ReasonForAppointment = a.ReasonForAppointment,
                Cases = a.Cases,
                Price = a.Price,
                DiagnosisCharges = a.DiagnosisCharges,
                ExtraCharges = a.ExtraCharges,
            }).ToList();

            return View("AppointmentReport", model);
        }

        public IActionResult PatientIpdReportHistroy(PatientReportDto patientReport)
        {
            ViewBag.patientipdreport = "active";

           //var model = _context.Ipd.Select(a => new PatientReportDto
           //    {
           //         Type = "Ipd",
           //         Ipdid = a.Ipdid,
           //         Patient = a.Patient,
           //         User = a.User,
           //         AdmitDate = a.AdmitDate,
           //         DischargeDate = a.DischargeDate,
           //         DiagnosisCharges = a.DiagnosisCharges,
           //         RoomType = a.RoomType,
           //         RoomCharges = a.RoomCharges,
           //         PerDayRoom = a.PerDayRoom,
           //         TotalRoomPrice = a.TotalRoomPrice,
           //         MediclaimName = a.MediclaimName,
           //         InsuranceNumber = a.InsuranceNumber,
           //     }).ToList();

           // if(patientReport.MultipleDiagnosis != null)
           // {
           //     model.ForEach(item => item.MultipleDiagnosis = patientReport.MultipleDiagnosis);
           // }

           // if(patientReport.MultipleExtraCharges != null) 
           // {
           //     model.ForEach(item => item.MultipleExtraCharges = patientReport.MultipleExtraCharges);
           // }

           // var appointmentList = _context.Appointment.Select(a => new PatientReportDto
           // {
           //     Type = "Appointment",
           //     Appointmentid = a.Appointmentid,
           //     Patient = a.Patient,
           //     User = a.User,
           //     AppointmentDate = a.AppointmentDate,
           //     AppointmentTime = a.AppointmentTime,
           //     AppointmentStatus = a.AppointmentStatus,
           //     ReasonForAppointment = a.ReasonForAppointment,
           //     Prescription = a.Prescription,
           //     Cases = a.Cases,
           //     Price = a.Price,
           //     DiagnosisCharges = a.DiagnosisCharges,      
           // }).ToList();

           // if (patientReport.MultipleDiagnosis != null)
           // {
           //     model.ForEach(item => item.MultipleDiagnosis = patientReport.MultipleDiagnosis);
           // }

           // if (patientReport.MultipleExtraCharges != null)
           // {
           //     model.ForEach(item => item.MultipleExtraCharges = patientReport.MultipleExtraCharges);
           // }

            var model = new List<PatientReportDto>();

            return View("PatientIpdHistroy", model);
        }
        
        public IActionResult DoctorRole()
        {
            var doctors = _context.Users.Where(u => u.Role == "doctor")
                              .Select(u => new
                              {
                                  Id = u.Id,
                                  FullName = $"Dr. {u.FirstName} {u.LastName}"
                              })
                              .ToList();
            return Json(doctors);
        }

        public IActionResult PatientName()
        {
            var patients = _context.Patient.Select(p => new
            {
                p.Patientid,
                FullName = $"{p.Firstname} {p.Lastname}"
            }).ToList();

            return Json(patients);
        }

        public IActionResult SearchReport(int? patientId, string? doctorId, string roomType, DateTime fromDate, DateTime toDate)
        {
            var reports = (from r in _context.Ipd
                          join p in _context.Patient on r.Patientid equals p.Patientid into patientGroup
                          from patient in patientGroup.DefaultIfEmpty()
                          join u in _context.Users on r.Id equals u.Id into doctorGroup
                          from doctor in doctorGroup.DefaultIfEmpty()
                          //where (patientId != null && r.Patientid == patientId)
                          //   && (doctorId != null && r.Id == doctorId)
                          //   && (!string.IsNullOrEmpty(roomType) && r.RoomType == roomType)
                             //&& (r.AdmitDate.Date >= fromDate.Date && r.AdmitDate.Date <= toDate.Date)
                          select new 
                          {
                              //Report = r,
                              Ipdid = r.Ipdid,
                              r.Id,
                              r.Patientid,
                              Patient = r.Patient,
                              User = r.User,
                              AdmitDate = r.AdmitDate,
                              DischargeDate = r.DischargeDate,
                              Diagnosis = r.Diagnosis,
                              Prescription = r.Prescription,
                              DiagnosisCharges = r.DiagnosisCharges,
                              ExtraCharges = r.ExtraCharges,
                              RoomType = r.RoomType,
                              RoomCharges = r.RoomCharges,
                              PerDayRoom = r.PerDayRoom,
                              TotalRoomPrice = r.TotalRoomPrice,
                              MediclaimName = r.MediclaimName,
                              InsuranceNumber = r.InsuranceNumber,
                              patientFullname = (patient.Firstname + " " + patient.Lastname),
                              doctorfullname = (doctor.FirstName + " " + doctor.LastName)
                          }).ToList();

            if (patientId != null)
            {
                reports = reports.Where(x => x.Patientid == patientId).ToList();
            }

            if (doctorId != null)
            {
                reports = reports.Where(x => x.Id == doctorId).ToList();
            }

            if (fromDate !=DateTime.MinValue && toDate != DateTime.MinValue)
            {
                reports = reports.Where(x => (fromDate.Date <= x.AdmitDate.Date) && (x.DischargeDate.Date <= toDate.Date)).ToList(); 
            }

            if (roomType != null)
            {
                reports = reports.Where(x => x.RoomType == roomType).ToList();
            }

            return Json(reports);
        }

        public IActionResult AppointmentReport(int? patientId, string? doctorId, string? status, string? cases, DateTime fromDate, DateTime toDate)
        {
            var appointment = (from a in _context.Appointment
                               join p in  _context.Patient on a.Patientid equals p.Patientid into patientgroup
                               from patient in patientgroup.DefaultIfEmpty()
                               join u in _context.Users on a.Id equals u.Id into doctorgroup
                               from doctor in doctorgroup.DefaultIfEmpty()
                               select new 
                               {
                                   Appointmentid = a.Appointmentid,
                                   a.Id,
                                   a.Patientid,
                                   Patient = a.Patient,
                                   User = a.User,
                                   AppointmentDate = a.AppointmentDate,
                                   AppointmentTime = a.AppointmentTime,
                                   AppointmentStatus = a.AppointmentStatus,
                                   ReasonForAppointment = a.ReasonForAppointment,
                                   Cases = a.Cases,
                                   Price = a.Price,
                                   DiagnosisCharges = a.DiagnosisCharges,
                                   ExtraCharges = a.ExtraCharges,
                                   patientFullname = (patient.Firstname + " " + patient.Lastname),
                                   doctorfullname = (doctor.FirstName + " " + doctor.LastName)
                               }).ToList();

            if (patientId != null)
            {
                appointment = appointment.Where(x => x.Patientid == patientId).ToList();
            }

            if (doctorId != null)
            {
                appointment = appointment.Where(x => x.Id == doctorId).ToList();
            }

            if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
            {
                appointment = appointment.Where(x => (fromDate.Date <= x.AppointmentDate.Date) && (x.AppointmentDate.Date <= toDate.Date)).ToList();
            }

            if (status != null)
            {
                appointment = appointment.Where(x => x.AppointmentStatus == status).ToList();
            }

            if (cases != null)
            {
                appointment = appointment.Where(x => x.Cases == cases).ToList();
            }

            return Json(appointment);
        }
    
        public IActionResult PatientHistory(int patientId)
        {
            var patienthistory = new List<PatientReportDto>();

            var model = _context.Ipd.Where(a => a.Patientid == patientId)
                .Select(a => new PatientReportDto
               {
                     Type = "Ipd",
                     Ipdid = a.Ipdid,
                     Patient = a.Patient,
                     User = a.User,
                     AdmitDate = a.AdmitDate,
                     DischargeDate = a.DischargeDate,
                     DiagnosisCharges = a.DiagnosisCharges,
                     MultipleExtraCharges = new List<string> {a.ExtraCharges },
                     RoomType = a.RoomType,
                     RoomCharges = a.RoomCharges,
                     PerDayRoom = a.PerDayRoom,
                     TotalRoomPrice = a.TotalRoomPrice,
                     MediclaimName = a.MediclaimName,
                     CashLess = a.CashLess,
                     InsuranceNumber = a.InsuranceNumber,
                     AppointmentStatus = "",
                     AppointmentTime = default(DateTime),
                     Price = default(int),
                     Cases = "",
                     ReasonForAppointment = "",
                 }).ToList();

             //if(patientReport.MultipleDiagnosis != null)
             //{
             //   model.ForEach(item => item.MultipleDiagnosis = patientReport.MultipleDiagnosis);
             //}

             //if(patientReport.MultipleExtraCharges != null) 
             //{
             //   model.ForEach(item => item.MultipleExtraCharges = patientReport.MultipleExtraCharges);
             //}

            patienthistory.AddRange(model);

             var appointmentList = _context.Appointment.Where(a => a.Patientid == patientId)
                .Select(a => new PatientReportDto
             {
                 Type = "Appointment",
                 Appointmentid = a.Appointmentid,
                 Patient = a.Patient,
                 User = a.User,
                 AppointmentDate = a.AppointmentDate,
                 AppointmentTime = a.AppointmentTime,
                 AppointmentStatus = a.AppointmentStatus,
                 MultipleExtraCharges = new List<string> {a.ExtraCharges },
                 ReasonForAppointment = a.ReasonForAppointment,
                 Prescription = a.Prescription,
                 Cases = a.Cases,
                 Price = a.Price,
                 DiagnosisCharges = a.DiagnosisCharges,    
                 RoomType = "",
                 RoomCharges = default(int),
                 PerDayRoom = default(int),
                 TotalRoomPrice = default(int),
                 MediclaimName = "",
                 CashLess = "",
                 InsuranceNumber = "",
                 AdmitDate = default(DateTime),
                 DischargeDate = default(DateTime),
             }).ToList();

             //if (patientReport.MultipleDiagnosis != null)
             //{
             //    model.ForEach(item => item.MultipleDiagnosis = patientReport.MultipleDiagnosis);
             //}

             //if (patientReport.MultipleExtraCharges != null)
             //{
             //   model.ForEach(item => item.MultipleExtraCharges = patientReport.MultipleExtraCharges);
             //}

             patienthistory.AddRange(appointmentList);

             return Json(patienthistory);
        }
    }
}
    