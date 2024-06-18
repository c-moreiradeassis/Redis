using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Setup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDapper();

            return services;
        }

        public static IServiceCollection AddDapper(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();

            services.AddScoped<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
