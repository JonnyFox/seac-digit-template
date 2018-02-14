using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class DocumentoService
    {
        SeacDigitTemplateContext _ctx;
        public DocumentoService(SeacDigitTemplateContext ctx) => _ctx = ctx;

        public Task<List<Documento>> GetAll() => _ctx.Documentos.ToListAsync();

        public Task<Documento> GetByIdAsync(int id) => _ctx.Documentos.SingleOrDefaultAsync(i => i.Id == id);
        public Documento GetById(int id) => _ctx.Documentos.SingleOrDefault(i => i.Id == id);

        public void DeleteByIdAsync(int id)
        {
            var document = this.GetById(id);

            _ctx.RigaDigitatas.RemoveRange(_ctx.RigaDigitatas.Where(k => k.DocumentoId == document.Id));
            _ctx.FeedbackList.RemoveRange(_ctx.FeedbackList.Where(k => k.DocumentoId == document.Id));
            _ctx.SaveChanges();
            _ctx.Documentos.Remove(document);
            _ctx.SaveChanges();

        }

        public async void SaveDocument(Documento documento)
        {
            
            var tmpDoc = _ctx.Documentos.Find(documento.Id);

            if (tmpDoc == null)
            {
                _ctx.Documentos.Add(documento);
                _ctx.SaveChanges();
            }
            else
            {
                _ctx.Entry(tmpDoc).CurrentValues.SetValues(documento);
                _ctx.SaveChanges();
            }
            var lastDoc = _ctx.Documentos.LastAsync();

            //Ciclo per assegnare gli id alle righe
            for (int i = 0; i < documento.rigaDigitataList.Count; i++)
            {
                // Per controllare se queste righe apparrtengono ad un nuovo documento
                if (documento.Id == 0)
                {
                    documento.rigaDigitataList[i].DocumentoId = lastDoc.Id;
                }
                //var tmpriga = await _ctx.RigaDigitatas.FindAsync(documento.rigaDigitataList[i].Id);
                // Per controllare se devo aggiungere una nuova riga al db
                switch (documento.rigaDigitataList[i].toAdd)
                {
                    case true:
                        documento.rigaDigitataList[i].toAdd = null;
                        _ctx.RigaDigitatas.Add(documento.rigaDigitataList[i]);
                        _ctx.SaveChanges();
                        break;
                    case false:
                        _ctx.RigaDigitatas.Remove(_ctx.RigaDigitatas.Find(documento.rigaDigitataList[i].Id));
                        _ctx.SaveChanges();
                        break;
                    case null:
                        _ctx.Entry(_ctx.RigaDigitatas.Find(documento.rigaDigitataList[i].Id)).CurrentValues.SetValues(documento.rigaDigitataList[i]);
                        _ctx.SaveChanges();
                        break;
                }
            }
            _ctx.SaveChanges();
        }
    }
}