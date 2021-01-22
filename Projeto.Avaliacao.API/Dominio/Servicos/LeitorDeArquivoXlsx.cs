using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Projeto.Avaliacao.API.Dtos;

namespace Projeto.Avaliacao.API.Dominio.Servicos
{
    public class LeitorDeArquivoXlsx
    {
        public static List<ItemArquivo> Ler(string arquivoXlsx)
        {
            var arquivoByte = Convert.FromBase64String(arquivoXlsx);
            MemoryStream documento = new MemoryStream(arquivoByte);
            List<ItemArquivo> itensArquivo = new List<ItemArquivo>();

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(documento, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

                foreach (Row r in sheetData.Elements<Row>())
                {
                    if (Convert.ToInt32(r.RowIndex.InnerText) == 1)
                        continue;

                    var listaElementos = r.Elements<Cell>().ToList();
                    var dataEntrega = DateTime.FromOADate(Convert.ToDouble(listaElementos[0].CellValue.InnerText));
                    var nomeProduto = listaElementos[1].CellValue.InnerText;

                    if (stringTable != null)
                    {
                        nomeProduto =
                           stringTable.SharedStringTable
                           .ElementAt(int.Parse(nomeProduto)).InnerText;
                    }

                    var quntidade = Convert.ToInt32(listaElementos[2].CellValue.InnerText);
                    var valorUnitario = Convert.ToDecimal(listaElementos[3].CellValue.InnerText);

                    itensArquivo.Add(new ItemArquivo
                    {
                        DataEntrega = dataEntrega,
                        NomeProduto = nomeProduto,
                        Quantidade = quntidade,
                        ValorUnitario = valorUnitario,
                        LinhaImpportada = Convert.ToInt32(r.RowIndex.InnerText)
                    });
                }
            }

            return itensArquivo;
        }
    }
}
