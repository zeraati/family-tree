using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTree.Migrations
{
    /// <inheritdoc />
    public partial class PersonSpouseRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonSpouse_Person",
                table: "PersonSpouse");

            migrationBuilder.DropIndex(
                name: "IX_PersonSpouse_PersonId",
                table: "PersonSpouse");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSpouse_PersonId",
                table: "PersonSpouse",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSpouse_Person",
                table: "PersonSpouse",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonSpouse_Person",
                table: "PersonSpouse");

            migrationBuilder.DropIndex(
                name: "IX_PersonSpouse_PersonId",
                table: "PersonSpouse");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSpouse_PersonId",
                table: "PersonSpouse",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSpouse_Person",
                table: "PersonSpouse",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
