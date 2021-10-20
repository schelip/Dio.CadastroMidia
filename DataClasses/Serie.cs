using System;
using System.Runtime.Serialization;
using Dio.CadastroMidia.DataRepository;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia.DataClasses
{
	[DataContract]
    public class Serie : MidiaEntidadeBase
    {
		[DataMember]
		public int Episodios { get; set; }
		[DataMember]
		public int Temporadas { get; set; }

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