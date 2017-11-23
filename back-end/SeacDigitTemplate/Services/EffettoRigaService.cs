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

            var applicationTemplate = await _applicazioneTemplateRigaService.GetTemplateAsync(effettoDocumento);

            if (applicationTemplate == null)
            {
                return RigaList;
            }

            var templatesRiga = await _templateRigaService.GetTemplateRigaAsync(applicationTemplate);

            templatesRiga.ForEach(tr => RigaList.Add(CreateEffettoRiga(rigaDigitata, tr, documento,effettoDocumento)));

            return RigaList;
        }


        private RigaDigitata CreateEffettoRiga(RigaDigitata rigaDigitata, TemplateRiga templateRiga, Documento documento, Documento effettoDocumento)
        {
            var newEffettoRiga = new RigaDigitata
            {
                TemplateGenerazioneEffettoRigaId = templateRiga.Id,
                DocumentoId = effettoDocumento.Id,
                
            };

            foreach (var templateEffettoField in TemplateEffettoStringProperties)
            {
                var templateEffettoFieldValue = templateEffettoField.GetValue(templateRiga) as string;

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
                        value = RigaDigitataProperties.Single(rdp => rdp.Name == templateEffettoFieldValue).GetValue(rigaDigitata);
                    }

                    currentEffettoProperty.SetValue(newEffettoRiga, value);
                }
            }
            return newEffettoRiga;
        }
        
    }
}
