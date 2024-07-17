using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReSale.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_ZipCode",
                schema: "public",
                table: "Customers",
                newName: "ShippingAddress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                schema: "public",
                table: "Customers",
                newName: "ShippingAddress_Street");

            migrationBuilder.RenameColumn(
                name: "Address_State",
                schema: "public",
                table: "Customers",
                newName: "ShippingAddress_State");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                schema: "public",
                table: "Customers",
                newName: "ShippingAddress_Country");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                schema: "public",
                table: "Customers",
                newName: "ShippingAddress_City");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_City",
                schema: "public",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Country",
                schema: "public",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_State",
                schema: "public",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Street",
                schema: "public",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_ZipCode",
                schema: "public",
                table: "Customers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress_City",
                schema: "public",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Country",
                schema: "public",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_State",
                schema: "public",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Street",
                schema: "public",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_ZipCode",
                schema: "public",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_ZipCode",
                schema: "public",
                table: "Customers",
                newName: "Address_ZipCode");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Street",
                schema: "public",
                table: "Customers",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_State",
                schema: "public",
                table: "Customers",
                newName: "Address_State");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Country",
                schema: "public",
                table: "Customers",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_City",
                schema: "public",
                table: "Customers",
                newName: "Address_City");
        }
    }
}
