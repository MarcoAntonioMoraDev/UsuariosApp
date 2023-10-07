using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        public virtual async Task Add(TEntity entity)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(entity);
                await dataContext.SaveChangesAsync();
            }
        }

        public virtual async Task Update(TEntity entity)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(entity);
                await dataContext.SaveChangesAsync();
            }
        }

        public virtual async Task Delete(TEntity entity)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Remove(entity);
                await dataContext.SaveChangesAsync();
            }
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Set<TEntity>().ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Set<TEntity>().FindAsync(id);
            }
        }
    }
}


