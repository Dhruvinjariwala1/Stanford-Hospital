using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StanfordHospital.Data;
using StanfordHospital.Models;

namespace StanfordHospital.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ILogger<UserController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Userdetails()
        {
            ViewBag.isuser = "active";
            return View();
        }

        public IActionResult User(List<User> users)
        {
            ViewBag.isuser = "active";

            var model = new List<User>();
            model = _context.Users.Select(u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNo = u.PhoneNo,
                Address = u.Address,
                BirthDate = u.BirthDate,
                Gender = u.Gender,
                //Image = u.Image,
            }).ToList();

            return View(model);
        }

        public IActionResult Create(User user) 
        {
            return View("AddUser", user);
        }

        public IActionResult Edit(User user)
        {
            var getUser = _context.Users.Where(u => u.Id == user.Id)
                .Select(u => new User
                { 
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNo = u.PhoneNo,
                    Address = u.Address,
                    BirthDate = u.BirthDate,
                    Gender = u.Gender,
                   // Image = u.Image,
            }).FirstOrDefault();

            return View("EditUser", getUser);
        }

        public IActionResult Delete(User user)
        {
            var getdeleteUser = _context.Users.Where(u => u.Id == user.Id)
                .Select(u => new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNo = u.PhoneNo,
                    Address = u.Address,
                    BirthDate = u.BirthDate,
                    Gender = u.Gender,
                   // Image = u.Image,
                }).FirstOrDefault();

            return View("DeleteUser", getdeleteUser);
        }

        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNo = user.PhoneNo,
                    Address = user.Address,
                    BirthDate = user.BirthDate,
                    Gender = user.Gender,
                    //Image = user.Image,
                };

                if (!string.IsNullOrEmpty(user.Id))
                {
                    // Edit
                    var users = _context.Users.Find(user.Id);
                    if (user != null)
                    {
                        user.FirstName = user.FirstName;
                        user.LastName = user.LastName;
                        user.Email = user.Email;
                        user.PhoneNo = user.PhoneNo;
                        user.Address = user.Address;
                        user.BirthDate = user.BirthDate;
                        user.Gender = user.Gender;
                        //user.Image = user.Image;
                        _context.SaveChanges();
                    }
                }
                else
                {
                    // Create
                    _context.Users.Add(applicationUser);
                    _context.SaveChanges();
                }
                return RedirectToAction("User");
            }
            return View("AddUser",user);
        }

        public IActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = _context.Users.Find(user.Id);
                if (applicationUser != null)
                {
                    applicationUser.FirstName = user.FirstName;
                    applicationUser.LastName = user.LastName;
                    applicationUser.Email = user.Email;
                    applicationUser.PhoneNo = user.PhoneNo;
                    applicationUser.Address = user.Address;
                    applicationUser.BirthDate = user.BirthDate;
                    applicationUser.Gender = user.Gender;
                    //applicationUser.Image = user.Image;
                    _context.Users.Update(applicationUser);
                    _context.SaveChanges();
                    return RedirectToAction("User");
                }
                else
                {
                    return NotFound();
                }
            }
            return View("EditUser",user);
        }

        public IActionResult DeleteUser(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return RedirectToAction("User"); 
            }
            return NotFound();
        }

    }
}
