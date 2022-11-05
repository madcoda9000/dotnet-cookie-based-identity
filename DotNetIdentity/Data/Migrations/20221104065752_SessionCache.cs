using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable 1591

namespace DotNetIdentity.Data.Migrations
{
    public partial class SessionCache : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSessionCache",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<byte[]>(type: "longblob", nullable: true),
                    ExpiresAtTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SlidingExpirationInSeconds = table.Column<int>(type: "int", nullable: false),
                    AbsoluteExpiration = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSessionCache", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "53d5b4fc-1011-44da-835d-73f42e929a51");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "44aebf0a-bfe2-4f9d-b0e1-a903b19f6073");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "747515a6-80d6-411e-bed2-f218fea90e1e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "6c93edc2-caa7-47be-a869-07f9bec789a8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3fe94b07-a6bc-4274-b24e-3a60937c26ad", "AQAAAAEAACcQAAAAEFhn1J+zF0V/2KPZgIv9yfBNDA2E9qn2rMIBNLjsqxIWfjdUPdWfTde48RTo1X3ryA==", "c499192b-cb15-4d6f-ba2d-2742c33b12f2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSessionCache");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "3925e099-a2d3-4d89-8bfe-7ba70f5cb040");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "6d4a6a0a-f3c1-4f81-b00d-4382531940c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "619d5df4-b945-445c-80e9-1374fd8efcb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "c59f82da-57a3-407c-824b-5e302ee2ed5d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e289740f-25ea-4af7-8adf-ea5f2a685447", "AQAAAAEAACcQAAAAEOTyCG3wnT1huKQ5dB3Wn125QqDaNVsR/hbK9waxmO+d7FAy3fIcX9msY62WUgzaYQ==", "d8292556-e17a-4aed-8e8e-14d50546c4fc" });
        }
    }
}
#pragma warning restore 1591 