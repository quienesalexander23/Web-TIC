using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTIC.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemPermissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ModuleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ResourceName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CanRead = table.Column<bool>(type: "boolean", nullable: false),
                    CanWrite = table.Column<bool>(type: "boolean", nullable: false),
                    CanApprove = table.Column<bool>(type: "boolean", nullable: false),
                    CanDelete = table.Column<bool>(type: "boolean", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemPermissions_RoleId",
                table: "SystemPermissions",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemPermissions");
        }
    }
}
