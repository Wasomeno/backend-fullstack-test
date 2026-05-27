namespace WarehouseSystemTest.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long ProductCategoryId { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public decimal Weight { get; set; }

    public ProductCategory? ProductCategory { get; set; }
    public ICollection<StockLevel> StockLevels { get; set; } = new List<StockLevel>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
