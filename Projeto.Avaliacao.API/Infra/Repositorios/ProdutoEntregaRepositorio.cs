using Projeto.Avaliacao.API.Dominio.Entidades;
using Projeto.Avaliacao.API.Dominio.Interfaces;
using Projeto.Avaliacao.API.Infra.Comum;

namespace Projeto.Avaliacao.API.Infra.Repositorios
{
    public class ProdutoEntregaRepositorio : BaseRepositorio<ProdutoEntrega>, IProdutoEntregaRepositorio
    {
        public ProdutoEntregaRepositorio(Contexto context) : base(context)
        {
        }
    }
}
