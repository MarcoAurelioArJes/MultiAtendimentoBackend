using System.ComponentModel.DataAnnotations.Schema;

namespace MultiAtendimento.API.Models
{
    public class Cliente : BaseModel
    {
        public string Nome { get; set; }

        [ForeignKey(nameof(Setor))]
        public int SetorId { get; set; }
        [ForeignKey(nameof(Empresa))]
        public string EmpresaCnpj { get; set; }

        public Setor Setor { get; set; }
    }
}
