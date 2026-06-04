namespace WarehouseSystemTest.Domain.Auth.Dto
{
    public class AuthTokenResultDto
    {
        public DateTime ExpiredAt { get; set; }
        public string Token { get; set; } = string.Empty;
    }

    public class AuthUserResultDto(Models.User user)
    {
        public Guid Id { get; set; } = user.Id;
        public string Name { get; set; } = user.Name;
        public string Email { get; set; } = user.Email;
        public string Role { get; set; } = user.Role;
    }
}
