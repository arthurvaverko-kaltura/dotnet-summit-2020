using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using KLogMonitor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace ApiAppNetCore
{
    public class Startup
    {
        private static readonly KLogger _Logger = new KLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddQueuePolicy(c=> { 
                c.MaxConcurrentRequests = Environment.ProcessorCount;
                c.RequestQueueLimit = 5000;
                
                });

            services.AddControllers()
                .AddApplicationPart(typeof(ApiControllers.DataController).Assembly)
                .AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            KLogger.Configure("log4net.config");
            _Logger.Info("Starting application");

            var httpContextAccessor = app.ApplicationServices.GetService<IHttpContextAccessor>();
            System.Web.HttpContext.Configure(httpContextAccessor);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConcurrencyLimiter();

            app.Map("/api", apiApp =>
            {
                //Setting trace identifier for logs
                apiApp.Use(async (ctx, next) =>
                {
                    ctx.Items[KLogger.REQUEST_ID_KEY] = ctx.TraceIdentifier;
                    await next();
                });


                apiApp.UseRouting();
                apiApp.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}");
                    endpoints.MapControllers();
                });
            });

        }
    }
}
