using WarehouseSystemTest.Constants.User;
using WarehouseSystemTest.Domain.Auth.Dto;
using WarehouseSystemTest.Domain.Auth.Util;
using WarehouseSystemTest.Domain.User.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace WarehouseSystemTest.Domain.Auth.Services
{
    public class AuthService
    {
        private readonly UserQueryRepository _userQueryRepository;
        private readonly UserStoreRepository _userStoreRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(
            UserQueryRepository userQueryRepository,
            UserStoreRepository userStoreRepository,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userQueryRepository = userQueryRepository;
            _userStoreRepository = userStoreRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthTokenResultDto> SignIn(AuthSignInDto payload)
        {
            var user = await _userQueryRepository.FindOneByEmail(payload.Email);
            if (user is null || !BC.Verify(payload.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Email or password is incorrect.");
            }

            return GenerateToken(user);
        }

        public async Task<AuthUserResultDto> Register(AuthRegisterDto payload)
        {
            if (!IsValidRole(payload.Role))
            {
                throw new UnprocessableEntityException("Role is invalid.");
            }

            if (await _userQueryRepository.IsEmailExists(payload.Email))
            {
                throw new UnprocessableEntityException("Email is already registered.");
            }

            var user = new Models.User
            {
                Id = Guid.NewGuid(),
                Name = payload.Name,
                Email = payload.Email,
                Password = BC.HashPassword(payload.Password),
                Role = payload.Role
            };

            var created = await _userStoreRepository.Create(user);
            return new AuthUserResultDto(created);
        }

        public async Task<AuthUserResultDto> Account()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("id")?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            var user = await _userQueryRepository.FindOneById(userId);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return new AuthUserResultDto(user);
        }

        private AuthTokenResultDto GenerateToken(Models.User user)
        {
            var tokenLifetimeInMinutes = int.Parse(_configuration["JWTSetting:LifetimeInMinutes"] ?? "60");
            var expiredAt = DateTime.UtcNow.AddMinutes(tokenLifetimeInMinutes);
            var secret = _configuration["JWTSetting:Secret"];

            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidOperationException("Missing JWTSetting:Secret configuration.");
            }

            return new AuthTokenResultDto
            {
                ExpiredAt = expiredAt,
                Token = AuthUtility.GenerateJwtToken(secret, user, expiredAt)
            };
        }

        private static bool IsValidRole(string role)
        {
            return role == UserRoleConstant.ADMIN ||
                   role == UserRoleConstant.SUPERVISOR ||
                   role == UserRoleConstant.WAREHOUSE_OPERATOR;
        }
    }
}
