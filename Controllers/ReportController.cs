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

            var model = new List<Report>();
            model = _context.Report.Select(a => new Report
            {
                Reportid = a.Reportid,
                Patient = a.Patient,
                User = a.User,
                AdmitDate = a.AdmitDate,
                DischargeDate = a.DischargeDate,
                Diagnosis = a.Diagnosis,
                Prescription = a.Prescription,
                DiagnosisCharges = a.DiagnosisCharges,
                ExtraCharges = a.ExtraCharges,
                RoomType = a.RoomType,
                RoomCharges = a.RoomCharges,
                PerDayRoom = a.PerDayRoom,
                TotalRoomPrice = a.TotalRoomPrice,
                MediclaimName = a.MediclaimName,
                InsuranceNumber = a.InsuranceNumber,
            }).ToList();

            return View("Report",model);
        }

        [HttpPost]
        public IActionResult AddReport(ReportDto report)
        {
            ViewBag.isreport = "active";
            if(ModelState.IsValid) 
            {
                try
                {
                    var Report = new Report
                    {
                        Patientid = report.Patientid,
                        Id = report.Id,
                        AdmitDate = report.AdmitDate,
                        DischargeDate = report.DischargeDate,
                        Prescription = report.Prescription,
                        DiagnosisCharges = report.DiagnosisCharges,
                        RoomType = report.RoomType,
                        RoomCharges = report.RoomCharges,
                        PerDayRoom = report.PerDayRoom,
                        TotalRoomPrice = report.TotalRoomPrice,
                        MediclaimName = report.MediclaimName,
                        InsuranceNumber = report.InsuranceNumber,
                    };
                    if (report.MultipleDiagnosis != null)
                    {
                        Report.Diagnosis = string.Join(",", report.MultipleDiagnosis);
                    }
                    if (report.MultipleExtraCharges != null)
                    {
                        Report.ExtraCharges = string.Join(",", report.MultipleExtraCharges);
                    }
                    _context.Report.Add(Report);
                    _context.SaveChanges();
                    TempData["Message"] = "In-Patient Department Report Added Successfully....";
                }
                catch (Exception ex) 
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View("AddReport",report);
        }

        [HttpPost]
        public IActionResult EditReport(ReportDto report)
        {
            ViewBag.isreport = "active";
            if(ModelState.IsValid)
            {
                var Report = _context.Report.Find(report.Reportid);
                if (Report != null) 
                {
                    Report.Reportid = report.Reportid;
                    Report.Patientid = report.Patientid;
                    Report.Id = report.Id;
                    Report.AdmitDate = report.AdmitDate;
                    Report.DischargeDate = report.DischargeDate;
                    if (report.MultipleDiagnosis != null)
                    {
                        Report.Diagnosis = string.Join(",", report.MultipleDiagnosis);
                    }
                    Report.Prescription = report.Prescription;
                    Report.DiagnosisCharges = report.DiagnosisCharges;
                    if (report.MultipleExtraCharges != null)
                    {
                        Report.ExtraCharges = string.Join(",", report.MultipleExtraCharges);
                    }
                    Report.RoomType = report.RoomType;
                    Report.RoomCharges = report.RoomCharges;
                    Report.PerDayRoom = report.PerDayRoom;
                    Report.TotalRoomPrice = report.TotalRoomPrice;
                    Report.MediclaimName = report.MediclaimName;
                    Report.InsuranceNumber = report.InsuranceNumber;
                    _context.Report.Update(Report);
                    _context.SaveChanges();
                    TempData["Message"] = "In-Patient Department Report Updated Successfully....";
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            return View("EditReport",report);
        }

        public IActionResult DeleteReport(int Reportid)
        {
            var report = _context.Report.Find(Reportid);
            if(report != null)
            {
                _context.Report.Remove(report);
                _context.SaveChanges();

                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public IActionResult Create(ReportDto report)
        {
            ViewBag.isreport = "active";
            return View("AddReport",report);
        }

        public IActionResult Edit(int Id)
        {
            ViewBag.isreport = "active";
            var editreport = _context.Report.Where(r => r.Reportid == Id).FirstOrDefault();
            if (editreport == null)
            {
                return NotFound();
            }

            var Diagnoses = editreport.Diagnosis?.Split(',').ToList();
            var ExtraCharges = editreport.ExtraCharges?.Split(',').ToList();

            var reportDto = new ReportDto
            {
                Reportid = editreport.Reportid,
                Patientid = editreport.Patientid,
                Id = editreport.Id,
                AdmitDate = editreport.AdmitDate,
                DischargeDate = editreport.DischargeDate,
                Prescription = editreport.Prescription,
                DiagnosisCharges = editreport.DiagnosisCharges,
                RoomType = editreport.RoomType,
                RoomCharges = editreport.RoomCharges,
                PerDayRoom = editreport.PerDayRoom,
                TotalRoomPrice = editreport.TotalRoomPrice,
                MediclaimName = editreport.MediclaimName,
                InsuranceNumber = editreport.InsuranceNumber,
                MultipleDiagnosis = Diagnoses,
                MultipleExtraCharges = ExtraCharges,
            };

            return View("EditReport", reportDto);
        }

        public IActionResult Delete(Report report)
        {
            var deletereport = _context.Report.Where(r => r.Reportid == report.Reportid)
                .Select(r => new Report
                {
                    Reportid = r.Reportid,
                    Patientid = r.Patientid,
                    Id = r.Id,
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
                }).FirstOrDefault();

            return View("DeleteReport", deletereport);
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
    }
}
    