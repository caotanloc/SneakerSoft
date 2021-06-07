using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM_ADIDAS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ASM_ADIDAS.Controllers
{
    public class ThemSanPhamController : Controller
    {
        private readonly QlBanHangContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ThemSanPhamController(QlBanHangContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }

        // GET: ThemSanPham
        public async Task<IActionResult> Index()
        {
            var qLBanHangContext = _context.Sanpham.Include(s => s.NhomSpNavigation);
            return View(await qLBanHangContext.ToListAsync());
        }

        // GET: ThemSanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham
                .Include(s => s.NhomSpNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // GET: ThemSanPham/Create
        public IActionResult Create()
        {
            ViewData["NhomSp"] = new SelectList(_context.NhomSp, "MaNhom", "TenNhom");
            return View();
        }

        // POST: ThemSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,DonGia,MoTaSp,ProfileImage,HinhAnh,NhomSp")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                if(sanpham.ProfileImage is null)
                {

                }
                else
                {
                    string rootpath = _hostingEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(sanpham.ProfileImage.FileName);
                    string ext = Path.GetExtension(sanpham.ProfileImage.FileName);
                    sanpham.HinhAnh = fileName = sanpham.TenSp + DateTime.Now.ToString("ddmmyyyy") + ext;
                    string path = Path.Combine(rootpath + "/images/", fileName);
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        await sanpham.ProfileImage.CopyToAsync(fs);
                    }
                }
               
                _context.Add(sanpham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhomSp"] = new SelectList(_context.NhomSp, "MaNhom", "TenNhom", sanpham.NhomSp);
            return View(sanpham);
        }
        private void UploadImage(IFormFile file)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images", file.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
        private void RemoveImage(string path)
        {
            //string path = Path.Combine(_hostingEnvironment.WebRootPath, "images", file.FileName);
            var getFile = new FileInfo(path);
            getFile.Delete();
        }
        // GET: ThemSanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            ViewData["NhomSp"] = new SelectList(_context.NhomSp, "MaNhom", "TenNhom", sanpham.NhomSp);
            return View(sanpham);
        }

        // POST: ThemSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,TenSp,DonGia,MoTaSp,HinhAnh,NhomSp")] Sanpham sanpham)
        {
            if (id != sanpham.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                sanpham.HinhAnh = sanpham.ProfileImage.FileName;
                var findSP = _context.Sanpham.SingleOrDefault(x => x.MaSp.Equals(id));
                string prevImage = Path.Combine(_hostingEnvironment.WebRootPath, "images", findSP.HinhAnh);
                try
                {
                    RemoveImage(prevImage);
                    UploadImage(sanpham.ProfileImage);
                    findSP.MaSp = id;
                    findSP.TenSp = sanpham.TenSp;
                    findSP.DonGia = sanpham.DonGia;
                    findSP.HinhAnh = sanpham.ProfileImage.FileName;
                    findSP.MoTaSp = sanpham.MoTaSp;
                    findSP.NhomSp = sanpham.NhomSp;
                    findSP.NhomSpNavigation = sanpham.NhomSpNavigation;

                    //_context.Update(sanpham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanphamExists(sanpham.MaSp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhomSp"] = new SelectList(_context.NhomSp, "MaNhom", "TenNhom", sanpham.NhomSp);
            return View(sanpham);
        }

        // GET: ThemSanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham
                .Include(s => s.NhomSpNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // POST: ThemSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanpham = await _context.Sanpham.FindAsync(id);
            _context.Sanpham.Remove(sanpham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanphamExists(int id)
        {
            return _context.Sanpham.Any(e => e.MaSp == id);
        }
    }
}
