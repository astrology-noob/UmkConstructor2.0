using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmkConstructor.Data.Migrations
{
    /// <inheritdoc />
    public partial class DisciplineCodeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SemesterTypeStudyYear",
                newName: "RelationId");

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Curricula",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Deprecated = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineRealSemester",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    SemesterCurriculumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineRealSemester", x => x.RelationId);
                    table.ForeignKey(
                        name: "FK_DisciplineRealSemester_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineRealSemester_SemesterCurriculum_SemesterCurriculumId",
                        column: x => x.SemesterCurriculumId,
                        principalTable: "SemesterCurriculum",
                        principalColumn: "RelationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineRealSemester_DisciplineId",
                table: "DisciplineRealSemester",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineRealSemester_SemesterCurriculumId",
                table: "DisciplineRealSemester",
                column: "SemesterCurriculumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplineRealSemester");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Curricula");

            migrationBuilder.RenameColumn(
                name: "RelationId",
                table: "SemesterTypeStudyYear",
                newName: "Id");
        }
    }
}
