using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovelierJoais.Migrations
{
    public partial class PopularSubcategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Subcategorias (SubcategoriaNome, Descricao, Link)" +
                                    "VALUES('Anel', 'Anel de todos tamanho', 'false')");

            migrationBuilder.Sql("INSERT INTO Subcategorias (SubcategoriaNome, Descricao, Link)" +
                                   "VALUES('Brinco', 'Brinco de todos os tipos', 'false')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Subcategorias");
        }
    }
}
