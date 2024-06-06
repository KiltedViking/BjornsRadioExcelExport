using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BjornsRadioExcelExport.Models;

public partial class Album
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Artist { get; set; } = null!;

    [StringLength(100)]
    public string Title { get; set; } = null!;

    [StringLength(4)]
    public string? ReleaseYear { get; set; }

    public int? Genre { get; set; }

    public int? Media { get; set; }

    [StringLength(150)]
    public string? Comments { get; set; }

    [ForeignKey("Genre")]
    [InverseProperty("Albums")]
    public virtual Genre? GenreNavigation { get; set; }

    [ForeignKey("Media")]
    [InverseProperty("Albums")]
    public virtual MediaType? MediaNavigation { get; set; }

    [InverseProperty("AlbumNavigation")]
    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
