﻿// <auto-generated />
using Angular5TF1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Angular5TF1.Migrations.Data
{
    [DbContext(typeof(DataContext))]
    [Migration("20180122102542_foreignKeyChange")]
    partial class foreignKeyChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Angular5TF1.Data.Model.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SearchTermId");

                    b.Property<string>("banner_url");

                    b.Property<string>("end_url");

                    b.Property<string>("event_id");

                    b.Property<string>("event_url");

                    b.Property<string>("eventname");

                    b.Property<int>("featured");

                    b.Property<string>("full_address");

                    b.Property<string>("label");

                    b.Property<string>("location");

                    b.Property<string>("object_type");

                    b.Property<string>("share_url");

                    b.Property<int>("start_time");

                    b.Property<string>("start_time_display");

                    b.Property<string>("thumb_url");

                    b.Property<string>("thumb_url_large");

                    b.HasKey("Id");

                    b.HasIndex("SearchTermId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Angular5TF1.Data.Model.SearchTerm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("SearchDate");

                    b.Property<string>("Term");

                    b.HasKey("Id");

                    b.ToTable("SearchTerms");
                });

            modelBuilder.Entity("Angular5TF1.Data.Model.Wikipedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SearchTermId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("SearchTermId")
                        .IsUnique();

                    b.ToTable("Wikipedias");
                });

            modelBuilder.Entity("Angular5TF1.Data.Model.Event", b =>
                {
                    b.HasOne("Angular5TF1.Data.Model.SearchTerm", "SearchTerm")
                        .WithMany("Events")
                        .HasForeignKey("SearchTermId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Angular5TF1.Data.Model.Wikipedia", b =>
                {
                    b.HasOne("Angular5TF1.Data.Model.SearchTerm", "SearchTerm")
                        .WithOne("Wikipedia")
                        .HasForeignKey("Angular5TF1.Data.Model.Wikipedia", "SearchTermId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
