using SeacDigitTemplate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using SeacDigitTemplate.Models;
using System.Threading.Tasks;
using SeacDigitTemplate.Data;
using System.Reflection;

namespace SeacDigitTemplate.Services
{
    public class EffettoService
    {
        private ApplicazioneTemplateEffettoService _applicazioneTemplateEffettoService;
        private TemplateEffettoService _templateEffettoService;

        private SeacDigitTemplateContext _ctx;

        private IEnumerable<PropertyInfo> _templateEffettoStringProperties;

        private IEnumerable<PropertyInfo> TemplateEffettoStringProperties
        {
            get
            {
                if (_templateEffettoStringProperties == null)
                {
                    _templateEffettoStringProperties = (typeof(TemplateEffetto)).GetProperties().Where(p => p.PropertyType == typeof(String));
                }

                return _templateEffettoStringProperties;
            }
        }

        private IEnumerable<PropertyInfo> _rigaDigitataProperties;

        private IEnumerable<PropertyInfo> RigaDigitataProperties
        {
            get
            {
                if (_rigaDigitataProperties == null)
                {
                    _rigaDigitataProperties = (typeof(RigaDigitata)).GetProperties();
                }

                return _rigaDigitataProperties;
            }
        }

        private IEnumerable<PropertyInfo> _effettoFields;

        private IEnumerable<PropertyInfo> EffettoProperties
        {
            get
            {
                if (_effettoFields == null)
                {
                    _effettoFields = (typeof(Effetto)).GetProperties();
                }

                return _effettoFields;
            }
        }


        public EffettoService(SeacDigitTemplateContext context, ApplicazioneTemplateEffettoService applicazioneTemplateEffettoService, TemplateEffettoService templateEffettoService)
        {
            _applicazioneTemplateEffettoService = applicazioneTemplateEffettoService;
            _templateEffettoService = templateEffettoService;
            _ctx = context;
        }

        public async Task<List<Effetto>> GetEffettosFromRigaDigitatasAsync(List<RigaDigitata> rigaDigitatas)
        {
            var effettos = new List<Effetto>();

            foreach (var rd in rigaDigitatas)
            {
                effettos.AddRange(await GetEffettosFromRigaDigitataAsync(rd));
            }

            return effettos;
        }

        public async Task<List<Effetto>> GetEffettosFromRigaDigitataAsync(RigaDigitata rigaDigitata)
        {
            var applicationTemplate = await _applicazioneTemplateEffettoService.GetTemplateAsync(rigaDigitata);

            var templates = await _templateEffettoService.GetTemplateEffettoAsync(applicationTemplate);

            var effettos = new List<Effetto>();

            templates.ForEach(t => effettos.Add(CreateEffetto(rigaDigitata, t)));

            return effettos;
        }

        private Effetto CreateEffetto(RigaDigitata rigaDigitata, TemplateEffetto templateEffetto)
        {
            var newEffetto = new Effetto
            {
                TemplateGenerazioneEffetto = templateEffetto.Id,
                RigaDigitataId = rigaDigitata.Id,
                DocumentoId = rigaDigitata.DocumentoId
                
            };

            foreach (var templateEffettoField in TemplateEffettoStringProperties)
            {
                var templateEffettoFieldValue = templateEffettoField.GetValue(templateEffetto) as string;

                if (templateEffettoFieldValue != null)
                {
                    Object value = null;

                    if (templateEffettoFieldValue.StartsWith("*"))
                    {
                        value = int.Parse(templateEffettoFieldValue.Substring(1));
                    }
                    else
                    {
                        value = RigaDigitataProperties.Single(rdp => rdp.Name == templateEffettoFieldValue).GetValue(rigaDigitata);
                    }

                    EffettoProperties.Single(ep => ep.Name == templateEffettoField.Name).SetValue(newEffetto, value);
                }
            }

            return newEffetto;
        }
    }
}
