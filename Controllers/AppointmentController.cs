﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StanfordHospital.Data;
using StanfordHospital.Models;

namespace StanfordHospital.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly ApplicationDbContext _context;

        public AppointmentController(ILogger<AppointmentController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult AppointmentDetails()
        //{
        //    ViewBag.isappointment = "active";
        //    return View();
        //}

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
                ReasonForAppointment = a.ReasonForAppointment,
            }).ToList();

            return View("Appointment", model);
        }

        [HttpPost]
        public IActionResult AddAppointment(Appointment appointment) 
        {
            if(ModelState.IsValid) 
            {
                var Appointment = new Appointment
                {
                    Patientid = appointment.Patientid,
                    Id = appointment.Id,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    AppointmentStatus = appointment.AppointmentStatus,
                    ReasonForAppointment = appointment.ReasonForAppointment,
                };

                if(Appointment.Appointmentid != 0) 
                {
                    //Edit
                    var appointments = _context.Appointment.Find(appointment.Appointmentid);
                    if(appointments != null) 
                    {
                        appointment.Patientid = appointment.Patientid;
                        appointment.Id = appointment.Id; 
                        appointment.AppointmentDate = appointment.AppointmentDate;
                        appointment.AppointmentTime = appointment.AppointmentTime;
                        appointment.AppointmentStatus = appointment.AppointmentStatus;
                        appointment.ReasonForAppointment = appointment.ReasonForAppointment;
                        _context.SaveChanges();
                        TempData["Message"] = "Appointment Updated Successfully....";
                    }
                }
                else
                {
                    //Create
                    _context.Appointment.Add(Appointment);
                    _context.SaveChanges();
                    TempData["Message"] = "Appointment Added Successfully....";
                }
                return RedirectToAction("Index");
            }
            return View("AddAppointment", appointment);
        }

        public IActionResult EditAppointment(Appointment appointment)
        {
            if(ModelState.IsValid) 
            {
                var Appointment = _context.Appointment.Find(appointment.Appointmentid);
                if(Appointment != null) 
                {
                    Appointment.Appointmentid = appointment.Appointmentid;
                    Appointment.Patientid = appointment.Patientid;
                    Appointment.Id = appointment.Id;
                    Appointment.AppointmentDate = appointment.AppointmentDate;
                    Appointment.AppointmentTime = appointment.AppointmentTime;
                    Appointment.AppointmentStatus = appointment.AppointmentStatus;
                    Appointment.ReasonForAppointment= appointment.ReasonForAppointment;
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
            if(appointment != null) 
            {
                _context.Appointment.Remove(appointment);
                _context.SaveChanges();

                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public IActionResult Create(Appointment appointment)
        {
            var patients = _context.Patient.Select(p => new {
                p.Patientid,
                FullName = $"{p.Firstname} {p.Lastname}"
            }).ToList();
            ViewBag.Patients = new SelectList(patients, "Patientid", "FullName");
            var doctors = _context.Users.Where(u => u.Role == "doctor")
                               .Select(u => new
                               {
                                   Id = u.Id,
                                   FullName = $"Dr. {u.FirstName} {u.LastName}"
                               })
                               .ToList();
            ViewBag.Doctors = new SelectList(doctors, "Id", "FullName");
            //ViewBag.Users = new SelectList(_context.Users, "Id", "FirstName");
            return View("AddAppointment", appointment);
        }

        public IActionResult Edit(int Id)
        {
            var patients = _context.Patient.Select(p => new {
                p.Patientid,
                FullName = $"{p.Firstname} {p.Lastname}"
            }).ToList();
            ViewBag.Patients = new SelectList(patients, "Patientid", "FullName");
            var doctors = _context.Users.Where(u => u.Role == "doctor")
                               .Select(u => new
                               {
                                   Id = u.Id,
                                   FullName = $"Dr. {u.FirstName} {u.LastName}"
                               })
                               .ToList();
            ViewBag.Doctors = new SelectList(doctors, "Id", "FullName");
            var editappointment = _context.Appointment.Where(a => a.Appointmentid == Id)
                .Select(a => new Appointment
                {
                    Appointmentid = a.Appointmentid,
                    Patientid = a.Patientid,
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    AppointmentStatus = a.AppointmentStatus,
                    ReasonForAppointment = a.ReasonForAppointment,
                }).FirstOrDefault();

            return View("EditAppointment", editappointment);
        }

        public IActionResult Delete(Appointment appointment) 
        {
            var deleteappointment = _context.Appointment.Where(a => a.Appointmentid ==  appointment.Appointmentid)
                .Select(a => new Appointment
                {
                    Appointmentid = a.Appointmentid,
                    Patientid = a.Patientid,
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    AppointmentStatus = a.AppointmentStatus,
                    ReasonForAppointment = a.ReasonForAppointment,
                }).FirstOrDefault();

            return View("DeleteAppointment", deleteappointment);
        }
    }
}
