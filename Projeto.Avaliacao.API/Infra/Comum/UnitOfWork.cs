using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto.Avaliacao.API.Dominio.Interfaces;

namespace Projeto.Avaliacao.API.Infra.Comum
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Contexto _Context;

        public UnitOfWork(Contexto context) { _Context = context; }

        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _Context.SaveChangesAsync();
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            _Context.ChangeTracker.Entries()
                .Where(e => e.Entity != null).ToList()
                .ForEach(e => e.State = EntityState.Detached);

            return Task.CompletedTask;
        }
    }
}
