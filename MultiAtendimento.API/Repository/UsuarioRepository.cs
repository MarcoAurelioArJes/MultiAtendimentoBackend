using Microsoft.EntityFrameworkCore;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository.BancoDeDados;

namespace MultiAtendimento.API.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ContextoDoBancoDeDados contextoDoBancoDeDados)
            : base(contextoDoBancoDeDados)
        {
        }

        public Usuario ObterPorId(int id)
        {
            return _dbSet
                        .Include(c => c.Empresa)
                        .Include(c => c.Setor)
                        .FirstOrDefault(c => c.Id == id);
        }

        public virtual List<Usuario> ObterTodosPorCnpjDaEmpresa(string cnpjDaEmpresa)
        {
            return _dbSet
                        .Include(c => c.Empresa)
                        .Include(c => c.Setor)
                        .Where(c => c.Empresa.Cnpj.Equals(cnpjDaEmpresa))
                        .ToList();
        }

        public Usuario ObterPorEmail(string email)
        {
            return _dbSet
                        .Where(u => u.Email == email).FirstOrDefault();
        }
    }
}
