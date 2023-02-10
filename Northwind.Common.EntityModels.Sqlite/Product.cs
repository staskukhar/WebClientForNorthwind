using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Index("CategoryId", Name = "CategoriesProducts")]
[Index("CategoryId", Name = "CategoryID")]
[Index("ProductName", Name = "ProductName")]
[Index("SupplierId", Name = "SupplierID")]
[Index("SupplierId", Name = "SuppliersProducts")]
public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column(TypeName = "nvarchar (40)")]
    [Required]
    [StringLength(40)]
    public string ProductName { get; set; } = null!;

    [Column("SupplierID", TypeName = "INT")]
    public int? SupplierId { get; set; }

    [Column("CategoryID", TypeName = "INT")]
    public int? CategoryId { get; set; }

    [Column(TypeName = "nvarchar (20)")]
    [StringLength(20)]
    public string? QuantityPerUnit { get; set; }

    [Column(TypeName = "money")]
    public decimal? UnitPrice { get; set; }

    [Column(TypeName = "smallint")]
    public long? UnitsInStock { get; set; }

    [Column(TypeName = "smallint")]
    public long? UnitsOnOrder { get; set; }

    [Column(TypeName = "smallint")]
    public long? ReorderLevel { get; set; }

    [Column(TypeName = "bit")]
    public bool? Discontinued { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    [ForeignKey("SupplierId")]
    [InverseProperty("Products")]
    public virtual Supplier? Supplier { get; set; }
}
