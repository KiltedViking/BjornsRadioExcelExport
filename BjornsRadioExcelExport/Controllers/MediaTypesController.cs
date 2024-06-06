using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BjornsRadioExcelExport.Data;
using BjornsRadioExcelExport.Models;

namespace BjornsRadioExcelExport.Controllers
{
    public class MediaTypesController : Controller
    {
        private readonly BjornsRadioContext _context;

        public MediaTypesController(BjornsRadioContext context)
        {
            _context = context;
        }

        // GET: MediaTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MediaTypes.ToListAsync());
        }

        // GET: MediaTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _context.MediaTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaType == null)
            {
                return NotFound();
            }

            return View(mediaType);
        }

        // GET: MediaTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MediaTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName,Comments")] MediaType mediaType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediaType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediaType);
        }

        // GET: MediaTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _context.MediaTypes.FindAsync(id);
            if (mediaType == null)
            {
                return NotFound();
            }
            return View(mediaType);
        }

        // POST: MediaTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName,Comments")] MediaType mediaType)
        {
            if (id != mediaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaTypeExists(mediaType.Id))
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
            return View(mediaType);
        }

        // GET: MediaTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _context.MediaTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaType == null)
            {
                return NotFound();
            }

            return View(mediaType);
        }

        // POST: MediaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaType = await _context.MediaTypes.FindAsync(id);
            if (mediaType != null)
            {
                _context.MediaTypes.Remove(mediaType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaTypeExists(int id)
        {
            return _context.MediaTypes.Any(e => e.Id == id);
        }
    }
}
