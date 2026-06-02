namespace WarehouseSystemTest.Domain.ProductCategory.Dto
{
    public class ProductCategoryCreateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public static Models.ProductCategory ToModel(ProductCategoryCreateDto data) =>
            new Models.ProductCategory
            {
                Name = data.Name,
                Code = data.Code,
            };
    }
}
