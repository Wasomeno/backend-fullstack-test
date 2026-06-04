using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseSystemTest.Domain.Auth.Dto;
using WarehouseSystemTest.Domain.Auth.Services;
using WarehouseSystemTest.Infrastructure.Shared;

namespace WarehouseSystemTest.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ApiResponse> Register(AuthRegisterDto payload)
        {
            var data = await _authService.Register(payload);
            return new ApiResponseData<AuthUserResultDto>(HttpStatusCode.Created, data);
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ApiResponse> SignIn(AuthSignInDto payload)
        {
            var data = await _authService.SignIn(payload);
            return new ApiResponseData<AuthTokenResultDto>(HttpStatusCode.OK, data);
        }

        [HttpGet("account")]
        public async Task<ApiResponse> Account()
        {
            var data = await _authService.Account();
            return new ApiResponseData<AuthUserResultDto>(HttpStatusCode.OK, data);
        }
    }
}
