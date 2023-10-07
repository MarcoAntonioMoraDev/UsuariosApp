using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Models
{
    /// <summary>
    /// Modelo de dados para envio de mensagens por email
    /// </summary>
    public class EmailMessageModel
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public bool IsHtml { get; set; }
    }
}



