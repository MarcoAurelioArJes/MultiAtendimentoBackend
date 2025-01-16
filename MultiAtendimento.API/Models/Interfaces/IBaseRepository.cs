namespace MultiAtendimento.API.Models.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Criar(T objeto);
        T Atualizar(T objeto);
        T ObterPorId(int id);
        List<T> ObterTodosPorCnpjDaEmpresa(string cnpjDaEmpresa);
        T Remover(T objeto);
    }
}
