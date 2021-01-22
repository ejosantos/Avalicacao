using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Avaliacao.API.Dominio.Entidades;
using Projeto.Avaliacao.API.Dominio.Interfaces;

namespace Projeto.Avaliacao.API.Dominio.Servicos
{
    public class ImportacaoDeArquivo : IImportacaoDeArquivo
    {
        public ImportacaoDeArquivo(IImportacaoRepositorio importacaoRepositorio)
        {
            _importacaoRepositorio = importacaoRepositorio;
        }

        private readonly IImportacaoRepositorio _importacaoRepositorio;

        public async Task<string> ImportarArquivo(string arquivo)
        {
            StringBuilder listErros = new StringBuilder();

            var itensArquivo = LeitorDeArquivoXlsx.Ler(arquivo);
            List<ProdutoEntrega> produtoEntregas = new List<ProdutoEntrega>();
            foreach (var itemArquivo in itensArquivo)
            {

                if (!EstaValido(itemArquivo.NomeProduto, itemArquivo.ValorUnitario, itemArquivo.Quantidade, itemArquivo.DataEntrega, listErros) || listErros.ToString() != string.Empty)
                {
                    listErros.AppendLine($"Linha {itemArquivo.LinhaImpportada}");
                    continue;
                }
                
                var produto = new Produto(itemArquivo.NomeProduto, itemArquivo.ValorUnitario);
                produtoEntregas.Add(new ProdutoEntrega(itemArquivo.DataEntrega, produto, itemArquivo.Quantidade));
            }
            if (produtoEntregas.Any())
                await _importacaoRepositorio.AddAsync(new Importacao(produtoEntregas));

            return listErros.ToString();
        }

        private bool EstaValido(string nome, decimal valorUnitario, int quantidade, DateTime dataEntrega, StringBuilder listaErros)
        {
            bool estáValido = true;
            if (string.IsNullOrWhiteSpace(nome))
            {
                listaErros.AppendLine("Nome do produto deve ser informado.");
                estáValido = false;
            }

            if (!string.IsNullOrWhiteSpace(nome) && nome.Length > 50)
            {
                listaErros.AppendLine($"Quantidade de caracteres inválido para o produto {nome}, deve possuir até 50.");
                estáValido = false;
            }

            if (valorUnitario <= 0)
            {
                listaErros.AppendLine($"Valor unitário do produto {nome}, deve ser maior que zero.");
                estáValido = false;
            }

            if (quantidade <= 0)
            {
                listaErros.AppendLine($"Quantidade informada para o produto {nome}, deve ser maior que zero.");
                estáValido = false;
            }

            if (dataEntrega <= DateTime.Today)
            {
                listaErros.AppendLine($"Data de entrega do produto {nome}, deve ser maior que hoje.");
                estáValido = false;
            }

            return estáValido;
        }
    }
}
