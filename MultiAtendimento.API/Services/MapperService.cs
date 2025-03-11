using AutoMapper;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.DTOs;

namespace MultiAtendimento.API.Services
{
    public class MapperService : Profile
    {
        public MapperService() 
        {
            CreateMap<SetorInput, Setor>();
            CreateMap<SetorCadastroEmpresaInput, Setor>();
            CreateMap<Setor, SetorView>();

            CreateMap<ClienteInput, Cliente>();
            
            CreateMap<Cliente, ClienteView>();

            CreateMap<UsuarioInput, Usuario>();
            CreateMap<UsuarioCadastroEmpresaInput, Usuario>();
            CreateMap<Usuario, UsuarioView>();

            CreateMap<AtualizarUsuarioInput, Usuario>();
            
            CreateMap<Chat, ChatView>();

            CreateMap<Mensagem, MensagemView>();

            CreateMap<Empresa, EmpresaView>();
        }
    }
}