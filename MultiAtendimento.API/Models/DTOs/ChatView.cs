using MultiAtendimento.API.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiAtendimento.API.Models.DTOs
{
    public class ChatView
    {
        public int Id { get; set; }
        public StatusDoChatEnum Status { get; set; }

        public string EmpresaCnpj { get; set; }
        public int? AtendenteId { get; set; }
        public int SetorId { get; set; }
        public int ClienteId { get; set; }

        public List<MensagemView> Mensagens { get; set; }
        public ClienteView Cliente { get; set; }
    }
}
