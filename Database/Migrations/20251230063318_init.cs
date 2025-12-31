using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_ProducttProductionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProductNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeConsumed = table.Column<float>(type: "real", nullable: false),
                    QualityInspectionResult = table.Column<bool>(type: "bit", nullable: false),
                    Param_Temperature = table.Column<float>(type: "real", nullable: true),
                    Param_Humidity = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ProducttProductionRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_ProducttProductionRecords");
        }
    }
}
