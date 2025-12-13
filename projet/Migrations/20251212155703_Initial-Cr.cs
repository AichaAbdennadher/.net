using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lignesMedicaments_medicaments_MedicamentID",
                table: "lignesMedicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_lignesMedicaments_ordonnances_ordID",
                table: "lignesMedicaments");

            migrationBuilder.DropIndex(
                name: "IX_lignesMedicaments_ordID",
                table: "lignesMedicaments");

            migrationBuilder.AlterColumn<Guid>(
                name: "PharmacienID",
                table: "ordonnances",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "qteDelivre",
                table: "lignesMedicaments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_patients_CIN",
                table: "patients",
                column: "CIN",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_lignesMedicaments_medicaments_MedicamentID",
                table: "lignesMedicaments",
                column: "MedicamentID",
                principalTable: "medicaments",
                principalColumn: "MedicamentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lignesMedicaments_medicaments_MedicamentID",
                table: "lignesMedicaments");

            migrationBuilder.DropIndex(
                name: "IX_patients_CIN",
                table: "patients");

            migrationBuilder.AlterColumn<Guid>(
                name: "PharmacienID",
                table: "ordonnances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "qteDelivre",
                table: "lignesMedicaments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_lignesMedicaments_ordID",
                table: "lignesMedicaments",
                column: "ordID");

            migrationBuilder.AddForeignKey(
                name: "FK_lignesMedicaments_medicaments_MedicamentID",
                table: "lignesMedicaments",
                column: "MedicamentID",
                principalTable: "medicaments",
                principalColumn: "MedicamentID");

            migrationBuilder.AddForeignKey(
                name: "FK_lignesMedicaments_ordonnances_ordID",
                table: "lignesMedicaments",
                column: "ordID",
                principalTable: "ordonnances",
                principalColumn: "OrdID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
