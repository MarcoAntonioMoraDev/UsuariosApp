using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Application.Dtos;
using UsuariosApp.Application.Interfaces;

namespace UsuariosApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAppService? _usuarioAppService; //Botão direito "Ações Rapidas e Refatorações"

        //Depois da ação acima
        //é criado o Contrutor de Injrção de dependencia
        public UsuariosController(IUsuarioAppService? usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }
        //--------------------------------------------------

        /// <summary>
        /// Serviço para autenticação de usuário e obtenção de TOKEN de acesso.
        /// </summary>
        [HttpPost]
        [Route("Autenticar")]
        [ProducesResponseType(typeof(AutenticarResponseDto), 200)]
        public async Task <IActionResult> Autenticar([FromBody] AutenticarRequestDto dto)
        {
            try
            {
                return StatusCode(200, await _usuarioAppService?.Autenticar(dto));
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        /// <summary>
        /// Serviço para criação de conta de acesso de usuário.
        /// </summary>
        [HttpPost]
        [Route("CriarConta")]
        [ProducesResponseType(typeof(CriarContaResponseDto), 201)]
        public async Task <IActionResult> CriarConta([FromBody] CriarContaRequestDto dto)
        {
            try
            {
                return StatusCode(201, await _usuarioAppService?.CriarConta(dto));
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}




