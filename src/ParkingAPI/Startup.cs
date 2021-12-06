using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parking.API.Repositories;
using Parking.Core.Interfaces;
using Parking.DataSource;

namespace Parking.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            // You need to decide which data provider to use here.  
            // For concurrent dictionary
            //var dataInstance = new ConcurrentDictionary();
            //services.AddSingleton<IParkingDataSource>(dataInstance);
            //For EF in memory DB - NOTE - a better way would be to take the time to not have the EF Core dependency in this project
            // The next line is always needed due to a dependency in Main
            services.AddDbContext<ParkingDBContext>(options => options.UseInMemoryDatabase(databaseName: "ParkingLot"),
            contextLifetime: ServiceLifetime.Scoped, 
            optionsLifetime: ServiceLifetime.Singleton);

            services.AddScoped<IParkingDataSource, EFCore>();

            services.AddScoped<IParkingLotRepository, ParkingLotRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ParkingDBContext>();

                DataGenerator.Initialize(context);
            }

        }
    }
}
