using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetIdentity.Data.Migrations
{
    public partial class Mod4_AppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "598ca16e-0574-412a-9f34-77b5d05bd791");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "730d595e-5e15-4ca4-98c7-bf79742e3113");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "09de2a9e-f67a-401d-8aae-92dfaf97b149");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "8eecfc4d-cfea-4462-aadd-b2b388e47e90");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89527f8a-d126-47f4-a445-eb0682654f1b", "AQAAAAEAACcQAAAAEG554fUZI4QFe/3z82voBKL/I69LsaVOAyGp3732y0ZowihOfYBfOWdAGLCdqqHgMw==", "9590bb65-2ed5-4cce-946f-639ea6d2ac13" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "83d2bb70-0176-4222-b47a-4e5d9dc1eeae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "e9c2db0d-362a-4e2f-b9d3-529618e59514");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "fbc3c1ed-0e92-427a-804d-aca92ba5061d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "05d474b3-d81e-4276-99c9-88383cb65801");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0f693c3-c3db-4a95-b3ce-4387fc4de225", "AQAAAAEAACcQAAAAEMfKD7wD5TwksQAM9AVvaDmG/+ufcuEKtkXLM+pxPVi1zRop5xF860aiQxQRJpd2lQ==", "c8b47936-9064-4762-8faa-ddf4d74f45f1" });
        }
    }
}
