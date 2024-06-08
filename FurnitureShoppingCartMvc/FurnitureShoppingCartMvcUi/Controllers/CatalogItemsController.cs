using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureShoppingCartMvcUi.Data;
using Microsoft.AspNetCore.Authorization;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CatalogItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CatalogItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.CatalogItems.ToListAsync());
        }

        // GET: CatalogItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogItem = await _context.CatalogItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogItem == null)
            {
                return NotFound();
            }

            return View(catalogItem);
        }

        // GET: CatalogItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CatalogItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Size,Description,Price,ImageUrl")] CatalogItem catalogItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalogItem);
        }

        // GET: CatalogItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogItem = await _context.CatalogItems.FindAsync(id);
            if (catalogItem == null)
            {
                return NotFound();
            }
            return View(catalogItem);
        }

        // POST: CatalogItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Size,Description,Price,ImageUrl")] CatalogItem catalogItem)
        {
            if (id != catalogItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogItemExists(catalogItem.Id))
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
            return View(catalogItem);
        }

        // GET: CatalogItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogItem = await _context.CatalogItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogItem == null)
            {
                return NotFound();
            }

            return View(catalogItem);
        }

        // POST: CatalogItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogItem = await _context.CatalogItems.FindAsync(id);
            if (catalogItem != null)
            {
                _context.CatalogItems.Remove(catalogItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogItemExists(int id)
        {
            return _context.CatalogItems.Any(e => e.Id == id);
        }
    }
}
