using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wmg.App.Domain.Models;
using Wmg.App.Domain.Services;

namespace Wmg.App
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
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IStatementParser>(s =>
            {
                IStatementParser parser;

                switch (GetCard(s))
                {
                    case Card.Barclaycard:
                        parser = new BarclaycardParser(s.GetService<ILogger<BarclaycardParser>>());
                        break;
                    case Card.Hsbc:
                        parser = new HsbcParser(s.GetService<ILogger<HsbcParser>>());
                        break;
                    default:
                        parser = new AmazoncardParser(s.GetService<ILogger<AmazoncardParser>>());
                        break;
                }

                return parser;

            });
            services.AddTransient<IClean>(s =>
            {
                switch (GetCard(s))
                {
                    case Card.Barclaycard:
                        return new BarclaycardCleaner();
                    case Card.Hsbc:
                        return new HsbcCleaner();
                    default:
                        return new AmazoncardCleaner();
                }
            });

            services.AddMvc();
        }

        private static Card GetCard(IServiceProvider s)
        {
            var httpContext = s.GetService<IHttpContextAccessor>().HttpContext;
            if (httpContext.Request.Path.Value.Contains("barclaycard")) return Card.Barclaycard;
            if (httpContext.Request.Path.Value.Contains("amazoncard")) return Card.Amazoncard;
            
            return Card.Hsbc;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
