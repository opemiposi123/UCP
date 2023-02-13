using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedsomedataannotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyForLoans_Collateral_CollateralId",
                table: "ApplyForLoans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collateral",
                table: "Collateral");

            migrationBuilder.RenameTable(
                name: "Collateral",
                newName: "Collaterals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collaterals",
                table: "Collaterals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyForLoans_Collaterals_CollateralId",
                table: "ApplyForLoans",
                column: "CollateralId",
                principalTable: "Collaterals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyForLoans_Collaterals_CollateralId",
                table: "ApplyForLoans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collaterals",
                table: "Collaterals");

            migrationBuilder.RenameTable(
                name: "Collaterals",
                newName: "Collateral");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collateral",
                table: "Collateral",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyForLoans_Collateral_CollateralId",
                table: "ApplyForLoans",
                column: "CollateralId",
                principalTable: "Collateral",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
