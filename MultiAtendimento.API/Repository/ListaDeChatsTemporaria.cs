using MultiAtendimento.API.Models;
using System.Collections.Concurrent;

namespace MultiAtendimento.API.Repository
{
    public class ListaDeChatsTemporaria
    {
        private readonly ConcurrentDictionary<string, Chat> _chats = new ConcurrentDictionary<string, Chat>();
        public ConcurrentDictionary<string, Chat> Chats => _chats;
    }
}