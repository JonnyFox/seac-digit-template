﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Dtos;
using System;

namespace SeacDigitTemplate.Migrations
{
    [DbContext(typeof(SeacDigitTemplateContex))]
    [Migration("20171018145523_Template")]
    partial class Template
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SeacDigitTemplate.Model.AliquotaIVA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Percentuale");

                    b.HasKey("Id");

                    b.ToTable("AliquotaIVAs");
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.Clifor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Istituzionale");

                    b.Property<string>("Nome");

                    b.Property<int>("Soggetto");

                    b.HasKey("Id");

                    b.ToTable("Clifors");
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.Conto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Contos");
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.Documento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Caratteristica");

                    b.Property<int>("CliforId");

                    b.Property<int>("Registro");

                    b.Property<int>("RiferimentoDocumentoId");

                    b.Property<decimal>("RitenutaAcconto");

                    b.Property<int>("Sospeso");

                    b.Property<int>("Tipo");

                    b.Property<decimal>("Totale");

                    b.HasKey("Id");

                    b.HasIndex("CliforId");

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.RigaDigitata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AliquotaIVAId");

                    b.Property<int>("ContoAvereId");

                    b.Property<int>("ContoDareId");

                    b.Property<int>("DocumentoId");

                    b.Property<decimal>("IVA");

                    b.Property<decimal>("Imponibile");

                    b.Property<string>("Note");

                    b.Property<decimal>("PercentualeIndeducibilita");

                    b.Property<decimal>("PercentualeIndetraibilita");

                    b.Property<int>("Settore");

                    b.Property<int>("TitoloInapplicabilita");

                    b.Property<int>("Trattamento");

                    b.Property<int>("VoceIVAId");

                    b.HasKey("Id");

                    b.HasIndex("AliquotaIVAId");

                    b.HasIndex("ContoAvereId");

                    b.HasIndex("ContoDareId");

                    b.HasIndex("DocumentoId");

                    b.HasIndex("VoceIVAId");

                    b.ToTable("RigaDigitatas");
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.SituazioneConto", b =>
                {
                    b.Property<int>("ContoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContoId1");

                    b.Property<decimal>("Valore");

                    b.Property<decimal>("Variazione");

                    b.HasKey("ContoId");

                    b.HasIndex("ContoId1");

                    b.ToTable("SituazioneContos");
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.SituazioneVoceIVA", b =>
                {
                    b.Property<int>("VoceIVAId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AliquotaIVAId");

                    b.Property<int>("TitoloInapplicabilita");

                    b.Property<int>("Trattamento");

                    b.Property<decimal>("Valore");

                    b.Property<int?>("VoceIVAId1");

                    b.HasKey("VoceIVAId");

                    b.HasIndex("AliquotaIVAId");

                    b.HasIndex("VoceIVAId1");

                    b.ToTable("SituazioneVoceIVAs");
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.VoceIVA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("VoceIVAs");
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.Documento", b =>
                {
                    b.HasOne("SeacDigitTemplate.Model.Clifor", "Clifor")
                        .WithMany()
                        .HasForeignKey("CliforId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.RigaDigitata", b =>
                {
                    b.HasOne("SeacDigitTemplate.Model.AliquotaIVA", "AliquotaIVA")
                        .WithMany()
                        .HasForeignKey("AliquotaIVAId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SeacDigitTemplate.Model.Conto", "ContoAvere")
                        .WithMany()
                        .HasForeignKey("ContoAvereId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SeacDigitTemplate.Model.Conto", "ContoDare")
                        .WithMany()
                        .HasForeignKey("ContoDareId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SeacDigitTemplate.Model.Documento", "Documento")
                        .WithMany()
                        .HasForeignKey("DocumentoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SeacDigitTemplate.Model.VoceIVA", "VoceIVA")
                        .WithMany()
                        .HasForeignKey("VoceIVAId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.SituazioneConto", b =>
                {
                    b.HasOne("SeacDigitTemplate.Model.Conto", "Conto")
                        .WithMany()
                        .HasForeignKey("ContoId1")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SeacDigitTemplate.Model.SituazioneVoceIVA", b =>
                {
                    b.HasOne("SeacDigitTemplate.Model.AliquotaIVA", "AliquotaIVA")
                        .WithMany()
                        .HasForeignKey("AliquotaIVAId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SeacDigitTemplate.Model.VoceIVA", "VoceIVA")
                        .WithMany()
                        .HasForeignKey("VoceIVAId1")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
