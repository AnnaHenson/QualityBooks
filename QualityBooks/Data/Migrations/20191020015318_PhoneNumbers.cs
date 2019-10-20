using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QualityBooks.Data.Migrations
{
    public partial class PhoneNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomePhone",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhone",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkPhone",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomePhone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MobilePhone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkPhone",
                table: "AspNetUsers");
        }
    }
}
