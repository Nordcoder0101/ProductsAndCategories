using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ProdsAndCats.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ProdsAndCats
{
    public class Startup
    {
		public IConfiguration Configuration {get;}
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddDbContext<ProdsAndCatsContext>(options => options.UseMySql(Configuration["DBInfo:ConnectionString"]));
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			if(env.IsDevelopment())
			{
            	app.UseDeveloperExceptionPage();
			}
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }
    }
}
