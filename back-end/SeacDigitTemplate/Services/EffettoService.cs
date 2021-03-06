﻿using SeacDigitTemplate.Model;
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
        private TemplateDocumentoService _templateDocumentoService;

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
        private IEnumerable<PropertyInfo> _documentoProprieties;

        private IEnumerable<PropertyInfo> DocumentoProprieties
        {
            get
            {
                if (_documentoProprieties == null)
                {
                    _documentoProprieties = (typeof(Documento)).GetProperties();
                }

                return _documentoProprieties;
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

        private IEnumerable<PropertyInfo> _effettoDocumentoFields;

        private IEnumerable<PropertyInfo> EffettoDocumentoProperties
        {
            get
            {
                if (_effettoDocumentoFields == null)
                {
                    _effettoDocumentoFields = (typeof(Documento)).GetProperties();
                }

                return _effettoDocumentoFields;
            }
        }

        public int x;
        public RigaDigitata empty = new RigaDigitata();

        public EffettoService(SeacDigitTemplateContext context, ApplicazioneTemplateEffettoService applicazioneTemplateEffettoService, TemplateEffettoService templateEffettoService, TemplateDocumentoService templateDocumentoService)
        {
            _templateDocumentoService = templateDocumentoService;
            _applicazioneTemplateEffettoService = applicazioneTemplateEffettoService;
            _templateEffettoService = templateEffettoService;
            _ctx = context;

        }

        public async Task<List<Effetto>> GetEffettosFromInputListAsync(Documento documento, List<RigaDigitata> rigaDigitataList)
        {

            var effettoList = new List<Effetto>();
            var effettoDocumentoList = new List<Documento>();

            foreach (var rd in rigaDigitataList)
            {
                effettoList.AddRange(await GetEffettoListFromInputAsync(rd, documento));
            }

            effettoList.AddRange(await GetEffettoListFromInputAsync(empty, documento));
            //return effettoList.Where(e => e.Valore != 0 || e.VariazioneFiscale != 0 || e.Imponibile != 0 || e.Iva != 0).ToList(); 
            return effettoList;
        }
        public async Task<List<Effetto>> GetEffettoListFromInputAsync(RigaDigitata rigaDigitata, Documento documento)
        {
            var effettoList = new List<Effetto>();

            var applicationTemplate = await _applicazioneTemplateEffettoService.GetTemplateAsync(rigaDigitata, documento);

            if (applicationTemplate == null)
            {
                return effettoList;
            }

            var templatesRiga = await _templateEffettoService.GetTemplateEffettoAsync(applicationTemplate);

            templatesRiga.ForEach(tr => effettoList.Add(CreateEffetto(rigaDigitata, tr, documento)));

            return effettoList;
        }
        // * Set the value
        // § Get the value from the documento
        // @ Get the value from the effettoDocumento
        // & Get the value from the rigaDigitata
        private Effetto CreateEffetto(RigaDigitata rigaDigitata, TemplateEffetto templateEffetto, Documento documento)
        {
            var newEffetto = new Effetto
            {
                Id = x++,
                TemplateGenerazioneEffettoId = templateEffetto.Id,
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
                            if (match.Value == "RitenutaAcconto" || match.Value == "Totale")
                            {
                                variables.Add(match.Value, Convert.ToDouble(DocumentoProprieties.Single(rdp => rdp.Name == match.Value).GetValue(documento)));
                            }
                            else
                            {
                                variables.Add(match.Value, Convert.ToDouble(RigaDigitataProperties.Single(rdp => rdp.Name == match.Value).GetValue(rigaDigitata)));
                            }
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

        public List<SituazioneConto> GetSituazioneConto(Documento documento, List<Effetto> effettoList)
        {

            var contoDareResult = effettoList
                .GroupBy(e => e.ContoDareId)
                .Where(g => g.Key.HasValue)
                .Select(kvp =>
                {
                    if (((int)documento.Sospeso == 3 || (int)documento.Sospeso == 2 && kvp.Key.Value != 4) || ((int)documento.Sospeso == 1 && kvp.Key.Value == 4))
                    {
                        return new SituazioneConto
                        {
                            ContoId = kvp.Key.Value,
                            Valore = 0,
                            Sospeso = kvp.Sum(v => v.Valore),
                            VariazioneFiscale = kvp.Sum(v => v.VariazioneFiscale)
                        };

                    }
                    else
                    {
                        return new SituazioneConto
                        {
                            ContoId = kvp.Key.Value,
                            Valore = kvp.Sum(v => v.Valore),
                            Sospeso = 0,
                            VariazioneFiscale = kvp.Sum(v => v.VariazioneFiscale)
                        };
                    }
                });

            var contoAvereResult = effettoList
                .GroupBy(e => e.ContoAvereId)
                .Where(g => g.Key.HasValue)
                .Select(kvp =>
                {
                    if (((int)documento.Sospeso == 3 || (int)documento.Sospeso == 2 && kvp.Key.Value != 4) || ((int)documento.Sospeso == 1 && kvp.Key.Value == 4))
                    {
                        return new SituazioneConto
                        {
                            ContoId = kvp.Key.Value,
                            Valore = 0,
                            Sospeso = -kvp.Sum(v => v.Valore),
                            VariazioneFiscale = -kvp.Sum(v => v.VariazioneFiscale)
                        };

                    }
                    else
                    {
                        return new SituazioneConto
                        {
                            ContoId = kvp.Key.Value,
                            Valore = -kvp.Sum(v => v.Valore),
                            Sospeso = 0,
                            VariazioneFiscale = -kvp.Sum(v => v.VariazioneFiscale)
                        };
                    }
                });

            var result = contoDareResult
                .Join(contoAvereResult, cd => cd.ContoId, ca => ca.ContoId, (ca, cd) => new SituazioneConto
                {
                    ContoId = ca.ContoId,
                    Valore = cd.Valore + ca.Valore,
                    VariazioneFiscale = cd.VariazioneFiscale + ca.VariazioneFiscale
                });

            var differenceAvere = contoAvereResult.Except(contoDareResult, new MyComparer());
            var differenceDare = contoDareResult.Except(contoAvereResult, new MyComparer());
            result = result.Union(differenceAvere);
            result = result.Union(differenceDare);
            return result.ToList();

        }

        public List<SituazioneVoceIva> GetSituazioneVoceIva(Documento documento, List<Effetto> effettoList)
        {
            return effettoList
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
                    if ((int)documento.Sospeso == 0 || (int)documento.Sospeso == 2)
                    {
                        return new SituazioneVoceIva
                        {
                            VoceIvaId = kvp.Key.VoceIvaId,
                            Trattamento = kvp.Key.Trattamento,
                            TitoloInapplicabilitaId = kvp.Key.TitoloInapplicabilitaId,
                            AliquotaIvaId = kvp.Key.AliquotaIvaId,
                            Imponibile = kvp.Sum(s => s.Imponibile),
                            Sospeso = 0,
                            Iva = kvp.Sum(s => s.Iva)
                        };
                    }
                    else
                    {
                        return new SituazioneVoceIva
                        {
                            VoceIvaId = kvp.Key.VoceIvaId,
                            Trattamento = kvp.Key.Trattamento,
                            TitoloInapplicabilitaId = kvp.Key.TitoloInapplicabilitaId,
                            AliquotaIvaId = kvp.Key.AliquotaIvaId,
                            Imponibile = 0,
                            Sospeso = kvp.Sum(s => s.Imponibile),
                            Iva = kvp.Sum(s => s.Iva)
                        };
                    }
                })
                .ToList();
        }

        internal class MyComparer : IEqualityComparer<SituazioneConto>
        {
            public bool Equals(SituazioneConto x, SituazioneConto y)
            {
                if (string.Equals(x.ContoId.ToString(), y.ContoId.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(SituazioneConto obj)
            {
                return obj.ContoId.GetHashCode();
            }
        }
    }
}
