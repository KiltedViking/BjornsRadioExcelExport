using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BjornsRadioExcelExport.Data;
using BjornsRadioExcelExport.Models;
using BjornsRadioExcelExport.ViewModels;
using ClosedXML.Excel;
using ClosedXML.Extensions;

namespace BjornsRadioExcelExport.Controllers
{
    public class SongRequestsController : Controller
    {
        #region *** Constructors and properties ********************************
        private readonly BjornsRadioContext _context;

        public SongRequestsController(BjornsRadioContext context)
        {
            _context = context;
        }

        #endregion


        #region *** Index and Details ******************************************
        // GET: SongRequests
        public async Task<IActionResult> Index()
        {
            var bjornsRadioContext = _context.SongRequests.Include(s => s.Song);
            return View(await bjornsRadioContext.ToListAsync());
        }

        // GET: SongRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songRequest = await _context.SongRequests
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songRequest == null)
            {
                return NotFound();
            }

            return View(songRequest);
        }
        #endregion


        #region *** Create *****************************************************
        // GET: SongRequests/Create
        public IActionResult Create()
        {
            // TODO 2024-06-16: Add album titile and artist
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Title");
            return View();
        }

        // POST: SongRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongId,MessageText,RequestDate,WishedPlayDate")] SongRequestCreateViewModel songRequestCreateVm)
        {
            if (ModelState.IsValid)
            {
                // Convert view model to instance of class
                var songRequest = songRequestCreateVm.ToSongRequest();
                _context.Add(songRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Title", songRequestCreateVm.SongId);
            return View(songRequestCreateVm);
        }
        #endregion


        #region *** Edit *******************************************************
        // GET: SongRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songRequest = await _context.SongRequests.FindAsync(id);
            if (songRequest == null)
            {
                return NotFound();
            }
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", songRequest.SongId);
            return View(songRequest);
        }

        // POST: SongRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SongId,MessageText,RequestDate,WishedPlayDate")] SongRequest songRequest)
        {
            if (id != songRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongRequestExists(songRequest.Id))
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
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", songRequest.SongId);
            return View(songRequest);
        }
        #endregion


        #region *** Delete ***************************************************
        // GET: SongRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songRequest = await _context.SongRequests
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songRequest == null)
            {
                return NotFound();
            }

            return View(songRequest);
        }

        // POST: SongRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songRequest = await _context.SongRequests.FindAsync(id);
            if (songRequest != null)
            {
                _context.SongRequests.Remove(songRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion



        #region *** Helper methods *******************************************
        private bool SongRequestExists(int id)
        {
            return _context.SongRequests.Any(e => e.Id == id);
        }
        #endregion
    }
}
