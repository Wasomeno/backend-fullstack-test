using WarehouseSystemTest.Domain.Product.Repositories;
using WarehouseSystemTest.Domain.Product.Dto;
using WarehouseSystemTest.Infrastructure.Dto;

namespace WarehouseSystemTest.Domain.Product.Services
{
    public class ProductService
    {
        private readonly ProductQueryRepository _queryRepository;
        private readonly ProductStoreRepository _storeRepository;

        public ProductService(
            ProductQueryRepository queryRepository,
            ProductStoreRepository storeRepository
        )
        {
            _queryRepository = queryRepository;
            _storeRepository = storeRepository;
        }

        public async Task<PaginationModel<ProductResultDto>> Index()
        {
            var result = await _queryRepository.Pagination();
            var formattedResult = ProductResultDto.MapRepo(result.Data);
            return PaginationModel<ProductResultDto>.Parse(formattedResult, result.Count, new QueryDto());
        }


        public async Task<ProductResultDto> Create(ProductCreateDto payload, Guid userId)
        {
            var data = ProductCreateDto.ToModel(payload);
            var cashIn = await _storeRepository.Create(data);
            return new ProductResultDto(cashIn);
        }

    }

}
