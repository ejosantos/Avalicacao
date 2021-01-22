using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Avaliacao.API.Dominio.Entidades;

namespace Projeto.Avaliacao.API.Infra.Mapeamento
{
    public class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ValorUnitario).HasPrecision(18, 2).IsRequired();
        }
    }
}
