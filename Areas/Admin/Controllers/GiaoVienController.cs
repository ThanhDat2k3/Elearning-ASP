using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elearning.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Elearning.Utilities;

namespace Elearning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiaoVienController: Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        public GiaoVienController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            return RedirectToAction(nameof(Index));
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


        [HttpGet("/Admin/GiaoVien/Details/{id}/{slug?}")]
        public IActionResult Details(long id, string? slug)
        {
            var teacher = _context.Teachers
                                  .Include(t => t.User)
                                  .FirstOrDefault(t => t.TeacherId == id);
            if (teacher == null) return NotFound();

            var userName = teacher.User != null ? teacher.User.FullName : teacher.Email ;
            var expectedSlug = Functions.GiaoVienSlugGeneration("gv", id, userName);

            if (string.IsNullOrEmpty(slug) || !slug.Equals(expectedSlug, StringComparison.OrdinalIgnoreCase))
            {
                return Redirect($"/Admin/GiaoVien/Details/{id}/{expectedSlug}");
            }

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long TeacherId,
                                  string UserFullName,
                                  string Email,
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

            // xử lý file upload
            if (AnhDaiDienFile != null && AnhDaiDienFile.Length > 0 && _env != null)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "teachers");
                Directory.CreateDirectory(uploadsFolder);
                var ext = Path.GetExtension(AnhDaiDienFile.FileName);
                var fileName = $"teacher_{TeacherId}_{DateTime.UtcNow.Ticks}{ext}";
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    AnhDaiDienFile.CopyTo(fs);
                }
                // lưu đường dẫn tương đối để hiển thị
                teacher.AnhDaiDien = $"/uploads/teachers/{fileName}";
            }

            // cập nhật Teacher
            teacher.HocVi = HocVi;
            teacher.NoiCongTac = NoiCongTac;
            teacher.GioiThieu = GioiThieu;
            // Teacher.Email tồn tại trong model trước đó, cập nhật nếu cần
            teacher.Email = Email;

            // cập nhật User (không cho sửa UserId)
            if (teacher.User != null)
            {
                if (!string.IsNullOrWhiteSpace(UserFullName))
                    teacher.User.FullName = UserFullName;

                if (!string.IsNullOrWhiteSpace(Email))
                    teacher.User.Email = Email;

                teacher.User.Phone = Phone;
                _context.Update(teacher.User);
            }

            _context.Update(teacher);
            _context.SaveChanges();

            // redirect về chi tiết với slug đúng
            var userName = teacher.User != null ? teacher.User.FullName : teacher.Email ?? $"gv-{teacher.TeacherId}";
            var slug = Functions.GiaoVienSlugGeneration("gv", teacher.TeacherId, userName);

            // Redirect về action Details trong Admin area
            return RedirectToAction("Details", "GiaoVien", new { area = "Admin", id = teacher.TeacherId, slug = slug });
        }
    }
}