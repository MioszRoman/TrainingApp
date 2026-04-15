using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trening_App.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanIdToHistoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdPlanu",
                table: "HistoriaTreningow",
                newName: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaTreningow_PlanId",
                table: "HistoriaTreningow",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoriaTreningow_Plany_PlanId",
                table: "HistoriaTreningow",
                column: "PlanId",
                principalTable: "Plany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoriaTreningow_Plany_PlanId",
                table: "HistoriaTreningow");

            migrationBuilder.DropIndex(
                name: "IX_HistoriaTreningow_PlanId",
                table: "HistoriaTreningow");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "HistoriaTreningow",
                newName: "IdPlanu");
        }
    }
}
