using System;
namespace Projeto.Avaliacao.API.Dtos
{
    public class DtoImportacao
    {
        public int Id { get; set; }
        public DateTime DataImportacao { get; set; }
        public int TotalItens { get; set; }
        public DateTime DataEntregaMaisProxima { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
