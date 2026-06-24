using CardVault.Application.Interfaces;
using CardVault.Application.Queries;
using CardVault.Domain.Repositories;
using CardVault.Infrastructure.Repositories;
using CardVault.Infrastructure.Helpers;
using CardVault.Infrastructure.Queries;

namespace CardVault.API.Configuration
{
    public static class InfrastructureServicesExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserQueries, UserQueries>();

            return services;
        }
    }
}
