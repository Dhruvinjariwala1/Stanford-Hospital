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
                AdmitDate = a.AdmitDate,
                DischargeDate = a.DischargeDate,
                Diagnosis = a.Diagnosis,
                Prescription = a.Prescription,
                DiagnosisCharges = a.DiagnosisCharges,
                ExtraCharges = a.ExtraCharges,
                RoomType = a.RoomType,
                RoomCharges = a.RoomCharges,
                PerDayRoom = a.PerDayRoom,
                TotalRoomPrice = a.TotalRoomPrice ,
            }).ToList();

            return View("Ipd",model);
        }

        [HttpPost]
        public IActionResult AddIpd(IpdDto ipd) 
        {
            ViewBag.isipd = "active";
            if (ModelState.IsValid)
            {
                var Ipd = new Ipd
                {
                    Patientid = ipd.Patientid,
                    Id = ipd.Id,
                    AdmitDate = ipd.AdmitDate,
                    DischargeDate = ipd.DischargeDate,
                    //Diagnosis = ipd.Diagnosis,
                    Prescription = ipd.Prescription,
                    DiagnosisCharges = ipd.DiagnosisCharges,
                    //ExtraCharges = ipd.ExtraCharges,
                    RoomType = ipd.RoomType,
                    RoomCharges = ipd.RoomCharges,
                    PerDayRoom = ipd.PerDayRoom,
                    TotalRoomPrice = ipd.TotalRoomPrice,
                };

                if (Ipd.Ipdid != 0)
                {
                    //Edit
                    var ipds = _context.Ipd.Find(ipd.Ipdid);
                    if (ipds != null)
                    {
                        ipd.Patientid = ipd.Patientid;
                        ipd.Id = ipd.Id;
                        ipd.AdmitDate = ipd.AdmitDate;
                        ipd.DischargeDate = ipd.DischargeDate;
                        if (ipd.MultipleDiagnosis != null)
                        {
                            Ipd.Diagnosis = string.Join(",", ipd.MultipleDiagnosis);
                        }
                        ipd.Prescription = ipd.Prescription;
                        ipd.DiagnosisCharges = ipd.DiagnosisCharges;
                        if (ipd.MultipleExtraCharges != null)
                        {
                            Ipd.ExtraCharges = string.Join(",", ipd.MultipleExtraCharges);
                        }
                        ipd.RoomType= ipd.RoomType;
                        ipd.RoomCharges = ipd.RoomCharges;
                        ipd.PerDayRoom = ipd.PerDayRoom;
                        ipd.TotalRoomPrice = ipd.TotalRoomPrice;
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
                    Ipd.AdmitDate = ipd.AdmitDate;
                    Ipd.DischargeDate = ipd.DischargeDate;
                    if (ipd.MultipleDiagnosis != null)
                    {
                        Ipd.Diagnosis = string.Join(",", ipd.MultipleDiagnosis);
                    }
                    Ipd.Prescription = ipd.Prescription;
                    Ipd.DiagnosisCharges = ipd.DiagnosisCharges;
                    if (ipd.MultipleExtraCharges != null)
                    {
                        Ipd.ExtraCharges = string.Join(",", ipd.MultipleExtraCharges);
                    }
                    Ipd.RoomType = ipd.RoomType;
                    Ipd.RoomCharges = ipd.RoomCharges;
                    Ipd.PerDayRoom = ipd.PerDayRoom;
                    Ipd.TotalRoomPrice = ipd.TotalRoomPrice;
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

        public IActionResult Create(IpdDto ipd) 
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
                   AdmitDate = a.AdmitDate,
                   DischargeDate = a.DischargeDate,
                   //Diagnosis = a.Diagnosis,
                   Prescription = a.Prescription,
                   DiagnosisCharges = a.DiagnosisCharges,
                   //ExtraCharges = a.ExtraCharges,
                   RoomType = a.RoomType,
                   RoomCharges = a.RoomCharges,
                   PerDayRoom = a.PerDayRoom,
                   TotalRoomPrice = a.TotalRoomPrice,
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
