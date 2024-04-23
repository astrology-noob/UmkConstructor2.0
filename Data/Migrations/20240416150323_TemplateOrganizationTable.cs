using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmkConstructor.Data.Migrations
{
    /// <inheritdoc />
    public partial class TemplateOrganizationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Template",
                table: "Template");

            migrationBuilder.RenameTable(
                name: "Template",
                newName: "Templates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Templates",
                table: "Templates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TemplateOrganizations",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateOrganizations", x => x.RelationId);
                    table.ForeignKey(
                        name: "FK_TemplateOrganizations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateOrganizations_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateOrganization_OrganizationId",
                table: "TemplateOrganizations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateOrganization_TemplateId",
                table: "TemplateOrganizations",
                column: "TemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Templates",
                table: "Templates");

            migrationBuilder.RenameTable(
                name: "Templates",
                newName: "Template");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Template",
                table: "Template",
                column: "Id");
        }
    }
}
