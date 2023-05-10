using System;
using HotelBooking.Repository.Base;
using HotelBooking.Services.Base;

namespace HotelBooking.Api.Installers
{
    public static class BusinessServiceInstaller
    {
        public static void AddBusinessServices(this IServiceCollection services/*, IConfigurationProvider configuration*/)
        {
            services.RegisterAllDirectImplementations<IService>(ServiceLifetime.Scoped);
            services.RegisterAllDirectImplementations<IRepository>(ServiceLifetime.Scoped);

            //services.AddDbContext<PlannerDbContext>(options => options.UseSqlServer("connectionString"));
        }
    }
}

