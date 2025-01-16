using AutoMapper;
using MultiAtendimento.API.Models.DTOs;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Models;
using System.Net;

namespace MultiAtendimento.API.Services
{
    public class SetorService
    {
        private readonly IMapper _mapper;
        private readonly ISetorRepository _setorRepository;
        private readonly IHttpContextAccessor _httpContext;

        public SetorService(IMapper mapper, ISetorRepository setorRepository, IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _setorRepository = setorRepository;
            _httpContext = httpContext;
        }

        public SetorView Criar(SetorInput setorInput)
        {
            var setor = _mapper.Map<Setor>(setorInput);
            
            var empresaCnpj = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("empresaCnpj"))?.Value;
            setor.EmpresaCnpj = empresaCnpj;

            var setorCriado = _setorRepository.Criar(setor);

            return _mapper.Map<SetorView>(setorCriado);
        }
        
        public Setor CriarSetorNoCadastroEmpresa(SetorCadastroEmpresaInput setorInput)
        {
            var setor = _mapper.Map<Setor>(setorInput);

            return _setorRepository.Criar(setor);
        }

        public SetorView Atualizar(int id, SetorInput setorInput)
        {
            var setorDb = _setorRepository.ObterPorId(id);
            if (setorDb is null)
                throw new BadHttpRequestException($"Não existe setor com o id {id}", (int)HttpStatusCode.NotFound);

            setorDb.Nome = setorInput.Nome;
            var setorAtualizado = _setorRepository.Atualizar(setorDb);

            return _mapper.Map<SetorView>(_setorRepository.ObterPorId(setorDb.Id));
        }

        public Setor ObterPorId(int id)
        {
            var setorDb = _setorRepository.ObterPorId(id);
            if (setorDb is null)
                throw new BadHttpRequestException($"Não existe setor com o id {id}", (int)HttpStatusCode.NotFound);

            return setorDb;
        }

        public List<Setor> ObterTodosOsSetores()
        {
            var empresaCnpj = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("empresaCnpj"))?.Value;
            return _setorRepository.ObterTodosPorCnpjDaEmpresa(empresaCnpj);
        }

        public List<Setor> ObterSetoresPorCnpj(string cnpj)
        {
            return _setorRepository.ObterTodosPorCnpjDaEmpresa(cnpj);
        }

        public void Remover(int id)
        {
            var setorDb = _setorRepository.ObterPorId(id);
            if (setorDb is null)
                throw new BadHttpRequestException($"Não existe setor com o id {id}", (int)HttpStatusCode.NotFound);
            _setorRepository.Remover(setorDb);
        }
    }
}
