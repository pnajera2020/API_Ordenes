using Business;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API_Ordenes
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        private const string API_CONNECTION = "MyWebApiConection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApiDBContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString(API_CONNECTION)));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Microservicio de órdenes",
                    Description = "" +
                    "",
                    License = new OpenApiLicense
                    {
                        Name = "C&A Systems"
                    }
                });
            });

            //20200917
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .WithMethods("POST", "PUT", "DELETE", "GET");
                                  });
            });

            services.AddScoped<IBsEstatus, BsEstatus>();
            services.AddScoped<IBsMetodoPago, BsMetodoPago>();
            services.AddScoped<IBsOrden, BsOrden>();
            services.AddScoped<IBsOrdenProducto, BsOrdenProducto>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
            });

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
