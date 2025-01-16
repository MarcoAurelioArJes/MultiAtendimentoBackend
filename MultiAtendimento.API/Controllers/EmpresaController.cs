using Microsoft.AspNetCore.Mvc;
using MultiAtendimento.API.Models.DTOs;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Services;
using System.Net;

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
                    Mensagem = ex.Message
                });
            }
        }
    }
}
