using WarehouseSystemTest.Infrastructure.Integrations.Http;

namespace WarehouseSystemTest
{
    public partial class Startup
    {
        public void Integrations(IServiceCollection services)
        {
            services.AddScoped<HttpIntegration>();
        }
    }
}
