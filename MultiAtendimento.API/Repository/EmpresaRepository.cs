using Microsoft.EntityFrameworkCore;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository.BancoDeDados;

namespace MultiAtendimento.API.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        protected readonly ContextoDoBancoDeDados _contextoDoBancoDeDados;
        
        public EmpresaRepository(ContextoDoBancoDeDados contextoDoBancoDeDados) 
        {
            _contextoDoBancoDeDados = contextoDoBancoDeDados;
        }

        public Empresa Criar(Empresa objeto)
        {
            _contextoDoBancoDeDados.Empresas.Add(objeto);
            _contextoDoBancoDeDados.SaveChanges();

            return objeto;
        }

        public Empresa ObterEmpresaPorCnpj(string cnpj)
        {
            return _contextoDoBancoDeDados.Empresas.FirstOrDefault(c => c.Cnpj.Equals(cnpj));
        }
    }
}
