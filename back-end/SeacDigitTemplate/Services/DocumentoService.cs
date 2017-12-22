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

        public async Task<int> DeleteByIdAsync(int id)
        {
            var document = await this.GetByIdAsync(id);
            if (document == null)
            {
                return 0;
            }
            _ctx.Documentos.Remove(document);
            return await _ctx.SaveChangesAsync();
        }

        public void SaveDocument(Documento documento)
        {
            var lastDoc = _ctx.Documentos.LastAsync().Result;
            var tmp = _ctx.Documentos.Find(documento.Id);
            // Per controllare se devo aggiungere il nuovo documento al db
            if (lastDoc.isGenerated != null)
            {
                if (tmp == null)
                {
                    //documento.isGenerated = false;
                    _ctx.Documentos.Add(documento);
                    _ctx.SaveChanges();
                }
                else
                {
                    _ctx.Entry(tmp).CurrentValues.SetValues(documento);
                    _ctx.SaveChanges();
                }
            }
            else if (tmp != null)
            {
                _ctx.Entry(tmp).CurrentValues.SetValues(documento);
                _ctx.SaveChanges();
            }
            lastDoc = _ctx.Documentos.LastAsync().Result;
            lastDoc.isGenerated = false;
            lastDoc = _ctx.Documentos.LastAsync().Result;
            //Ciclo per assegnare gli id alle righe
            for (int i = 0; i < documento.rigaDigitataList.Count; i++)
            {
                // Per controllare se queste righe apparrtengono ad un nuovo documento
                if (documento.Id == 0)
                {
                    documento.rigaDigitataList[i].DocumentoId = lastDoc.Id;
                }
                var tmpriga = _ctx.RigaDigitatas.Find(documento.rigaDigitataList[i].Id);
                // Per controllare se devo aggiungere una nuova riga al db
                if (tmpriga == null)
                {
                    _ctx.RigaDigitatas.Add(documento.rigaDigitataList[i]);
                    _ctx.SaveChanges();
                }
                else if (documento.rigaDigitataList[i].toAdd == false)
                {
                    _ctx.RigaDigitatas.Remove(tmpriga);
                }
                else
                {
                    _ctx.Entry(tmpriga).CurrentValues.SetValues(documento.rigaDigitataList[i]);
                }
            }
            _ctx.SaveChanges();
        }
    }
}
