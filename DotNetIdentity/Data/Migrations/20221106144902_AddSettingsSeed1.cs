using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable 1591

namespace DotNetIdentity.Data.Migrations
{    
    public partial class AddSettingsSeed1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Name", "Type", "Value" },
                values: new object[,]
                {
                    { "LdapBaseDn", "LdapSettings", "DC=YOUR,DC=Domain,DC=com" },
                    { "LdapDomainController", "LdapSettings", "YOUR_Domaincontroller_FQDN" },
                    { "LdapDomainName", "LdapSettings", "YOUR_Domainname" },
                    { "LdapEnabled", "LdapSettings", "true" },
                    { "LdapGroup", "LdapSettings", "YOUR_Ldap_Group" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "4055a78d-dabc-435f-bd4d-4d6c7993bbe8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "51cc2d45-f378-442d-bda7-32f1901cc1b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "05d0a390-7945-4758-a811-7a98b5b36fbc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "4db6f761-1658-4f3e-b1bd-57a001b1fa8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db4123c5-2bb9-4a6c-bc11-8e41f472a3cd", "AQAAAAEAACcQAAAAEPVaj06s+VHLDthZsu27xIrQsFUHebsnoeN3e8ExT102TIF+vUa+Zlbj4g7EUl09vQ==", "1cfee861-b100-4e49-a10b-72cb02cde9d7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "LdapBaseDn", "LdapSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "LdapDomainController", "LdapSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "LdapDomainName", "LdapSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "LdapEnabled", "LdapSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "LdapGroup", "LdapSettings" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "fcca32ee-4cee-445f-8be0-92375f7a12d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "6594291f-d352-4269-855b-8abc0ab18e01");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "99b2b129-74d0-40ec-8892-e679d560c74f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "07f2c15c-a1a3-4840-87a6-645446d15f0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bd8f427-6b8a-444c-b34e-ba42537137da", "AQAAAAEAACcQAAAAEImvoKdoqh4RAms6fLIxbS8vshGY7MoW37zCdI/DIKK/br9dE33l7dugU7bxT4YiTQ==", "9188049f-056e-45f0-bb3b-706ce2102631" });
        }
    }
    #pragma warning restore 1591
}
