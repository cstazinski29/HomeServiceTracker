using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeServiceTracker.Server.Migrations
{
    public partial class AddForeignKeyReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduledServiceDate",
                table: "ScheduledServices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledServices_HomeId",
                table: "ScheduledServices",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledServices_ServiceItemId",
                table: "ScheduledServices",
                column: "ServiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledServices_ServiceProviderId",
                table: "ScheduledServices",
                column: "ServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledServices_HomeInfo_HomeId",
                table: "ScheduledServices",
                column: "HomeId",
                principalTable: "HomeInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledServices_ServiceItems_ServiceItemId",
                table: "ScheduledServices",
                column: "ServiceItemId",
                principalTable: "ServiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledServices_ServiceProviders_ServiceProviderId",
                table: "ScheduledServices",
                column: "ServiceProviderId",
                principalTable: "ServiceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledServices_HomeInfo_HomeId",
                table: "ScheduledServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledServices_ServiceItems_ServiceItemId",
                table: "ScheduledServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledServices_ServiceProviders_ServiceProviderId",
                table: "ScheduledServices");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledServices_HomeId",
                table: "ScheduledServices");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledServices_ServiceItemId",
                table: "ScheduledServices");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledServices_ServiceProviderId",
                table: "ScheduledServices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduledServiceDate",
                table: "ScheduledServices",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
