namespace WarehouseSystemTest.Domain.Product.Dto
{
    public class ProductResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string SKU { get; set; }
        public string Weight { get; set; }
        public ProductCategoryDto? ProductCategory { get; set; }

        public ProductResultDto(Models.Product product)
        {
            Id = product?.Id ?? Guid.Empty;
            Name = product.Name;
            Unit = product.Unit;
            SKU = product.SKU;
            Weight = product.Weight;
            ProductCategory = product.ProductCategory != null
                ? new ProductCategoryDto(product.ProductCategory)
                : null;
        }

        public static List<ProductResultDto> MapRepo(List<Models.Product> data) =>
            data?.Select(d => new ProductResultDto(d)).ToList() ?? [];
    }

    public class ProductCategoryDto(Models.ProductCategory productCategory)
    {
        public Guid Id { get; set; } = productCategory.Id;
        public string Name { get; set; } = productCategory.Name;
        public string Code { get; set; } = productCategory.Code;
    }
}
