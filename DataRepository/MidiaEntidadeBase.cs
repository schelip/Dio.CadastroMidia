using System;
using System.Runtime.Serialization;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia.DataRepository
{
    [DataContract]
    public abstract class MidiaEntidadeBase : EntidadeBase
    {
        [DataMember]
        public Genero Genero { get; set; }
        [DataMember]
		public string Titulo { get; set; }
        [DataMember]
		public string Descricao { get; set; }
        [DataMember]
		public int Ano { get; set; }

        protected MidiaEntidadeBase(int id, Genero genero, string titulo, string descricao, int ano)
		{
			this.Id = id;
			this.Genero = genero;
			this.Titulo = titulo;
			this.Descricao = descricao;
			this.Ano = ano;
            this.Excluido = false;
		}

        public override string ToString()
		{
			// Environment.NewLine https://docs.microsoft.com/en-us/dotnet/api/system.environment.newline?view=netcore-3.1
            string retorno = "";
            retorno += "Excluido: " + this.Excluido + Environment.NewLine;
            retorno += "Gênero: " + this.Genero.ToString() + Environment.NewLine;
            retorno += "Titulo: " + this.Titulo + Environment.NewLine;
            retorno += "Descrição: " + this.Descricao + Environment.NewLine;
            retorno += "Ano de Início: " + this.Ano + Environment.NewLine;
			return retorno;
		}

        public void Excluir() {
            this.Excluido = true;
        }
    }
}