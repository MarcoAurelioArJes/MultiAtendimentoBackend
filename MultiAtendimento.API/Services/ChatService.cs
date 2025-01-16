using AutoMapper;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.DTOs;
using MultiAtendimento.API.Models.Enums;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository;
using System.Net;

namespace MultiAtendimento.API.Services
{
    public class ChatService
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        public ChatService(IChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public Chat Criar(Cliente cliente)
        {
            var chat = new Chat
            {
                Atendente = null,
                Setor = cliente.Setor,
                Cliente = cliente,
                Status = StatusDoChatEnum.Nenhum,
                Empresa = cliente.Empresa
            };

            _chatRepository.Criar(chat);
            return chat;
        }

        public void AdicionarMensagem(Mensagem mensagem)
        {
            if (string.IsNullOrWhiteSpace(mensagem.Conteudo))
                return;

            _chatRepository.AdicionarMensagem(mensagem);
        }

        public void AdicionarAtendente(int chatId, int atendenteId)
        {
            var chatDb = _chatRepository.ObterPorId(chatId);

            chatDb.AtendenteId = atendenteId;

            _chatRepository.Atualizar(chatDb);
        }

        public List<ChatView> ObterChatsDoUsuarioLogado(int idUsuario, int setorId, CargoEnum cargoEnum)
        {
            var listaDeChats = _mapper.Map<List<ChatView>>(_chatRepository.ObterChatsDoUsuario(idUsuario, setorId, cargoEnum));
            return listaDeChats;
        }

        public Chat ObterChatPorId(int chatId)
        {
            var chat = _chatRepository.ObterPorId(chatId);
            return chat;
        }
    }
}
