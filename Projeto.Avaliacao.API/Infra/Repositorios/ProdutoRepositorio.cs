using System;
using Projeto.Avaliacao.API.Dominio.Entidades;
using Projeto.Avaliacao.API.Dominio.Interfaces;
using Projeto.Avaliacao.API.Infra.Comum;

namespace Projeto.Avaliacao.API.Infra.Repositorios
{
    public class ProdutoRepositorio : BaseRepositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(Contexto context) : base(context)
        {
        }
    }
}
