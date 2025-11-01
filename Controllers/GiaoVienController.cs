using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Elearning.Models;
using Elearning.Areas.Admin.Models;

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
    [Route("/gv-{id:long}-{slug}.html",Name ="ChiTietGiaoVien")]
    public IActionResult ChiTietGiaoVien(long? id)
    {
        if (id == null) return NotFound();
        var gv = _context.vwTeacherInfos.FirstOrDefault(m => (m.TeacherId == id) && (m.IsActive == true));
        if (gv == null) return NotFound();
        return View(gv);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
