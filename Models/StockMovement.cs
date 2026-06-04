using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseSystemTest.Models
{
    [Table("stock_movement")]
    public class StockMovement
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("type")]
        [MaxLength(50)]
        [Required]
        public string Type { get; set; } = string.Empty;

        [Column("movement_code")]
        [MaxLength(256)]
        [Required]
        public string MovementCode { get; set; } = string.Empty;

        [Column("qty")]
        [MaxLength(256)]
        [Required]
        public int Qty { get; set; }

        [Column("status")]
        [MaxLength(50)]
        [Required]
        public string Status { get; set; } = string.Empty;

        [Column("created_by_id")]
        public Guid? CreatedById { get; set; }

        [Column("completed_by_id")]
        public Guid? CompletedById { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Column("completed_at")]
        [Required]
        public DateTime? CompletedAt { get; set; }

        [Column("product_id")]
        [Required]
        public Guid ProductId { get; set; }

        [Column("supplier_id")]
        public Guid? SupplierId { get; set; }

        [Column("warehouse_location_from_id")]
        public Guid? WarehouseLocationFromId { get; set; }

        [Column("warehouse_location_to_id")]
        public Guid? WarehouseLocationToId { get; set; }

        [ForeignKey(nameof(WarehouseLocationFromId))]
        public WarehouseLocation? WarehouseLocationFrom { get; set; }

        [ForeignKey(nameof(WarehouseLocationToId))]
        public WarehouseLocation? WarehouseLocationTo { get; set; }

        [ForeignKey(nameof(CompletedById))]
        public User? CompletedBy { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public User? CreatedBy { get; set; }

        public Product? Product { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
