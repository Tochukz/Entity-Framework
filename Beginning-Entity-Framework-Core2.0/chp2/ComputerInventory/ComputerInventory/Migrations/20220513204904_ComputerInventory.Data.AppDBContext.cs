using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComputerInventory.Migrations
{
    public partial class ComputerInventoryDataAppDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachienyType",
                columns: table => new
                {
                    MachineTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachienyType", x => x.MachineTypeID);
                });

            migrationBuilder.CreateTable(
                name: "OperatingSys",
                columns: table => new
                {
                    OperatingSysID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: false),
                    StillSupported = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSys", x => x.OperatingSysID);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyPrivider",
                columns: table => new
                {
                    WarrantyProviderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SupportExtension = table.Column<int>(type: "int", nullable: true),
                    SupportNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyPrivider", x => x.WarrantyProviderID);
                });

            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    MachineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    GeneralRole = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    InstalledRoles = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    OperatingSysID = table.Column<int>(type: "int", nullable: false),
                    MachineTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.MachineID);
                    table.ForeignKey(
                        name: "FK_MachineType",
                        column: x => x.MachineTypeID,
                        principalTable: "MachienyType",
                        principalColumn: "MachineTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OperatingSys",
                        column: x => x.OperatingSysID,
                        principalTable: "OperatingSys",
                        principalColumn: "OperatingSysID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MachineWarranty",
                columns: table => new
                {
                    MachineWarrntyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTag = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    WarrantyExpiration = table.Column<DateTime>(type: "date", nullable: false),
                    MachineID = table.Column<int>(type: "int", nullable: false),
                    WarrantyProviderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineWarranty", x => x.MachineWarrntyID);
                    table.ForeignKey(
                        name: "FK_WarrantyProvider",
                        column: x => x.WarrantyProviderID,
                        principalTable: "WarrantyPrivider",
                        principalColumn: "WarrantyProviderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportTicket",
                columns: table => new
                {
                    SupportTicketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateReported = table.Column<DateTime>(type: "date", nullable: false),
                    DateResolved = table.Column<DateTime>(type: "date", nullable: true),
                    IssueDescription = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    IssueDetails = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    TicketOpenedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MachineID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicket", x => x.SupportTicketID);
                    table.ForeignKey(
                        name: "FK_Machine",
                        column: x => x.MachineID,
                        principalTable: "Machine",
                        principalColumn: "MachineID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportLog",
                columns: table => new
                {
                    SupportLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupportLogEntryDate = table.Column<DateTime>(type: "date", nullable: false),
                    SupportLogEntry = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    SupportLogUpdatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SupportTicketID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportLog", x => x.SupportLogID);
                    table.ForeignKey(
                        name: "FK_SupportTicket",
                        column: x => x.SupportTicketID,
                        principalTable: "SupportTicket",
                        principalColumn: "SupportTicketID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Machine_MachineTypeID",
                table: "Machine",
                column: "MachineTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Machine_OperatingSysID",
                table: "Machine",
                column: "OperatingSysID");

            migrationBuilder.CreateIndex(
                name: "IX_MachineWarranty_WarrantyProviderID",
                table: "MachineWarranty",
                column: "WarrantyProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportLog_SupportTicketID",
                table: "SupportLog",
                column: "SupportTicketID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicket_MachineID",
                table: "SupportTicket",
                column: "MachineID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineWarranty");

            migrationBuilder.DropTable(
                name: "SupportLog");

            migrationBuilder.DropTable(
                name: "WarrantyPrivider");

            migrationBuilder.DropTable(
                name: "SupportTicket");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "MachienyType");

            migrationBuilder.DropTable(
                name: "OperatingSys");
        }
    }
}
