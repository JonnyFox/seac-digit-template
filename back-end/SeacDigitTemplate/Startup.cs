using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Services;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Models;
using System.Collections.Generic;
using System.Linq;

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
            services.AddDbContext<SeacDigitTemplateContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
            services.AddTransient<DocumentoService>();
            services.AddTransient<RigaDigitataService>();
            services.AddTransient<SituazioneVoceIvaService>();
            services.AddTransient<SituazioneContoService>();
            services.AddTransient<TitoloInapplicabilitaService>();
            services.AddTransient<ApplicazioneTemplateEffettoService>();
            services.AddTransient<TemplateEffettoService>();
            services.AddCors();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
            cfg.CreateMap<Documento, DocumentoDto>();
            cfg.CreateMap<DocumentoDto, Documento>();
            cfg.CreateMap<RigaDigitata, RigaDigitataDto>();
            cfg.CreateMap<RigaDigitataDto, RigaDigitata>();
            cfg.CreateMap<SituazioneVoceIva, SituazioneVoceIvaDto>();
            cfg.CreateMap<SituazioneVoceIvaDto, SituazioneVoceIva>();
            cfg.CreateMap<Conto, ContoDto>();
            cfg.CreateMap<ContoDto, Conto>();
            cfg.CreateMap<Clifor, CliforDto>();
            cfg.CreateMap<CliforDto, Clifor>();

            cfg.CreateMap<AliquotaIva, AliquotaIvaDto>();
            cfg.CreateMap<AliquotaIvaDto, AliquotaIva>();
            cfg.CreateMap<Effetto, EffettoDto>();
            cfg.CreateMap<EffettoDto, Effetto>();
            cfg.CreateMap<SituazioneConto, SituazioneContoDto>();
            cfg.CreateMap<SituazioneContoDto, SituazioneConto>();
            cfg.CreateMap<TitoloInapplicabilita, TitoloInapplicabilitaDto>();
            cfg.CreateMap<TitoloInapplicabilitaDto, TitoloInapplicabilita>();
            cfg.CreateMap<VoceIva, VoceIvaDto>();
            cfg.CreateMap<VoceIvaDto, VoceIva>();

            cfg.CreateMap<Effetto, SituazioneContoDto>();
            cfg.CreateMap<Effetto, SituazioneVoceIvaDto>();

            cfg.CreateMap<Effetto, EffettoContoDto>();
            cfg.CreateMap<Effetto, EffettoIvaDto>();
            cfg.CreateMap<SituazioneVoceIva, SituazioneVoceIvaDto>();
            cfg.CreateMap<SituazioneConto, SituazioneContoDto>();
            cfg.CreateMap<List<Effetto>, EffettoCalcoloDto>()
                .ForMember(dest => dest.EffettoContos, opt => opt.MapFrom(src => src.Where(e => e.ContoAvereId != null || e.ContoDareId != null)))
                .ForMember(dest => dest.EffettoIvas, opt => opt.MapFrom(src => src.Where(e => e.VoceIvaId != null)));
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("connectionParams.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            app.UseCors(bb => bb.AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
