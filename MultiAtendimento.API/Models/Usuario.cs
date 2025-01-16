using MultiAtendimento.API.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiAtendimento.API.Models
{
    public class Usuario : BaseModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public CargoEnum Cargo { get; set; }
        public bool AdministradorPrincipal { get; set; }

        [ForeignKey(nameof(Setor))]
        public int? SetorId { get; set; }

        [ForeignKey(nameof(Empresa))]
        public string EmpresaCnpj { get; set; }

        public Setor? Setor { get; set; }
    }
}
