using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiAtendimento.API.Models;
using MultiAtendimento.API.Models.Constantes;
using MultiAtendimento.API.Models.DTOs;
using MultiAtendimento.API.Models.Enums;
using MultiAtendimento.API.Services;
using System.Net;
using System.Security.Claims;

namespace MultiAtendimento.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("obterChatsDoUsuario")]
        public IActionResult ObterChatsDoUsuario()
        {
            try
            {
                var idUsuario = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value;
                var setorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "setorId").Value;
                var cargo = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
                int idUsuarioInt = int.TryParse(idUsuario, out int resultadoIdUsuario) ? resultadoIdUsuario : 0;
                int setorIdInt = int.TryParse(idUsuario, out int resultadoSetorId) ? resultadoSetorId : 0;
                CargoEnum cargoEnum = Enum.Parse<CargoEnum>(cargo);
                var chats = _chatService.ObterChatsDoUsuarioLogado(idUsuarioInt, setorIdInt, cargoEnum);
                return Ok(new RetornoPadraoView<List<ChatView>>
                {
                    Mensagem = "Lista de chats obtidas com sucesso!",
                    Resultado = chats
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
