﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zugetextet.formulare.Data;

#nullable disable

namespace zugetextet.formulare.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221209095436_NewFormAttributes")]
    partial class NewFormAttributes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("zugetextet.formulare.Data.Models.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("zugetextet.formulare.Data.Models.AttachmentVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("FileBytes")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FormDataId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OriginalAttachmentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FormDataId");

                    b.HasIndex("OriginalAttachmentId");

                    b.ToTable("AttachmentVersion");
                });

            modelBuilder.Entity("zugetextet.formulare.Data.Models.FormData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("ConditionsOfParticipationConsent")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FormId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("Images")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsUnderage")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("Kurzbibliographie")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("Kurzbiographie")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("Lyrik")
                        .HasColumnType("TEXT");

                    b.Property<bool>("OriginatorAndPublicationConsent")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("ParentalConsent")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("Prosa")
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Zipcode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.HasIndex("Images");

                    b.HasIndex("Kurzbibliographie");

                    b.HasIndex("Kurzbiographie");

                    b.HasIndex("Lyrik");

                    b.HasIndex("ParentalConsent");

                    b.HasIndex("Prosa");

                    b.ToTable("FormData");
                });

            modelBuilder.Entity("zugetextet.formulare.Data.Models.Forms", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AmountLyrik")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("From")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ImagesIsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("ProsaIsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Until")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("zugetextet.formulare.Data.Models.LoginData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LoginData");
                });

            modelBuilder.Entity("zugetextet.formulare.Data.Models.AttachmentVersion", b =>
                {
                    b.HasOne("zugetextet.formulare.Data.Models.FormData", "FormData")
                        .WithMany()
                        .HasForeignKey("FormDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("zugetextet.formulare.Data.Models.Attachment", "OriginalAttachment")
                        .WithMany()
                        .HasForeignKey("OriginalAttachmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormData");

                    b.Navigation("OriginalAttachment");
                });

            modelBuilder.Entity("zugetextet.formulare.Data.Models.FormData", b =>
                {
                    b.HasOne("zugetextet.formulare.Data.Models.Forms", "Form")
                        .WithMany()
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("zugetextet.formulare.Data.Models.Attachment", "AttachmentImages")
                        .WithMany()
                        .HasForeignKey("Images");

                    b.HasOne("zugetextet.formulare.Data.Models.Attachment", "AttachmentKurzbibliographie")
                        .WithMany()
                        .HasForeignKey("Kurzbibliographie");

                    b.HasOne("zugetextet.formulare.Data.Models.Attachment", "AttachmentKurzbiographie")
                        .WithMany()
                        .HasForeignKey("Kurzbiographie");

                    b.HasOne("zugetextet.formulare.Data.Models.Attachment", "AttachmentLyrik")
                        .WithMany()
                        .HasForeignKey("Lyrik");

                    b.HasOne("zugetextet.formulare.Data.Models.Attachment", "AttachmentConsent")
                        .WithMany()
                        .HasForeignKey("ParentalConsent");

                    b.HasOne("zugetextet.formulare.Data.Models.Attachment", "AttachmentProsa")
                        .WithMany()
                        .HasForeignKey("Prosa");

                    b.Navigation("AttachmentConsent");

                    b.Navigation("AttachmentImages");

                    b.Navigation("AttachmentKurzbibliographie");

                    b.Navigation("AttachmentKurzbiographie");

                    b.Navigation("AttachmentLyrik");

                    b.Navigation("AttachmentProsa");

                    b.Navigation("Form");
                });
#pragma warning restore 612, 618
        }
    }
}
