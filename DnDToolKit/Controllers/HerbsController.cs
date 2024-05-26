using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnDToolKit.Data;
using DnDToolKit.Models;

namespace DnDToolKit.Controllers
{
    public class HerbsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HerbsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Herbs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Herb.ToListAsync());
        }

        // GET: Herbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var herb = await _context.Herb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (herb == null)
            {
                return NotFound();
            }

            return View(herb);
        }

        // GET: Herbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Herbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,place,description,Price")] Herb herb)
        {
            if (ModelState.IsValid)
            {
                bool herbExists = _context.Herb.FirstOrDefault(x => x.name == herb.name) != null;

                if (herbExists)
                {
                    Console.WriteLine("herb exists");
                    ModelState.AddModelError("name", "herb Already exists");
                    return View(herb);
                }
                else;
                {
                    _context.Add(herb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(herb);
        }

        // GET: Herbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var herb = await _context.Herb.FindAsync(id);
            if (herb == null)
            {
                return NotFound();
            }
            return View(herb);
        }

        // POST: Herbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,place,description,Price")] Herb herb)
        {
            if (id != herb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(herb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HerbExists(herb.Id))
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
            return View(herb);
        }

        // GET: Herbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var herb = await _context.Herb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (herb == null)
            {
                return NotFound();
            }

            return View(herb);
        }

        // POST: Herbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var herb = await _context.Herb.FindAsync(id);
            if (herb != null)
            {
                _context.Herb.Remove(herb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HerbExists(int id)
        {
            return _context.Herb.Any(e => e.Id == id);
        }
    }
}
