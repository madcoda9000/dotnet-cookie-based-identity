using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable 1591

namespace DotNetIdentity.Data.Migrations
{
    public partial class SessionCache_Mod1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "AppSessionCache",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "a79fea86-2a4c-4b00-8bdf-be7ba146f432");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "4457bd66-a98a-45a3-b5ac-113432c10318");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "15f61f78-eb42-4bf9-a25d-ab98776500df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "5557772c-a953-4663-9823-775045c328b3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8bd09316-769d-4e3c-b8e7-546d4c10328d", "AQAAAAEAACcQAAAAEMhv/Srsl/U0ePBs8x0NZL82Ff4FsfGJ1gYbBRimD/vFDDC7hVLyH2tU80l+0ySgxg==", "4a76ef23-70b7-43bd-adf9-2f9280056169" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "AppSessionCache",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
    }
}
#pragma warning restore 1591 