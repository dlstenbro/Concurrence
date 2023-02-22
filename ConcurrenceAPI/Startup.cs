using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace ConcurrenceAPI
{
    public class Startup
    {
        private const string _CORSName = "CORSSettings";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configure CORS 
            // TODO: use localhost for now
            //       it's not recommended since there could be middleman attacks from other services on the machine
            const string allowed_hosts = "localhost";

            services.AddCors(options =>
            {
                options.AddPolicy(_CORSName, policy =>
                {
                    policy.WithOrigins()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed( origin => new Uri(origin).Host == allowed_hosts);
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConcurrenceApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConcurrenceApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_CORSName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
