using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SantaShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftRequest",
                columns: table => new
                {
                    ChildId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftRequest", x => new { x.ChildId, x.GiftId });
                    table.ForeignKey(
                        name: "FK_GiftRequest_Child",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiftRequest_Gift",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gifts",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("101b175e-e102-477e-ab21-c975c001c3b9"), "Barbie", 10 },
                    { new Guid("1d92ef48-9a6e-4f1f-bf3d-bfc6d12513b6"), "Mittens", 5 },
                    { new Guid("6f339fa9-da7e-4e2d-8000-337c6c7e8697"), "Cryon’s", 10 },
                    { new Guid("a4acd1b5-61e7-4dfe-bf0e-37c4659d41e6"), "Candies", 5 },
                    { new Guid("aaa4862e-edeb-4a38-93d8-18d1027fe030"), "PSP", 50 },
                    { new Guid("deb212df-a31a-4208-a8fa-cc3dc84f34b5"), "Rocket", 45 },
                    { new Guid("e18f979c-5770-4696-b33e-8010d3c2849f"), "Lego", 15 },
                    { new Guid("f3e2cc25-264c-4eff-ab71-b04f829ebbd6"), "RC Car", 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Children_Name_Age",
                table: "Children",
                columns: new[] { "Name", "Age" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiftRequest_GiftId",
                table: "GiftRequest",
                column: "GiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiftRequest");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "Gifts");
        }
    }
}
