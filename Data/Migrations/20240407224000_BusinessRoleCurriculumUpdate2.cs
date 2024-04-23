using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmkConstructor.Data.Migrations
{
    /// <inheritdoc />
    public partial class BusinessRoleCurriculumUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Curricula_BusinessRoleId",
                table: "Curricula",
                column: "BusinessRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curricula_BusinessRoles_BusinessRoleId",
                table: "Curricula",
                column: "BusinessRoleId",
                principalTable: "BusinessRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curricula_BusinessRoles_BusinessRoleId",
                table: "Curricula");

            migrationBuilder.DropIndex(
                name: "IX_Curricula_BusinessRoleId",
                table: "Curricula");
        }
    }
}
