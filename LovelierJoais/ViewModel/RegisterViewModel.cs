using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LovelierJoais.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = "Informe o nome completo")]
        [Display(Name = "Nome Completo")]
        [MinLength(8)]
        [MaxLength(60)]
        public string nomeCompleto { get; set; }

        [Required(ErrorMessage = "Informe o CPF")]
        [StringLength(11)]
        public string cpf { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento")]
        public DateTime dataNascimento { get; set; }
        [Required(ErrorMessage = "Informe o DDD")]
        [StringLength(2)]
        public string ddd { get; set; }
        [Required(ErrorMessage = "Informe o telefone")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        public string cep { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        public string endereco { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        public string bairro { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        public string cidade { get; set; }

        public string complemento { get; set; }
    }
}
