﻿using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.DTOs;
using MultiAtendimento.API.Models.Enums;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository;

namespace MultiAtendimento.API.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly UsuarioService _usuarioService;
        private readonly SetorService _setorService;

        public EmpresaService(IEmpresaRepository empresaRepository, UsuarioService usuarioService, SetorService setorService)
        {
            _empresaRepository = empresaRepository;
            _usuarioService = usuarioService;
            _setorService = setorService;
        }

        public void Criar(CadastroEmpresaInput cadastroEmpresaInput)
        {
            _usuarioService.LancarExcecaoCasoEmailJaExista(cadastroEmpresaInput.Email, 0);

            var empresaExiste = _empresaRepository.ObterEmpresaPorCnpj(cadastroEmpresaInput.Cnpj) != null;
            if (empresaExiste)
                throw new BadHttpRequestException("CNPJ já cadastrado no sistema, tente realizar login utilizando o usuário admin");

            var empresa = new Empresa
            {
                Cnpj = cadastroEmpresaInput.Cnpj,
                Nome = cadastroEmpresaInput.NomeEmpresa
            };
            _empresaRepository.Criar(empresa);

            var setor = new SetorCadastroEmpresaInput
            {
                EmpresaCnpj = empresa.Cnpj,
                Nome = "Admin"
            };
            var setorDb = _setorService.CriarSetorNoCadastroEmpresa(setor);

            var usuario = new UsuarioCadastroEmpresaInput
            {
                Nome = cadastroEmpresaInput.NomeUsuario,
                Senha = cadastroEmpresaInput.Senha,
                EmpresaCnpj = cadastroEmpresaInput.Cnpj,
                Email = cadastroEmpresaInput.Email,
                Cargo = CargoEnum.ADMIN,
                SetorId = setorDb.Id,
                AdministradorPrincipal = true
            };
            _usuarioService.CriarUsuarioNoCadastroEmpresa(usuario);
        }
    }
}
