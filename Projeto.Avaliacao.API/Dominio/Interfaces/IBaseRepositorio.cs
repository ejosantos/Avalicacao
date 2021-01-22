using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Avaliacao.API.Dominio.Interfaces
{
    public interface IBaseRepositorio<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> DeleteAsync(int id);
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> UpdateAsync(TEntity obj);
    }
}
