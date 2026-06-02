namespace WarehouseSystemTest.Domain.Product.Dto
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string SKU { get; set; }
        public string Weight { get; set; }
        public Guid ProductCategoryId { get; set; }

        public static WarehouseSystemTest.Models.Product ToModel(ProductCreateDto data)
        {
            var result = new WarehouseSystemTest.Models.Product
            {
                Name = data.Name,
                Unit = data.Unit,
                SKU = data.SKU,
                Weight = data.Weight,
                ProductCategoryId = data.ProductCategoryId
            };
            return result;
        }
    }
}
