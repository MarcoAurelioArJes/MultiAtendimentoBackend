using Microsoft.AspNetCore.Mvc;
using MultiAtendimento.API.Services;
using MultiAtendimento.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.Enums;
using MultiAtendimento.API.Models.Constantes;

namespace MultiAtendimento.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = $"{nameof(CargoEnum.ADMIN)}")]
    public class SetorController : ControllerBase
    {
        private readonly SetorService _setorService;
        public SetorController(SetorService setorService)
        {
            _setorService = setorService;
        }

        [HttpPost("criar")]
        public IActionResult Criar([FromBody] SetorInput setorInput)
        {
            try
            {
                var setorCriado = _setorService.Criar(setorInput);
                return Ok(new RetornoPadraoView<SetorView>
                {
                    Mensagem = "Setor criado com sucesso!",
                    Resultado = setorCriado
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
                    Mensagem = MensagemDeErroConstantes.OcorreuUmErroInesperado
                });
            }
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar(string id, [FromBody] SetorInput setorInput)
        {
            try
            {
                var idInteiro = int.TryParse(id, out int resultado) ? resultado
                                                                    : throw new ArgumentException("Necessário informar no parâmetro da URL um número", "id");

                var setorAtualizado = _setorService.Atualizar(idInteiro, setorInput);
                return Ok(new RetornoPadraoView<SetorView>
                {
                    Mensagem = "Setor atualizado com sucesso!",
                    Resultado = setorAtualizado
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
                    Mensagem = MensagemDeErroConstantes.OcorreuUmErroInesperado
                });
            }
        }

        [HttpGet("obterSetores")]
        public IActionResult Setores()
        {
            try
            {
                var setores = _setorService.ObterTodosOsSetores();
                return Ok(new RetornoPadraoView<List<Setor>>
                {
                    Mensagem = "Lista de setores obtidas com sucesso!",
                    Resultado = setores
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
                    Mensagem = MensagemDeErroConstantes.OcorreuUmErroInesperado
                });
            }
        }

        [AllowAnonymous]
        [HttpGet("obterSetoresPorCnpj/{cnpj}")]
        public IActionResult ObterSetoresPorCnpj(string cnpj)
        {
            try
            {
                var setores = _setorService.ObterSetoresPorCnpj(cnpj);
                return Ok(new RetornoPadraoView<List<Setor>>
                {
                    Mensagem = "Lista de setores obtidas com sucesso!",
                    Resultado = setores
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
                    Mensagem = MensagemDeErroConstantes.OcorreuUmErroInesperado
                });
            }
        }

        [HttpDelete("remover/{id}")]
        public IActionResult Remover(string id)
        {
            try
            {
                var idInteiro = int.TryParse(id, out int resultado) ? resultado
                                                                    : throw new ArgumentException("Necessário informar no parâmetro da URL um número", "id");
                _setorService.Remover(idInteiro);
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
                    Mensagem = MensagemDeErroConstantes.OcorreuUmErroInesperado
                });
            }
        }
    }
}
