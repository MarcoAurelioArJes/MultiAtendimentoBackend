using MultiAtendimento.API.Extensions;
using MultiAtendimento.API.Models.Enums;

namespace MultiAtendimento.API.Models.DTOs
{
    public class CargosView
    {
        public CargoEnum Codigo { get; set; }
        public string Nome => Codigo.ObterDescricao();
    }
}