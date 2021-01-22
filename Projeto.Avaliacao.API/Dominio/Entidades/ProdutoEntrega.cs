using System;

namespace Projeto.Avaliacao.API.Dominio.Entidades
{
    public class ProdutoEntrega
    {
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public virtual Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public virtual Importacao Importacao { get; set; }

        protected ProdutoEntrega() { }

        public ProdutoEntrega(DateTime dataEntrega, Produto produto, int quantidade)
        {
            new RegraException<Produto>().
                Quando(dataEntrega <= DateTime.Today, "Data de entrega deve ser maior que hoje.")
                .Quando(quantidade <= 0, "Quantidade deve ser maior que zero.")
                .Quando(Produto != null, "Produto deve ser informado.")
                .EntaoDispara();

            DataEntrega = dataEntrega;
            Produto = produto;
            Quantidade = quantidade;
        }

        internal decimal ValorTotal()
        {
            return Quantidade * Produto.ValorUnitario;
        }
    }
}
