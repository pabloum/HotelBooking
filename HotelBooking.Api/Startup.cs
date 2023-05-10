using HotelBooking.Api.Installers;
using HotelBooking.Api.Middleware;

namespace HotelBooking.Api
{
    public partial class Startup
    {
        public IConfiguration configRoot { get; }

        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddAuthentication( ...

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Last hotel in Cancun Booking", Version = "v1" });
            });

            services.AddBusinessServices();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseCors("MyAllowSpecificationOrigins");

            //app.UseAuthentication();
            //app.UseAuthorization();
            //app.UseResponseLogging();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }

}

