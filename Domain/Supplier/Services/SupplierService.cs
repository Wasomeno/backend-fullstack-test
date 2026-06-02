using WarehouseSystemTest.Domain.Supplier.Dto;
using WarehouseSystemTest.Domain.Supplier.Repositories;
using WarehouseSystemTest.Infrastructure.Dto;

namespace WarehouseSystemTest.Domain.Supplier.Services
{
    public class SupplierService
    {
        private readonly SupplierQueryRepository _queryRepository;
        private readonly SupplierStoreRepository _storeRepository;

        public SupplierService(
            SupplierQueryRepository queryRepository,
            SupplierStoreRepository storeRepository
        )
        {
            _queryRepository = queryRepository;
            _storeRepository = storeRepository;
        }

        public async Task<PaginationModel<SupplierResultDto>> Index()
        {
            var result = await _queryRepository.Pagination();
            var formattedResult = SupplierResultDto.MapRepo(result.Data);
            return PaginationModel<SupplierResultDto>.Parse(formattedResult, result.Count, new QueryDto());
        }

        public async Task<SupplierResultDto?> Detail(Guid id)
        {
            var data = await _queryRepository.FindOneById(id);
            if (data is null) return null;
            return new SupplierResultDto(data);
        }

        public async Task<SupplierResultDto> Create(SupplierCreateDto payload)
        {
            var data = SupplierCreateDto.ToModel(payload);
            var created = await _storeRepository.Create(data);
            return new SupplierResultDto(created);
        }

        public async Task<SupplierResultDto?> Update(Guid id, SupplierUpdateDto payload)
        {
            var existing = await _queryRepository.FindOneById(id);
            if (existing is null) return null;

            existing.Name = payload.Name;
            existing.Address = payload.Address;
            existing.PhoneNumber = payload.PhoneNumber;
            existing.Email = payload.Email;

            var updated = await _storeRepository.Update(existing);
            return new SupplierResultDto(updated);
        }
    }
}
