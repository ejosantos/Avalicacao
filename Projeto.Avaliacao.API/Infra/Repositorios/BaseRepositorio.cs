using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto.Avaliacao.API.Dominio.Interfaces;
using Projeto.Avaliacao.API.Infra.Comum;

namespace Projeto.Avaliacao.API.Infra.Repositorios
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : class
    {
        protected readonly Contexto _Context;
        protected readonly DbSet<TEntity> _DbSet;

        public BaseRepositorio(Contexto context)
        {
            _Context = context;
            _DbSet = _Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            await _DbSet.AddAsync(obj);
            return obj;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var obj = await _DbSet.FindAsync(id);

            if (obj == null) return null;

            _DbSet.Remove(obj);
            return obj;
        }

        public async Task<TEntity> GetAsync(int id) => await _DbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await Task.FromResult<IEnumerable<TEntity>>(_DbSet.AsNoTracking());

        public async Task<TEntity> UpdateAsync(TEntity obj)
        {
            await Task.Run(() =>
            {
                _Context.Entry(obj).State = EntityState.Modified;
            });
            return obj;

        }
    }
}
