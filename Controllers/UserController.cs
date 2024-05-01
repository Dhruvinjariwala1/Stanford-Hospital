using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StanfordHospital.Data;
using StanfordHospital.Models;


namespace StanfordHospital.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(ILogger<UserController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _roleManager = roleManager;
        }

        public IActionResult isusereditprofile()
        {
            ViewBag.isusereditprofile = "active";
            return View();
        }

        //public IActionResult Userdetails()
        //{
        //    ViewBag.isuser = "active";
        //    return View();
        //}

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
                Role = u.Role
            }).ToList();

            return View(model);
        }

        public IActionResult Create(User user) 
        {
            return View("AddUser", user);
        }

        public async Task<IActionResult> Edit(User user)
        {
            var getUser = await _context.Users.Where(u => u.Id == user.Id)
                .Select(u => new User
                { 
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNo = u.PhoneNo,
                    Address = u.Address,
                    BirthDate = u.BirthDate,
                    Gender = u.Gender
            }).FirstOrDefaultAsync();

            if(getUser != null) 
            {
                var appplicationUser = await _userManager.FindByIdAsync(getUser.Id);
                var userRoles = await _userManager.GetRolesAsync(appplicationUser);
                if (userRoles.Count() > 0)
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == userRoles[0]);
                    if (role != null)
                    {
                        getUser.Role = role.Name;
                    }
                }
            }
            return View("EditUser", getUser);
        }

        public IActionResult Delete(User user)
        {
            var deleteUser = _context.Users.Where(u => u.Id == user.Id)
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

            return View("DeleteUser", deleteUser);
        }

        public async Task<IActionResult> AddUser(User user)
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
                    Role = user.Role,
                    EmailConfirmed = true,
                    UserName = user.Email/*Guid.NewGuid().ToString().Replace('-','a'),*/
                };

                if (!string.IsNullOrEmpty(user.Id))
                {
                    // Edit
                    var users = await _context.Users.FindAsync(user.Id);
                    if (user != null)
                    {
                        user.FirstName = user.FirstName;
                        user.LastName = user.LastName;
                        user.Email = user.Email;
                        user.PhoneNo = user.PhoneNo;
                        user.Address = user.Address;
                        user.BirthDate = user.BirthDate;
                        user.Gender = user.Gender;
                        user.Role = user.Role;
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    // Create
                    var isEmailExist = await _context.Users.Where(x => x.Id != user.Id && x.Email == user.Email).FirstOrDefaultAsync();
                    if(isEmailExist != null) 
                    {
                        ModelState.AddModelError("Email", "Email already exist");
                    }
                    else
                    {
                        var userResult = await _userManager.CreateAsync(applicationUser, "Test@123");
                        if (userResult.Succeeded)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(applicationUser, user.Role);
                            if (roleResult.Succeeded)
                            {
                                TempData["SuccessMessage"] = "User Added Successfully....";
                                return RedirectToAction("User");
                            }
                        }
                    } 
                }
            }
            TempData["ErrorMessage"] = "Failed to Add User.";
            return View("AddUser",user);
        }

        public async Task<IActionResult> EditUser(User user)
        {
            var isEmailExist = await _context.Users.Where(x => x.Id != user.Id && x.Email == user.Email).FirstOrDefaultAsync();
            if(isEmailExist != null) 
            {
                ModelState.AddModelError("Email", "Email already exist");   
            }

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
                    applicationUser.Role = user.Role;
                    _context.Users.Update(applicationUser);
                    await _context.SaveChangesAsync();

                    var userRoles = await _userManager.GetRolesAsync(applicationUser);
                    var removeRoleResult = await _userManager.RemoveFromRolesAsync(applicationUser, userRoles);
                    if (removeRoleResult != null)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(applicationUser, user.Role);
                        if (roleResult.Succeeded)
                        {
                            TempData["SuccessMessage"] = "User Updated Successfully....";
                            return RedirectToAction("User");
                        }
                    }
                    return RedirectToAction("User");
                }
                else
                {
                    return NotFound();
                }
            }
            TempData["ErrorMessage"] = "Failed to Update User.";
            return View("EditUser",user);
        }

        public IActionResult DeleteUser(string id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();

                return Json(new { status = true });
                //return RedirectToAction("User"); 
            }
            return Json(new { status = false });
            //return NotFound();
        }

        public async Task<IActionResult> EditProfile()
        {
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var applicationUser = _context.Users.Find(currentUserId);
            return View("EditProfile", applicationUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ApplicationUser user, IFormFile ImageFile)
        {
            if(ModelState.IsValid) 
            {
                var updateuser = _context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                if(updateuser != null) 
                {
                    updateuser.FirstName = user.FirstName;
                    updateuser.LastName = user.LastName;
                    updateuser.Email = user.Email;
                    updateuser.PhoneNo = user.PhoneNo;
                    updateuser.Address = user.Address;
                    updateuser.BirthDate = user.BirthDate;
                    updateuser.Gender = user.Gender;
                    //updateuser.Image = user.Image;
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        // Generate a unique file name to avoid conflicts
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "dist/img");
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Save the file to the specified path
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }

                        // Update user's image path
                        updateuser.Image = uniqueFileName;
                    }


                    _context.SaveChanges();
                }
            }
            return View("EditProfile", user);
        }
        public IActionResult RoleAction()
        {
            var roles = _context.Roles
                        .Select(r => new SelectListItem { Value = r.Id, Text = r.Name })
                        .ToList();

            return Json(roles);
        }
    }
}
