using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_UtshobKotha.Migrations
{
    /// <inheritdoc />
    public partial class ImprovedEventController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentEnrolled",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentEnrolled",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Events");
        }
    }
}
