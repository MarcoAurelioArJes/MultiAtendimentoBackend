using System.ComponentModel.DataAnnotations;

namespace MultiAtendimento.API.Models
{
    public class Empresa
    {
        [Key]
        [MaxLength(14)]
        public string Cnpj { get; set; }
        public string Nome { get; set; }
    }
}
