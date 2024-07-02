using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReSale.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_User_IdentityId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "public",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityId",
                schema: "public",
                table: "Users",
                column: "IdentityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_IdentityId",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                schema: "public",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "public",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
