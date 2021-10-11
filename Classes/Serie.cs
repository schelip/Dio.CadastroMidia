using System;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia.Classes
{
    public class Serie : MidiaEntidadeBase
    {
        // TODO Atributos

        // Métodos
		public Serie(int id, Genero genero, string titulo, string descricao, int ano)
		: base(id, genero, titulo, descricao, ano)
		{

		}
    }
}