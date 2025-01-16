using MultiAtendimento.API.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiAtendimento.API.Models
{
    public class Mensagem : BaseModel
    {
        public string Conteudo { get; set; }
        public CargoEnum Remetente { get; set; }

        [ForeignKey(nameof(Empresa))]
        public string EmpresaCnpj { get; set; }
        [ForeignKey(nameof(Chat))]
        public int ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
