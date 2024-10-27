using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TechStackStudies.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "technologies",
                columns: table => new
                {
                    technology_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    is_framework_or_lib = table.Column<bool>(type: "boolean", nullable: false),
                    CurrentVersion = table.Column<float>(type: "real", nullable: false),
                    category = table.Column<string>(type: "varchar", maxLength: 8, nullable: false),
                    skill_level = table.Column<string>(type: "varchar", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_technologies", x => x.technology_id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_technologies_id",
                table: "technologies",
                column: "technology_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "technologies");
        }
    }
}
