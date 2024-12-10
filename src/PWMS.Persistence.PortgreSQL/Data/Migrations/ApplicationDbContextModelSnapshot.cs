﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PWMS.Persistence.PortgreSQL.Data;

#nullable disable

namespace PWMS.Persistence.PortgreSQL.Data.Migrations;

[DbContext(typeof(ApplicationDbContext))]
partial class ApplicationDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.10")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("text");

                b.Property<string>("ClaimValue")
                    .HasColumnType("text");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims", (string)null);
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("text");

                b.Property<string>("ClaimValue")
                    .HasColumnType("text");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims", (string)null);
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("text");

                b.Property<string>("ProviderKey")
                    .HasColumnType("text");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("text");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins", (string)null);
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("text");

                b.Property<string>("RoleId")
                    .HasColumnType("text");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles", (string)null);
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("text");

                b.Property<string>("LoginProvider")
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.Property<string>("Value")
                    .HasColumnType("text");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Addresses.Entities.Address", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<string>("AddressLine")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.Property<int>("AddressType")
                    .HasColumnType("integer");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<Guid?>("WarehouseId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.ToTable("Addresses", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Auth.Entities.Role", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("text");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex");

                b.ToTable("AspNetRoles", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Auth.Entities.User", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("text");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("integer");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("text");

                b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("boolean");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("boolean");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("text");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("text");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("boolean");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("text");

                b.Property<Guid?>("SelectedSiteId")
                    .HasColumnType("uuid");

                b.Property<Guid?>("SelectedWarehouseId")
                    .HasColumnType("uuid");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("boolean");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex");

                b.HasIndex("SelectedSiteId");

                b.HasIndex("SelectedWarehouseId");

                b.ToTable("AspNetUsers", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Core.Sites.Entities.Site", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.Property<string>("OwnerId")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("OwnerId");

                b.ToTable("Sites", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Core.Warehouses.Entities.Warehouse", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.Property<string>("OwnerId")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid>("SiteId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.HasIndex("OwnerId");

                b.HasIndex("SiteId");

                b.ToTable("Warehouses", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Entities.UnitOfMeasure", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid?>("WarehouseId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.ToTable("UnitOfMeasures", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.AlternateItem", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<int>("AlternateItemType")
                    .HasColumnType("integer");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<Guid>("ItemId")
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<Guid>("UnitOfMeasureId")
                    .HasColumnType("uuid");

                b.Property<string>("Value")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid?>("WarehouseId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.HasIndex("ItemId");

                b.HasIndex("UnitOfMeasureId");

                b.ToTable("AlternateItems", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.Footprint", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<bool>("Default")
                    .HasColumnType("boolean");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid>("ItemId")
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid?>("WarehouseId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.HasIndex("ItemId");

                b.ToTable("Footprints", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.FootprintDetail", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<Guid>("FoorptinrId")
                    .HasColumnType("uuid");

                b.Property<int>("GrossWeight")
                    .HasColumnType("integer");

                b.Property<int>("Height")
                    .HasColumnType("integer");

                b.Property<int>("Length")
                    .HasColumnType("integer");

                b.Property<int>("Level")
                    .HasColumnType("integer");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<int>("NetWeight")
                    .HasColumnType("integer");

                b.Property<bool>("Receive")
                    .HasColumnType("boolean");

                b.Property<Guid>("UnitOfMeasureId")
                    .HasColumnType("uuid");

                b.Property<int>("UnitQuantity")
                    .HasColumnType("integer");

                b.Property<Guid?>("WarehouseId")
                    .HasColumnType("uuid");

                b.Property<int>("Width")
                    .HasColumnType("integer");

                b.HasKey("Id");

                b.HasIndex("FoorptinrId");

                b.HasIndex("UnitOfMeasureId");

                b.ToTable("FootprintDetails", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.Item", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid>("ItemFamilyId")
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<bool>("ReceiveStatus")
                    .HasColumnType("boolean");

                b.Property<string>("ShortDescription")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid?>("WarehouseId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.HasIndex("ItemFamilyId");

                b.ToTable("Items", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.ItemFamily", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<string>("Description")
                    .HasColumnType("text");

                b.Property<Guid>("ItemFamilyGroupId")
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid?>("WarehouseId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.HasIndex("ItemFamilyGroupId");

                b.ToTable("ItemFamilies", (string)null);
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.ItemFamilyGroup", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTime?>("Created")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("CreatedBy")
                    .HasColumnType("text");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<DateTime?>("Modified")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("ModifiedBy")
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid?>("WarehouseId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.ToTable("ItemFamilyGroups", (string)null);
            });

        modelBuilder.Entity("SiteUser", b =>
            {
                b.Property<Guid>("UserSitesId")
                    .HasColumnType("uuid");

                b.Property<string>("UsersId")
                    .HasColumnType("text");

                b.HasKey("UserSitesId", "UsersId");

                b.HasIndex("UsersId");

                b.ToTable("SitesUsers", (string)null);
            });

        modelBuilder.Entity("SiteUser1", b =>
            {
                b.Property<Guid>("AdminSitesId")
                    .HasColumnType("uuid");

                b.Property<string>("AdminsId")
                    .HasColumnType("text");

                b.HasKey("AdminSitesId", "AdminsId");

                b.HasIndex("AdminsId");

                b.ToTable("SitesAdmins", (string)null);
            });

        modelBuilder.Entity("UserWarehouse", b =>
            {
                b.Property<Guid>("UserWarehousesId")
                    .HasColumnType("uuid");

                b.Property<string>("UsersId")
                    .HasColumnType("text");

                b.HasKey("UserWarehousesId", "UsersId");

                b.HasIndex("UsersId");

                b.ToTable("WarehousesUsers", (string)null);
            });

        modelBuilder.Entity("UserWarehouse1", b =>
            {
                b.Property<Guid>("AdminWarehousesId")
                    .HasColumnType("uuid");

                b.Property<string>("AdminsId")
                    .HasColumnType("text");

                b.HasKey("AdminWarehousesId", "AdminsId");

                b.HasIndex("AdminsId");

                b.ToTable("WarehousesAdmins", (string)null);
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.HasOne("PWMS.Domain.Auth.Entities.Role", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.HasOne("PWMS.Domain.Auth.Entities.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.HasOne("PWMS.Domain.Auth.Entities.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.HasOne("PWMS.Domain.Auth.Entities.Role", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("PWMS.Domain.Auth.Entities.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.HasOne("PWMS.Domain.Auth.Entities.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("PWMS.Domain.Auth.Entities.User", b =>
            {
                b.HasOne("PWMS.Domain.Core.Sites.Entities.Site", "SelectedSite")
                    .WithMany("UsersSelected")
                    .HasForeignKey("SelectedSiteId");

                b.HasOne("PWMS.Domain.Core.Warehouses.Entities.Warehouse", "SelectedWarehouse")
                    .WithMany("UsersSelected")
                    .HasForeignKey("SelectedWarehouseId");

                b.Navigation("SelectedSite");

                b.Navigation("SelectedWarehouse");
            });

        modelBuilder.Entity("PWMS.Domain.Core.Sites.Entities.Site", b =>
            {
                b.HasOne("PWMS.Domain.Auth.Entities.User", "Owner")
                    .WithMany()
                    .HasForeignKey("OwnerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Owner");
            });

        modelBuilder.Entity("PWMS.Domain.Core.Warehouses.Entities.Warehouse", b =>
            {
                b.HasOne("PWMS.Domain.Auth.Entities.User", "Owner")
                    .WithMany()
                    .HasForeignKey("OwnerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("PWMS.Domain.Core.Sites.Entities.Site", "Site")
                    .WithMany("Warehouses")
                    .HasForeignKey("SiteId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Owner");

                b.Navigation("Site");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.AlternateItem", b =>
            {
                b.HasOne("PWMS.Domain.Inventories.Items.Entities.Item", "Item")
                    .WithMany("AlternateItems")
                    .HasForeignKey("ItemId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("PWMS.Domain.Inventories.Entities.UnitOfMeasure", "UnitOfMeasure")
                    .WithMany("AlternateItems")
                    .HasForeignKey("UnitOfMeasureId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Item");

                b.Navigation("UnitOfMeasure");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.Footprint", b =>
            {
                b.HasOne("PWMS.Domain.Inventories.Items.Entities.Item", "Item")
                    .WithMany("Footprints")
                    .HasForeignKey("ItemId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Item");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.FootprintDetail", b =>
            {
                b.HasOne("PWMS.Domain.Inventories.Items.Entities.Footprint", "Footprint")
                    .WithMany("FootprintDetails")
                    .HasForeignKey("FoorptinrId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("PWMS.Domain.Inventories.Entities.UnitOfMeasure", "UnitOfMeasure")
                    .WithMany("FootprintDetails")
                    .HasForeignKey("UnitOfMeasureId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Footprint");

                b.Navigation("UnitOfMeasure");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.Item", b =>
            {
                b.HasOne("PWMS.Domain.Inventories.Items.Entities.ItemFamily", "ItemFamily")
                    .WithMany("Items")
                    .HasForeignKey("ItemFamilyId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("ItemFamily");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.ItemFamily", b =>
            {
                b.HasOne("PWMS.Domain.Inventories.Items.Entities.ItemFamilyGroup", "ItemFamilyGroup")
                    .WithMany("ItemFamilies")
                    .HasForeignKey("ItemFamilyGroupId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("ItemFamilyGroup");
            });

        modelBuilder.Entity("SiteUser", b =>
            {
                b.HasOne("PWMS.Domain.Core.Sites.Entities.Site", null)
                    .WithMany()
                    .HasForeignKey("UserSitesId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("PWMS.Domain.Auth.Entities.User", null)
                    .WithMany()
                    .HasForeignKey("UsersId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("SiteUser1", b =>
            {
                b.HasOne("PWMS.Domain.Core.Sites.Entities.Site", null)
                    .WithMany()
                    .HasForeignKey("AdminSitesId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("PWMS.Domain.Auth.Entities.User", null)
                    .WithMany()
                    .HasForeignKey("AdminsId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("UserWarehouse", b =>
            {
                b.HasOne("PWMS.Domain.Core.Warehouses.Entities.Warehouse", null)
                    .WithMany()
                    .HasForeignKey("UserWarehousesId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("PWMS.Domain.Auth.Entities.User", null)
                    .WithMany()
                    .HasForeignKey("UsersId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("UserWarehouse1", b =>
            {
                b.HasOne("PWMS.Domain.Core.Warehouses.Entities.Warehouse", null)
                    .WithMany()
                    .HasForeignKey("AdminWarehousesId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("PWMS.Domain.Auth.Entities.User", null)
                    .WithMany()
                    .HasForeignKey("AdminsId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("PWMS.Domain.Core.Sites.Entities.Site", b =>
            {
                b.Navigation("UsersSelected");

                b.Navigation("Warehouses");
            });

        modelBuilder.Entity("PWMS.Domain.Core.Warehouses.Entities.Warehouse", b =>
            {
                b.Navigation("UsersSelected");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Entities.UnitOfMeasure", b =>
            {
                b.Navigation("AlternateItems");

                b.Navigation("FootprintDetails");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.Footprint", b =>
            {
                b.Navigation("FootprintDetails");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.Item", b =>
            {
                b.Navigation("AlternateItems");

                b.Navigation("Footprints");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.ItemFamily", b =>
            {
                b.Navigation("Items");
            });

        modelBuilder.Entity("PWMS.Domain.Inventories.Items.Entities.ItemFamilyGroup", b =>
            {
                b.Navigation("ItemFamilies");
            });
#pragma warning restore 612, 618
    }
}
