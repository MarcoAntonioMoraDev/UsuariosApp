using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Dtos;
using Xunit;

namespace UsuariosApp.Tests
{
    public class UsuariosTest
    {
        private Faker _faker = new Faker("pt_BR");

        private static string _email = string.Empty;
        private static string _senha = string.Empty;

        [Fact]
        [Trait("CriarConta_ReturnsCreated", "1")]
        public void CriarConta_ReturnsCreated()
        {
            //conectando na API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //gerando os dados do usuário
            var dto = new CriarContaRequestDto
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Internet.Email().ToLower(),
                Senha = "@Teste123",
                SenhaConfirmacao = "@Teste123"
            };

            //serializando os dados em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(dto),
                Encoding.UTF8, "application/json");

            //fazendo a requisição para a API e capturando a resposta
            var response = client.PostAsync("api/usuarios/criarconta", jsonRequest).Result;

            //verificando se a resposta é 201
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //guardar o email e a senha do usuário cadastrado
            _email = dto.Email;
            _senha = dto.Senha;
        }

        [Fact]
        [Trait("CriarConta_ReturnsBadRequest", "2")]
        public void CriarConta_ReturnsBadRequest()
        {
            //conectando na API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //gerando os dados do usuário
            var dto = new CriarContaRequestDto
            {
                Nome = _faker.Person.FullName,
                Email = _email,
                Senha = _senha,
                SenhaConfirmacao = _senha
            };

            //serializando os dados em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(dto),
                Encoding.UTF8, "application/json");

            //fazendo a requisição para a API e capturando a resposta
            var response = client.PostAsync("api/usuarios/criarconta", jsonRequest).Result;

            //verificando se a resposta é 201
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Autenticar_ReturnsOk", "3")]
        public void Autenticar_ReturnsOk()
        {
            //conectando na API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //gerando os dados do usuário
            var dto = new AutenticarRequestDto
            {
                Email = _email,
                Senha = _senha
            };

            //serializando os dados em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(dto),
                Encoding.UTF8, "application/json");

            //fazendo a requisição para a API e capturando a resposta
            var response = client.PostAsync("api/usuarios/autenticar", jsonRequest).Result;

            //verificando se a resposta é 201
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Autenticar_ReturnsBadRequest", "4")]
        public void Autenticar_ReturnsBadRequest()
        {
            //conectando na API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //gerando os dados do usuário
            var dto = new AutenticarRequestDto
            {
                Email = _faker.Internet.Email(),
                Senha = "@Teste321"
            };

            //serializando os dados em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(dto),
                Encoding.UTF8, "application/json");

            //fazendo a requisição para a API e capturando a resposta
            var response = client.PostAsync("api/usuarios/autenticar", jsonRequest).Result;

            //verificando se a resposta é 201
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}



