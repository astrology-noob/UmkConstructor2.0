using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmkConstructor.Data.Migrations
{
    /// <inheritdoc />
    public partial class SemesterBusinessRoleTableDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterBusinessRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SemesterBusinessRole",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessRoleId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterBusinessRole", x => x.RelationId);
                    table.ForeignKey(
                        name: "FK_SemesterBusinessRole_BusinessRoles_BusinessRoleId",
                        column: x => x.BusinessRoleId,
                        principalTable: "BusinessRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterBusinessRole_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "RelationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterBusinessRole_BusinessRoleId",
                table: "SemesterBusinessRole",
                column: "BusinessRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterBusinessRole_SemesterId",
                table: "SemesterBusinessRole",
                column: "SemesterId");
        }
    }
}
