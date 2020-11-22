using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BearTracApi.Models;
using Microsoft.Extensions.Options;
using BearTracApi.Services;

namespace BearTracApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5000",
                                            "https://localhost:5001",
                                            "https://localhost:5500",
                                            "https://localhost:5501",
                                            "http://localhost:5000",
                                            "http://localhost:5001",
                                            "http://localhost:5500",
                                            "http://localhost:5501",
                                            "https://zc0rp10.github.io").AllowAnyMethod().AllowAnyHeader();
                    });
            });
            // requires using Microsoft.Extensions.Options
            services.Configure<BearTracDatabaseSettings>(
                Configuration.GetSection(nameof(BearTracDatabaseSettings)));

            services.AddSingleton<IBearTracDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<BearTracDatabaseSettings>>().Value);

            services.AddSingleton<ApplicationService>();
            services.AddSingleton<TicketService>();

            services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
