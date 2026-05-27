namespace WarehouseSystemTest.Models;

public class StockMovement
{
    public long Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string MovementCodeNumber { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public long? SupplierId { get; set; }
    public long ProductId { get; set; }
    public long? WarehouseLocationId { get; set; }
    public string Status { get; set; } = string.Empty;
    public long CreatedById { get; set; }
    public long? CompletedById { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime MovedAt { get; set; }
    public long? WarehouseLocationFromId { get; set; }
    public long? WarehouseLocationToId { get; set; }
    public string? Notes { get; set; }

    public Supplier? Supplier { get; set; }
    public Product? Product { get; set; }
    public WarehouseLocation? WarehouseLocation { get; set; }
    public User? CreatedBy { get; set; }
    public User? CompletedBy { get; set; }
    public WarehouseLocation? WarehouseLocationFrom { get; set; }
    public WarehouseLocation? WarehouseLocationTo { get; set; }
}
