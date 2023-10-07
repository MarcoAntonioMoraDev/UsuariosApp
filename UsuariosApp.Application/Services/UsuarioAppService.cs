using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Dtos;
using UsuariosApp.Application.Interfaces;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        //atributo
        private readonly IUsuarioDomainService? _usuarioDomainService;

        //construtor para injeção de dependência
        public UsuarioAppService(IUsuarioDomainService? usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public async Task<AutenticarResponseDto> Autenticar(AutenticarRequestDto dto)
        {
            var usuario = await _usuarioDomainService?.Autenticar(dto.Email, dto.Senha);

            return new AutenticarResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraAcesso = DateTime.Now,
                DataHoraExpiracao = DateTime.Now,
                AccessToken = usuario.AccessToken
            };
        }

        public async Task<CriarContaResponseDto> CriarConta(CriarContaRequestDto dto)
        {
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            await _usuarioDomainService?.CriarConta(usuario);

            return new CriarContaResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraCadastro = DateTime.Now
            };
        }
    }
}



