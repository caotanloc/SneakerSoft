using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM_ADIDAS.Models;

namespace ASM_ADIDAS.Controllers
{
    public class ThemNhomController : Controller
    {
        private readonly QlBanHangContext _context;

        public ThemNhomController(QlBanHangContext context)
        {
            _context = context;
        }

        // GET: ThemNhom
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhomSp.ToListAsync());
        }

        // GET: ThemNhom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhomSp = await _context.NhomSp
                .FirstOrDefaultAsync(m => m.MaNhom == id);
            if (nhomSp == null)
            {
                return NotFound();
            }

            return View(nhomSp);
        }

        // GET: ThemNhom/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ThemNhom/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNhom,TenNhom")] NhomSp nhomSp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhomSp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhomSp);
        }

        // GET: ThemNhom/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhomSp = await _context.NhomSp.FindAsync(id);
            if (nhomSp == null)
            {
                return NotFound();
            }
            return View(nhomSp);
        }

        // POST: ThemNhom/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNhom,TenNhom")] NhomSp nhomSp)
        {
            if (id != nhomSp.MaNhom)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhomSp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhomSpExists(nhomSp.MaNhom))
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
            return View(nhomSp);
        }

        // GET: ThemNhom/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhomSp = await _context.NhomSp
                .FirstOrDefaultAsync(m => m.MaNhom == id);
            if (nhomSp == null)
            {
                return NotFound();
            }

            return View(nhomSp);
        }

        // POST: ThemNhom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhomSp = await _context.NhomSp.FindAsync(id);
            _context.NhomSp.Remove(nhomSp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhomSpExists(int id)
        {
            return _context.NhomSp.Any(e => e.MaNhom == id);
        }
    }
}
