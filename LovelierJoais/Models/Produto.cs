using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovelierJoais.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage ="O nome do produto deve ser informado")]
        [Display(Name = "Nome do Produto")]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto deve ser informada")]
        [Display(Name = "Descrição do produto")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição não pode exceder {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "O descrição detalhada do produto deve ser informada")]
        [Display(Name = "Descrição detalhada do produto")]
        [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada não pode exceder {1} caracteres")]
        public string  DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre 1 e 999,99")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho da Imagem")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Destaque?")]
        public bool Destaque { get; set; }

        [Display(Name = "Promoção?")]
        public bool Promocao { get; set; }

        [Required(ErrorMessage = "Informe a quantidade do produto que tem em estoque")]        
        public int Estoque { get; set; }

        public int CategoriaId { get; set; }

        public int SubCategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }
    }
}
