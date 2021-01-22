using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Avaliacao.API.Dominio.Entidades
{
    public class Importacao
    {
        public int Id { get; set; }
        public DateTime DataImportacao { get; set; }
        public int TotalItens { get; set; }
        public DateTime DataEntregaMaisProxima { get; set; }
        public decimal ValorTotal { get; set; }

        public virtual ICollection<ProdutoEntrega> ProdutoEntregas { get; set; }

        protected Importacao() { }

        public Importacao(List<ProdutoEntrega> produtoEntregas)
        {
            DataImportacao = DateTime.Now;
            TotalItens = produtoEntregas.Count();
            DataEntregaMaisProxima = produtoEntregas.Min(x => x.DataEntrega);
            ValorTotal = produtoEntregas.Sum(x => (x.Produto.ValorUnitario * x.Quantidade));

            ProdutoEntregas = produtoEntregas;
        }
    }
}
