using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Consumers
{
    public class EmailMessageConsumer : BackgroundService
    {
        //atributos
        private readonly IServiceProvider? _serviceProvider;
        private IConnection? _connection;
        private IModel? _model;

        //construtor para injeção de dependencia e inicialização dos atributos
        public EmailMessageConsumer(IServiceProvider? serviceProvider)
        {
            _serviceProvider = serviceProvider;

            //conectando no servidor do RabbitMQ
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(MessageSettings.Url)
            };

            _connection = connectionFactory.CreateConnection();

            //acessando a fila
            _model = _connection.CreateModel();
            _model.QueueDeclare(
                        queue: MessageSettings.QueueName, //nome da fila
                        durable: true, // fila não será apagada
                        autoDelete: false, //não remover as mensagens da fila automaticamente
                        exclusive: false, //permitir várias conexões simultaneas na fila
                        arguments: null
                        );
        }

        /// <summary>
        /// Método utilizado para ler cada mensagem contida na fila e envia-la por email
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //objeto para ler o conteudo da fila
            var consumer = new EventingBasicConsumer(_model);

            //programando a leitura
            consumer.Received += async (sender, args) =>
            {
                //lendo o conteudo da mensagem da fila como string
                var payload = Encoding.UTF8.GetString(args.Body.ToArray());

                //deserializando para objeto (lendo)
                var emailMessageModel = JsonConvert.DeserializeObject<EmailMessageModel>(payload);

                try
                {
                    SendMail(emailMessageModel); //enviando o email                    
                    _model?.BasicAck(args.DeliveryTag, false); //removendo da fila
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            };

            //executando a leitura
            //false -> não remover mensagens da fila automaticamente
            _model.BasicConsume(MessageSettings.QueueName, false, consumer);
        }

        /// <summary>
        /// Método para realizar o envio do email
        /// </summary>
        private void SendMail(EmailMessageModel model)
        {
            var email = "cotiaulajava@outlook.com";
            var senha = "@Admin123456";
            var smtp = "smtp-mail.outlook.com";
            var porta = 587;

            #region montando o email

            var mailMessage = new MailMessage(email, model.To);
            mailMessage.Subject = model.Subject;
            mailMessage.Body = model.Body;
            mailMessage.IsBodyHtml = model.IsHtml;

            #endregion

            #region enviando o email

            var smtpClient = new SmtpClient(smtp, porta);
            smtpClient.Credentials = new NetworkCredential(email, senha);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

            #endregion
        }
    }
}


