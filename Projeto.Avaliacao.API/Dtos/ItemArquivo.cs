using System;
namespace Projeto.Avaliacao.API.Dtos
{
    public class ItemArquivo
    {
        public DateTime DataEntrega { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public int  LinhaImpportada { get; set; }
    }
}
        