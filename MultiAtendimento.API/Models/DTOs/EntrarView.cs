using MultiAtendimento.API.Models.Enums;

namespace MultiAtendimento.API.Models.DTOs
{
    public class EntrarView
    {
        public string TokenDeAcesso { get; set; }
        public string TipoDoToken { get; set; }
        public DateTime ExpiraEm { get; set; }
        public string NomeUsuario { get; set; }
        public CargoEnum CargoUsuario { get; set; }
    }
}
