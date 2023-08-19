﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NpBioApi.Models;

#nullable disable

namespace NpBioApi.Migrations
{
    [DbContext(typeof(NpBioApiContext))]
    partial class NpBioApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NpBioApi.Models.Park", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Acres")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.Property<string>("ParkCode")
                        .HasColumnType("longtext");

                    b.Property<string>("ParkName")
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Parks");
                });

            modelBuilder.Entity("NpBioApi.Models.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Abundance")
                        .HasColumnType("longtext");

                    b.Property<string>("Category")
                        .HasColumnType("longtext");

                    b.Property<string>("CommonNames")
                        .HasColumnType("longtext");

                    b.Property<string>("ConservationStatus")
                        .HasColumnType("longtext");

                    b.Property<string>("Family")
                        .HasColumnType("longtext");

                    b.Property<string>("Nativeness")
                        .HasColumnType("longtext");

                    b.Property<string>("Occurrence")
                        .HasColumnType("longtext");

                    b.Property<string>("Order")
                        .HasColumnType("longtext");

                    b.Property<int>("ParkId")
                        .HasColumnType("int");

                    b.Property<string>("ParkName")
                        .HasColumnType("longtext");

                    b.Property<string>("RecordStatus")
                        .HasColumnType("longtext");

                    b.Property<string>("ScientificName")
                        .HasColumnType("longtext");

                    b.Property<string>("Seasonality")
                        .HasColumnType("longtext");

                    b.Property<string>("SpeciesId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ParkId");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("NpBioApi.Models.Species", b =>
                {
                    b.HasOne("NpBioApi.Models.Park", "Park")
                        .WithMany("Species")
                        .HasForeignKey("ParkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Park");
                });

            modelBuilder.Entity("NpBioApi.Models.Park", b =>
                {
                    b.Navigation("Species");
                });
#pragma warning restore 612, 618
        }
    }
}
