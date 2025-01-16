using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository.BancoDeDados;

namespace MultiAtendimento.API.Repository
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ContextoDoBancoDeDados contextoDoBancoDeDados) : base(contextoDoBancoDeDados)
        {
        }
    }
}
