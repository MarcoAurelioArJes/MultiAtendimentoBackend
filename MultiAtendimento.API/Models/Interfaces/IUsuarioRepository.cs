namespace MultiAtendimento.API.Models.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario ObterPorEmail(string email);
    }


}
