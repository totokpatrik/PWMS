using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWMS.Persistence.PortgreSQL.Data.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseAndSite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SelectedSiteId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sites_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SitesAdmins",
                columns: table => new
                {
                    AdminSitesId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitesAdmins", x => new { x.AdminSitesId, x.AdminsId });
                    table.ForeignKey(
                        name: "FK_SitesAdmins_AspNetUsers_AdminsId",
                        column: x => x.AdminsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SitesAdmins_Sites_AdminSitesId",
                        column: x => x.AdminSitesId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SitesUsers",
                columns: table => new
                {
                    UserSitesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitesUsers", x => new { x.UserSitesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SitesUsers_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SitesUsers_Sites_UserSitesId",
                        column: x => x.UserSitesId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SiteId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouses_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehousesAdmins",
                columns: table => new
                {
                    AdminWarehousesId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehousesAdmins", x => new { x.AdminWarehousesId, x.AdminsId });
                    table.ForeignKey(
                        name: "FK_WarehousesAdmins_AspNetUsers_AdminsId",
                        column: x => x.AdminsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehousesAdmins_Warehouses_AdminWarehousesId",
                        column: x => x.AdminWarehousesId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehousesUsers",
                columns: table => new
                {
                    UserWarehousesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehousesUsers", x => new { x.UserWarehousesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_WarehousesUsers_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehousesUsers_Warehouses_UserWarehousesId",
                        column: x => x.UserWarehousesId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SelectedSiteId",
                table: "AspNetUsers",
                column: "SelectedSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_OwnerId",
                table: "Sites",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SitesAdmins_AdminsId",
                table: "SitesAdmins",
                column: "AdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_SitesUsers_UsersId",
                table: "SitesUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_OwnerId",
                table: "Warehouses",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_SiteId",
                table: "Warehouses",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousesAdmins_AdminsId",
                table: "WarehousesAdmins",
                column: "AdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousesUsers_UsersId",
                table: "WarehousesUsers",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sites_SelectedSiteId",
                table: "AspNetUsers",
                column: "SelectedSiteId",
                principalTable: "Sites",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sites_SelectedSiteId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SitesAdmins");

            migrationBuilder.DropTable(
                name: "SitesUsers");

            migrationBuilder.DropTable(
                name: "WarehousesAdmins");

            migrationBuilder.DropTable(
                name: "WarehousesUsers");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SelectedSiteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SelectedSiteId",
                table: "AspNetUsers");
        }
    }
}
