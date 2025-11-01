using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elearning.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elearning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiaoVienController: Controller
    {
        private readonly DataContext _context;
        public GiaoVienController(DataContext context)
        {
            _context = context;
        }
        /* public IActionResult Index()
          {
              var mnList = _context.Teachers.OrderBy(m => m.TeacherId).ToList();
              return View(mnList);
          }*/
        public IActionResult Index()
        {
            var teacher = new Teacher();
             return View(teacher);
        }
    }
}