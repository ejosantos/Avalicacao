using System;
using Microsoft.EntityFrameworkCore;
using Projeto.Avaliacao.API.Infra.Mapeamento;

namespace Projeto.Avaliacao.API.Infra.Comum
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProdutoConfig());
            builder.ApplyConfiguration(new ProdutoEntregaConfig());
            builder.ApplyConfiguration(new ImportacaoConfig());

            base.OnModelCreating(builder);
        }
    }
}
