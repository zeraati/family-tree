using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTree.Migrations
{
    /// <inheritdoc />
    public partial class PersonSpouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonFamily_Spouse",
                table: "PersonFamily");

            migrationBuilder.DropIndex(
                name: "IX_PersonFamily_SpouseId",
                table: "PersonFamily");

            migrationBuilder.DropColumn(
                name: "SpouseId",
                table: "PersonFamily");

            migrationBuilder.CreateTable(
                name: "PersonSpouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpouseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSpouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonSpouse_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonSpouse_Spouse",
                        column: x => x.SpouseId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonSpouse_PersonId",
                table: "PersonSpouse",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonSpouse_SpouseId",
                table: "PersonSpouse",
                column: "SpouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonSpouse");

            migrationBuilder.AddColumn<int>(
                name: "SpouseId",
                table: "PersonFamily",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonFamily_SpouseId",
                table: "PersonFamily",
                column: "SpouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonFamily_Spouse",
                table: "PersonFamily",
                column: "SpouseId",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
