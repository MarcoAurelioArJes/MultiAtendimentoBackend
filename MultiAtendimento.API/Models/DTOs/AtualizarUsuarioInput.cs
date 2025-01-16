using MultiAtendimento.API.Models.Enums;

namespace MultiAtendimento.API.Models
{
    public class AtualizarUsuarioInput
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int SetorId { get; set; }
        public CargoEnum Cargo { get; set; }
    }
}
