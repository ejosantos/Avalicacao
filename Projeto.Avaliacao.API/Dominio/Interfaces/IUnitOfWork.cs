using System;
using System.Threading;
using System.Threading.Tasks;

namespace Projeto.Avaliacao.API.Dominio.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
