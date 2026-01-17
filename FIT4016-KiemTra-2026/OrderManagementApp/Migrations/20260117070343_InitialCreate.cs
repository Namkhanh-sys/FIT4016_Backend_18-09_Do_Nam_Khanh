using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock_quantity = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    order_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    customer_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    customer_email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    delivery_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category", "created_at", "description", "name", "price", "sku", "stock_quantity", "updated_at" },
                values: new object[,]
                {
                    { 1, "Furniture", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 1.", "High-end Product 1", 115.50m, "SKU-2026001", 101, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Accessories", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 2.", "High-end Product 2", 130.50m, "SKU-2026002", 102, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Electronics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 3.", "High-end Product 3", 145.50m, "SKU-2026003", 103, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Furniture", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 4.", "High-end Product 4", 160.50m, "SKU-2026004", 104, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Accessories", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 5.", "High-end Product 5", 175.50m, "SKU-2026005", 105, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Electronics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 6.", "High-end Product 6", 190.50m, "SKU-2026006", 106, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Furniture", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 7.", "High-end Product 7", 205.50m, "SKU-2026007", 107, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Accessories", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 8.", "High-end Product 8", 220.50m, "SKU-2026008", 108, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Electronics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 9.", "High-end Product 9", 235.50m, "SKU-2026009", 109, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Furniture", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 10.", "High-end Product 10", 250.50m, "SKU-2026010", 110, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "Accessories", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 11.", "High-end Product 11", 265.50m, "SKU-2026011", 111, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Electronics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 12.", "High-end Product 12", 280.50m, "SKU-2026012", 112, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "Furniture", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 13.", "High-end Product 13", 295.50m, "SKU-2026013", 113, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "Accessories", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 14.", "High-end Product 14", 310.50m, "SKU-2026014", 114, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "Electronics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the official description for product item number 15.", "High-end Product 15", 325.50m, "SKU-2026015", 115, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "id", "created_at", "customer_email", "customer_name", "delivery_date", "order_date", "order_number", "product_id", "quantity", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client1@ordermail.com", "Customer User 1", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1001", 1, 2, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client2@ordermail.com", "Customer User 2", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1002", 2, 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client3@ordermail.com", "Customer User 3", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1003", 3, 4, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client4@ordermail.com", "Customer User 4", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1004", 4, 1, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client5@ordermail.com", "Customer User 5", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1005", 5, 2, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client6@ordermail.com", "Customer User 6", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1006", 6, 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client7@ordermail.com", "Customer User 7", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1007", 7, 4, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client8@ordermail.com", "Customer User 8", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1008", 8, 1, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client9@ordermail.com", "Customer User 9", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1009", 9, 2, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client10@ordermail.com", "Customer User 10", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1010", 10, 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client11@ordermail.com", "Customer User 11", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1011", 11, 4, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client12@ordermail.com", "Customer User 12", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1012", 12, 1, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client13@ordermail.com", "Customer User 13", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1013", 13, 2, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client14@ordermail.com", "Customer User 14", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1014", 14, 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client15@ordermail.com", "Customer User 15", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1015", 15, 4, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client16@ordermail.com", "Customer User 16", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1016", 1, 1, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client17@ordermail.com", "Customer User 17", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1017", 2, 2, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client18@ordermail.com", "Customer User 18", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1018", 3, 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client19@ordermail.com", "Customer User 19", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1019", 4, 4, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client20@ordermail.com", "Customer User 20", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1020", 5, 1, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client21@ordermail.com", "Customer User 21", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1021", 6, 2, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client22@ordermail.com", "Customer User 22", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1022", 7, 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client23@ordermail.com", "Customer User 23", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1023", 8, 4, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client24@ordermail.com", "Customer User 24", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1024", 9, 1, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client25@ordermail.com", "Customer User 25", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1025", 10, 2, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client26@ordermail.com", "Customer User 26", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1026", 11, 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client27@ordermail.com", "Customer User 27", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1027", 12, 4, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client28@ordermail.com", "Customer User 28", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1028", 13, 1, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client29@ordermail.com", "Customer User 29", null, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1029", 14, 2, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.client30@ordermail.com", "Customer User 30", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORD-20260117-1030", 15, 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_email",
                table: "orders",
                column: "customer_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_order_number",
                table: "orders",
                column: "order_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_product_id",
                table: "orders",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_name",
                table: "products",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_sku",
                table: "products",
                column: "sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
