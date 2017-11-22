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
    public class EffettoDocumentoService
    {
        private ApplicazioneTemplateEffettoService _applicazioneTemplateEffettoService;
        private TemplateDocumentoService _templateDocumentoService;
        private ApplicazioneTemplateDocumentoService _applicazioneTemplateDocumentoService;

        private SeacDigitTemplateContext _ctx;

        private IEnumerable<PropertyInfo> _templateEffettoDocumentoStringProperties;

        private IEnumerable<PropertyInfo> TemplateEffettoDocumentoStringProperties
        {
            get
            {
                if (_templateEffettoDocumentoStringProperties == null)
                {
                    _templateEffettoDocumentoStringProperties = (typeof(TemplateDocumento)).GetProperties().Where(p => p.PropertyType == typeof(String));
                }

                return _templateEffettoDocumentoStringProperties;
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







        public EffettoDocumentoService(SeacDigitTemplateContext context, ApplicazioneTemplateEffettoService applicazioneTemplateEffettoService, TemplateDocumentoService templateDocumentoService, ApplicazioneTemplateDocumentoService applicazioneTemplateDocumentoService)
        {
            _applicazioneTemplateDocumentoService = applicazioneTemplateDocumentoService;
            _templateDocumentoService = templateDocumentoService;
            _applicazioneTemplateEffettoService = applicazioneTemplateEffettoService;
            _ctx = context;

        }

        public async Task<List<EffettoDocumento>> GetDocumentoListFromInputListAsync(Documento documento, List<RigaDigitata> rigaDigitataList)
        {
            
            var effettoDocumentoList = new List<EffettoDocumento>();

            foreach (var rd in rigaDigitataList)
            {

                effettoDocumentoList.AddRange(await GetEffettoDocumentoListFromInputAsync(rd, documento));
            }
            //return effetto.EffettoList.Where(e => e.Valore != 0 || e.VariazioneFiscale != 0 || e.Imponibile != 0 || e.Iva != 0).ToList();     A COSA SERVE QUESTA COSA  <<<!!>>>
            return effettoDocumentoList;
        }
        public async Task<List<EffettoDocumento>> GetEffettoDocumentoListFromInputAsync(RigaDigitata rigaDigitata, Documento documento)
        {
            var effettoDocumentoList = new List<EffettoDocumento>();

            var applicationTemplate = await _applicazioneTemplateDocumentoService.GetTemplateAsync(rigaDigitata, documento);

            if (applicationTemplate == null)
            {
                return effettoDocumentoList;
            }
            var templatesDocumento = await _templateDocumentoService.GetTemplateDocumentoAsync(applicationTemplate);

            templatesDocumento.ForEach(td => effettoDocumentoList.Add(CreateEffettoDocumento(rigaDigitata, td, documento)));

            return effettoDocumentoList;
        }

        private EffettoDocumento CreateEffettoDocumento(RigaDigitata rigaDigitata, TemplateDocumento templateEffetto, Documento documento)
        {
            var newDocumento = new EffettoDocumento
            {
                TemplateGenerazioneEffetto = templateEffetto.Id,
                RiferimentoDocumentoId = documento.Id
            };
            foreach (var templateEffettoField in TemplateEffettoDocumentoStringProperties)
            {
                var templateEffettoFieldValue = templateEffettoField.GetValue(templateEffetto) as string;

                if (templateEffettoFieldValue != null)
                {
                    Object value = null;

                    var currentEffettoProperty = EffettoDocumentoProperties.Single(ep => ep.Name == templateEffettoField.Name);

                    if (templateEffettoFieldValue.StartsWith("*"))
                    {
                        if (currentEffettoProperty.PropertyType == typeof(int) || currentEffettoProperty.PropertyType == typeof(int?))
                        {
                            value = templateEffettoFieldValue.Substring(1).ToNullableInt();
                        }
                        else
                        {
                            value = Enum.Parse(currentEffettoProperty.PropertyType, templateEffettoFieldValue.Substring(1));
                            //value = Enum.Parse(Nullable.GetUnderlyingType(currentEffettoProperty.PropertyType), templateEffettoFieldValue.Substring(1));
                        }
                    }
                    else
                    {
                        value = DocumentoProprieties.Single(rdp => rdp.Name == templateEffettoFieldValue).GetValue(documento);
                    }

                    currentEffettoProperty.SetValue(newDocumento, value);
                }
            }

            return newDocumento;

        }
    }
}
