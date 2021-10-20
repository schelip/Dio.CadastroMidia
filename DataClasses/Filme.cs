using System;
using System.Drawing;
using System.Runtime.Serialization;
using Dio.CadastroMidia.DataRepository;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia.DataClasses
{
    [DataContract]
    public class Filme : MidiaEntidadeBase
    {
        [DataMember]
        public int Duracao { get; set; }

        public Filme(int id, Genero genero, string titulo, string descricao, int ano, int duracao, Image imagem)
        : base(id, genero, titulo, descricao, ano, imagem)
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