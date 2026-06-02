using Microsoft.AspNetCore.Mvc;
using WarehouseSystemTest.Domain.Supplier.Dto;
using WarehouseSystemTest.Domain.Supplier.Services;
using WarehouseSystemTest.Infrastructure.Shared;
using System.Net;

namespace WarehouseSystemTest.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _supplierService;

        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ApiResponse> Index()
        {
            var paginationResult = await _supplierService.Index();
            return new ApiResponsePagination<SupplierResultDto>(HttpStatusCode.OK, paginationResult);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> Show(Guid id)
        {
            var data = await _supplierService.Detail(id);
            if (data is null)
                return new ApiResponseData<object>(HttpStatusCode.NotFound, null);
            return new ApiResponseData<SupplierResultDto>(HttpStatusCode.OK, data);
        }

        [HttpPost]
        public async Task<ApiResponse> Store(SupplierCreateDto payload)
        {
            var data = await _supplierService.Create(payload);
            return new ApiResponseData<SupplierResultDto>(HttpStatusCode.Created, data);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(Guid id, SupplierUpdateDto payload)
        {
            var data = await _supplierService.Update(id, payload);
            if (data is null)
                return new ApiResponseData<object>(HttpStatusCode.NotFound, null);
            return new ApiResponseData<SupplierResultDto>(HttpStatusCode.OK, data);
        }
    }
}
