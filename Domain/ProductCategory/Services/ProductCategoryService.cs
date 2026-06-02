using WarehouseSystemTest.Domain.ProductCategory.Dto;
using WarehouseSystemTest.Domain.ProductCategory.Repositories;
using WarehouseSystemTest.Infrastructure.Dto;

namespace WarehouseSystemTest.Domain.ProductCategory.Services
{
    public class ProductCategoryService
    {
        private readonly ProductCategoryQueryRepository _queryRepository;
        private readonly ProductCategoryStoreRepository _storeRepository;

        public ProductCategoryService(
            ProductCategoryQueryRepository queryRepository,
            ProductCategoryStoreRepository storeRepository
        )
        {
            _queryRepository = queryRepository;
            _storeRepository = storeRepository;
        }

        public async Task<PaginationModel<ProductCategoryResultDto>> Index()
        {
            var result = await _queryRepository.Pagination();
            var formattedResult = ProductCategoryResultDto.MapRepo(result.Data);
            return PaginationModel<ProductCategoryResultDto>.Parse(formattedResult, result.Count, new QueryDto());
        }

        public async Task<ProductCategoryResultDto> Create(ProductCategoryCreateDto payload)
        {
            var data = ProductCategoryCreateDto.ToModel(payload);
            var created = await _storeRepository.Create(data);
            return new ProductCategoryResultDto(created);
        }

        public async Task<ProductCategoryResultDto?> Update(Guid id, ProductCategoryUpdateDto payload)
        {
            var existing = await _queryRepository.FindOneById(id);
            if (existing is null) return null;

            existing.Name = payload.Name;
            existing.Code = payload.Code;

            var updated = await _storeRepository.Update(existing);
            return new ProductCategoryResultDto(updated);
        }
    }
}
