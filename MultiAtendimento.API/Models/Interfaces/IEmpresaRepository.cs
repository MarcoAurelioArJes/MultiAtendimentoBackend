namespace MultiAtendimento.API.Models.Interfaces
{
    public interface IEmpresaRepository
    {
        Empresa Criar(Empresa objeto);
        public Empresa ObterEmpresaPorCnpj(string cnpj);
    }
}
