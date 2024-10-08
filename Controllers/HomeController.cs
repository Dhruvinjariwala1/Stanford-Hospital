﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StanfordHospital.Data;
using StanfordHospital.Models;
using System.Data;
using System.Diagnostics;

namespace StanfordHospital.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            ViewBag.isdashboard = "active";
            DateTime today = DateTime.Today;
            var dischargedate = _context.Ipd.Where(i => i.DischargeDate.Date == today).ToList();
            decimal totalpayment = dischargedate.Sum(i => (i.RoomCharges ?? 0) * (i.PerDayRoom ?? 0) + (i.DiagnosisCharges ?? 0));
            ViewBag.TotalPayment = totalpayment;

            var todayappointmentdate = _context.Appointment.Where(a => a.AppointmentDate.Date == today).ToList();
            decimal appointmentpayment = todayappointmentdate.Sum(a => (a.DiagnosisCharges ?? 0) + (a.Price ?? 0));
            ViewBag.AppointmentPayment = appointmentpayment;

            int todaydischargedatecount = _context.Ipd.Count(i => i.DischargeDate.Date == today);
            
            ViewBag.TodayDischargeDateCount = todaydischargedatecount;

            int todayappointmentpatientcount = _context.Appointment.Count(a => a.AppointmentDate.Date == today);
            ViewBag.AppointmentCount = todayappointmentpatientcount;

            var currentyear = today.Year;
            var monthlycount = _context.Ipd
                .Where(i => i.DischargeDate.Year == currentyear)
                .GroupBy(i => i.DischargeDate.Month)
                .Select(g => new {Month = g.Key,Count = g.Count()}).ToList();

            var monthlydate = new int[12];
            foreach(var i in monthlycount)
            {
                monthlydate[i.Month -1] = i.Count;
            }
            ViewBag.MonthlyDateCount = monthlydate;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Role(List<Roles> roles)
        {
            ViewBag.isrole = "active";

            var model = new List<Roles>();
            model = _context.Roles.Select(x => new Roles
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return View(model);
        }
        

        public async Task<IActionResult> AddRole(Roles roleName)
        {
            if (ModelState.IsValid)
            {
                

                if (!string.IsNullOrEmpty(roleName.Id))
                {
                    // Edit
                    var role = _context.Roles.Find(roleName.Id);
                    if (role != null)
                    {
                        role.Name = roleName.Name;
                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Role Updated SuccessFully....";
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    // Create
                    var identityRole = new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = roleName.Name,
                    };

                    var result = await _roleManager.CreateAsync(identityRole);
                    if (result.Succeeded) 
                    {
                        TempData["Message"] = "Role Added SuccessFully....";
                    }
                    else
                    {
                        foreach (var error in result.Errors) 
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(roleName);
                    }
                }
                return RedirectToAction("Role");
            }
            return View(roleName);
            //return RedirectToAction("Role");
        }

        public IActionResult EditRole(Roles roleName)
        {
            if (ModelState.IsValid)
            {
                var identityRole = _context.Roles.Find(roleName.Id);
                if (identityRole != null)
                {
                    identityRole.Name = roleName.Name;
                    _context.Roles.Update(identityRole);
                    _context.SaveChanges();
                    return RedirectToAction("Role");
                }
                else
                {
                    return NotFound();
                }
            }
            return View(roleName);
            //return RedirectToAction("Role");
        }

        public IActionResult DeleteRole(string id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();

                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public IActionResult GetRoleById(string id)
        {
            var role = _context.Roles.Find(id);
            return Json(role);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
