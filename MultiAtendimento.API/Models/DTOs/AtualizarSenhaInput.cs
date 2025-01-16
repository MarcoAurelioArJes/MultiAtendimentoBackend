using System.ComponentModel.DataAnnotations;

namespace MultiAtendimento.API.Models.DTOs
{
    public class AtualizarSenhaInput
    {
        public string Senha { get; set; }
        [Compare(nameof(Senha), ErrorMessage = "As senhas informadas são diferentes")]
        public string CompararSenha { get; set; }
    }
}