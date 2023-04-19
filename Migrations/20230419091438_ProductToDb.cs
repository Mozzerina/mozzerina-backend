using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mozzerina.Migrations
{
    /// <inheritdoc />
    public partial class ProductToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Callories = table.Column<int>(type: "int", nullable: false),
                    Short = table.Column<int>(type: "int", nullable: false),
                    Tall = table.Column<int>(type: "int", nullable: false),
                    Grande = table.Column<int>(type: "int", nullable: false),
                    Venti = table.Column<int>(type: "int", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergens = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nutrition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubProductId",
                table: "Products",
                column: "SubProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
