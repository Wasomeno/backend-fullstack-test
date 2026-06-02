using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseSystemTest.Models
{
    [Table("stock_level")]
    public class StockLevel
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }

        [Column("warehouse_location_id")]
        [MaxLength(256)]
        [Required]
        public Guid WarehouseLocationId { get; set; }

        [Column("product_id")]
        [MaxLength(256)]
        [Required]
        public Guid ProductId { get; set; }

        [Column("qty")]
        [MaxLength(256)]
        [Required]
        public int Qty { get; set; }

        public WarehouseLocation? WarehouseLocation { get; set; }
        public Product? Product { get; set; }
    }
}
