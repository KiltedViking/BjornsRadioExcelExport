using BjornsRadioExcelExport.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BjornsRadioExcelExport.ViewModels
{
    public class PlaylistExportViewModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime PlayDate { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        public string? Comments { get; set; }

        //[InverseProperty("Playlist")]
        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
        public string PlaylistSongsCsv { get; set; } = string.Empty;

        public string ToString()
        {
            return this.PlaylistSongs.Aggregate("", (curr, pls) => curr + pls.Song.Title + ",");
        }
    }
}
