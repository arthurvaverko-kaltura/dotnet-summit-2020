using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using KLogMonitor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoapCore;

namespace IngestNetCore
{
    public class Startup
    {

        private static readonly KLogger _Logger = new KLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMvc(o =>
            {
                o.EnableEndpointRouting = false;
            });

            services.AddScoped<Ingest.IService, IngestService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            KLogger.Configure("log4net.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (ctx, next) =>
            {
                ctx.Request.EnableBuffering();
                using (var reader = new StreamReader(ctx.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    var bodyStr = await reader.ReadToEndAsync();
                    _Logger.Info(bodyStr);

                }

                ctx.Request.Body.Seek(0, SeekOrigin.Begin);

                await next();
            });

            var httpContextAccessor = app.ApplicationServices.GetService<IHttpContextAccessor>();
            System.Web.HttpContext.Configure(httpContextAccessor);

            BasicHttpBinding ingestBinding = new BasicHttpBinding() { ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas() { MaxStringContentLength = int.MaxValue } };
            app.UseSoapEndpoint<Ingest.IService>("/Service.svc", ingestBinding, SoapSerializer.DataContractSerializer, caseInsensitivePath: true);

            app.UseMvc();
        }
    }
}
