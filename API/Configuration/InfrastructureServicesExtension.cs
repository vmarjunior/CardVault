using CardVault.Application.Interfaces;
using CardVault.Application.Queries;
using CardVault.Domain.Repositories;
using CardVault.Infrastructure.ExternalServices;
using CardVault.Infrastructure.Helpers;
using CardVault.Infrastructure.Queries;
using CardVault.Infrastructure.Repositories;

namespace CardVault.API.Configuration
{
    public static class InfrastructureServicesExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IUserCardRepository, UserCardRepository>();
            services.AddScoped<ICardSetRepository, CardSetRepository>();
            services.AddScoped<IDeckRepository, DeckRepository>();

            //Helpers
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //External Services
            services.AddHttpClient<ICardExternalService, ScryfallService>(client =>
            {
                // Scryfall REQUIRES a User-Agent and Accept header
                client.DefaultRequestHeaders.Add("User-Agent", "CardVaultApp/1.0");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            //Queries
            services.AddScoped<IUserQueries, UserQueries>();

            return services;
        }
    }
}
