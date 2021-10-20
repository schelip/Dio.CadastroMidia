using System;
using Dio.CadastroMidia.DataClasses;
using Dio.CadastroMidia.DataRepository;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia.Services
{
    public class FilmeCrud : MidiaCrudBase<Filme>
    {          
        protected override Filme Novo(int id) 
		{
			foreach (int i in System.Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, System.Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int.TryParse(Console.ReadLine(), out int entradaGenero);

			Console.Write("Digite o Título do Filme: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Lançamento do Filme: ");
			int.TryParse(Console.ReadLine(), out int entradaAno);

			Console.Write("Digite a Descrição do Filme: ");
			string entradaDescricao = Console.ReadLine();

			Console.Write("Digite a duração em minutos: ");
			int.TryParse(Console.ReadLine(), out int entradaDuracao);

			return new Filme(id: (id == -1) ? s_repositorio.ProximoId() : id,
							 genero: (Genero) entradaGenero,
							 titulo: entradaTitulo,
							 ano: entradaAno,
							 descricao: entradaDescricao,
							 duracao: entradaDuracao);
		}
    }
}