using SeacDigitTemplate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using SeacDigitTemplate.Models;
using System.Threading.Tasks;
using SeacDigitTemplate.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using Jace;
using SeacDigitTemplate.Extensions;
using Microsoft.EntityFrameworkCore;

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

        //public async Task<List<SituazioneVoceIva>> 

        public async Task<List<SituazioneConto>> CreateSituazioneContoAsync(List<Effetto> effettos)
        {
            var _situazioneConto = new List<SituazioneConto>();
            var _valore = 0.0m;
            foreach (var effetto in effettos)
            {
                _valore = 0.0m;
                if((_situazioneConto.Count(cd => cd.ContoId == effetto.ContoDareId) != 0))
                {
                    foreach (var t in _situazioneConto.Where(w => w.ContoId== effetto.ContoDareId))
                    {
                        t.Valore += effetto.Valore;
                        t.Variazione += effetto.VariazioneF;
                    }
                }
                else
                {
                    _valore = effetto.Valore;
                    _situazioneConto.Add(new SituazioneConto { Valore = effetto.Valore, ContoId = (int)effetto.ContoDareId, Variazione = effetto.VariazioneF });

                }

                _valore = 0.0m;

                if ((_situazioneConto.Count(cd => cd.ContoId == effetto.ContoAvereId) != 0))
                {
                    foreach (var t in _situazioneConto.Where(w => w.ContoId == effetto.ContoAvereId))
                    {
                        t.Valore -= -effetto.Valore; // converti in negativo 
                        t.Variazione -= -effetto.VariazioneF; // converti in negativo 
                    }
                }
                else
                {
                    _valore = -effetto.Valore;
                    _situazioneConto.Add(new SituazioneConto { Valore = effetto.Valore, ContoId = (int)effetto.ContoAvereId, Variazione = effetto.VariazioneF });

                }
            }
            
            return _situazioneConto; 
        }

        public async Task<List<SituazioneVoceIva>> CreateSituazioneVoceIvaAsync(List<Effetto> effettos)
        {
            var _situazioneVoceIva = new List<SituazioneVoceIva>();
            var _valore = 0.0m;

            foreach (var effetto in effettos.Where(ef => ef.VoceIvaId != null))
            {
                var currentVoceIva = _situazioneVoceIva
                
                    .Where(si => si.VoceIvaId == effetto.VoceIvaId)
                    .Where(si => si.Trattamento == effetto.Trattamento)
                    .Where(si => si.TitoloInapplicabilita == effetto.TitoloInapplicabilitaId)
                    .FirstOrDefault(si => si.AliquotaIvaId == effetto.AliquotaIvaId);
                if(currentVoceIva != null)
                {
                    currentVoceIva.Valore += effetto.Valore;
                }
                else
                {
                    _situazioneVoceIva.Add(new SituazioneVoceIva
                    {
                        VoceIva = effetto.VoceIva,
                        Trattamento = effetto.Trattamento,
                        TitoloInapplicabilita = effetto.TitoloInapplicabilitaId,
                        AliquotaIva = effetto.AliquotaIva,
                        Valore = _valore
                    });
                }
            }
            return _situazioneVoceIva;
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

                    var currentEffettoProperty = EffettoProperties.Single(ep => ep.Name == templateEffettoField.Name);

                    if (templateEffettoFieldValue.StartsWith("*"))
                    {
                        if (currentEffettoProperty.PropertyType == typeof(int) || currentEffettoProperty.PropertyType == typeof(int?))
                        {
                            value = templateEffettoFieldValue.Substring(1).ToNullableInt();
                        }
                        else
                        {
                            value = Enum.Parse(Nullable.GetUnderlyingType(currentEffettoProperty.PropertyType), templateEffettoFieldValue.Substring(1));
                        }
                    }
                    else if (templateEffettoFieldValue.StartsWith("#"))
                    {
                        var formula = templateEffettoFieldValue.Substring(1);
                        var regex = new Regex(@"[^\d\W]+");

                        var variables = new Dictionary<string, double>();

                        foreach (Match match in regex.Matches(formula))
                        {
                            variables.Add(match.Value, Convert.ToDouble(RigaDigitataProperties.Single(rdp => rdp.Name == match.Value).GetValue(rigaDigitata)));
                        }

                        value = Convert.ToDecimal(new CalculationEngine().Calculate(formula, variables));
                    }
                    else
                    {
                        value = RigaDigitataProperties.Single(rdp => rdp.Name == templateEffettoFieldValue).GetValue(rigaDigitata);
                    }

                    currentEffettoProperty.SetValue(newEffetto, value);
                }
            }

            return newEffetto;
        }


    }
}
