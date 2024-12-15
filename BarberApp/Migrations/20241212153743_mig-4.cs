using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberApp.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Admins_AdminID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Barbers_Admins_AdminID",
                table: "Barbers");

            migrationBuilder.DropForeignKey(
                name: "FK_Expanses_Admins_AdminID",
                table: "Expanses");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Admins_AdminID",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "Services",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "Expanses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "Barbers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Admins_AdminID",
                table: "Appointments",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Barbers_Admins_AdminID",
                table: "Barbers",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expanses_Admins_AdminID",
                table: "Expanses",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Admins_AdminID",
                table: "Services",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Admins_AdminID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Barbers_Admins_AdminID",
                table: "Barbers");

            migrationBuilder.DropForeignKey(
                name: "FK_Expanses_Admins_AdminID",
                table: "Expanses");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Admins_AdminID",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "Services",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "Expanses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "Barbers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "Appointments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Admins_AdminID",
                table: "Appointments",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_Barbers_Admins_AdminID",
                table: "Barbers",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_Expanses_Admins_AdminID",
                table: "Expanses",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Admins_AdminID",
                table: "Services",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID");
        }
    }
}
