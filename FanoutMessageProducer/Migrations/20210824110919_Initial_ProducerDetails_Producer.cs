using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FanoutMessageProducer.Migrations
{
    public partial class Initial_ProducerDetails_Producer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProducerDetails_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProducerName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ProducerMessage = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducerDetails_Table", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProducerDetails_Table");
        }
    }
}
