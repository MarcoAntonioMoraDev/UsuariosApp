using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Models;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Producers
{
    public class EmailMessageProducer : IEmailMessageProducer
    {
        public void Send(EmailMessageModel model)
        {
            #region Conectando na fila do servidor de mensageria

            //criando um objeto de 'fábrica de conexões'
            var connectionFactory = new ConnectionFactory()
            {
                //caminho do servidor do rabbitmq (instância)
                Uri = new Uri(MessageSettings.Url)
            };

            //abrindo conexão com o servidor da mensageria
            using (var connection = connectionFactory.CreateConnection())
            {
                //acessando a fila
                using (var queueModel = connection.CreateModel())
                {
                    //conectando / criando a fila
                    queueModel.QueueDeclare(
                        queue: MessageSettings.QueueName, //nome da fila
                        durable: true, // fila não será apagada
                        autoDelete: false, //não remover as mensagens da fila automaticamente
                        exclusive: false, //permitir várias conexões simultaneas na fila
                        arguments: null
                        );

                    //escrevendo conteudo na fila
                    queueModel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: MessageSettings.QueueName,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))
                        );
                }
            }

            #endregion

        }
    }
}


