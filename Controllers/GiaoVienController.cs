using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Elearning.Models;
using Elearning.Areas.Admin.Models;
using Elearning.Utilities;

namespace Elearning.Controllers;

public class GiaoVienController : Controller
{
    private readonly ILogger<GiaoVienController> _logger;
    private readonly DataContext _context;
    public GiaoVienController(ILogger<GiaoVienController> logger,DataContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }
    [Route("/gv-{id:long}-{slug}.html", Name = "ChiTietGiaoVien")]
    public IActionResult ChiTietGiaoVien(long? id, string? slug)
    {
        if (id == null) return NotFound();

        var gv = _context.vwTeacherInfos.FirstOrDefault(m => (m.TeacherId == id) && (m.IsActive == true));
        if (gv == null) return NotFound();

        // Build expected slug from teacher name (fallback nếu null)
        var expectedSlug = Functions.Slugify(gv.TenGiangVien ?? gv.EmailHeThong ?? $"gv-{id}");

        // Nếu slug khác hoặc không có thì chuyển hướng tới url đúng (SEO canonical)
        if (string.IsNullOrEmpty(slug) || !slug.Equals(expectedSlug, StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToRoute("ChiTietGiaoVien", new { id = id, slug = expectedSlug });
        }

        return View(gv);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
