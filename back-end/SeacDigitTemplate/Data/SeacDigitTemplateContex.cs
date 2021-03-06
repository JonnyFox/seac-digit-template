﻿using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System;
using System.Linq;

namespace SeacDigitTemplate.Data
{
    public class SeacDigitTemplateContext : DbContext
    {
        public SeacDigitTemplateContext(DbContextOptions<SeacDigitTemplateContext> options) : base(options)
        {

        }

        public DbSet<Clifor> Clifors { get; set; }
        public DbSet<AliquotaIva> AliquotaIvas { get; set; }
        public DbSet<Conto> Contos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<RigaDigitata> RigaDigitatas { get; set; }
        public DbSet<SituazioneConto> SituazioneContos { get; set; }
        public DbSet<SituazioneVoceIva> SituazioneVoceIVAs { get; set; }
        public DbSet<VoceIva> VoceIvas { get; set; }
        public DbSet<TitoloInapplicabilita> TitoloInapplicabilitas { get; set; }
        public DbSet<ApplicazioneTemplateEffetto> ApplicazioneTemplateEffettoList { get; set; }
        public DbSet<ApplicazioneTemplateDocumento> ApplicazioneTemplateDocumentoList { get; set; }
        public DbSet<ApplicazioneTemplateRiga> ApplicazioneTemplateRigaList { get; set; }
        public DbSet<TemplateEffetto> TemplateEffettoList { get; set; }
        public DbSet<TemplateDocumento> TemplateDocumentoList { get; set; }
        public DbSet<TemplateRiga> TemplateEffettoRigaList { get; set; }
        public DbSet<Feedback> FeedbackList { get; set; }



        protected override void OnModelCreating(ModelBuilder mb)
        {
            foreach (var relationship in mb.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(mb);
            mb.Entity<SituazioneConto>().HasKey(sc => sc.ContoId);
            mb.Entity<SituazioneVoceIva>().HasKey(sv => sv.VoceIvaId);

        }


    }
}
