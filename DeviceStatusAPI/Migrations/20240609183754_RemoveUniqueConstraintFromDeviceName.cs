using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeviceStatusAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueConstraintFromDeviceName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceName",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceName",
                table: "Devices",
                column: "DeviceName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceName",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceName",
                table: "Devices",
                column: "DeviceName",
                unique: true);
        }
    }
}
