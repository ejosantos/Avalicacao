using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Projeto.Avaliacao.API.Dominio.Entidades
{
    public class RegraException : Exception
    {
        private readonly Expression<Func<object, object>> _objeto = x => x;
        protected readonly IList<ViolacaoDeRegra> _erros = new List<ViolacaoDeRegra>();
        public IEnumerable<ViolacaoDeRegra> Erros { get { return _erros; } }

        internal void AdicionarErroAoModelo(string mensagem)
        {
            _erros.Add(new ViolacaoDeRegra { Propriedade = _objeto, Mensagem = mensagem });
        }

        public RegraException Quando(bool condicao, string mensagem)
        {
            if (condicao)
                AdicionarErroAoModelo(mensagem);

            return this;
        }

        public void EntaoDispara()
        {
            if (_erros.Any())
                throw this;
        }
    }

    public class RegraException<TModelo> : RegraException
    {
        internal void AdicionarErroPara<TPropriedade>(Expression<Func<TModelo, TPropriedade>> propriedade, string mensagem)
        {
            _erros.Add(new ViolacaoDeRegra { Propriedade = propriedade, Mensagem = mensagem });
        }

        public bool PossuiErroComAMensagemIgualA(string mensagem)
        {
            return Erros.Any(e => e.Mensagem.Equals(mensagem));
        }
    }

    public static class ExtensoesDeRegrasException
    {
        public static void DispararExcecaoComMensagem(this RegraException dominioException, string mensagem)
        {
            dominioException.AdicionarErroAoModelo(mensagem);
            throw dominioException;
        }

        public static string MensagensDeErroEmTexto(this RegraException ex)
        {
            var stringBuilder = new StringBuilder();
            foreach (var erro in ex.Erros)
            {
                stringBuilder.Append(erro.Mensagem);
                stringBuilder.Append(" ");
            }
            return stringBuilder.Length > 0 ? stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString() : stringBuilder.ToString();
        }
    }

    public class ViolacaoDeRegra
    {
        public LambdaExpression Propriedade { get; internal set; }
        public string Mensagem { get; internal set; }
    }
}
