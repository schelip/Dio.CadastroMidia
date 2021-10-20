using System;
using System.Drawing;
using Dio.CadastroMidia.DataClasses;
using Dio.CadastroMidia.DataRepository;
using Dio.CadastroMidia.Helpers;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia.Services
{
    public class SerieCrud : MidiaCrudBase<Serie>
    {
        protected override Serie Novo(int id) 
		{
			typeof(Genero).Lista();
			Console.Write("Digite o gênero entre as opções acima: ");
			int.TryParse(Console.ReadLine(), out int entradaGenero);

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int.TryParse(Console.ReadLine(), out int entradaAno);

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Console.Write("Digite o Número de Episódios: ");
			int.TryParse(Console.ReadLine(), out int entradaEpisodios);

			Console.Write("Digite o Número de Temporadas: ");
			int.TryParse(Console.ReadLine(), out int entradaTemporadas);

			Image entradaImagem = null;
			if (Program.UsarImagens)
			{
				Console.Write("Digite o caminho para a Imagem de capa (<ENTER> para vazio): ");
				entradaImagem = Image.FromFile(Console.ReadLine());
			}

			return new Serie(id: (id == -1) ? s_repositorio.ProximoId() : id,
							 genero: (Genero) entradaGenero,
							 titulo: entradaTitulo,
							 ano: entradaAno,
							 descricao: entradaDescricao,
							 episodios: entradaEpisodios,
							 temporadas: entradaTemporadas,
							 imagem: entradaImagem);
		}
    }
}