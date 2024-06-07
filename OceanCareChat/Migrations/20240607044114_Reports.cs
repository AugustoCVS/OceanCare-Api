using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OceanCareChat.Migrations
{
    /// <inheritdoc />
    public partial class Reports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TrashType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TrashLocation = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TrashDescription = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    OceanUserId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_OceanUser_OceanUserId",
                        column: x => x.OceanUserId,
                        principalTable: "OceanUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_OceanUserId",
                table: "Reports",
                column: "OceanUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
