﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PWMS.Infrastructure.Data.Context;

#nullable disable

namespace PWMS.Api.Migrations.EventStore
{
    [DbContext(typeof(EventStoreDbContext))]
    [Migration("20241010072402_EventStore_Initial")]
    partial class EventStore_Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PWMS.Core.SharedKernel.EventStore", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("character varying(255)")
                        .HasComment("JSON serialized event");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.HasKey("Id");

                    b.ToTable("EventStores");
                });
#pragma warning restore 612, 618
        }
    }
}