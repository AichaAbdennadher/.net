using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCrea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordonnances_patients_PatientID",
                table: "ordonnances");

            migrationBuilder.AddForeignKey(
                name: "FK_ordonnances_patients_PatientID",
                table: "ordonnances",
                column: "PatientID",
                principalTable: "patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordonnances_patients_PatientID",
                table: "ordonnances");

            migrationBuilder.AddForeignKey(
                name: "FK_ordonnances_patients_PatientID",
                table: "ordonnances",
                column: "PatientID",
                principalTable: "patients",
                principalColumn: "PatientID");
        }
    }
}
