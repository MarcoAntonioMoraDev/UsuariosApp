using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models;

namespace UsuariosApp.Domain.Interfaces.Messages
{
    /// <summary>
    /// Interface para operações de mensageria de envio de email
    /// </summary>
    public interface IEmailMessageProducer
    {
        /// <summary>
        /// metodo para enviar uma mensagem de email para a fila
        /// </summary>
        void Send(EmailMessageModel model);
    }
}
