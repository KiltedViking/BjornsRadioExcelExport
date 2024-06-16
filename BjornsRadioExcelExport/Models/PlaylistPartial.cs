namespace BjornsRadioExcelExport.Models
{
    public partial class Playlist
    {
        public string SongsCsv()
        {
            return this.PlaylistSongs.Aggregate("", (curr, pls) => curr + pls.Song.Title + ",");
        }
    }
}
