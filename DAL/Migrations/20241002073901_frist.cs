using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class frist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department_TBL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department_TBL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees_TBL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees_TBL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_TBL_Department_TBL_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department_TBL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_TBL_Employees_TBL_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees_TBL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_TBL_ManagerId",
                table: "Department_TBL",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TBL_DepartmentId",
                table: "Employees_TBL",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TBL_ManagerId",
                table: "Employees_TBL",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_TBL_Employees_TBL_ManagerId",
                table: "Department_TBL",
                column: "ManagerId",
                principalTable: "Employees_TBL",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_TBL_Employees_TBL_ManagerId",
                table: "Department_TBL");

            migrationBuilder.DropTable(
                name: "Employees_TBL");

            migrationBuilder.DropTable(
                name: "Department_TBL");
        }
    }
}
