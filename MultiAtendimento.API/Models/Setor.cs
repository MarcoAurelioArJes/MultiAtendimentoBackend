using System.ComponentModel.DataAnnotations.Schema;

namespace MultiAtendimento.API.Models
{
    public class Setor : BaseModel
    {
        public string Nome { get; set; }
        [ForeignKey(nameof(Empresa))]
        public string EmpresaCnpj { get; set; }
    }
}
