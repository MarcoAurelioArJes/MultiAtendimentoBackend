using System.ComponentModel.DataAnnotations;

namespace MultiAtendimento.API.Models.DTOs
{
    public class CadastroEmpresaInput
    {
        [Required(ErrorMessage = "NomeEmpresa é um campo obrigatório")]
        public string NomeEmpresa { get; set; }
        [Required(ErrorMessage = "Cnpj é um campo obrigatório")]
        public string Cnpj { get; set; }
        [Required(ErrorMessage = "NomeUsuario é um campo obrigatório")]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "Email é um campo obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é um campo obrigatório")]
        public string Senha { get; set; }
        [Compare(nameof(Senha), ErrorMessage = "As senhas informadas são diferentes")]
        public string CompararSenha { get; set; }
    }
}
