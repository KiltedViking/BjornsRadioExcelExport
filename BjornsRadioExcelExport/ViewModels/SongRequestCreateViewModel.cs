using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BjornsRadioExcelExport.Models;
using Microsoft.EntityFrameworkCore;

namespace BjornsRadioExcelExport.ViewModels;

public partial class SongRequestCreateViewModel

{
    [Required]
    public int SongId { get; set; }

    [StringLength(1000)]
    public string? MessageText { get; set; }

    public DateTime? RequestDate { get; set; }

    [Required]
    public DateTime WishedPlayDate { get; set; }

    public SongRequest ToSongRequest()
    {
        return new SongRequest()
        {
            SongId = SongId,
            MessageText = MessageText,
            RequestDate = RequestDate,
            WishedPlayDate = WishedPlayDate
        };
    }
}
