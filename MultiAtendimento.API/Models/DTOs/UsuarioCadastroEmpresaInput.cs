using MultiAtendimento.API.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiAtendimento.API.Models.DTOs
{
    public class UsuarioCadastroEmpresaInput
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public CargoEnum Cargo { get; set; }
        public bool AdministradorPrincipal { get; set; }
        public int? SetorId { get; set; }
        public string EmpresaCnpj { get; set; }
    }
}
