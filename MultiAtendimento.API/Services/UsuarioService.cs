using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.DTOs;
using MultiAtendimento.API.Models.Enums;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository;
using System.Net;
using static System.Net.WebRequestMethods;

namespace MultiAtendimento.API.Services
{
    public class UsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly SetorService _setorService;
        private readonly IHttpContextAccessor _httpContext;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository, IEmpresaRepository empresaRepository, SetorService setorService, IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _empresaRepository = empresaRepository;
            _setorService = setorService;
            _httpContext = httpContext;
        }

        public UsuarioView Criar(UsuarioInput usuarioInput)
        {
            LancarExcecaoCasoEmailJaExista(usuarioInput.Email, 0);

            var usuario = _mapper.Map<Usuario>(usuarioInput);
            
            var cnpjEmpresa = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "empresaCnpj")?.Value;
            usuario.EmpresaCnpj = _empresaRepository.ObterEmpresaPorCnpj(cnpjEmpresa).Cnpj;
            
            var setor = _setorService.ObterPorId(usuarioInput.SetorId);
            usuario.SetorId = setor.Id;
            
            usuario.Senha = HashService.ObterSenhaHash(usuarioInput.Senha);
            return _mapper.Map<UsuarioView>(_usuarioRepository.Criar(usuario));
        }
        
        public void CriarUsuarioNoCadastroEmpresa(UsuarioCadastroEmpresaInput usuarioInput)
        {
            LancarExcecaoCasoEmailJaExista(usuarioInput.Email, 0);

            var usuario = _mapper.Map<Usuario>(usuarioInput);
            usuario.Senha = HashService.ObterSenhaHash(usuarioInput.Senha);

            _usuarioRepository.Criar(usuario);
        }

        public UsuarioView Atualizar(int id, AtualizarUsuarioInput usuarioInput)
        {
            var usuarioRegister = _usuarioRepository.ObterPorId(id);

            LancarExcecaoCasoEmailJaExista(usuarioInput.Email, usuarioRegister.Id);
            if (usuarioRegister is null)
                throw new BadHttpRequestException($"Usuário com ID {id} não encontrado", (int)HttpStatusCode.NotFound);
            if (usuarioRegister.AdministradorPrincipal && usuarioInput.Cargo != CargoEnum.ADMIN)
                throw new BadHttpRequestException($"Administrador principal não pode ter o cargo alterado", (int)HttpStatusCode.Forbidden);

            usuarioRegister.Nome = usuarioInput.Nome;
            usuarioRegister.Cargo = usuarioInput.Cargo;
            usuarioRegister.SetorId = usuarioInput.SetorId;

            var usuarioAtualizado = _usuarioRepository.Atualizar(usuarioRegister);

            return _mapper.Map<UsuarioView>(_usuarioRepository.ObterPorId(usuarioAtualizado.Id));
        }

        public Usuario ObterPorId(int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);
            if (usuario is null)
                throw new BadHttpRequestException($"Usuário com ID {id} não encontrado", (int)HttpStatusCode.NotFound);
            return usuario;
        }

        public void LancarExcecaoCasoEmailJaExista(string email, int id)
        {
            var usuario = _usuarioRepository.ObterPorEmail(email);
            if (usuario is not null && usuario.Id != id)
                throw new BadHttpRequestException($"Email já cadastrado", (int)HttpStatusCode.BadRequest);
        }

        public Usuario ObterPorEmail(string email)
        {
            var usuario = _usuarioRepository.ObterPorEmail(email);
            if (usuario is null)
                throw new BadHttpRequestException($"Não há nenhum usuario associdado e esse email", (int)HttpStatusCode.NotFound);
            return usuario;
        }

        public List<UsuarioView> ObterTodosOsUsuariosPorCnpjDaEmpresa(string cnpj)
        {
            var usuarios = _usuarioRepository.ObterTodosPorCnpjDaEmpresa(cnpj);
            return _mapper.Map<List<UsuarioView>>(usuarios);
        }

        public EntrarView Entrar(EntrarInput entrarInput)
        {
            var usuario = _usuarioRepository.ObterPorEmail(entrarInput.Email);
            if (usuario != null && HashService.ObterSeASenhaEhValida(entrarInput.Senha, usuario.Senha))
                return TokenService.ObterInformacoesDoLogin(usuario);

            throw new BadHttpRequestException("As credenciais de acesso do usuário são inválidas", StatusCodes.Status401Unauthorized);
        }

        public void Remover(int id)
        {
            var usuarioRegister = _usuarioRepository.ObterPorId(id);

            if (usuarioRegister is null)
                throw new BadHttpRequestException($"Usuário com ID {id} não encontrado", (int)HttpStatusCode.NotFound);
            if (usuarioRegister.AdministradorPrincipal)
                throw new BadHttpRequestException($"Não é possível remover o administrador principal", (int)HttpStatusCode.Forbidden);

            _usuarioRepository.Remover(usuarioRegister);
        }

        public string ObterEmailParaRecuperarSenhaHTML(Usuario usuario)
        {
            #region EstilizacaoHTML
            string estilo = @"                                <style>
                                    body {
                                        font-family: Arial, sans-serif;
                                        margin: 0;
                                        padding: 0;
                                        background-color: #f4f4f4;
                                    }
                                    .email-container {
                                        max-width: 600px;
                                        margin: 20px auto;
                                        background-color: #ffffff;
                                        padding: 20px;
                                        border-radius: 8px;
                                        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                                    }
                                    .header {
                                        text-align: center;
                                        margin-bottom: 20px;
                                    }
                                    .header h1 {
                                        color: #333333;
                                    }
                                    .content {
                                        font-size: 16px;
                                        line-height: 1.6;
                                        color: #333333;
                                    }
                                    .button {
                                        display: block;
                                        width: 200px;
                                        margin: 20px auto;
                                        padding: 12px;
                                        text-align: center;
                                        background-color: #4CAF50;
                                        color: white;
                                        text-decoration: none;
                                        border-radius: 4px;
                                        font-size: 16px;
                                    }
                                    .button:hover {
                                        background-color: #45a049;
                                    }
                                    .footer {
                                        text-align: center;
                                        font-size: 12px;
                                        color: #777777;
                                        margin-top: 30px;
                                    }
                                </style>";
            #endregion

            string token = TokenService.ObterTokenParaRecuperarSenha(usuario);
            string corpoEmail = @$"
                        <html>
                            <head>
                                {estilo}
                            </head>
                            <body>
                                <div class='email-container'>
                                    <div class='header'>
                                        <h1>Recuperação de Senha</h1>
                                    </div>
                                    <div class='content'>
                                        <p>Olá, {usuario.Nome.Split(" ").First()}</p>
                                        <p>Recebemos uma solicitação para recuperar a senha da sua conta.</p>
                                        <p>Para redefinir sua senha, clique no link abaixo:</p>
                                        <a href='http://localhost:3000/atualizarSenha?token={token}' class='button'>Redefinir Senha</a>
                                        <p>Se você não solicitou a recuperação de senha, ignore este e-mail.</p>
                                        <p>Atenciosamente,<br>Equipe de Suporte</p>
                                    </div>
                                    <div class='footer'>
                                        <p>Este é um e-mail automático. Por favor, não responda.</p>
                                    </div>
                                </div>
                            </body>
                        </html>";

                return corpoEmail;
        }

        public void AtualizarSenha(int id, AtualizarSenhaInput atualizarSenhaInput)
        {
            var usuario = ObterPorId(id);
            usuario.Senha = HashService.ObterSenhaHash(atualizarSenhaInput.Senha);

            _usuarioRepository.Atualizar(usuario);
        }
    }
}