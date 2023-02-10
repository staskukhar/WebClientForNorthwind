using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Keyless]
public partial class EmployeeTerritory
{
    [Column("EmployeeID", TypeName = "INT")]
    public int EmployeeId { get; set; }

    [Column("TerritoryID", TypeName = "nvarchar] (20")]
    [Required]
    public string TerritoryId { get; set; } = null!;
}
