using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmkConstructor.Data.Migrations
{
    /// <inheritdoc />
    public partial class SemestersUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurriculumJoins");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.CreateTable(
                name: "SemesterTypeStudyYear",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterTypeId = table.Column<int>(type: "int", nullable: false),
                    StudyYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterTypeStudyYear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterTypeStudyYear_SemesterTypes_SemesterTypeId",
                        column: x => x.SemesterTypeId,
                        principalTable: "SemesterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterTypeStudyYear_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealSemesters",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterTypeStudyYearId = table.Column<int>(type: "int", nullable: false),
                    HoursTotal = table.Column<int>(type: "int", nullable: false),
                    IndividualWork = table.Column<int>(type: "int", nullable: false),
                    WeekCount = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealSemesters", x => x.RelationId);
                    table.ForeignKey(
                        name: "FK_RealSemesters_SemesterTypeStudyYear_SemesterTypeStudyYearId",
                        column: x => x.SemesterTypeStudyYearId,
                        principalTable: "SemesterTypeStudyYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_SemesterBusinessRole_RealSemesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "RealSemesters",
                        principalColumn: "RelationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterCurriculum",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurriculumId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterCurriculum", x => x.RelationId);
                    table.ForeignKey(
                        name: "FK_SemesterCurriculum_Curricula_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterCurriculum_RealSemesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "RealSemesters",
                        principalColumn: "RelationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_SemesterTypeStudyYearId",
                table: "RealSemesters",
                column: "SemesterTypeStudyYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterBusinessRole_BusinessRoleId",
                table: "SemesterBusinessRole",
                column: "BusinessRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterBusinessRole_SemesterId",
                table: "SemesterBusinessRole",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterCurriculum_CurriculumId",
                table: "SemesterCurriculum",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterCurriculum_SemesterId",
                table: "SemesterCurriculum",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterTypeStudyYear_SemesterTypeId",
                table: "SemesterTypeStudyYear",
                column: "SemesterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterTypeStudyYear_StudyYearId",
                table: "SemesterTypeStudyYear",
                column: "StudyYearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterBusinessRole");

            migrationBuilder.DropTable(
                name: "SemesterCurriculum");

            migrationBuilder.DropTable(
                name: "RealSemesters");

            migrationBuilder.DropTable(
                name: "SemesterTypeStudyYear");

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterTypeId = table.Column<int>(type: "int", nullable: false),
                    StudyYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_SemesterTypes_SemesterTypeId",
                        column: x => x.SemesterTypeId,
                        principalTable: "SemesterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Semesters_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumJoins",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessRoleId = table.Column<int>(type: "int", nullable: false),
                    CurriculumId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    HoursTotal = table.Column<int>(type: "int", nullable: false),
                    IndividualWork = table.Column<int>(type: "int", nullable: false),
                    WeekCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumJoins", x => x.RelationId);
                    table.ForeignKey(
                        name: "FK_CurriculumJoins_BusinessRoles_BusinessRoleId",
                        column: x => x.BusinessRoleId,
                        principalTable: "BusinessRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumJoins_Curricula_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumJoins_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumJoins_BusinessRoleId",
                table: "CurriculumJoins",
                column: "BusinessRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumJoins_CurriculumId",
                table: "CurriculumJoins",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumJoins_SemesterId",
                table: "CurriculumJoins",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_SemesterTypeId",
                table: "Semesters",
                column: "SemesterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_StudyYearId",
                table: "Semesters",
                column: "StudyYearId");
        }
    }
}
