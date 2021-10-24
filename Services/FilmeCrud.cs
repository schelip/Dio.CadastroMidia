using System;
using System.Drawing;
using Dio.CadastroMidia.DataClasses;
using Dio.CadastroMidia.DataRepository;
using Dio.CadastroMidia.Enum;
using Dio.CadastroMidia.Helpers;

namespace Dio.CadastroMidia.Services
{
    public class FilmeCrud : MidiaCrudBase<Filme>
    {          
        protected override Filme Novo(int id) 
		{
			typeof(Genero).Lista();
			Console.Write("Digite o gênero entre as opções acima >> ");
			int.TryParse(Console.ReadLine(), out int entradaGenero);

			Console.Write("Digite o Título do Filme >> ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Lançamento do Filme >> ");
			int.TryParse(Console.ReadLine(), out int entradaAno);

			Console.Write("Digite a Descrição do Filme >> ");
			string entradaDescricao = Console.ReadLine();

			Console.Write("Digite a duração em minutos >> ");
			int.TryParse(Console.ReadLine(), out int entradaDuracao);

			Image entradaImagem = null;
			if (Program.UsarImagens)
			{
				Console.Write("Digite o caminho para a Imagem do poster (<ENTER> para vazio) >> ");
				string entrada = Console.ReadLine();
				entradaImagem = entrada != ("") ? Image.FromFile(entrada) : null;
			}

			return new Filme(id: (id == -1) ? s_repositorio.ProximoId() : id,
							 genero: (Genero) entradaGenero,
							 titulo: entradaTitulo,
							 ano: entradaAno,
							 descricao: entradaDescricao,
							 duracao: entradaDuracao,
							 imagem: entradaImagem);
		}
    }
}