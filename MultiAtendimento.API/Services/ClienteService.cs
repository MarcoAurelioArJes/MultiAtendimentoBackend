using AutoMapper;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.DTOs;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository;

namespace MultiAtendimento.API.Services
{
    public class ClienteService
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly SetorService _setorService;

        public ClienteService(IMapper mapper, IClienteRepository clienteRepository, SetorService setorService)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _setorService = setorService;
        }

        public Cliente Criar(ClienteInput clienteInput)
        {
            var cliente = _mapper.Map<Cliente>(clienteInput);

            var setor = _setorService.ObterPorId(clienteInput.SetorId);
            cliente.Setor = setor;
            cliente.Empresa = setor.Empresa;

            _clienteRepository.Criar(cliente);

            return cliente;
        }
    }
}
