﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PWMS.Persistence.PortgreSQL.Data.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20241016103232_InitialCreate")]
partial class InitialCreate
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.10")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("PWMS.Domain.Addresses.Entities.Address", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<string>("AddressLine")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.ToTable("Addresses", (string)null);
            });
#pragma warning restore 612, 618
    }
}
