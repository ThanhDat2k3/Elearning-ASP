using System;
using System.Linq;
using Elearning.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Elearning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MonHocController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger<MonHocController> _logger;

        public MonHocController(DataContext context, ILogger<MonHocController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var subjects = _context.Subjects
                                   .OrderBy(s => s.SubjectId)
                                   .ToList();
            return View(subjects);
        }

        public IActionResult Create()
        {
            var subject = new Subject();
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateConfirm(Subject subject)
        {
            if (subject == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View("Create", subject);
            }

            _context.Subjects.Add(subject);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(long? id)
        {
            if (id == null) return NotFound();
            var subject = _context.Subjects.FirstOrDefault(s => s.SubjectId == id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        public IActionResult Delete(long? id)
        {
            if (id == null) return NotFound();
            var subject = _context.Subjects.FirstOrDefault(s => s.SubjectId == id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var subject = _context.Subjects.FirstOrDefault(s => s.SubjectId == id);
                if (subject == null) return NotFound();
            _context.Subjects.Remove(subject);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long SubjectId, string SubjectName, string? Description)
        {
            var subject = _context.Subjects.FirstOrDefault(s => s.SubjectId == SubjectId);
            if (subject == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(SubjectName))
                subject.SubjectName = SubjectName;
            subject.Description = Description;

            _context.Update(subject);
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = subject.SubjectId });
        }
    }
}