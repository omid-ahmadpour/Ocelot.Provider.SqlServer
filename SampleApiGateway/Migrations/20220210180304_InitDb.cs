using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleApiGateway.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OcelotGlobalConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatewayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RequestIdKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BaseUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DownstreamScheme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ServiceDiscoveryProvider = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    QoSOptions = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LoadBalancerOptions = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    HttpHandlerOptions = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcelotGlobalConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OcelotRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcelotRoutes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OcelotGlobalConfigurations");

            migrationBuilder.DropTable(
                name: "OcelotRoutes");
        }
    }
}
