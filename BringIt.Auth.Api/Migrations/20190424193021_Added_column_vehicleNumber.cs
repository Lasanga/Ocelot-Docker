using Microsoft.EntityFrameworkCore.Migrations;

namespace BringIt.Auth.Api.Migrations
{
    public partial class Added_column_vehicleNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehicleNumber",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleNumber",
                table: "AspNetUsers");
        }
    }
}
