using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedsomefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "paymentFrequency",
                table: "ApplyForLoans",
                newName: "PaymentFrequency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentFrequency",
                table: "ApplyForLoans",
                newName: "paymentFrequency");
        }
    }
}
