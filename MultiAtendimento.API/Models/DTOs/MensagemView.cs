using MultiAtendimento.API.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiAtendimento.API.Models.DTOs
{
    public class MensagemView
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Conteudo { get; set; }
        public CargoEnum Remetente { get; set; }
    }
}
