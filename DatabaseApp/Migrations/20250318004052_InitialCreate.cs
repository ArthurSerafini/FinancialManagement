using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseConstructor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentInfos",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    TotalInstallments = table.Column<int>(type: "INTEGER", nullable: false),
                    InstallmentsPaid = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPrice = table.Column<double>(type: "REAL", nullable: false),
                    PricePaid = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfos", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentsTable",
                columns: table => new
                {
                    InstallmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    PaidValue = table.Column<float>(type: "REAL", nullable: false),
                    Paid = table.Column<bool>(type: "INTEGER", nullable: false),
                    PaymentId = table.Column<int>(type: "INTEGER", nullable: false),
                    paymentInfoPaymentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentsTable", x => x.InstallmentId);
                    table.ForeignKey(
                        name: "FK_InstallmentsTable_PaymentInfos_paymentInfoPaymentId",
                        column: x => x.paymentInfoPaymentId,
                        principalTable: "PaymentInfos",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentsTable_paymentInfoPaymentId",
                table: "InstallmentsTable",
                column: "paymentInfoPaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallmentsTable");

            migrationBuilder.DropTable(
                name: "PaymentInfos");
        }
    }
}
