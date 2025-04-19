﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diplom_Utkin.Migrations
{
    /// <inheritdoc />
    public partial class startPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "typeOfUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typeOfUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urisidikciiy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urisidikciiy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loggin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfUserId = table.Column<int>(type: "int", nullable: false),
                    Maney = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_typeOfUser_TypeOfUserId",
                        column: x => x.TypeOfUserId,
                        principalTable: "typeOfUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brokers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrisidikciiyId = table.Column<int>(type: "int", nullable: false),
                    NameBroker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourseFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brokers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brokers_Urisidikciiy_UrisidikciiyId",
                        column: x => x.UrisidikciiyId,
                        principalTable: "Urisidikciiy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrokersId = table.Column<int>(type: "int", nullable: false),
                    NameInvestTool = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestTools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestTools_Brokers_BrokersId",
                        column: x => x.BrokersId,
                        principalTable: "Brokers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dvizhenieSredstvs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvestToolsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Money = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvizhenieSredstvs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dvizhenieSredstvs_InvestTools_InvestToolsId",
                        column: x => x.InvestToolsId,
                        principalTable: "InvestTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dvizhenieSredstvs_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestToolHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataIzmrneniiy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvestToolsId = table.Column<int>(type: "int", nullable: false),
                    AllManey = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestToolHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestToolHistory_InvestTools_InvestToolsId",
                        column: x => x.InvestToolsId,
                        principalTable: "InvestTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestToolHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InvestToolId = table.Column<int>(type: "int", nullable: false),
                    AllManey = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => new { x.UserId, x.InvestToolId });
                    table.ForeignKey(
                        name: "FK_Portfolio_InvestTools_InvestToolId",
                        column: x => x.InvestToolId,
                        principalTable: "InvestTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portfolio_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_UrisidikciiyId",
                table: "Brokers",
                column: "UrisidikciiyId");

            migrationBuilder.CreateIndex(
                name: "IX_dvizhenieSredstvs_InvestToolsId",
                table: "dvizhenieSredstvs",
                column: "InvestToolsId");

            migrationBuilder.CreateIndex(
                name: "IX_dvizhenieSredstvs_UserId",
                table: "dvizhenieSredstvs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestToolHistory_InvestToolsId",
                table: "InvestToolHistory",
                column: "InvestToolsId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestToolHistory_UserId",
                table: "InvestToolHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestTools_BrokersId",
                table: "InvestTools",
                column: "BrokersId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_InvestToolId",
                table: "Portfolio",
                column: "InvestToolId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TypeOfUserId",
                table: "User",
                column: "TypeOfUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dvizhenieSredstvs");

            migrationBuilder.DropTable(
                name: "InvestToolHistory");

            migrationBuilder.DropTable(
                name: "Portfolio");

            migrationBuilder.DropTable(
                name: "InvestTools");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Brokers");

            migrationBuilder.DropTable(
                name: "typeOfUser");

            migrationBuilder.DropTable(
                name: "Urisidikciiy");
        }
    }
}
