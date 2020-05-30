using Microsoft.EntityFrameworkCore.Migrations;

namespace PayMeDAL.Migrations
{
    public partial class uwu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31e014ca-8de1-474a-9315-825e0ea9fda6");

            migrationBuilder.AlterColumn<decimal>(
                name: "Money",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Money", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UniqueCode", "UserName" },
                values: new object[] { "4da5660c-1ec3-43b1-a56f-aea3175a283b", 0, "d8396810-9848-4460-b04d-3242043625e1", "a@a.com", false, null, null, false, null, 0m, "A@A.COM", "ADMIN", "AQAAAAEAACcQAAAAEOgAIvm87ek6D5Q0RnuCxCAMYhfNNjijKxmLus66OlKiO3jMNDxI0mMd7qvLSGYj5w==", null, false, "446459d8-1157-40fe-a7ff-045a2ea1267b", false, "AZERTY", "a@a.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4da5660c-1ec3-43b1-a56f-aea3175a283b");

            migrationBuilder.AlterColumn<double>(
                name: "Money",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Money", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UniqueCode", "UserName" },
                values: new object[] { "31e014ca-8de1-474a-9315-825e0ea9fda6", 0, "308eeb65-30a4-4070-8645-2be777d2d76e", "a@a.com", false, null, null, false, null, 0.0, "A@A.COM", "ADMIN", "AQAAAAEAACcQAAAAELuDq9gTqgnffJAR0FfWAG7fBKvCI8xe0dj9biQ5dhX0HXlrrJmO6m4b03Zg6A7pXg==", null, false, "4879d1b6-56e0-4615-b207-9f9d094dcbe3", false, "AZERTY", "a@a.com" });
        }
    }
}
