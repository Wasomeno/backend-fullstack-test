using WarehouseSystemTest.Domain.Warehouse.Repositories;
using WarehouseSystemTest.Domain.Warehouse.Dto;
using WarehouseSystemTest.Infrastructure.Dto;

namespace WarehouseSystemTest.Domain.Warehouse.Services
{
    public class WarehouseService
    {
        private readonly WarehouseQueryRepository _queryRepository;
        private readonly WarehouseStoreRepository _storeRepository;

        public WarehouseService(
            WarehouseQueryRepository queryRepository,
            WarehouseStoreRepository storeRepository
        )
        {
            _queryRepository = queryRepository;
            _storeRepository = storeRepository;
        }

        public async Task<PaginationModel<WarehouseResultDto>> Index()
        {
            var result = await _queryRepository.Pagination();
            var formattedResult = WarehouseResultDto.MapRepo(result.Data);
            return PaginationModel<WarehouseResultDto>.Parse(formattedResult, result.Count, new QueryDto());
        }


        public async Task<WarehouseResultDto> Create(WarehouseCreateDto payload, Guid userId)
        {
            var data = WarehouseCreateDto.ToModel(payload);
            var cashIn = await _storeRepository.Create(data);
            return new WarehouseResultDto(cashIn);
        }

    }

}
