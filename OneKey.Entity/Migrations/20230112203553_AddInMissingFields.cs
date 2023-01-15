using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneKey.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddInMissingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Passwords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailsFor",
                table: "Passwords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Passwords",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Passwords");

            migrationBuilder.DropColumn(
                name: "DetailsFor",
                table: "Passwords");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Passwords");
        }
    }
}
