using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReSale.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddPasswordReset : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "PasswordResetToken",
            schema: "public",
            table: "Users",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "PasswordResetTokenExpires",
            schema: "public",
            table: "Users",
            type: "timestamp with time zone",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "PasswordResetToken",
            schema: "public",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "PasswordResetTokenExpires",
            schema: "public",
            table: "Users");
    }
}
