using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Services;
using SeacDigitTemplate.Model;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SeacDigitTemplate
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SeacDigitTemplateContex>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(opt =>
            {
                //opt.RespectBrowserAcceptHeader = true;
                //opt.OutputFormatters.RemoveType<TextOutputFormatter>();
                //opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            });
            services.AddTransient<EffettoService>();
            services.AddTransient<ContoService>();
            services.AddTransient<CliforService>();
            services.AddTransient<AliquotaIvaService>();
            services.AddTransient<VoceIvaService>();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("connectionParams.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            app.UseCors(bb => bb.AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }


    }
}
