using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;

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
            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("connectionParams.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            app.UseMvc();
        }
    }
}
