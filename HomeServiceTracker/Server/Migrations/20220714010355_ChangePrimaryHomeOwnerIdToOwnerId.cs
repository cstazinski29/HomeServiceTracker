using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeServiceTracker.Server.Migrations
{
    public partial class ChangePrimaryHomeOwnerIdToOwnerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrimaryHomeownerId",
                table: "HomeInfo",
                newName: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "HomeInfo",
                newName: "PrimaryHomeownerId");
        }
    }
}
