using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Avaliacao.API.Dominio.Entidades;

namespace Projeto.Avaliacao.API.Infra.Mapeamento
{
    public class ImportacaoConfig : IEntityTypeConfiguration<Importacao>
    {
        public void Configure(EntityTypeBuilder<Importacao> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.DataImportacao).IsRequired();
            builder.Property(x => x.DataEntregaMaisProxima).IsRequired();
            builder.Property(x => x.TotalItens).IsRequired();
            builder.Property(x => x.ValorTotal).HasPrecision(18, 2).IsRequired();
        }
    }
}
