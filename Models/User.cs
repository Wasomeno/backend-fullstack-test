namespace WarehouseSystemTest.Models;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public ICollection<StockMovement> CreatedStockMovements { get; set; } = new List<StockMovement>();
    public ICollection<StockMovement> CompletedStockMovements { get; set; } = new List<StockMovement>();
}
