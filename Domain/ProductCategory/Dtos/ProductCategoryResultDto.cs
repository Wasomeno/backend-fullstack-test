namespace WarehouseSystemTest.Domain.ProductCategory.Dto
{
    public class ProductCategoryResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ProductCategoryResultDto(Models.ProductCategory productCategory)
        {
            Id = productCategory.Id;
            Name = productCategory.Name;
            Code = productCategory.Code;
        }

        public static List<ProductCategoryResultDto> MapRepo(List<Models.ProductCategory> data) =>
            data?.Select(d => new ProductCategoryResultDto(d)).ToList() ?? [];
    }
}
