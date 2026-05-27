namespace WarehouseSystemTest.Models;

public class ProductCategory
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
