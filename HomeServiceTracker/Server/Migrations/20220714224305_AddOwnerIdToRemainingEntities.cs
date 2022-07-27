using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeServiceTracker.Server.Migrations
{
    public partial class AddOwnerIdToRemainingEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ServiceProviders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ServiceItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ScheduledServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ServiceItems");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ScheduledServices");
        }
    }
}
