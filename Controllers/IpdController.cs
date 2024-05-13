using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StanfordHospital.Data;
using StanfordHospital.Models;
using StanfordHospital.Models.Dtos;

namespace StanfordHospital.Controllers
{
    public class IpdController : Controller
    {
        private readonly ILogger<IpdController> _logger;
        private readonly ApplicationDbContext _context;

        public IpdController(ILogger<IpdController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index() 
        {
            ViewBag.isipd = "active";

            var model = new List<Ipd>();
            model = _context.Ipd.Select(a => new Ipd
            {
                Ipdid = a.Ipdid,
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
                RoomType = a.RoomType,
                RoomCharges = a.RoomCharges,
            }).ToList();

            return View("Ipd",model);
        }

        [HttpPost]
        public IActionResult AddIpd(Ipd ipd) 
        {
            ViewBag.isipd = "active";
            if (ModelState.IsValid)
            {
                var Ipd = new Ipd
                {
                    Patientid = ipd.Patientid,
                    Id = ipd.Id,
                    AppointmentDate = ipd.AppointmentDate,
                    AppointmentTime = ipd.AppointmentTime,
                    AppointmentStatus = ipd.AppointmentStatus,
                    Diagnosis = ipd.Diagnosis,
                    Prescription = ipd.Prescription,
                    ReasonForAppointment = ipd.ReasonForAppointment,
                    Cases = ipd.Cases,
                    Price = ipd.Price,
                    DiagnosisCharges = ipd.DiagnosisCharges,
                    ExtraCharges = ipd.ExtraCharges,
                    RoomType = ipd.RoomType,
                    RoomCharges = ipd.RoomCharges,
                };

                if (Ipd.Ipdid != 0)
                {
                    //Edit
                    var ipds = _context.Ipd.Find(ipd.Ipdid);
                    if (ipds != null)
                    {
                        ipd.Patientid = ipd.Patientid;
                        ipd.Id = ipd.Id;
                        ipd.AppointmentDate = ipd.AppointmentDate;
                        ipd.AppointmentTime = ipd.AppointmentTime;
                        ipd.AppointmentStatus = ipd.AppointmentStatus;
                        ipd.Diagnosis = ipd.Diagnosis;
                        ipd.Prescription = ipd.Prescription;
                        ipd.ReasonForAppointment = ipd.ReasonForAppointment;
                        ipd.Cases = ipd.Cases;
                        ipd.Price = ipd.Price;
                        ipd.DiagnosisCharges = ipd.DiagnosisCharges;
                        ipd.ExtraCharges = ipd.ExtraCharges;
                        ipd.RoomType = ipd.RoomType;
                        ipd.RoomCharges = ipd.RoomCharges;
                        _context.SaveChanges();
                        TempData["Message"] = "In-Patient Department Updated Successfully....";
                    }
                }
                else
                {
                    //Create
                    _context.Ipd.Add(Ipd);
                    _context.SaveChanges();
                    TempData["Message"] = "In-Patient Department Added Successfully....";
                }
                return RedirectToAction("Index");
            }
            return View("AddIpd", ipd);
        }

        [HttpPost]
        public IActionResult EditIpd(IpdDto ipd)
        {
            ViewBag.isipd = "active";
            if (ModelState.IsValid)
            {
                var Ipd = _context.Ipd.Find(ipd.Ipdid);
                if (Ipd != null)
                {
                    Ipd.Ipdid = ipd.Ipdid;
                    Ipd.Patientid = ipd.Patientid;
                    Ipd.Id = ipd.Id;
                    Ipd.AppointmentDate = ipd.AppointmentDate;
                    Ipd.AppointmentTime = ipd.AppointmentTime;
                    Ipd.AppointmentStatus = ipd.AppointmentStatus;
                    if (ipd.MultipleDiagnosis != null)
                    {
                        Ipd.Diagnosis = string.Join(",", ipd.MultipleDiagnosis);
                    }
                    Ipd.Prescription = ipd.Prescription;
                    Ipd.ReasonForAppointment = ipd.ReasonForAppointment;
                    Ipd.Cases = ipd.Cases;
                    Ipd.Price = ipd.Price;
                    Ipd.DiagnosisCharges = ipd.DiagnosisCharges;
                    if (ipd.MultipleExtraCharges != null)
                    {
                        Ipd.ExtraCharges = string.Join(",", ipd.MultipleExtraCharges);
                    }
                    if (ipd.MultipleRoomType != null)
                    {
                        Ipd.RoomType = string.Join(",", ipd.MultipleRoomType);
                    }
                    Ipd.RoomCharges = ipd.RoomCharges;
                    _context.Ipd.Update(Ipd);
                    _context.SaveChanges();
                    TempData["Message"] = "In-Patient Department Updated Successfully....";
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            return View("EditIpd", ipd);
        }

        public IActionResult DeleteIpd(int Ipdid) 
        {
            var ipd = _context.Ipd.Find(Ipdid);
            if (ipd != null)
            {
                _context.Ipd.Remove(ipd);
                _context.SaveChanges();

                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public IActionResult Create(Ipd ipd) 
        {
            ViewBag.isipd = "active";
            var patients = _context.Patient.Select(p => new {
                p.Patientid,
                FullName = $"{p.Firstname} {p.Lastname}"
            }).ToList();
            ViewBag.Patients = new SelectList(patients, "Patientid", "FullName");
            //var doctors = _context.Users.Where(u => u.Role == "doctor")
            //                   .Select(u => new
            //                   {
            //                       Id = u.Id,
            //                       FullName = $"Dr. {u.FirstName} {u.LastName}"
            //                   })
            //                   .ToList();
            //ViewBag.Doctors = new SelectList(doctors, "Id", "FullName");
            //ViewBag.Users = new SelectList(_context.Users, "Id", "FirstName");
            return View("AddIpd", ipd);
        }

        public IActionResult Edit(int Id) 
        {
            ViewBag.isipd = "active";
            var patients = _context.Patient.Select(p => new {
                p.Patientid,
                FullName = $"{p.Firstname} {p.Lastname}"
            }).ToList();
            ViewBag.Patients = new SelectList(patients, "Patientid", "FullName");
            //var doctors = _context.Users.Where(u => u.Role == "doctor")
            //                   .Select(u => new
            //                   {
            //                       Id = u.Id,
            //                       FullName = $"Dr. {u.FirstName} {u.LastName}"
            //                   })
            //                   .ToList();
            //ViewBag.Doctors = new SelectList(doctors, "Id", "FullName");
            var editipds = _context.Ipd.Where(a => a.Ipdid == Id)
               .Select(a => new IpdDto
               {
                   Ipdid = a.Ipdid,
                   Patientid = a.Patientid,
                   Id = a.Id,
                   AppointmentDate = a.AppointmentDate,
                   AppointmentTime = a.AppointmentTime,
                   AppointmentStatus = a.AppointmentStatus,
                   //Diagnosis = a.Diagnosis,
                   Prescription = a.Prescription,
                   ReasonForAppointment = a.ReasonForAppointment,
                   Cases = a.Cases,
                   Price = a.Price,
                   DiagnosisCharges = a.DiagnosisCharges,
                   //ExtraCharges = a.ExtraCharges,
                   //RoomType = a.RoomType,
                   RoomCharges = a.RoomCharges,
               }).FirstOrDefault();

            return View("EditIpd", editipds);
        }

        public IActionResult Delete(Ipd ipd)
        {
            var deleteipd = _context.Ipd.Where(a => a.Ipdid == ipd.Ipdid)
                .Select(a => new Ipd
                {
                    Ipdid = a.Ipdid,
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
                    RoomType = a.RoomType,
                    RoomCharges = a.RoomCharges,
                }).FirstOrDefault();

            return View("DeleteIpd", deleteipd);
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
            var patients = _context.Patient.Select(p => new {
                p.Patientid,
                FullName = $"{p.Firstname} {p.Lastname}"
            }).ToList();

            return Json(patients);
        }
    }
}
