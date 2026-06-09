using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTIC.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLogAuditoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogAuditoria",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ActionType = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAuditoria", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogAuditoria");
        }
    }
}
