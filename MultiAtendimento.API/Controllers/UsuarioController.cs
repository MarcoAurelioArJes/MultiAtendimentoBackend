using System.Net;
using Microsoft.AspNetCore.Mvc;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Services;
using MultiAtendimento.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using MultiAtendimento.API.Models.Enums;

namespace MultiAtendimento.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = $"{nameof(CargoEnum.ADMIN)}")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("criar")]
        public IActionResult Criar([FromBody] UsuarioInput usuarioInput)
        {
            try
            {
                var usuarioCriado = _usuarioService.Criar(usuarioInput);
                return Ok(new RetornoPadraoView<UsuarioView>
                {
                    Mensagem = "Usuário criado com sucesso!",
                    Resultado = usuarioCriado
                });
            }
            catch (BadHttpRequestException badHttpRequestException)
            {
                return StatusCode(badHttpRequestException.StatusCode, new RetornoPadraoView<object>
                {
                    Mensagem = badHttpRequestException.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new RetornoPadraoView<object>
                {
                    Mensagem = ex.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("entrar")]
        public IActionResult Entrar([FromBody] EntrarInput entrarInput)
        {
            try
            {
                var retorno = _usuarioService.Entrar(entrarInput);
                return Ok(new RetornoPadraoView<EntrarView>
                {
                    Mensagem = "Login efetuado com sucesso!",
                    Resultado = retorno
                });
            }
            catch (BadHttpRequestException badHttpRequestException)
            {
                return StatusCode(badHttpRequestException.StatusCode, new RetornoPadraoView<object>
                {
                    Mensagem = badHttpRequestException.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new RetornoPadraoView<object>
                {
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar(string id, [FromBody] AtualizarUsuarioInput usuarioInput)
        {
            try
            {
                var idInteiro = int.TryParse(id, out int resultado) ? resultado : throw new ArgumentException("Necessário informar no parâmetro da URL um número", nameof(id));
                var usuarioAtualizado = _usuarioService.Atualizar(idInteiro, usuarioInput);
                return Ok(new RetornoPadraoView<UsuarioView>
                {
                    Mensagem = "Usuário atualizado com sucesso!",
                    Resultado = usuarioAtualizado
                });
            }
            catch (BadHttpRequestException badHttpRequestException)
            {
                return StatusCode(badHttpRequestException.StatusCode, new RetornoPadraoView<object>
                {
                    Mensagem = badHttpRequestException.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new RetornoPadraoView<object>
                {
                    Mensagem = ex.Message
                });
            }
        }


        [HttpGet("obterPorId/{id}")]
        public IActionResult ObterPorId(string id)
        {
            try
            {
                var idInteiro = int.TryParse(id, out int resultado) ? resultado : throw new ArgumentException("Necessário informar no parâmetro da URL um número", nameof(id));
                var usuario = _usuarioService.ObterPorId(idInteiro);
                return Ok(new RetornoPadraoView<Usuario>
                {
                    Mensagem = "Usuário obtido com sucesso!",
                    Resultado = usuario
                });
            }
            catch (BadHttpRequestException badHttpRequestException)
            {
                return StatusCode(badHttpRequestException.StatusCode, new RetornoPadraoView<object>
                {
                    Mensagem = badHttpRequestException.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new RetornoPadraoView<object>
                {
                    Mensagem = ex.Message
                });
            }
        }

        [HttpGet("obterUsuarios")]
        public IActionResult ObterUsuarios()
        {
            try
            {
                var cnpj = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("empresaCnpj")).Value;
                var usuarios = _usuarioService.ObterTodosOsUsuariosPorCnpjDaEmpresa(cnpj);
                return Ok(new RetornoPadraoView<List<UsuarioView>>
                {
                    Mensagem = "Lista de usuários obtidas com sucesso!",
                    Resultado = usuarios
                });
            }
            catch (BadHttpRequestException badHttpRequestException)
            {
                return StatusCode(badHttpRequestException.StatusCode, new RetornoPadraoView<object>
                {
                    Mensagem = badHttpRequestException.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new RetornoPadraoView<object>
                {
                    Mensagem = ex.Message
                });
            }
        }

        [HttpDelete("remover/{id}")]
        public IActionResult Remover(string id)
        {
            try
            {
                var idSolicitante = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value;
                if(idSolicitante == id)
                    throw new ArgumentException("Não é possível remover o próprio usuário");

                var idInteiro = int.TryParse(id, out int resultado) ? resultado
                                                                    : throw new ArgumentException("Necessário informar no parâmetro da URL um número", "id");
                _usuarioService.Remover(idInteiro);
                return NoContent();
            }
            catch (BadHttpRequestException badHttpRequestException)
            {
                return StatusCode(badHttpRequestException.StatusCode, new RetornoPadraoView<object>
                {
                    Mensagem = badHttpRequestException.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new RetornoPadraoView<object>
                {
                    Mensagem = ex.Message
                });
            }
        }

        [HttpGet("obterCargos")]
        public IActionResult ObterCargos()
        {
            try
            {
                return Ok(new RetornoPadraoView<List<CargosView>>
                {
                    Mensagem = "Lista de cargos obtidas com sucesso!",
                    Resultado = new List<CargosView>
                    {
                        new CargosView
                        {
                            Codigo = CargoEnum.ADMIN
                        },
                        new CargosView
                        {
                            Codigo = CargoEnum.ATENDENTE
                        }
                    }
                });
            }
            catch (BadHttpRequestException badHttpRequestException)
            {
                return StatusCode(badHttpRequestException.StatusCode, new RetornoPadraoView<object>
                {
                    Mensagem = badHttpRequestException.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new RetornoPadraoView<object>
                {
                    Mensagem = ex.Message
                });
            }
        }


        [AllowAnonymous]
        [HttpPost("enviarEmailParaRecuperarSenha")]
        public IActionResult EnviarEmailParaRecuperarSenha([FromBody] EnviarEmailParaRecuperarSenhaInput enviarEmailParaRecuperarSenhaInput)
        {
            try
            {
                var usuarioPorEmail = _usuarioService.ObterPorEmail(enviarEmailParaRecuperarSenhaInput.Email);
                var html = _usuarioService.ObterEmailParaRecuperarSenhaHTML(usuarioPorEmail);
                EmailService.EnviarEmail([usuarioPorEmail.Email], "Recuperar senha", html);

                return Ok(new RetornoPadraoView<object>
                {
                    Mensagem = "E-mail para recuperar senha enviado com sucesso!"
                });
            }
            catch (BadHttpRequestException badHttpRequestException)
            {
                return StatusCode(badHttpRequestException.StatusCode, new RetornoPadraoView<object>
                {
                    Mensagem = badHttpRequestException.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new RetornoPadraoView<object>
                {
                    Mensagem = ex.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpPut("atualizarSenha")]
        public IActionResult AtualizarSenha([FromBody] AtualizarSenhaInput atualizarSenhaInput)
        {
            try
            {
                if (HttpContext.Request.Headers.TryGetValue("UserToken", out var value))
                {
                    var token = TokenService.ObterTokenRecuperarSenhaSeForValido(value);
                    var idSolicitante = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value;
                    _usuarioService.AtualizarSenha(int.TryParse(idSolicitante, out int resultado) ? resultado : 0, atualizarSenhaInput);
                }

                return Ok(new RetornoPadraoView<object>
                {
                    Mensagem = "E-mail para recuperar senha enviado com sucesso!"
                });
            }
            catch (BadHttpRequestException badHttpRequestException)
            {
                return StatusCode(badHttpRequestException.StatusCode, new RetornoPadraoView<object>
                {
                    Mensagem = badHttpRequestException.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new RetornoPadraoView<object>
                {
                    Mensagem = ex.Message
                });
            }
        }
    }
}