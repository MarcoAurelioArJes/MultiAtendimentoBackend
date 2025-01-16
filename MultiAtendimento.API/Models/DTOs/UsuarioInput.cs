using MultiAtendimento.API.Models.Enums;

namespace MultiAtendimento.API.Models
{
    public class UsuarioInput
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public CargoEnum Cargo { get; set; }
        public int SetorId { get; set; }
    }
}
