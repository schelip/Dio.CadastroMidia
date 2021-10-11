using System;
using System.Collections.Generic;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia.Classes
{
    public abstract class MidiaEntidadeBase : EntidadeBase
    {
        // Atributos
        protected Genero Genero { get; set; }
		protected string Titulo { get; set; }
		protected string Descricao { get; set; }
		protected int Ano { get; set; }

        // Métodos
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

        public string retornaTitulo()
		{
			return this.Titulo;
		}

        public void Excluir() {
            this.Excluido = true;
        }
    }
}