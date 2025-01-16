using Microsoft.EntityFrameworkCore;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository.BancoDeDados;

namespace MultiAtendimento.API.Repository
{
    public class SetorRepository : BaseRepository<Setor>, ISetorRepository
    {
        public SetorRepository(ContextoDoBancoDeDados contextoDoBancoDeDados) 
            : base(contextoDoBancoDeDados)
        {
        }
    }
}
