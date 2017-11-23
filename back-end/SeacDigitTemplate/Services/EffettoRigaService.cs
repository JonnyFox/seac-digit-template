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
        private TemplateEffettoRigaService _templateEffettoRigaService;
        private ApplicazioneTemplateEffettoService _applicazioneTemplateEffettoService;
        private ApplicazioneTemplateEffettoRigaService _applicazioneTemplateEffettoRigaService;
        private ApplicazioneTemplateDocumentoService _applicazioneTemplateDocumentoService;

        private SeacDigitTemplateContext _ctx;

        private IEnumerable<PropertyInfo> _templateEffettoRigaStringProperties;

        private IEnumerable<PropertyInfo> TemplateEffettoRigaStringProperties
        {
            get
            {
                if (_templateEffettoRigaStringProperties == null)
                {
                    _templateEffettoRigaStringProperties = (typeof(TemplateEffettoRiga)).GetProperties().Where(p => p.PropertyType == typeof(String));
                }

                return _templateEffettoRigaStringProperties;
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
                    _effettoDocumentoFields = (typeof(EffettoDocumento)).GetProperties();
                }

                return _effettoDocumentoFields;
            }
        }
        public EffettoRigaService(SeacDigitTemplateContext context, ApplicazioneTemplateEffettoService applicazioneTemplateEffettoService, TemplateEffettoService templateEffettoService, TemplateDocumentoService templateDocumentoService, ApplicazioneTemplateDocumentoService applicazioneTemplateDocumentoService, TemplateEffettoRigaService templateEffettoRigaService, ApplicazioneTemplateEffettoRigaService applicazioneTemplateEffettoRigaService)
        {
            _templateEffettoRigaService = templateEffettoRigaService;
            _templateDocumentoService = templateDocumentoService;
            _templateEffettoService = templateEffettoService;
            _applicazioneTemplateEffettoRigaService = applicazioneTemplateEffettoRigaService;
            _applicazioneTemplateDocumentoService = applicazioneTemplateDocumentoService;
            _applicazioneTemplateEffettoService = applicazioneTemplateEffettoService;
            _ctx = context;

        }

        public async Task<List<EffettoRiga>> GetEffettosFromInputListAsync(Documento documento, List<RigaDigitata> rigaDigitataList, List<EffettoDocumento> effettoDocumentoList)
        {

            var effettoRigaList = new List<EffettoRiga>();


            foreach (var rd in rigaDigitataList)
            {
                foreach (var ed in effettoDocumentoList)
                {
                    effettoRigaList.AddRange(await GetEffettoRigaListFromInputAsync(rd, documento, ed));
                }
            }
            //return effettoList.Where(e => e.Valore != 0 || e.VariazioneFiscale != 0 || e.Imponibile != 0 || e.Iva != 0).ToList(); 
            return effettoRigaList;
        }
        public async Task<List<EffettoRiga>> GetEffettoRigaListFromInputAsync(RigaDigitata rigaDigitata, Documento documento, EffettoDocumento effettoDocumento)
        {
            var effettoRigaList = new List<EffettoRiga>();

            var applicationTemplate = await _applicazioneTemplateEffettoRigaService.GetTemplateAsync(effettoDocumento);

            if (applicationTemplate == null)
            {
                return effettoRigaList;
            }

            var templatesRiga = await _templateEffettoRigaService.GetTemplateEffettoRigaAsync(applicationTemplate);

            templatesRiga.ForEach(tr => effettoRigaList.Add(CreateEffettoRiga(rigaDigitata, tr, documento,effettoDocumento)));

            return effettoRigaList;
        }


        private EffettoRiga CreateEffettoRiga(RigaDigitata rigaDigitata, TemplateEffettoRiga templateEffettoRiga, Documento documento, EffettoDocumento effettoDocumento)
        {
            var newEffettoRiga = new EffettoRiga
            {
                TemplateGenerazioneEffettoRiga = templateEffettoRiga.Id,
                RigaDigitataId = rigaDigitata.Id,
                DocumentoId = rigaDigitata.DocumentoId,
                EffettoDocumentoId = effettoDocumento.Id
            };

            foreach (var templateEffettoField in TemplateEffettoStringProperties)
            {
                var templateEffettoFieldValue = templateEffettoField.GetValue(templateEffettoRiga) as string;

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
