using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BjornsRadioExcelExport.Models;

public partial class MediaType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string TypeName { get; set; } = null!;

    [StringLength(150)]
    public string? Comments { get; set; }

    [InverseProperty("MediaNavigation")]
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
