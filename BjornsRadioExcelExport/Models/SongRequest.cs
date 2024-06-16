using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BjornsRadioExcelExport.Models;

public partial class SongRequest
{
    [Key]
    public int Id { get; set; }

    public int SongId { get; set; }

    [StringLength(1000)]
    public string? MessageText { get; set; }

    public DateTime? RequestDate { get; set; }

    public DateTime WishedPlayDate { get; set; }

    [ForeignKey("SongId")]
    [InverseProperty("SongRequests")]
    public virtual Song Song { get; set; } = null!;
}
