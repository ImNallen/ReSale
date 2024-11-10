using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReSale.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddActivity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.CreateTable(
            name: "Activities",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Activities", x => x.Id));

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "Activities",
            schema: "public");
}
