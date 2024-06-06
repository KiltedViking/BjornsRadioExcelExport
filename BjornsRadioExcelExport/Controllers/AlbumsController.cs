using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BjornsRadioExcelExport.Data;
using BjornsRadioExcelExport.Models;
using ClosedXML.Excel;
using ClosedXML.Extensions;

namespace BjornsRadioExcelExport.Controllers
{
    public class AlbumsController : Controller
    {
        #region *** Constructors and properties ********************************
        private readonly BjornsRadioContext _context;

        public AlbumsController(BjornsRadioContext context)
        {
            _context = context;
        }
        #endregion


        #region *** Index and Details ******************************************
        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var bjornsRadioContext = _context.Albums.Include(a => a.GenreNavigation).Include(a => a.MediaNavigation);
            return View(await bjornsRadioContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.GenreNavigation)
                .Include(a => a.MediaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }
        #endregion


        #region *** Create *****************************************************
        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Id");
            ViewData["Media"] = new SelectList(_context.MediaTypes, "Id", "Id");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Artist,Title,ReleaseYear,Genre,Media,Comments")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Id", album.Genre);
            ViewData["Media"] = new SelectList(_context.MediaTypes, "Id", "Id", album.Media);
            return View(album);
        }
        #endregion


        #region *** Edit *******************************************************
        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Id", album.Genre);
            ViewData["Media"] = new SelectList(_context.MediaTypes, "Id", "Id", album.Media);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Artist,Title,ReleaseYear,Genre,Media,Comments")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
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
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Id", album.Genre);
            ViewData["Media"] = new SelectList(_context.MediaTypes, "Id", "Id", album.Media);
            return View(album);
        }
        #endregion


        #region *** Delete *****************************************************
        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.GenreNavigation)
                .Include(a => a.MediaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region *** ExportToExcel **********************************************
        public IActionResult ExportToExcel()
        {
            string dateString = String.Format("{0:dd MMM yyyy}", new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day));

            using (var wb = GenerateWorkbook())
            {
                return wb.Deliver($"Albums_{dateString}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
        #endregion


        #region *** Helper methods *********************************************
        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }

        private XLWorkbook GenerateWorkbook()
        {
            var query = from album in _context.Albums
                        select album;

            // Create workbook and worksheet
            XLWorkbook wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Albums");
            ws.Cell(1, 1).InsertTable(query);

            return wb;
        }
        #endregion
    }
}
