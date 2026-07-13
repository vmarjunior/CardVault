using CardVault.Application.Interfaces;
using CardVault.Application.Services;

namespace CardVault.API.Configuration
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //Application Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IUserCardService, UserCardService>();
            services.AddScoped<IDeckService, DeckService>();

            return services;
        }
    }
}
