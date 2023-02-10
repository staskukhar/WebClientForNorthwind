﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Keyless]
public partial class Territory
{
    [Column("TerritoryID", TypeName = "nvarchar] (20")]
    [Required]
    public string TerritoryId { get; set; } = null!;

    [Column(TypeName = "nchar] (50")]
    [Required]
    public string TerritoryDescription { get; set; } = null!;

    [Column("RegionID", TypeName = "INT")]
    public int RegionId { get; set; }
}
