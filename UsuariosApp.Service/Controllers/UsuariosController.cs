using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Application.Dtos;

namespace UsuariosApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        /// <summary>
        /// Serviço para autenticação de usuário e obtenção de TOKEN de acesso.
        /// </summary>
        [HttpPost]
        [Route("Autenticar")]
        [ProducesResponseType(typeof(AutenticarResponseDto), 200)]
        public IActionResult Autenticar([FromBody] AutenticarRequestDto dto)
        {
            return Ok();
        }

        /// <summary>
        /// Serviço para criação de conta de acesso de usuário.
        /// </summary>
        [HttpPost]
        [Route("CriarConta")]
        [ProducesResponseType(typeof(CriarContaResponseDto), 201)]
        public IActionResult CriarConta([FromBody] CriarContaRequestDto dto)
        {
            return Ok();
        }
    }
}