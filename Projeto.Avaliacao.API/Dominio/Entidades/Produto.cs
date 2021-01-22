namespace Projeto.Avaliacao.API.Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }

        protected Produto()
        {
        }

        public Produto(string nome, decimal valorUnitario)
        {
            new RegraException<Produto>().
                Quando(string.IsNullOrWhiteSpace(nome), "Nome do produto deve ser informado.")
                .Quando(!string.IsNullOrWhiteSpace(nome) && nome.Length > 50, "Nome do produto deve possuir no máximo 50 caracteres.")
                .Quando(valorUnitario <= 0, "Valor unitário do produto deve ser maior que zero.")
                .EntaoDispara();

            Nome = nome;
            ValorUnitario = valorUnitario;
        }
    }
}
