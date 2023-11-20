using Application.Services;
using Infrastructure.DataAccess;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddDbContext<HospitalDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Connection")));
        }
    }
}
