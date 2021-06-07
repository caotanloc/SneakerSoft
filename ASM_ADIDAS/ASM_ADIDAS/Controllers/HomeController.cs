using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ASM_ADIDAS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASM_ADIDAS.Extensions;
using ASM_ADIDAS.ViewModels;

namespace ASM_ADIDAS.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly QlBanHangContext _context;

        public HomeController(QlBanHangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var QL_BanHang1 = _context.Sanpham.Include(s => s.NhomSpNavigation);

            var session = SessionsHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");

            if (session != null)
            {
                List<ProductViewModel> sanphams = new List<ProductViewModel>();
                foreach (var item in session)
                {
                    //var findItem = _context.Sanpham.Where(x => x.MaSp.Equals(item.ProductID)).FirstOrDefault();
                    var findItem = QL_BanHang1.Single(x => x.MaSp.Equals(item.ProductID));

                    sanphams.Add(new ProductViewModel(item.ProductID, item.Quantity, findItem.TenSp, findItem.DonGia, findItem.HinhAnh));
                }
                ViewData["SessionCart"] = sanphams;
            }
            else ViewData["SessionCart"] = null;

            //ViewData["Nike"] = Nike;
            //ViewData["Adidas"] = Adidas;
            //ViewData["Quanao"] = Quanao;
            //ViewData["Phukien"] = Phukien;

            return View(await QL_BanHang1.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Shop()
        {
            var QL_BanHang1 = _context.Sanpham.Include(s => s.NhomSpNavigation);
            return View(await QL_BanHang1.ToListAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
