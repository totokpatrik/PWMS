using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace PWMS.Api.Migrations.EventStore
{
    /// <inheritdoc />
    public partial class EventStore_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false, comment: "JSON serialized event"),
                    MessageType = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStores", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventStores");
        }
    }
}
