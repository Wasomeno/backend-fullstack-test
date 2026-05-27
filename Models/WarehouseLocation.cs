namespace WarehouseSystemTest.Models;

public class WarehouseLocation
{
    public long Id { get; set; }
    public long WarehouseId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Zone { get; set; } = string.Empty;
    public string Rack { get; set; } = string.Empty;
    public string Bin { get; set; } = string.Empty;

    public Warehouse? Warehouse { get; set; }
    public ICollection<StockLevel> StockLevels { get; set; } = new List<StockLevel>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public ICollection<StockMovement> SourceStockMovements { get; set; } = new List<StockMovement>();
    public ICollection<StockMovement> DestinationStockMovements { get; set; } = new List<StockMovement>();
}
