using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elearning.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elearning.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

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

            var userName = teacher.User != null ? teacher.User.FullName : teacher.Email ?? "";
            var expectedSlug = Functions.GiaoVienSlugGeneration("gv", id, userName);

            if (string.IsNullOrEmpty(slug) || !slug.Equals(expectedSlug, StringComparison.OrdinalIgnoreCase))
            {
                return Redirect($"/Admin/GiaoVien/Details/{id}/{expectedSlug}");
            }

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long TeacherId, IFormFile? AnhDaiDienFile)
        {
            var teacher = _context.Teachers.Include(t => t.User).FirstOrDefault(t => t.TeacherId == TeacherId);
            if (teacher == null) return NotFound();

            // xử lý file upload nếu có
            if (AnhDaiDienFile != null && AnhDaiDienFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "teachers");
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

            // Lấy các trường khác từ form
            var form = Request.Form;
            teacher.NoiCongTac = form["NoiCongTac"];
            teacher.HocVi = form["HocVi"];
            teacher.GioiThieu = form["GioiThieu"];
            teacher.Email = form["Email"];
            // nếu muốn cập nhật ảnh từ input text (không khuyến nghị), có thể đọc form["AnhDaiDien"]

            if (teacher.User != null)
            {
                var newFullName = form["UserFullName"].ToString();
                if (!string.IsNullOrWhiteSpace(newFullName))
                {
                    teacher.User.FullName = newFullName;
                    _context.Update(teacher.User);
                }
            }

            _context.Update(teacher);
            _context.SaveChanges();

            var userName = teacher.User != null ? teacher.User.FullName : teacher.Email ?? "";
            var slug = Functions.GiaoVienSlugGeneration("gv", teacher.TeacherId, userName);

            return Redirect($"/Admin/GiaoVien/Details/{teacher.TeacherId}/{slug}");
        }
    }
}