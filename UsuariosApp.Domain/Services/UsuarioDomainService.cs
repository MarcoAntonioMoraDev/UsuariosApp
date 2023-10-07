using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Helpers;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Security;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Models;

namespace UsuariosApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository? _usuarioRepository;
        private readonly IHistoricoAtividadeRepository? _historicoAtividadeRepository;
        private readonly ITokenSecurity? _tokenSecurity;
        private readonly IEmailMessageProducer? _emailMessageProducer;

        public UsuarioDomainService(IUsuarioRepository? usuarioRepository, IHistoricoAtividadeRepository? historicoAtividadeRepository, ITokenSecurity? tokenSecurity, IEmailMessageProducer? emailMessageProducer)
        {
            _usuarioRepository = usuarioRepository;
            _historicoAtividadeRepository = historicoAtividadeRepository;
            _tokenSecurity = tokenSecurity;
            _emailMessageProducer = emailMessageProducer;
        }

        public async Task<Usuario> Autenticar(string email, string senha)
        {
            //buscar o usuário no banco de dados
            var usuario = await _usuarioRepository?.Find(email, SHA1Helper.Encrypt(senha));

            //verificando se o usuário não foi encontrado
            if (usuario == null)
                throw new ApplicationException("Acesso negado. Usuário não encontrado.");

            //gerando o token do usuário
            usuario.AccessToken = _tokenSecurity?.CreateToken(usuario);

            //criando o histórico de atividade
            var historicoAtividade = new HistoricoAtividade
            {
                Id = Guid.NewGuid(),
                DataHora = DateTime.Now,
                Descricao = $"Autenticação de usuário: {usuario.Nome}, email: {usuario.Email}",
                UsuarioId = usuario.Id
            };

            //gravar o histórico de atividade
            await _historicoAtividadeRepository?.Add(historicoAtividade);

            return usuario; //retornando o usuário
        }

        public async Task CriarConta(Usuario usuario)
        {
            //verificar se o email já está cadastrado no banco de dados
            if (await _usuarioRepository?.Find(usuario.Email) != null)
                throw new ApplicationException("O email informado já está cadastrado. Tente outro.");

            //criptografar a senha do usuário
            usuario.Senha = SHA1Helper.Encrypt(usuario.Senha);

            //gravar o usuário no banco de dados
            await _usuarioRepository?.Add(usuario);

            //criando o histórico de atividade
            var historicoAtividade = new HistoricoAtividade
            {
                Id = Guid.NewGuid(),
                DataHora = DateTime.Now,
                Descricao = $"Cadastro de conta de usuário: {usuario.Nome}, email: {usuario.Email}",
                UsuarioId = usuario.Id
            };

            //gravar o histórico de atividade
            await _historicoAtividadeRepository?.Add(historicoAtividade);

            //escrevendo uma mensagem de boas vindas para o usuário
            var emailMessageModel = new EmailMessageModel
            {
                To = usuario.Email,
                Subject = "Conta de usuário criada com sucesso - API Usuários COTI Informática",
                Body = @$"
                    <div>
                        <h3>Parabéns {usuario.Nome}, sua conta foi criada com sucesso!</h3>
                        <p>Você foi cadastrado em nossa base de usuários em {DateTime.Now}</p>
                        <p>Utilize sua conta para acessar os recursos da aplicação</p>
                        <p>Att, Equipe COTI</p>
                    </div>
                ",
                IsHtml = true
            };

            //enviando a mensagem para o usuário
            _emailMessageProducer?.Send(emailMessageModel);
        }
    }
}



