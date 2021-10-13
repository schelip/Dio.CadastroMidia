using System;
using Dio.CadastroMidia.DataRepository;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia.DataClasses
{
    public class Filme : MidiaEntidadeBase
    {
        // Atributos
        private int Duracao { get; set; }

        // Métodos
        public Filme(int id, Genero genero, string titulo, string descricao, int ano, int duracao)
        : base(id, genero, titulo, descricao, ano)
        {
            this.Duracao = duracao;
        }

        public override string ToString()
        {
			var retorno = base.ToString();
			retorno += "Duracao: "+ this.Duracao + Environment.NewLine;
            return retorno;
        }
    }
}