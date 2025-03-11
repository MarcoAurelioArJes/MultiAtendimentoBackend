using System.Net;
using Microsoft.AspNetCore.Mvc;
using MultiAtendimento.API.Services;
using MultiAtendimento.API.Models.DTOs;
using MultiAtendimento.API.Models.Constantes;
using Microsoft.AspNetCore.Authorization;

namespace MultiAtendimento.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _empresaService;
        public EmpresaController(EmpresaService empresaService) 
        {
            _empresaService = empresaService;
        }

        [HttpPost("Registrar")]
        public IActionResult Registrar([FromBody] CadastroEmpresaInput primeiroCadastroInput)
        {
            try
            {
                _empresaService.Criar(primeiroCadastroInput);
                return Created();
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

        [Authorize]
        [HttpGet("ObterInformacoesEmpresaAtual")]
        public IActionResult ObterInformacoesEmpresaAtual()
        {
            try
            {
                var empresa = _empresaService.ObterInformacoesEmpresaAtual();
                return Ok(new RetornoPadraoView<EmpresaView>
                {
                    Mensagem = "Empresa obtida com sucesso!",
                    Resultado = empresa
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
    }
}
