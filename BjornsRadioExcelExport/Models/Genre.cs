using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BjornsRadioExcelExport.Models;

public partial class Genre
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string GenreName { get; set; } = null!;

    [StringLength(150)]
    public string? Comments { get; set; }

    [InverseProperty("GenreNavigation")]
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
