using System;
using HotelBooking.Repository.InMemoryData;
using HotelBooking.Repository.Base;
using HotelBooking.Services.Base;
using HotelBooking.Services.Providers.Contracts;

namespace HotelBooking.Api.Installers
{
    public static class BusinessServiceInstaller
    {
        public static void AddBusinessServices(this IServiceCollection services/*, IConfigurationProvider configuration*/)
        {
            services.RegisterAllDirectImplementations<IService>(ServiceLifetime.Scoped);
            services.RegisterAllDirectImplementations<IRepository>(ServiceLifetime.Scoped);
            services.RegisterAllDirectImplementations<IProvider>(ServiceLifetime.Scoped);

            //services.AddDbContext<PlannerDbContext>(options => options.UseSqlServer("connectionString"));
        }

        public static void InjectAdditionalInterfaces(this IServiceCollection services)
        {
            services.AddSingleton<IInMemoryData, InMemoryData>();
        }
    }
}

