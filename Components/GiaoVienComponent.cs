using Elearning.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
namespace Elearning.Components
{
    [ViewComponent(Name = "ChiTietGiaoVien")]
    public class GiaoVienComponent:ViewComponent
    {
        private readonly DataContext _context;
        public GiaoVienComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfGiaoVien = (from p in _context.vwTeacherInfos
                                  where (p.IsActive == true)
                                  orderby p.TeacherId descending
                                  select p).Take(8).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listOfGiaoVien));
        }

    }
}