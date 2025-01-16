using MultiAtendimento.API.Models;
using Microsoft.EntityFrameworkCore;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository.BancoDeDados;
using MultiAtendimento.API.Models.Enums;

namespace MultiAtendimento.API.Repository
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        private readonly IMensagemRepository _mensagemRepository;
        private IHttpContextAccessor _httpContextAccessor;
        public ChatRepository(ContextoDoBancoDeDados contextoDoBancoDeDados, IMensagemRepository mensagemRepository, IHttpContextAccessor httpContextAccessor) : base(contextoDoBancoDeDados)
        {
            _mensagemRepository = mensagemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public void AdicionarMensagem(Mensagem mensagem)
        {
            _mensagemRepository.Criar(mensagem);
            _contextoDoBancoDeDados.SaveChanges();
        }

        public List<Chat> ObterChatsDoUsuario(int idUsuario, int setorId, CargoEnum cargoEnum)
        {
            var empresaCnpj = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "empresaCnpj").Value;
            var chatsPorUsuario = _dbSet
                        .Include(c => c.Mensagens)
                        .Include(c => c.Cliente)
                        .Where(c => (cargoEnum == CargoEnum.ADMIN
                                 || (c.SetorId == setorId && c.AtendenteId == null)
                                 || (c.AtendenteId == idUsuario))
                                 && c.EmpresaCnpj.Equals(empresaCnpj));

            return chatsPorUsuario.ToList();
        }
    }
}