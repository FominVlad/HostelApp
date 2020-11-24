using HostelDB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HostelDB
{
    public static class ServiceProviderExtensions
    {
        public static void AddHostelUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<HostelUnitOfWork>();
        }
    }
}
