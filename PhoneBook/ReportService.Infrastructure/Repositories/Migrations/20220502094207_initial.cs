using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportService.Infrastructure.Repositories.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reportdb");

            migrationBuilder.CreateTable(
                name: "reports",
                schema: "reportdb",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    requestdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reportdetails",
                schema: "reportdb",
                columns: table => new
                {
                    reportid = table.Column<Guid>(type: "uuid", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    peoplecount = table.Column<int>(type: "integer", nullable: false),
                    phonenumbercount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportdetails", x => x.reportid);
                    table.ForeignKey(
                        name: "FK_reportdetails_reports_reportid",
                        column: x => x.reportid,
                        principalSchema: "reportdb",
                        principalTable: "reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reportdetails",
                schema: "reportdb");

            migrationBuilder.DropTable(
                name: "reports",
                schema: "reportdb");
        }
    }
}
