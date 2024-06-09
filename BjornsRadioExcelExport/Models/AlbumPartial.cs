using System.Text;

namespace BjornsRadioExcelExport.Models
{
    public partial class Album
    {
        /// <summary>
        /// Returns a comma-separated string with all songs on album
        /// </summary>
        public string SongsCsv { get {
                // Loop works, but how efficient is it?
                //var sb = new StringBuilder();
                //foreach (var song in Songs)
                //{
                //    sb.Append(song.Title + ", ");
                //}
                //return sb.ToString();

                // Lenght property doesn't return lenght of string... :-S
                //var length = sb.Length;
                //return sb.ToString(0, length - 2);

                // Agregate works in pages, but is it more efficient than loop?
                return Songs.Aggregate("", (curr, song) => curr + song.AlbumOrder + ". " + song.Title + ", ");

                // Length property in String class doesn't return a value either... :-S
                //var list = Songs.Aggregate("", (curr, song) => curr + song.Title + ", ");
                //return list.Substring(0, list.Length - 1);
            }
        }
    }
}
