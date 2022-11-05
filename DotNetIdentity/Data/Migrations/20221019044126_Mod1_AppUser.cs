using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable 1591

namespace DotNetIdentity.Data.Migrations
{
    public partial class Mod1_AppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "9a39f005-16f0-42d4-bb0d-33d5250177a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "2eab52be-daa1-4a52-aef1-322c41a5cd6f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "60b81ca7-84d3-403f-9fa2-4312d4dd8167");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "e5e19bf7-09fe-4101-bb4a-0f9cdb206da4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "Department", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17455578-a93e-4338-8e2d-091a1932702d", "", "AQAAAAEAACcQAAAAEEbierr9P0D8F/KxxDwS8DXQXrH7gWHHz9WDdOjGCHpSffhlfiAS0/82FKReADlA0g==", "44c2e14c-f74d-4448-992d-cfc8f9b09cec" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "02777964-1879-4805-98d5-498b6078cf3c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "c984a790-efc9-49c5-b3c2-30213ec5f615");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "b2236f39-1070-4476-87d3-abe32eb16fa8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "c0c408e6-b279-438e-9de0-8acd866daa03");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b468dc01-1c53-48bf-882e-cf27afdc7640", "AQAAAAEAACcQAAAAEJClOm3r29w6Eo+tBCaRuTfIFU4j3cN00LeSogD5cvzidE7tJLGqFVxGmBOgkVXnbg==", "76c0693e-e62c-4465-bc84-f2759e34e133" });
        }
    }
}
#pragma warning restore 1591 