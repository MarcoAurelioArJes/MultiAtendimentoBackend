using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository.BancoDeDados;

namespace MultiAtendimento.API.Repository
{
    public class MensagemRepository : BaseRepository<Mensagem>, IMensagemRepository
    {
        public MensagemRepository(ContextoDoBancoDeDados contextoDoBancoDeDados) : base(contextoDoBancoDeDados)
        {
        }
    }
}
