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
    public class EffettoRigaService
    {
        private TemplateEffettoService _templateEffettoService;
        private TemplateDocumentoService _templateDocumentoService;
        private TemplateRigaService _templateRigaService;
        private ApplicazioneTemplateEffettoService _applicazioneTemplateEffettoService;
        private ApplicazioneTemplateRigaService _applicazioneTemplateRigaService;
        private ApplicazioneTemplateDocumentoService _applicazioneTemplateDocumentoService;

        private SeacDigitTemplateContext _ctx;

        private IEnumerable<PropertyInfo> _templateRigaStringProperties;

        private IEnumerable<PropertyInfo> TemplateRigaStringProperties
        {
            get
            {
                if (_templateRigaStringProperties == null)
                {
                    _templateRigaStringProperties = (typeof(TemplateRiga)).GetProperties().Where(p => p.PropertyType == typeof(String));
                }

                return _templateRigaStringProperties;
            }
        }

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
        public EffettoRigaService(SeacDigitTemplateContext context, ApplicazioneTemplateEffettoService applicazioneTemplateEffettoService, TemplateEffettoService templateEffettoService, TemplateDocumentoService templateDocumentoService, ApplicazioneTemplateDocumentoService applicazioneTemplateDocumentoService, TemplateRigaService templateRigaService, ApplicazioneTemplateRigaService applicazioneTemplateRigaService)
        {
            _templateRigaService = templateRigaService;
            _templateDocumentoService = templateDocumentoService;
            _templateEffettoService = templateEffettoService;
            _applicazioneTemplateRigaService = applicazioneTemplateRigaService;
            _applicazioneTemplateDocumentoService = applicazioneTemplateDocumentoService;
            _applicazioneTemplateEffettoService = applicazioneTemplateEffettoService;
            _ctx = context;

        }

        public int x = 1;

        public async Task<List<RigaDigitata>> GetEffettoRigaListFromInputListAsync(Documento documento, List<RigaDigitata> rigaDigitataList, List<Documento> effettoDocumentoList)
        {

            var effettoRigaList = new List<RigaDigitata>();


            foreach (var rd in rigaDigitataList)
            {
                foreach (var ed in effettoDocumentoList)
                {
                    effettoRigaList.AddRange(await GetRigaListFromInputAsync(rd, documento, ed));
                }
            }
            //return effettoList.Where(e => e.Valore != 0 || e.VariazioneFiscale != 0 || e.Imponibile != 0 || e.Iva != 0).ToList(); 
            return effettoRigaList;
        }
        public async Task<List<RigaDigitata>> GetRigaListFromInputAsync(RigaDigitata rigaDigitata, Documento documento, Documento effettoDocumento)
        {
            var RigaList = new List<RigaDigitata>();

            var applicationTemplate = await _applicazioneTemplateRigaService.GetTemplateAsync(effettoDocumento, rigaDigitata, documento);

            if (applicationTemplate == null)
            {
                return RigaList;
            }

            var templatesRiga = await _templateRigaService.GetTemplateRigaAsync(applicationTemplate);

            templatesRiga.ForEach(tr => RigaList.Add(CreateEffettoRiga(rigaDigitata, tr, documento, effettoDocumento)));

            return RigaList;
        }
        // * Set the value
        // § Get the value from the documento
        // @ Get the value from the effettoDocumento
        // & Get the value from the rigaDigitata
        private RigaDigitata CreateEffettoRiga(RigaDigitata rigaDigitata, TemplateRiga templateRiga, Documento documento, Documento effettoDocumento)
        {
            var newEffettoRiga = new RigaDigitata
            {
                Id = x++,
                TemplateGenerazioneEffettoRigaId = templateRiga.Id,
                DocumentoId = effettoDocumento.Id
            };

            foreach (var templateEffettoField in TemplateRigaStringProperties)
            {
                var templateRigaFieldValue = templateEffettoField.GetValue(templateRiga) as string;

                if (templateRigaFieldValue != null)
                {
                    Object value = null;

                    var currentRigaProperty = RigaDigitataProperties.Single(ep => ep.Name == templateEffettoField.Name);

                    if (templateRigaFieldValue.StartsWith("*"))
                    {
                        if (currentRigaProperty.PropertyType == typeof(int) || currentRigaProperty.PropertyType == typeof(int?))
                        {
                            value = templateRigaFieldValue.Substring(1).ToNullableInt();
                        }
                        else if (currentRigaProperty.PropertyType == typeof(decimal) || currentRigaProperty.PropertyType == typeof(decimal?))
                        {
                            value = Convert.ToDecimal(templateRigaFieldValue.Substring(1));
                        }
                        else
                        {
                            value = Enum.Parse(currentRigaProperty.PropertyType, templateRigaFieldValue.Substring(1));
                        }

                    }
                    else if (templateRigaFieldValue.StartsWith("§"))
                    {
                        value = RigaDigitataProperties.Single(rdp => rdp.Name == templateRigaFieldValue.Substring(1)).GetValue(documento);
                    }
                    else if (templateRigaFieldValue.StartsWith("@"))
                    {
                        value = RigaDigitataProperties.Single(rdp => rdp.Name == templateRigaFieldValue.Substring(1)).GetValue(effettoDocumento);
                    }
                    else if (templateRigaFieldValue.StartsWith("&"))
                    {
                        value = RigaDigitataProperties.Single(rdp => rdp.Name == templateRigaFieldValue.Substring(1)).GetValue(rigaDigitata);
                    }
                    else if (templateRigaFieldValue.StartsWith("#"))
                    {
                        var formula = templateRigaFieldValue.Substring(1);
                        var regex = new Regex(@"[^\d\W]+");

                        var variables = new Dictionary<string, double>();

                        foreach (Match match in regex.Matches(formula))
                        {
                            if (match.Value == "RitenutaAcconto")
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
                        value = RigaDigitataProperties.Single(rdp => rdp.Name == templateRigaFieldValue).GetValue(rigaDigitata);
                    }

                    currentRigaProperty.SetValue(newEffettoRiga, value);
                }
            }
            return newEffettoRiga;
        }

    }
}
