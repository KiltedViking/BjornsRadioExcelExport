using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BjornsRadioExcelExport.Models;

public partial class Song
{
    [Key]
    public int Id { get; set; }

    public int Album { get; set; }

    public byte AlbumOrder { get; set; }

    [StringLength(75)]
    public string Title { get; set; } = null!;

    [StringLength(150)]
    public string? Comments { get; set; }

    [ForeignKey("Album")]
    [InverseProperty("Songs")]
    public virtual Album AlbumNavigation { get; set; } = null!;

    [InverseProperty("Song")]
    public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();

    [InverseProperty("Song")]
    public virtual ICollection<SongRequest> SongRequests { get; set; } = new List<SongRequest>();
}
