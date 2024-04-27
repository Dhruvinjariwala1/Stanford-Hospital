using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StanfordHospital.Data;
using StanfordHospital.Models;

namespace StanfordHospital.Controllers
{

    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private readonly ApplicationDbContext _context;

        public PatientController(ILogger<PatientController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult PatientDetails()
        //{
        //    ViewBag.ispatient = "active";
        //    return View();
        //}

        public IActionResult Patient(List<Patient> patients)
        {
            ViewBag.ispatient = "active";

            var model = new List<Patient>();
            model = _context.Patient.Select(p => new Patient
            {
               Patientid = p.Patientid,
               Firstname = p.Firstname,
               Lastname = p.Lastname,
               Gender = p.Gender,
               DateOfBirth = p.DateOfBirth,
               ContactNumber = p.ContactNumber,
               EmailId = p.EmailId,
               Age = p.Age,
               Address = p.Address,
            }).ToList();

            return View(model);
        }

        public IActionResult AddPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var Patient = new Patient
                {
                    Firstname = patient.Firstname,
                    Lastname = patient.Lastname,
                    Gender = patient.Gender,
                    DateOfBirth = patient.DateOfBirth,
                    ContactNumber = patient.ContactNumber,
                    EmailId = patient.EmailId,
                    Age = patient.Age,
                    Address = patient.Address,
                };

                if (patient.Patientid != 0)
                {
                    //Edit
                    var patients = _context.Patient.Find(patient.Patientid);
                    if (Patient != null)
                    {
                        patient.Patientid = patient.Patientid;
                        patient.Firstname = patient.Firstname;
                        patient.Lastname = patient.Lastname;
                        patient.Gender = patient.Gender;
                        patient.DateOfBirth = patient.DateOfBirth;
                        patient.ContactNumber = patient.ContactNumber;
                        patient.EmailId = patient.EmailId;
                        patient.Age = patient.Age;
                        patient.Address = patient.Address;
                        _context.SaveChanges();
                        TempData["Message"] = "Patient Updated Successfully....";
                    }
                }
                else
                {
                    //Create
                    _context.Patient.Add(Patient);
                    _context.SaveChanges();
                    TempData["Message"] = "Patient Added Successfully....";
                }
                return RedirectToAction("Patient");
            }

            return View("AddPatient", patient);
        }

        public IActionResult EditPatient(Patient patient)
        {
            if(ModelState.IsValid) 
            {
                var Patient = _context.Patient.Find(patient.Patientid);
                if(Patient != null) 
                {
                    Patient.Patientid = patient.Patientid;
                    Patient.Firstname = patient.Firstname;
                    Patient.Lastname = patient.Lastname;
                    Patient.Gender = patient.Gender;
                    Patient.DateOfBirth = patient.DateOfBirth;
                    Patient.ContactNumber = patient.ContactNumber;
                    Patient.EmailId = Patient.EmailId;
                    Patient.Age = patient.Age;
                    Patient.Address = patient.Address;
                    _context.Patient.Update(Patient);
                    _context.SaveChanges();
                    TempData["Message"] = "Patient Updated Successfully....";
                    return RedirectToAction("Patient");
                }
                else
                {
                    return NotFound();
                }
            }
            return View("EditPatient",patient);
        }

        public IActionResult DeletePatient(int Patientid) 
        {
            var patient = _context.Patient.Find(Patientid);
            if(patient != null) 
            {
                _context.Patient.Remove(patient);
                _context.SaveChanges();

                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public IActionResult Create(Patient patient)
        {
            return View("AddPatient",patient);
        }

        public IActionResult Edit(int Id) 
        {
            var editpatient = _context.Patient.Where(p => p.Patientid == Id)
                .Select(p => new Patient
                {
                    Patientid = p.Patientid,
                    Firstname = p.Firstname,
                    Lastname = p.Lastname,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    ContactNumber = p.ContactNumber,
                    EmailId = p.EmailId,
                    Age = p.Age,
                    Address = p.Address,
                }).FirstOrDefault();

            return View("EditPatient",editpatient);
        }

        public IActionResult Delete(Patient patient) 
        {
            var deletepatient = _context.Patient.Where(p => p.Patientid == patient.Patientid)
                .Select(p => new Patient
                {
                    Firstname= p.Firstname,
                    Lastname= p.Lastname,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    ContactNumber = p.ContactNumber,
                    EmailId = p.EmailId,
                    Age = p.Age,
                    Address = p.Address,
                }).FirstOrDefault();

            return View("DeletePatient", deletepatient);
        }
    }
}
