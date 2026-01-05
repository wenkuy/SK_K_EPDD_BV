using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_MaterialRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualityPArams_IsQualified = table.Column<bool>(type: "bit", nullable: true),
                    QualityPArams_IsDeformed = table.Column<bool>(type: "bit", nullable: true),
                    QualityPArams_IsBurred = table.Column<bool>(type: "bit", nullable: true),
                    QualityPArams_Weight = table.Column<float>(type: "real", nullable: true),
                    QualityPArams_Height = table.Column<float>(type: "real", nullable: true),
                    QualityPArams_Width = table.Column<float>(type: "real", nullable: true),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MaterialRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_MaterialRecords");
        }
    }
}
