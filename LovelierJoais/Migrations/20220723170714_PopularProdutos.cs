using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovelierJoais.Migrations
{
    public partial class PopularProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId, SubcategoriaId,DescricaoCurta,DescricaoDetalhada,Estoque,ImagemUrl,Promocao,Destaque,Nome,Preco) VALUES(1, 1,'Anel 1011. Folhas vazadas. Garantia de 3 meses no banho.','Anel 1011. Folhas vazadas. Garantia de 3 meses no banho.Anel 1011. Folhas vazadas. Garantia de 3 meses no banho.Anel 1011. Folhas vazadas. Garantia de 3 meses no banho.',10, 'https://images.yampi.me/assets/stores/oculosnow/uploads/images/anel-noivado-na-lua-em-prata-925-esterlina-regulavel-60a57954acca9-medium.png', 0 , 0,'Anel Noivado', 129.90)");
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId, SubcategoriaId,DescricaoCurta,DescricaoDetalhada,Estoque,ImagemUrl,Promocao,Destaque,Nome,Preco) VALUES(1, 1,'ANEL RAINHA EM PRATA 925 ESTERLINA - REGULÁVEL','ANEL RAINHA EM PRATA 925 ESTERLINA - REGULÁVELANEL RAINHA EM PRATA 925 ESTERLINA - REGULÁVELANEL RAINHA EM PRATA 925 ESTERLINA - REGULÁVELANEL RAINHA EM PRATA 925 ESTERLINA - REGULÁVEL',10, 'https://images.yampi.me/assets/stores/oculosnow/uploads/images/anel-rainha-em-prata-925-esterlina-regulavel-60a57a6b7193b-medium.png', 0 , 0,'ANEL RAINHA', 99.90)");
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId, SubcategoriaId,DescricaoCurta,DescricaoDetalhada,Estoque,ImagemUrl,Promocao,Destaque,Nome,Preco) VALUES(2, 2,'BRINCOS DISPARES OLHO GREGO E LUA','BRINCOS DISPARES OLHO GREGO E LUA BRINCOS DISPARES OLHO GREGO E LUA BRINCOS DISPARES OLHO GREGO E LUA',2, 'https://br.todomoda.com/brincos-dispares-olho-grego-e-lua/p', 0 , 0,'ANEL RAINHA', 99.90)");
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId, SubcategoriaId,DescricaoCurta,DescricaoDetalhada,Estoque,ImagemUrl,Promocao,Destaque,Nome,Preco) VALUES(2, 2,'KIT 3 EAR CUFF DISPARES ESMALTADO ALL IS LOVE','KIT 3 EAR CUFF DISPARES ESMALTADO ALL IS LOVEKIT 3 EAR CUFF DISPARES ESMALTADO ALL IS LOVEKIT 3 EAR CUFF DISPARES ESMALTADO ALL IS LOVE',2, 'https://todomoda.vteximg.com.br/arquivos/ids/165456-1000-1000/76802701.jpg?v=637914296614130000', 0 , 0,'ANEL RAINHA', 99.90)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Produtos");
        }
    }
}
