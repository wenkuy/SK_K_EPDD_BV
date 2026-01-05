using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_LineProductionRecords",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    ProductConsumed = table.Column<float>(type: "real", nullable: false),
                    ProductsOutput_Product_1 = table.Column<int>(type: "int", nullable: true),
                    ProductsOutput_Product_2 = table.Column<int>(type: "int", nullable: true),
                    ProductsOutput_Product_3 = table.Column<int>(type: "int", nullable: true),
                    ProductsOutput_Product_4 = table.Column<int>(type: "int", nullable: true),
                    ProductsOutput_Product_5 = table.Column<int>(type: "int", nullable: true),
                    ProductsRate_Product1Rate = table.Column<float>(type: "real", nullable: true),
                    ProductsRate_Product2Rate = table.Column<float>(type: "real", nullable: true),
                    ProductsRate_Product3Rate = table.Column<float>(type: "real", nullable: true),
                    ProductsRate_Product4Rate = table.Column<float>(type: "real", nullable: true),
                    ProductsRate_Product5Rate = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LineProductionRecords", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_LineProductionRecords");
        }
    }
}
