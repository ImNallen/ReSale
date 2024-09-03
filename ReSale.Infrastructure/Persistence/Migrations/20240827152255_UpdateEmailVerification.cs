using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReSale.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmailVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationToken",
                schema: "public",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailVerificationToken",
                schema: "public",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerificationToken",
                schema: "public",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "VerificationToken",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: string.Empty);
        }
    }
}
