using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BjornsRadioExcelExport.Models;

[PrimaryKey("PlaylistId", "SongId")]
public partial class PlaylistSong
{
    [Key]
    public int PlaylistId { get; set; }

    [Key]
    public int SongId { get; set; }

    public int PlayOrder { get; set; }

    [ForeignKey("PlaylistId")]
    [InverseProperty("PlaylistSongs")]
    public virtual Playlist Playlist { get; set; } = null!;

    [ForeignKey("SongId")]
    [InverseProperty("PlaylistSongs")]
    public virtual Song Song { get; set; } = null!;
}
