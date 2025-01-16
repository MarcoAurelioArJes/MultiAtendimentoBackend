using Microsoft.EntityFrameworkCore;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository.BancoDeDados;

namespace MultiAtendimento.API.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly ContextoDoBancoDeDados _contextoDoBancoDeDados;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ContextoDoBancoDeDados contextoDoBancoDeDados)
        {
            _contextoDoBancoDeDados = contextoDoBancoDeDados;
            _dbSet = _contextoDoBancoDeDados.Set<T>();
        }

        public T Criar(T objeto)
        {
            _dbSet.Add(objeto);
            _contextoDoBancoDeDados.SaveChanges();

            return objeto;
        }

        public T Atualizar(T objeto)
        {
            var entry = _dbSet.Entry(objeto);
            entry.State = EntityState.Detached;
            _dbSet.Update(entry.Entity);
            _contextoDoBancoDeDados.SaveChanges();

            return entry.Entity;
        }

        public virtual T ObterPorId(int id)
        {
            return _dbSet
                        .Include(c => c.Empresa)
                        .FirstOrDefault(c => c.Id == id);
        }

        public virtual List<T> ObterTodosPorCnpjDaEmpresa(string cnpjDaEmpresa)
        {
            return _dbSet
                        .Include(c => c.Empresa)
                        .Where(c => c.Empresa.Cnpj.Equals(cnpjDaEmpresa))
                        .ToList();
        }

        public T Remover(T objeto)
        {
            _dbSet.Remove(objeto);
           _contextoDoBancoDeDados.SaveChanges();
    
            return objeto;
        }
    }
}
