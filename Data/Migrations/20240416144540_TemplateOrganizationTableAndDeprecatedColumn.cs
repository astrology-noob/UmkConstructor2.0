using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmkConstructor.Data.Migrations
{
    /// <inheritdoc />
    public partial class TemplateOrganizationTableAndDeprecatedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deprecated",
                table: "Disciplines",
                newName: "IsDeprecated");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Curricula",
                newName: "Edition");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "Curricula",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "Curricula");

            migrationBuilder.RenameColumn(
                name: "IsDeprecated",
                table: "Disciplines",
                newName: "Deprecated");

            migrationBuilder.RenameColumn(
                name: "Edition",
                table: "Curricula",
                newName: "Version");
        }
    }
}
