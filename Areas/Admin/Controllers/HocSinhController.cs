using System;
using System.IO;
using System.Linq;
using Elearning.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elearning.Utilities;

namespace DoAnElearning.Areas.Admin.Controllers
{
     [Area("Admin")]
    public class HocSinhController : Controller
    {
         private readonly DataContext _context;
        private readonly ILogger<HocSinhController> _logger;
        public HocSinhController(DataContext context, ILogger<HocSinhController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Create()
        {
            var student = new Student();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateConfirm([Bind("ClassId,StudentCode,BirthDate,User")] Student student, string? Password, IFormFile? AnhDaiDienFile)
        {
            // Ensure nested User object exists for binding
            if (student.User == null)
                student.User = new User();

            // Set password from separate input before validation
            student.User.PasswordHash = Password ?? string.Empty;

            // Remove any existing ModelState entries for bound keys so TryValidateModel can re-run cleanly
            var keysToRemove = ModelState.Keys.Where(k => k.StartsWith("User.") || k == nameof(Student.ClassId) || k == nameof(Student.StudentCode) || k == nameof(Student.BirthDate)).ToList();
            foreach (var k in keysToRemove)
                ModelState.Remove(k);

            // Re-validate the composed model
            if (!TryValidateModel(student))
            {
                return View("Create", student);
            }

            // Check duplicate email
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == student.User.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("User.Email", "Email đã tồn tại.");
                return View("Create", student);
            }

            // Find or create a student role
            var studentRole = _context.Roles
                                      .FirstOrDefault(r => r.RoleName != null && (r.RoleName.ToLower().Contains("student") || r.RoleName.ToLower().Contains("học sinh") || r.RoleName.ToLower().Contains("hocsinh")));
            if (studentRole == null)
            {
                studentRole = _context.Roles.FirstOrDefault();
                if (studentRole == null)
                {
                    studentRole = new Role { RoleName = "Student" };
                    _context.Roles.Add(studentRole);
                    _context.SaveChanges();
                }
            }

            // Create the user
            var user = new User
            {
                FullName = student.User.FullName,
                Email = student.User.Email,
                Phone = student.User.Phone,
                PasswordHash = student.User.PasswordHash,
                CreatedAt = DateTime.Now,
                IsActive = true,
                RoleId = studentRole.RoleId
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Create the student linked to the user
            var newStudent = new Student
            {
                UserId = user.UserId,
                ClassId = student.ClassId,
                StudentCode = student.StudentCode,
                BirthDate = student.BirthDate
            };

            _context.Students.Add(newStudent);
            _context.SaveChanges();

            var userName = user.FullName ?? "NoName";
            var slug = Functions.HocSinhSlugGeneration("hs", newStudent.StudentId, userName);

            return RedirectToAction("Details", "HocSinh", new { area = "Admin", id = newStudent.StudentId, slug = slug });
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null) return NotFound();
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null) return NotFound();
            var user = _context.Users.FirstOrDefault(u => u.Students.Any(s => s.StudentId == id));
            if (user == null) return NotFound();

            user.IsActive = false;
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ToggleActive(int id)
        {
            var student = _context.Students
                                  .Include(s => s.User)
                                  .FirstOrDefault(s => s.StudentId == id);
            if (student == null) return NotFound();

            student.User.IsActive = !student.User.IsActive;
            _context.Update(student.User);
            _context.SaveChanges();
            var Username = student.User.FullName;
            var slug= Functions.HocSinhSlugGeneration("hs", student.StudentId, Username);
            return RedirectToAction("Details", "HocSinh", new { area = "Admin", id = student.StudentId, slug = slug });
        }
        [HttpGet("/Admin/HocSinh/Details/{id}/{slug?}")]
        public IActionResult Details(int id, string? slug)
        {
            var student = _context.Students
                                  .Include(s => s.User)
                                  .FirstOrDefault(s => s.StudentId == id);
            if (student == null) return NotFound();

            var Username = student.User.FullName;
            var Slug = Functions.HocSinhSlugGeneration("hs", student.StudentId, Username);
            return View(student);
        }
         [HttpPost]
         [ValidateAntiForgeryToken]
         public IActionResult Edit(int StudentId,
                  string UserFullName,
                  string LoginEmail,
                  string? Phone,
                  long? ClassId,
                  string? StudentCode,
                  DateOnly? BirthDate
                  //IFormFile? AnhDaiDienFile
                  )
        {
            var student = _context.Students
                                  .Include(s => s.User)
                                  .FirstOrDefault(s => s.StudentId == StudentId);
            if (student == null) return NotFound();
/*
            if (AnhDaiDienFile != null && AnhDaiDienFile.Length > 0)
            {
                var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var uploadsFolder = Path.Combine(webRoot, "uploads", "students");
                Directory.CreateDirectory(uploadsFolder);
                var ext = Path.GetExtension(AnhDaiDienFile.FileName);
                var fileName = $"student_{StudentId}_{DateTime.UtcNow.Ticks}{ext}";
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    AnhDaiDienFile.CopyTo(fs);
                }

                // nếu Student có property AnhDaiDien
                student.AnhDaiDien = $"/uploads/students/{fileName}";
            }
*/          
            student.ClassId = ClassId;
            student.BirthDate = BirthDate;
            student.StudentCode = StudentCode;
            if (student.User != null)
            {
                if (!string.IsNullOrWhiteSpace(UserFullName))
                    student.User.FullName = UserFullName;
                if (!string.IsNullOrWhiteSpace(LoginEmail))
                    student.User.Email = LoginEmail;

                student.User.Phone = Phone;
                _context.Update(student.User);
            }

            _context.Update(student);
            _context.SaveChanges();

            var userName = student.User != null ? student.User.FullName : "NoName";
            var slug = Functions.HocSinhSlugGeneration("hs", student.StudentId, userName);

            return RedirectToAction("Details", "HocSinh", new { area = "Admin", id = student.StudentId, slug = slug });
        }

        public IActionResult Index()
        {
            var students = _context.Students
                                   .Include(s => s.User)
                                   .ToList();
            return View(students);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}