using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Linq;

namespace SeacDigitTemplate.Data
{
    public class SeacDigitTemplateContex : DbContext
    {
        public SeacDigitTemplateContex(DbContextOptions<SeacDigitTemplateContex> options) : base(options)
        {

        }

        public DbSet<Clifor> Clifors { get; set; }
        public DbSet<AliquotaIva> AliquotaIVAs { get; set; }
        public DbSet<Conto> Contos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<RigaDigitata> RigaDigitatas { get; set; }
        public DbSet<SituazioneConto> SituazioneContos { get; set; }
        public DbSet<SituazioneVoceIVA> SituazioneVoceIVAs { get; set; }
        public DbSet<VoceIva> VoceIVAs { get; set; }
        public DbSet<TitoloInapplicabilita> TitoloInapplicabilitas { get; set; }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            foreach (var relationship in mb.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(mb);
            mb.Entity<SituazioneConto>().HasKey(sc => sc.ContoId);
            mb.Entity<SituazioneVoceIVA>().HasKey(sv => sv.VoceIVAId);

        }
    }
}
