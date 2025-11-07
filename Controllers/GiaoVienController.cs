using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elearning.Areas.Admin.Models;
using Elearning.Models;
using Elearning.Utilities;

namespace Elearning.Controllers;

public class GiaoVienController : Controller
{
    private readonly ILogger<GiaoVienController> _logger;
    private readonly DataContext _context;
    public GiaoVienController(ILogger<GiaoVienController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("/GiaoVien/Details/{id}/{slug?}")]
    public IActionResult Details(long id, string? slug)
    {
        var teacher = _context.Teachers
                                .Include(t => t.User)
                                .FirstOrDefault(t => t.TeacherId == id);
        if (teacher == null) return NotFound();

        string userName = teacher.User.FullName;
        var Slug = Functions.GiaoVienSlugGeneration("gv", id, userName);
        var vw = new Elearning.Models.vwTeacherInfo
        {
            TeacherID = teacher.TeacherId,
            UserID = teacher.UserId,
            TenGiangVien = teacher.User != null ? teacher.User.FullName : teacher.Email,
            EmailHeThong = teacher.User?.Email,
            EmailRieng = teacher.Email,
            SoDienThoai = teacher.User?.Phone,
            HocVi = teacher.HocVi,
            NoiCongTac = teacher.NoiCongTac,
            ChuyenMon = teacher.ChuyenMon,
            GioiThieu = teacher.GioiThieu,
            AnhDaiDien = teacher.AnhDaiDien,
            IsActive = teacher.User?.IsActive
        };

        var list = new List<Elearning.Models.vwTeacherInfo> { vw };
        return View(list);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new Elearning.Areas.Admin.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
