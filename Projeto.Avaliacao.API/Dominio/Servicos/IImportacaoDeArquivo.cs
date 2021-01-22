using System.Threading.Tasks;

namespace Projeto.Avaliacao.API.Dominio.Servicos
{
    public interface IImportacaoDeArquivo
    {
        Task<string> ImportarArquivo(string arquivo);
    }
}