using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Repositories;

namespace UsuariosApp.Infra.Data.Repositories
{
    public class HistoricoAtividadeRepository
        : BaseRepository<HistoricoAtividade>, IHistoricoAtividadeRepository
    {

    }
}
