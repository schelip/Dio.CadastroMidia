using System;
using Dio.CadastroMidia.DataRepository;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia.DataClasses
{
    public class Serie : MidiaEntidadeBase
    {
        // Atributos
		private int Episodios { get; set; }
		private int Temporadas { get; set; }

        // Métodos
		public Serie(int id, Genero genero, string titulo, string descricao, int ano, int episodios, int temporadas)
		: base(id, genero, titulo, descricao, ano)
		{
			this.Episodios = episodios;
			this.Temporadas = temporadas;
		}

        public override string ToString()
        {
			var retorno = base.ToString();
			retorno += "Total Episodios: "+ this.Episodios + Environment.NewLine;
			retorno += "Total Temporadas: "+ this.Temporadas;
            return retorno;
        }
    }
}