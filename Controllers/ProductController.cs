using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseSystemTest.Infrastructure.Database;
using WarehouseSystemTest.Models;
using WarehouseSystemTest.Infrastructure.Shared;
using WarehouseSystemTest.Domain.Product.Services;
using WarehouseSystemTest.Domain.Product.Dto;
using System.Net;

namespace WarehouseSystemTest.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(ProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ApiResponse> Index()
        {
            var paginationResult = await _productService.Index();
            return new ApiResponsePagination<ProductResultDto>(HttpStatusCode.OK, paginationResult);
        }

        [HttpPost]
        public async Task<ApiResponse> Store(ProductCreateDto payload)
        {
            _ = Guid.TryParse(_httpContextAccessor.HttpContext!.User.FindFirst("id")?.Value, out Guid userId);
            var data = await _productService.Create(payload, userId);
            return new ApiResponseData<ProductResultDto>(HttpStatusCode.Created, data);
        }
    }
}
