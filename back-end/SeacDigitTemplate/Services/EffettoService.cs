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


        public List<SituazioneConto> GetSituazioneConto(List<Effetto> effettos)
        {

            var contoDareResult = effettos
                .GroupBy(e => e.ContoDareId)
                .Where(g => g.Key.HasValue)
                .Select(kvp =>
                {
                    return new SituazioneConto
                    {
                        ContoId = kvp.Key.Value,
                        Valore = kvp.Sum(v => v.Valore),
                        Variazione = kvp.Sum(v => v.VariazioneF)
                    };
                });

            var contoAvereResult = effettos
                .GroupBy(e => e.ContoAvereId)
                .Where(g => g.Key.HasValue)
                .Select(kvp =>
                {
                    return new SituazioneConto
                    {
                        ContoId = kvp.Key.Value,
                        Valore = -kvp.Sum(v => v.Valore),
                        Variazione = 0 //kvp.Sum(v => v.VariazioneF)
                    };
                });

            var result = contoDareResult
                .Join(contoAvereResult, cd => cd.ContoId, ca => ca.ContoId, (ca, cd) => new SituazioneConto
                {
                    ContoId = ca.ContoId,
                    Valore = cd.Valore + ca.Valore,
                    Variazione = cd.Variazione + ca.Variazione
                });

            result = result.Union(contoAvereResult);
            result = result.Union(contoDareResult);

            return result.ToList();

        }

        public List<SituazioneVoceIva> GetSituazioneVoceIva(List<Effetto> effettos)
        {

            var x = effettos
                .GroupBy(e => new
                {
                    e.VoceIvaId,
                    e.Trattamento,
                    e.TitoloInapplicabilitaId,
                    e.AliquotaIvaId
                })
                .Where(g => g.Key.VoceIvaId != null)
                .Select(kvp =>
                {
                    return new SituazioneVoceIva
                    {
                        VoceIvaId = kvp.Key.VoceIvaId.Value,
                        Trattamento = kvp.Key.Trattamento.Value,
                        TitoloInapplicabilitaId = kvp.Key.TitoloInapplicabilitaId,
                        AliquotaIvaId = kvp.Key.AliquotaIvaId,
                        Valore = kvp.Sum(s => s.Valore)
                        
                    };
                });

            return x.ToList();



            //var situazioneVoceIva = new List<SituazioneVoceIva>();
            //var valore = 0.0m;

            //foreach (var effetto in effettos.Where(ef => ef.VoceIvaId != null))
            //{
            //    var currentVoceIva = situazioneVoceIva
            //        .Where(si => si.VoceIvaId == effetto.VoceIvaId)
            //        .Where(si => si.Trattamento == effetto.Trattamento)
            //        .Where(si => si.TitoloInapplicabilita == effetto.TitoloInapplicabilitaId)
            //        .FirstOrDefault(si => si.AliquotaIvaId == effetto.AliquotaIvaId);

            //    if (currentVoceIva != null)
            //    {
            //        currentVoceIva.Valore += effetto.Valore;
            //    }
            //    else
            //    {
            //        situazioneVoceIva.Add(new SituazioneVoceIva
            //        {
            //            VoceIva = effetto.VoceIva,
            //            Trattamento = effetto.Trattamento,
            //            TitoloInapplicabilita = effetto.TitoloInapplicabilitaId,
            //            AliquotaIva = effetto.AliquotaIva,
            //            Valore = valore
            //        });
            //    }
            //}

            //return situazioneVoceIva;
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
