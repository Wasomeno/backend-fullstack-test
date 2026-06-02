namespace WarehouseSystemTest.Domain.ProductCategory.Dto
{
    public class ProductCategoryUpdateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public static Models.ProductCategory ToModel(ProductCategoryUpdateDto data) =>
            new Models.ProductCategory
            {
                Name = data.Name,
                Code = data.Code,
            };
    }
}
