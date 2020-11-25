using HostelDB.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.WEB.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddHostelUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<HostelUnitOfWork>();
        }
    }
}
