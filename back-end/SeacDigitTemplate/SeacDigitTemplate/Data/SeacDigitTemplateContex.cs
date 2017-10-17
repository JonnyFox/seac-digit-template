using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Data
{
    public class SeacDigitTemplateContex : DbContext
    {
        public SeacDigitTemplateContex(DbContextOptions<SeacDigitTemplateContex> options) : base(options)
        {

        }

        public DbSet<AliquotaIVA> AliquotaIvas { get; set; }
        public DbSet<Conto> Contos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<RigaDigitata> RigaDigitatas { get; set;}
        public DbSet<SituazioneConto> SituazioneContos { get; set;}
        public DbSet<SituazioneVoceIVA> SituazioneVoceIVAs { get; set;}
        public DbSet<VoceIVA> VoceIVAs { get; set; }
    }
}
