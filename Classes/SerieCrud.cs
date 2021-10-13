using System;
using System.Reflection;
using Dio.CadastroMidia.Classes.Util;
using Dio.CadastroMidia.Enum;
using Dio.CadastroMidia.Interfaces;

namespace Dio.CadastroMidia.Classes
{
    public class SerieCrud : ICrud<Serie>
    {
        private static MidiaRepositorio<Serie> _repositorio = new MidiaRepositorio<Serie>();
        public MidiaRepositorio<Serie> repositorio { get => _repositorio; }
		public string InitCrud()
		{
			while (true)
			{
				Console.WriteLine();
				
				switch (ObterOperacao())
				{
					case "1":
						Listar();
						break;
					case "2":
						Inserir();
						break;
					case "3":
						Atualizar();
						break;
					case "4":
						Excluir();
						break;
					case "5":
						Visualizar();
						break;
					case "C":
						Console.Clear();
						break;
					case "T":
						return "T";
					case "X":
						return "X";

					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
		
        public void Inserir()
		{
			Console.WriteLine("Inserir nova série");
			
			_repositorio.Insere(NovaSerie(-1));
		}

        public void Listar()
		{
			Console.WriteLine("Listar séries");

			var lista = _repositorio.Lista();

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

        public void Atualizar()
		{
			Console.Write("Digite o id da série: ");
			int.TryParse(Console.ReadLine(), out int indiceSerie);
			Serie serie = _repositorio.RetornaPorId(indiceSerie);
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
				_repositorio.Substitui(indiceSerie, NovaSerie(indiceSerie));
				return;
			}

			int.TryParse(entrada, out int indiceAtributo);

			Console.Write("Informe o novo valor desejado: ");
			string valor = Console.ReadLine();
			_repositorio.Atualiza(indiceSerie, atts[indiceAtributo], valor, serie);
		}

        public void Excluir()
		{
			Console.Write("Digite o id da série: ");
			int.TryParse(Console.ReadLine(), out int indiceSerie);

			_repositorio.Exclui(indiceSerie);
		}

        public void Visualizar()
		{
			Console.Write("Digite o id da série: ");
			int.TryParse(Console.ReadLine(), out int indiceSerie);

			var serie = _repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        // Util

        /// <param name="id"> id da serie gerada (-1 utiliza <c>ProximoId()<c>) </param>
        private static Serie NovaSerie(int id) 
		{
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in System.Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, System.Enum.GetName(typeof(Genero), i));
			}
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

			return new Serie(id: (id == -1) ? _repositorio.ProximoId() : id,
							 genero: (Genero) entradaGenero,
							 titulo: entradaTitulo,
							 ano: entradaAno,
							 descricao: entradaDescricao,
							 episodios: entradaEpisodios,
							 temporadas: entradaTemporadas);
		}

        private static string ObterOperacao()
		{
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("T- Trocar tipo de midia");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}