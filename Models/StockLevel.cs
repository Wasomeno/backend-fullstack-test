namespace WarehouseSystemTest.Models;

public class StockLevel
{
    public long Id { get; set; }
    public long WarehouseLocationId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }

    public WarehouseLocation? WarehouseLocation { get; set; }
    public Product? Product { get; set; }
}
