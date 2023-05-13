using System;
using HotelBooking.Persistence.Context;
using HotelBooking.Persistence.InMemoryData;
using HotelBooking.Repository.Base;
using HotelBooking.Services.Base;
using HotelBooking.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Installers
{
    public static class BusinessServiceInstaller
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterAllDirectImplementations<IService>(ServiceLifetime.Scoped);
            services.RegisterAllDirectImplementations<IRepository>(ServiceLifetime.Scoped);
            services.RegisterAllDirectImplementations<IProvider>(ServiceLifetime.Scoped);

            services.AddDbContext<HotelBookingDbContext>(options =>
                options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:HotelBookinDatabase")));

            using (var context = services.BuildServiceProvider().GetRequiredService<HotelBookingDbContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public static void InjectAdditionalInterfaces(this IServiceCollection services)
        {
            services.AddSingleton<IInMemoryData, InMemoryData>();
        }
    }
}