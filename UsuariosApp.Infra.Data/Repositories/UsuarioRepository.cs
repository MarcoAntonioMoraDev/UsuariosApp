using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.Infra.Data.Repositories
{
    public class UsuarioRepository
        : BaseRepository<Usuario>, IUsuarioRepository
    {
        public async Task<Usuario> Find(string email)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Usuario
                    .FirstOrDefaultAsync(u => u.Email.Equals(email));
            }
        }

        public async Task<Usuario> Find(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Usuario
                    .FirstOrDefaultAsync(u => u.Email.Equals(email)
                                      && u.Senha.Equals(senha));
            }
        }
    }
}



