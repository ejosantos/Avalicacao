using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Avaliacao.API.Dominio.Entidades;

namespace Projeto.Avaliacao.API.Infra.Mapeamento
{
    public class ProdutoEntregaConfig : IEntityTypeConfiguration<ProdutoEntrega>
    {
        public void Configure(EntityTypeBuilder<ProdutoEntrega> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.DataEntrega);
            builder.Property(x => x.Quantidade);

            builder.HasOne(x => x.Importacao).WithMany(x => x.ProdutoEntregas).HasForeignKey("IdImportacao");
        }
    }
}
