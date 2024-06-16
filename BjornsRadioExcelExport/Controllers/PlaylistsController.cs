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
    public class PlaylistsController : Controller
    {
        #region *** Constructors and preoperties *****************************
        private readonly BjornsRadioContext _context;
        public PlaylistsController(BjornsRadioContext context)
        {
            _context = context;
        }
        #endregion


        #region *** Index and Details ******************************************
        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Playlists.ToListAsync());
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }
        #endregion


        #region *** Create *****************************************************
        // GET: Playlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayDate,Name,Comments")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playlist);
        }
        #endregion


        #region *** Edit *******************************************************
        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayDate,Name,Comments")] Playlist playlist)
        {
            if (id != playlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.Id))
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
            return View(playlist);
        }
        #endregion


        #region *** Delete *****************************************************
        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region *** Custom actions *******************************************

        #region *** ExportToExcel ********************************************
        public IActionResult ExportToExcel()
        {
            // Generate date string for filename
            string dateString = String.Format("{0:yyyy_MMM_dd}", new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day));

            // Get Excel file and return to client
            using (var wb = GenerateWorkbook())
            {
                return wb.Deliver($"Playlists_{dateString}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }


        private XLWorkbook GenerateWorkbook()
        {
            // TODO 2024-06-16: Fix song list (which isn't the same as songs in album as songs in playlist is a M:M relationship)
            var query = from playlist in _context.Playlists.Include(pl => pl.PlaylistSongs).ThenInclude(pls => pls.Song)
                        select new PlaylistExportViewModel
                        {
                            Id = playlist.Id,
                            Name = playlist.Name,
                            PlayDate = playlist.PlayDate,
                            Comments = playlist.Comments,
                            PlaylistSongsCsv = playlist.SongsCsv()
                        };

            // Create workbook and worksheet, and add data to worksheet
            XLWorkbook wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Playlists");
            ws.Cell(1, 1).InsertTable(query);
            // Set widths of columns
            ws.Column(2).Width = 30;
            ws.Column(3).Width = 15;
            ws.Column(4).Width = 30;
            // Freeze first row and first two columns
            ws.SheetView.Freeze(1, 2);

            return wb;
        }
        #endregion

        #endregion


        #region *** Helper methods *********************************************
        private bool PlaylistExists(int id)
        {
            return _context.Playlists.Any(e => e.Id == id);
        }
        #endregion
    }
}
