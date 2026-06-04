using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using WarehouseSystemTest.Domain.Auth.Util;
using WarehouseSystemTest.Infrastructure.Shared;

namespace WarehouseSystemTest.Infrastructure.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuthorizationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            if (path is not null && (path.StartsWith("/swagger") || path.StartsWith("/openapi")))
            {
                await _next(context);
                return;
            }

            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() is not null)
            {
                await _next(context);
                return;
            }

            var authorizationHeader = context.Request.Headers.Authorization.ToString();
            if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                await WriteUnauthorized(context, "Missing bearer token.");
                return;
            }

            var token = authorizationHeader["Bearer ".Length..];
            var secret = _configuration["JWTSetting:Secret"];
            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidOperationException("Missing JWTSetting:Secret configuration.");
            }

            try
            {
                context.User = AuthUtility.ValidateJwtToken(secret, token);
                await _next(context);
            }
            catch
            {
                await WriteUnauthorized(context, "Invalid or expired token.");
            }
        }

        private static async Task WriteUnauthorized(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new ApiResponseError(HttpStatusCode.Unauthorized, message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
