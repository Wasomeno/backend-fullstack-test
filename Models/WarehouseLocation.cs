namespace WarehouseSystemTest.Models;

public class WarehouseLocation
{
    public Guid Id { get; set; }
    public Guid WarehouseId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Zone { get; set; } = string.Empty;
    public string Rack { get; set; } = string.Empty;
    public string Bin { get; set; } = string.Empty;

    public Warehouse? Warehouse { get; set; }
    public ICollection<StockLevel> StockLevels { get; set; } = new List<StockLevel>();
}
