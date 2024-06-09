using BjornsRadioExcelExport.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BjornsRadioExcelExport.ViewModels
{
    /// <summary>
    /// View model for Excel export of albums.
    /// </summary>
    public class AlbumExportViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Artist { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? ReleaseYear { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty!;
        public string Media { get; set; } = string.Empty!;
        public string? Comments { get; set; }
        public string SongsList { get; set; } = string.Empty!;
    }
}
