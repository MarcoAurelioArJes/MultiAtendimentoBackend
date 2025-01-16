using System.ComponentModel.DataAnnotations;

namespace MultiAtendimento.API.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime DataCadastro { get; } = DateTime.Now;
    }
}
