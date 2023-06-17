using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddDistrictEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Coordinates",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DistrictId",
                table: "Coordinates",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "Coordinates",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DistrictId",
                table: "Cities",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_CityId",
                table: "Coordinates",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_DistrictId",
                table: "Coordinates",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_RegionId",
                table: "Coordinates",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_DistrictId",
                table: "Cities",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_RegionId",
                table: "Districts",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Districts_DistrictId",
                table: "Cities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Cities_CityId",
                table: "Coordinates",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Districts_DistrictId",
                table: "Coordinates",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Regions_RegionId",
                table: "Coordinates",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Districts_DistrictId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Cities_CityId",
                table: "Coordinates");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Districts_DistrictId",
                table: "Coordinates");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Regions_RegionId",
                table: "Coordinates");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Coordinates_CityId",
                table: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Coordinates_DistrictId",
                table: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Coordinates_RegionId",
                table: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Cities_DistrictId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Cities");
        }
    }
}
