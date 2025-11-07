using System;
using Elearning.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elearning.Utilities;

namespace Elearning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiaoVienController: Controller
    {
        private readonly DataContext _context;
        private readonly ILogger<GiaoVienController> _logger;
        public GiaoVienController(DataContext context, ILogger<GiaoVienController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult ThemMoi()
        {
            var teacher = new Teacher();
            return View(teacher);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var teacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == id);
            if (teacher == null) return NotFound();
            var user = _context.Users.FirstOrDefault(u => u.Teachers.Any(t => t.TeacherId == id));
            if (user == null) return NotFound();

            user.IsActive = false;
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            
            var teachers = _context.Teachers
                                   .Include(t => t.User)
                                  // .Where(t => t.User.IsActive != false) 
                                   .OrderBy(t => t.TeacherId)
                                   .ToList();
            return View(teachers);
        }

        [HttpGet]
        public IActionResult ToggleActive(int id)
        {
            var teacher = _context.Teachers
                                  .Include(t => t.User)
                                  .FirstOrDefault(t => t.TeacherId == id);
            if (teacher == null) return NotFound();
            if (teacher.User == null) return NotFound();
            if (teacher.User.IsActive == true)
                teacher.User.IsActive = false;
            else
                teacher.User.IsActive = true;
            _context.Update(teacher.User);
            _context.SaveChanges();

            var userName = teacher.User.FullName;
            var slug = Functions.GiaoVienSlugGeneration("gv", teacher.TeacherId, userName);

            return RedirectToAction("Details", "GiaoVien", new { area = "Admin", id = teacher.TeacherId, slug = slug });
        }

        [HttpGet("/Admin/GiaoVien/Details/{id}/{slug?}")]
        public IActionResult Details(long id, string? slug)
        {
            var teacher = _context.Teachers
                                  .Include(t => t.User)
                                  .FirstOrDefault(t => t.TeacherId == id);
            if (teacher == null) return NotFound();

            var userName = teacher.User.FullName;
            var Slug = Functions.GiaoVienSlugGeneration("gv", id, userName);
            return View(teacher);
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(long TeacherId,
                  string UserFullName,
                  string EmailPrivate,
                  string LoginEmail,
                  string? Phone,
                  IFormFile? AnhDaiDienFile,
                  string? HocVi,
                  string? NoiCongTac,
                  string? GioiThieu)
        {
            var teacher = _context.Teachers
                                  .Include(t => t.User)
                                  .FirstOrDefault(t => t.TeacherId == TeacherId);
            if (teacher == null) return NotFound();


            if (AnhDaiDienFile != null && AnhDaiDienFile.Length > 0)
            {
                var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var uploadsFolder = Path.Combine(webRoot, "uploads", "teachers");
                Directory.CreateDirectory(uploadsFolder);
                var ext = Path.GetExtension(AnhDaiDienFile.FileName);
                var fileName = $"teacher_{TeacherId}_{DateTime.UtcNow.Ticks}{ext}";
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    AnhDaiDienFile.CopyTo(fs);
                }

                teacher.AnhDaiDien = $"/uploads/teachers/{fileName}";
            }

            teacher.HocVi = HocVi;
            teacher.NoiCongTac = NoiCongTac;
            teacher.GioiThieu = GioiThieu;
            teacher.Email = EmailPrivate;

            if (teacher.User != null)
            {
                if (!string.IsNullOrWhiteSpace(UserFullName))
                    teacher.User.FullName = UserFullName;
                if (!string.IsNullOrWhiteSpace(LoginEmail))
                    teacher.User.Email = LoginEmail;

                teacher.User.Phone = Phone;
                _context.Update(teacher.User);
            }

            _context.Update(teacher);
            _context.SaveChanges();
            string userName;
            if (teacher.User != null)
                userName = teacher.User.FullName;
            else
                userName = "NoName";
            var slug = Functions.GiaoVienSlugGeneration("gv", teacher.TeacherId, userName);

            return RedirectToAction("Details", "GiaoVien", new { area = "Admin", id = teacher.TeacherId, slug = slug });
        }
    }
}