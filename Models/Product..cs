namespace WarehouseSystemTest.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;

    public ProductCategory? ProductCategory { get; set; }
}
