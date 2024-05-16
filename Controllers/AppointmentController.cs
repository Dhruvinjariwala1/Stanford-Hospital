using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StanfordHospital.Data;
using StanfordHospital.Models;
using StanfordHospital.Models.Dtos;
using System.Data;
using System.Security.Claims;

namespace StanfordHospital.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly ApplicationDbContext _context;

        public AppointmentController(ILogger<AppointmentController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult AppointmentPrint(int id)
        {
            var model = new Appointment();
            model = _context.Appointment.Where(x => x.Appointmentid == id).Select(a => new Appointment
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
            }).FirstOrDefault();
            return View(model);
        }

        public IActionResult Doctor()
        {
            ViewBag.isdoctor = "active";

            var loggedInDoctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var currentDate = DateTime.Today;

            var model = _context.Appointment
                .Where(a => a.AppointmentDate.Date == currentDate && a.User.Id == loggedInDoctorId)
                .Select(a => new Appointment
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
                })
                .ToList();

            return View("Appointment", model);
        }

        public IActionResult Index()
        {
            ViewBag.isappointment = "active";

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

            return View("Appointment", model);
        }

        [HttpPost]
        public IActionResult AddAppointment(AppointmentDto appointment)
        {
            ViewBag.isappointment = "active";
            if (ModelState.IsValid)
            {
                try
                {
                    //Create
                    var Appointment = new Appointment
                    {
                        Patientid = appointment.Patientid,
                        Id = appointment.Id,
                        AppointmentDate = appointment.AppointmentDate,
                        AppointmentTime = appointment.AppointmentTime,
                        AppointmentStatus = appointment.AppointmentStatus,
                        Prescription = appointment.Prescription,
                        ReasonForAppointment = appointment.ReasonForAppointment,
                        Cases = appointment.Cases,
                        Price = appointment.Price,
                        DiagnosisCharges = appointment.DiagnosisCharges,
                    };

                    if (appointment.MultipleDiagnosis != null)
                    {
                        Appointment.Diagnosis = string.Join(",", appointment.MultipleDiagnosis);
                    }
                    if (appointment.MultipleExtraCharges != null)
                    {
                        Appointment.ExtraCharges = string.Join(",", appointment.MultipleExtraCharges);
                    }
                    _context.Appointment.Add(Appointment);
                    _context.SaveChanges();
                    TempData["Message"] = "Appointment Added Successfully....";
                }
                catch (Exception ex)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View("AddAppointment", appointment);
        }

        [HttpPost]
        public IActionResult EditAppointment(AppointmentDto appointment)
        {
            ViewBag.isappointment = "active";
            if (ModelState.IsValid)
            {
                var Appointment = _context.Appointment.Find(appointment.Appointmentid);
                if (Appointment != null)
                {
                    Appointment.Appointmentid = appointment.Appointmentid;
                    Appointment.Patientid = appointment.Patientid;
                    Appointment.Id = appointment.Id;
                    Appointment.AppointmentDate = appointment.AppointmentDate;
                    Appointment.AppointmentTime = appointment.AppointmentTime;
                    Appointment.AppointmentStatus = appointment.AppointmentStatus;
                    if (appointment.MultipleDiagnosis != null)
                    {
                        Appointment.Diagnosis = string.Join(",", appointment.MultipleDiagnosis);
                    }
                    Appointment.Prescription = appointment.Prescription;
                    Appointment.ReasonForAppointment = appointment.ReasonForAppointment;
                    Appointment.Cases = appointment.Cases;
                    Appointment.Price = appointment.Price;
                    Appointment.DiagnosisCharges = appointment.DiagnosisCharges;
                    if (appointment.MultipleExtraCharges != null)
                    {
                        Appointment.ExtraCharges = string.Join(",", appointment.MultipleExtraCharges);
                    }
                    _context.Appointment.Update(Appointment);
                    _context.SaveChanges();
                    TempData["Message"] = "Appointment Updated Successfully....";
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            return View("EditAppointment", appointment);
        }

        public IActionResult DeleteAppointment(int Appointmentid)
        {
            var appointment = _context.Appointment.Find(Appointmentid);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
                _context.SaveChanges();

                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public IActionResult Create(AppointmentDto appointment)
        {
            ViewBag.isappointment = "active";
            return View("AddAppointment", appointment);
        }

        public IActionResult Edit(int Id)
        {
            ViewBag.isappointment = "active";
            

            var editappointment = _context.Appointment.Where(e => e.Appointmentid == Id).FirstOrDefault();
            if (editappointment == null)
            {
                return NotFound();
            }

            var Diagnoses = editappointment.Diagnosis?.Split(',').ToList();
            var ExtraCharges = editappointment.ExtraCharges?.Split(',').ToList();

            var AppointmentDto = _context.Appointment.Where(a => a.Appointmentid == Id)
                .Select(a => new AppointmentDto
                {
                    Appointmentid = editappointment.Appointmentid,
                    Patientid = editappointment.Patientid,
                    Id = editappointment.Id,
                    AppointmentDate = editappointment.AppointmentDate,
                    AppointmentTime = editappointment.AppointmentTime,
                    AppointmentStatus = editappointment.AppointmentStatus,
                    MultipleDiagnosis = Diagnoses,
                    Prescription = editappointment.Prescription,
                    ReasonForAppointment = editappointment.ReasonForAppointment,
                    Cases = editappointment.Cases,
                    Price = editappointment.Price,
                    DiagnosisCharges = editappointment.DiagnosisCharges,
                    MultipleExtraCharges = ExtraCharges
                }).FirstOrDefault();

            return View("EditAppointment", AppointmentDto);
        }

        public IActionResult Delete(Appointment appointment)
        {
            var deleteappointment = _context.Appointment.Where(a => a.Appointmentid == appointment.Appointmentid)
                .Select(a => new Appointment
                {
                    Appointmentid = a.Appointmentid,
                    Patientid = a.Patientid,
                    Id = a.Id,
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
                }).FirstOrDefault();

            return View("DeleteAppointment", deleteappointment);
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
