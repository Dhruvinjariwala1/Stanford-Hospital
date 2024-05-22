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
    }
}
    