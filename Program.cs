using System;
using System.Reflection;
using Dio.CadastroMidia.Classes;
using Dio.CadastroMidia.Classes.Util;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia
{
    class Program
    {
        static MidiaRepositorio<Serie> repositorio = new MidiaRepositorio<Serie>();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;
					case "X":
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

		private static void ListarSeries()
		{
			Console.WriteLine("Listar séries");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova série");
			
			repositorio.Insere(NovaSerie(-1));
		}

		private static void AtualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie;
			int.TryParse(Console.ReadLine(), out indiceSerie);
			Serie serie = repositorio.RetornaPorId(indiceSerie);
			PropertyInfo[] atts = serie.GetType().GetProperties(BindingFlags.NonPublic|BindingFlags.Instance);
			atts = atts.SubArray(0, atts.Length - 2);

			int i = 0;
			foreach (var att in atts)
			{
				Console.WriteLine("{0} - {1}: {2}", i++, att.Name, att.GetValue(serie, null));
			}
			Console.WriteLine("T - TODOS OS CAMPOS");
			
			Console.Write("Informe qual campo deseja atualizar entre as opções acima: ");
			
			string entrada = Console.ReadLine();
			
			if (entrada == "T")
			{
				repositorio.Substitui(indiceSerie, NovaSerie(indiceSerie));
				return;
			}

			int indiceAtributo;
			int.TryParse(entrada, out indiceAtributo);

			Console.Write("Informe o novo valor desejado: ");
			string valor = Console.ReadLine();
			repositorio.Atualiza(indiceSerie, atts[indiceAtributo], valor, serie);
		}

        private static void ExcluirSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie;
			int.TryParse(Console.ReadLine(), out indiceSerie);

			repositorio.Exclui(indiceSerie);
		}

        private static void VisualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie;
			int.TryParse(Console.ReadLine(), out indiceSerie);

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

		// Util
        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}

		private static Serie NovaSerie(int id) // Id == -1 -> ProximoId()
		{
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in System.Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, System.Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero;
			int.TryParse(Console.ReadLine(), out entradaGenero);

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno;
			int.TryParse(Console.ReadLine(), out entradaAno);

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Console.Write("Digite o Número de Episódios: ");
			int entradaEpisodios;
			int.TryParse(Console.ReadLine(), out entradaEpisodios);

			Console.Write("Digite o Número de Temporadas: ");
			int entradaTemporadas;
			int.TryParse(Console.ReadLine(), out entradaTemporadas);

			return new Serie(id: (id == -1) ? repositorio.ProximoId() : id,
							 genero: (Genero) entradaGenero,
							 titulo: entradaTitulo,
							 ano: entradaAno,
							 descricao: entradaDescricao,
							 episodios: entradaEpisodios,
							 temporadas: entradaTemporadas);
		}
    }
}
