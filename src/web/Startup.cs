using System;
using Calme.Domain.Models;
using Calme.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Calme
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
            services.AddTransient<IStatementParser>(s => GetCard(s) == Card.Barclaycard
                ? (IStatementParser) new BarclaycardParser(s.GetService<ILogger<BarclaycardParser>>())
                : new HsbcParser(s.GetService<ILogger<HsbcParser>>()));
            services.AddTransient<IClean>(s => GetCard(s) == Card.Barclaycard
                ? (IClean) new BarclaycardCleaner()
                : new HsbcCleaner());
            services.AddMvc();
        }

        private static Card GetCard(IServiceProvider s)
        {
            var httpContext = s.GetService<IHttpContextAccessor>().HttpContext;
            if (httpContext.Request.Path.Value.Contains("barclaycard"))
            {
                return Card.Barclaycard;
            }

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
