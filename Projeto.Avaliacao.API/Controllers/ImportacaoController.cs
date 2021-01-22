using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Avaliacao.API.Dominio.Interfaces;
using Projeto.Avaliacao.API.Dominio.Servicos;
using Projeto.Avaliacao.API.Dtos;
using System.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projeto.Avaliacao.API.Controllers
{
    [Route("api/[controller]")]
    public class ImportacaoController : Controller
    {
        private readonly IImportacaoDeArquivo _importacaoDeArquivo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImportacaoRepositorio _importacaoRepositorio;

        public ImportacaoController(IImportacaoDeArquivo importacaoDeArquivo, IUnitOfWork unitOfWork, IImportacaoRepositorio importacaoRepositorio)
        {
            _importacaoDeArquivo = importacaoDeArquivo;
            _unitOfWork = unitOfWork;
            _importacaoRepositorio = importacaoRepositorio;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<DtoImportacao>> Get()
        {
            var importacoes = await _importacaoRepositorio.GetAllAsync();

            return importacoes.Select(x => new DtoImportacao
            {
                DataEntregaMaisProxima = x.DataEntregaMaisProxima,
                DataImportacao = x.DataImportacao,
                Id = x.Id,
                TotalItens = x.TotalItens,
                ValorTotal = x.ValorTotal
            });
        }

        // GET api/values/5
        [HttpGet("{idImportacao}")]
        public async Task<ActionResult> Get(int idImportacao)
        {
            var produtosImportacao = await _importacaoRepositorio.GetAsync(idImportacao);

            if (produtosImportacao == null)
                return NotFound("Importação não existe.");

            return Ok(produtosImportacao.ProdutoEntregas.Select(x => new DtoProdutoEntrega
            {
                DataEntrega = x.DataEntrega,
                NomeProduto = x.Produto.Nome,
                Quantidade = x.Quantidade,
                ValorUnitario = x.Produto.ValorUnitario,
                ValorTotal = x.ValorTotal()
            }).ToList());
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string arquivo)
        {
            var erros = await _importacaoDeArquivo.ImportarArquivo(arquivo);

            await _unitOfWork.CommitAsync();

            if (!string.IsNullOrWhiteSpace(erros))
                return BadRequest(erros);

            return Ok("Arquivo importado com sucesso.");
        }
    }
}
