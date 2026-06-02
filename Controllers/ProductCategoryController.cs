using Microsoft.AspNetCore.Mvc;
using WarehouseSystemTest.Domain.ProductCategory.Dto;
using WarehouseSystemTest.Domain.ProductCategory.Services;
using WarehouseSystemTest.Infrastructure.Shared;
using System.Net;

namespace WarehouseSystemTest.Controllers
{
    [Route("api/product-categories")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProductCategoryService _productCategoryService;

        public ProductCategoryController(ProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<ApiResponse> Index()
        {
            var paginationResult = await _productCategoryService.Index();
            return new ApiResponsePagination<ProductCategoryResultDto>(HttpStatusCode.OK, paginationResult);
        }

        [HttpPost]
        public async Task<ApiResponse> Store(ProductCategoryCreateDto payload)
        {
            var data = await _productCategoryService.Create(payload);
            return new ApiResponseData<ProductCategoryResultDto>(HttpStatusCode.Created, data);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(Guid id, ProductCategoryUpdateDto payload)
        {
            var data = await _productCategoryService.Update(id, payload);
            if (data is null)
                return new ApiResponseData<object>(HttpStatusCode.NotFound, null);
            return new ApiResponseData<ProductCategoryResultDto>(HttpStatusCode.OK, data);
        }
    }
}
