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
using WarehouseSystemTest.Domain.Warehouse.Services;
using WarehouseSystemTest.Domain.Warehouse.Dto;
using System.Net;

namespace WarehouseSystemTest.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WarehouseController(WarehouseService warehouseService, IHttpContextAccessor httpContextAccessor)
        {
            _warehouseService = warehouseService;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/warehouses
        [HttpGet]
        public async Task<ApiResponse> Index()
        {
            var paginationResult = await _warehouseService.Index();
            return new ApiResponsePagination<WarehouseResultDto>(HttpStatusCode.OK, paginationResult);
        }

        [HttpPost]
        public async Task<ApiResponse> Store(WarehouseCreateDto payload)
        {
            _ = Guid.TryParse(_httpContextAccessor.HttpContext!.User.FindFirst("id")?.Value, out Guid userId);
            var data = await _warehouseService.Create(payload, userId);
            return new ApiResponseData<WarehouseResultDto>(HttpStatusCode.Created, data);
        }
    }
}
