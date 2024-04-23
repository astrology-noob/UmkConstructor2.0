using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmkConstructor.Data.Migrations
{
    /// <inheritdoc />
    public partial class SemestersTableCorrectNameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealSemesters_SemesterTypeStudyYear_SemesterTypeStudyYearId",
                table: "RealSemesters");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterBusinessRole_RealSemesters_SemesterId",
                table: "SemesterBusinessRole");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterCurriculum_RealSemesters_SemesterId",
                table: "SemesterCurriculum");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RealSemesters",
                table: "RealSemesters");

            migrationBuilder.RenameTable(
                name: "RealSemesters",
                newName: "Semesters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "RelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterBusinessRole_Semesters_SemesterId",
                table: "SemesterBusinessRole",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "RelationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterCurriculum_Semesters_SemesterId",
                table: "SemesterCurriculum",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "RelationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_SemesterTypeStudyYear_SemesterTypeStudyYearId",
                table: "Semesters",
                column: "SemesterTypeStudyYearId",
                principalTable: "SemesterTypeStudyYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SemesterBusinessRole_Semesters_SemesterId",
                table: "SemesterBusinessRole");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterCurriculum_Semesters_SemesterId",
                table: "SemesterCurriculum");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_SemesterTypeStudyYear_SemesterTypeStudyYearId",
                table: "Semesters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.RenameTable(
                name: "Semesters",
                newName: "RealSemesters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RealSemesters",
                table: "RealSemesters",
                column: "RelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealSemesters_SemesterTypeStudyYear_SemesterTypeStudyYearId",
                table: "RealSemesters",
                column: "SemesterTypeStudyYearId",
                principalTable: "SemesterTypeStudyYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterBusinessRole_RealSemesters_SemesterId",
                table: "SemesterBusinessRole",
                column: "SemesterId",
                principalTable: "RealSemesters",
                principalColumn: "RelationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterCurriculum_RealSemesters_SemesterId",
                table: "SemesterCurriculum",
                column: "SemesterId",
                principalTable: "RealSemesters",
                principalColumn: "RelationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
