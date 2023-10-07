using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Entities;

namespace UsuarioApp.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public Usuario Find(string email)
        {
            throw new NotImplementedException();
        }

        public Usuario Find(string email, string senha)
        {
            throw new NotImplementedException();
        }
    }
}
