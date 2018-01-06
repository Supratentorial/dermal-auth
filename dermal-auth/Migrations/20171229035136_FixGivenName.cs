using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dermal.auth.Migrations
{
    public partial class FixGivenName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiveName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "GiveName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
