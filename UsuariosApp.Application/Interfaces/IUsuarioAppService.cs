using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Dtos;

namespace UsuariosApp.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<AutenticarResponseDto> Autenticar(AutenticarRequestDto dto);
        Task<CriarContaResponseDto> CriarConta(CriarContaRequestDto dto);
    }
}



