using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Entities;

namespace UsuarioApp.Infra.Data.Repositories
{
    public class HistoricoAtividadeRepository 
        : BaseRepository<HistoricoAtividade> , IHistoricoAtividadeRepository
    {
    }
}
