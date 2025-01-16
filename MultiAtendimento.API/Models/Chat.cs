using MultiAtendimento.API.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiAtendimento.API.Models
{
    public class Chat : BaseModel
    {
        public StatusDoChatEnum Status { get; set; }

        [ForeignKey(nameof(Empresa))]
        public string EmpresaCnpj { get; set; }
        [ForeignKey(nameof(Atendente))]
        public int? AtendenteId { get; set; }
        [ForeignKey(nameof(Setor))]
        public int SetorId { get; set; }
        [ForeignKey(nameof(Cliente))]
        public int ClienteId { get; set; }

        public Usuario? Atendente { get; set; }
        public Setor? Setor { get; set; }
        public Cliente Cliente { get; set; }

        public List<Mensagem> Mensagens { get; set; } = new List<Mensagem>();
    }
}
