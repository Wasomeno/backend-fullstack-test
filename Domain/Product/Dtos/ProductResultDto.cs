namespace WarehouseSystemTest.Domain.Product.Dto
{
    public class ProductResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string SKU { get; set; }
        public string Weight { get; set; }

        public ProductResultDto(Models.Product product)
        {
            Id = product?.Id ?? Guid.Empty;
            Name = product.Name;
            Unit = product.Unit;
            SKU = product.SKU;
            Weight = product.Weight;
        }

        public static List<ProductResultDto> MapRepo(List<Models.Product> data)
        {
            return data?.Select(data => new ProductResultDto(data)).ToList() ?? [];
        }
    }
}
