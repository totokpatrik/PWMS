using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWMS.Persistence.PortgreSQL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddressType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressType",
                table: "Addresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "Addresses");
        }
    }
}
