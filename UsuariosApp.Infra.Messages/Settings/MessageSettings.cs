using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Messages.Settings
{
    public static class MessageSettings
    {
        /// <summary>
        /// Endereço do servidor de mensageria (Message Broker)
        /// </summary>
        public static string Url => "amqps://crysfeco:5ccmK8rmcGB3PLTyr0oJy38l6HKGba3O@possum.lmq.cloudamqp.com/crysfeco";

        /// <summary>
        /// Nome da fila no servidor de mensageria
        /// </summary>
        public static string QueueName => "email_message";
    }
}


