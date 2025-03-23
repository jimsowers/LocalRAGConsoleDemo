using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalRagConsoleDemo.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmbeddingNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float[]>(
                name: "Embedding",
                table: "Documents",
                type: "real[]",
                nullable: true,
                oldClrType: typeof(float[]),
                oldType: "real[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float[]>(
                name: "Embedding",
                table: "Documents",
                type: "real[]",
                nullable: false,
                defaultValue: new float[0],
                oldClrType: typeof(float[]),
                oldType: "real[]",
                oldNullable: true);
        }
    }
}
