using MultiAtendimento.API.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiAtendimento.API.Models.DTOs
{
    public class UsuarioView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public CargoEnum Cargo { get; set; }
        public bool AdministradorPrincipal { get; set; }

        public SetorView? Setor { get; set; }
    }
}