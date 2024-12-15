using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberApp.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminID",
                table: "Appointments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AdminID",
                table: "Appointments",
                column: "AdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Admins_AdminID",
                table: "Appointments",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Admins_AdminID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AdminID",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Appointments");
        }
    }
}
