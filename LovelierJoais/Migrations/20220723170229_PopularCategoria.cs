using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovelierJoais.Migrations
{
    public partial class PopularCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, Descricao, Link)" +
                                    "VALUES('Prata', 'Joias feita com prata', 'false')");

            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, Descricao, Link)" +
                                   "VALUES('Folheado', 'Joias folheadas a ouro', 'false')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");

        }
    }
}
