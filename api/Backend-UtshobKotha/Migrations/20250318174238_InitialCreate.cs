using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_UtshobKotha.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewUserRegistration",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewUserRegistration", x => x.Email);
                });

            migrationBuilder.InsertData(
                table: "NewUserRegistration",
                columns: new[] { "Email", "Name", "Password", "Role", "UserID" },
                values: new object[] { "test@gmail.com", "Shamim", "test", "ADMIN", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewUserRegistration");
        }
    }
}
