using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddBrigadeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BrigadeId",
                table: "Complaints",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brigade",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrigadeNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brigade", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_BrigadeId",
                table: "Complaints",
                column: "BrigadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Brigade_BrigadeId",
                table: "Complaints",
                column: "BrigadeId",
                principalTable: "Brigade",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Brigade_BrigadeId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "Brigade");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_BrigadeId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "BrigadeId",
                table: "Complaints");
        }
    }
}
