using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Dio.CadastroMidia.Helpers;
using Dio.CadastroMidia.Interfaces;

namespace Dio.CadastroMidia.DataRepository
{
    public abstract class MidiaCrudBase<T> : ICrud<T> where T: MidiaEntidadeBase
    {
		protected MidiaRepositorio<T> s_repositorio = new MidiaRepositorio<T>();
		public MidiaRepositorio<T> Repositorio { get => s_repositorio; }
		private static string s_name = typeof(T).Name;

        public string InitCrud()
		{
			while (true)
			{
				Console.WriteLine();
				string opcao = ObterOperacao();
				switch (opcao)
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
						Restaurar();
						break;
					case "6":
						Visualizar();
						break;
					case "7":
						if (Program.UsarImagens)
							VisualizarImagem();
						else goto default;
						break;
					case "C":
						Program.ImprimirCabecalho();
						break;
					case "T":
						return "T";
					case "X":
						return "X";
					default:
						Console.WriteLine($"Erro: Opção {opcao} inválida.");
						break;
				}
			}
		}
       
        public void Inserir()
		{
			Console.WriteLine($"/// Inserindo {s_name}");

			try
			{
				T midia = Novo(-1);
				s_repositorio.Insere(midia);
				Console.WriteLine($"Objeto {s_name} com ID {midia.Id} Inserido");
			}
			catch (Exception)
			{
				Console.WriteLine("Erro na criação do objeto. Tentar novamente? (S/N) >> ");
				if (Console.ReadLine().ToUpper() == "S")
					Novo(-1);
				else
					return;
			}
		}
       
        public void Listar()
		{
			Console.WriteLine($"/// Listando {s_name}s ");

			if (s_repositorio.Lista.Count == 0)
			{
				Console.WriteLine("Nada cadastrado.");
				return;
			}

			foreach (T midia in s_repositorio.Lista)
			{
                bool excluido = midia.Excluido;
                
				Console.WriteLine("#ID {0:00} | {1} {2}", midia.Id, midia.Titulo, (excluido ? "*Excluído*" : ""));
			}
		}
       
        public void Atualizar()
        {
			Console.WriteLine($"/// Atualizando {s_name}");
			
			T midia = ObterMidia();

			PropertyInfo[] atts = midia.GetType().GetProperties();
			atts = atts.Where(att => att.Name != "Imagem")
					.ToArray()
					.SubArray(0, atts.Length - 2);

			int i = 0;
			foreach (var att in atts)
			{
				Console.WriteLine(" {0:00} | {1}: {2}", i++, att.Name, att.GetValue(midia, null));
			}
			if (Program.UsarImagens)
				Console.WriteLine(" {0:00} | Atualizar imagem", i);
			Console.WriteLine(" T  | TODOS OS CAMPOS");
			
			Console.Write("Informe qual campo deseja atualizar entre as opções acima >> ");
			
			string entrada = Console.ReadLine().ToUpper();
			
			if (entrada == "T")
			{
				try
				{
					s_repositorio.Substitui(midia.Id, Novo(midia.Id));
					return;
				}
				catch (Exception)
				{
					Console.WriteLine("Erro na criação do objeto. Tentar novamente? (S/N) >> ");
					if (Console.ReadLine().ToUpper() == "S")
						s_repositorio.Substitui(midia.Id, Novo(midia.Id));
					else
						return;
				}
			}

			if (entrada == i.ToString())
			{
				Console.Write("Informe o caminho para a nova imagem >> ");
				string caminho = Console.ReadLine();
				try
				{
					midia.Imagem = Image.FromFile(caminho);
				}
				catch (Exception)
				{
					Console.WriteLine("Erro ao carregar a imagem");
				}
				return;
			}

			try
			{
				int.TryParse(entrada, out int indiceAtributo);
				PropertyInfo selected = atts[indiceAtributo];
			
				if (selected.PropertyType.IsEnum)
					selected.PropertyType.Lista();

				Console.Write("Informe o novo valor desejado >> ");
				string valor = Console.ReadLine();
				s_repositorio.Atualiza(midia.Id, selected, valor, midia);
			}
			catch (ArgumentException)
			{
				Console.WriteLine("Erro: opção inválida");
				return;
			}

			Console.WriteLine($"Objeto {s_name} com ID {midia.Id} Atualizado");
		}
        
        public void Excluir()
		{
			Console.WriteLine($"/// Excluindo {s_name}");
			try
			{
				int id = ObterId();
				s_repositorio.Exclui(id);
				Console.WriteLine($"Objeto {s_name} com ID {id} Excluído");
			}
			catch (ArgumentOutOfRangeException)
			{
				Console.WriteLine("Erro: ID não encontrado");
				return;
			}
		}

		public void Restaurar()
		{
			Console.WriteLine($"/// Restaurando {s_name}");
			try
			{
				int id = ObterId();
				s_repositorio.Restaura(id);
				Console.WriteLine($"Objeto {s_name} com ID {id} Restaurado");
			}
			catch (ArgumentOutOfRangeException)
			{
				Console.WriteLine("Erro: ID não encontrado");
				return;
			}
		}
        
        public void Visualizar()
		{
			Console.WriteLine($"/// Visualizar {s_name}:");
			
			T midia = ObterMidia();
			if (midia == null) return;

			if (Program.UsarImagens && midia.Imagem != null)
			{
				string[] strings = midia.ToString().Split("\n");
				Image thumb = Drawing.ToThubmnail(midia.Imagem, 55);
				Drawing.ImprimeImagem(thumb, strings);
			}
			else Console.WriteLine(midia);
			Console.WriteLine("<Pressione qualquer tecla para continuar>");
			Console.ReadLine();
		}

		public void VisualizarImagem()
		{
			T midia = ObterMidia();
			if (midia == null) return;

			if (midia.Imagem == null)
			{
				Console.WriteLine("Nenhuma imagem cadastrada");
				return;
			}

			Image thumb = Drawing.ToThubmnail(midia.Imagem, 118);
			Drawing.ImprimeImagem(thumb);
			Console.WriteLine("<Pressione qualquer tecla para continuar>");
			Console.ReadLine();
		}

		public void LimpaExcluidos()
		{
			s_repositorio.Carregar(s_repositorio.Lista.Where(m => !m.Excluido).ToList());
		}

		// Util
        private static string ObterOperacao()
		{
			Console.WriteLine($"Midia <{s_name}> Selecionada:");
			Console.WriteLine(" 01 | Listar");
			Console.WriteLine(" 02 | Inserir");
			Console.WriteLine(" 03 | Atualizar");
			Console.WriteLine(" 04 | Excluir");
			Console.WriteLine(" 05 | Restaurar");
			Console.WriteLine(" 06 | Visualizar");
			if (Program.UsarImagens)
				Console.WriteLine(" 07 | Visualizar imagem de capa");
			Console.WriteLine(" C  | Limpar Tela");
			Console.WriteLine(" T  | Trocar tipo de midia (Atual: {0})", s_name);
			Console.WriteLine(" X  | Sair");
			Console.Write("Informe a opção desejada >> ");

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();

			return opcaoUsuario;
		}

        private T ObterMidia()
        {
            Console.Write("Digite o id do registro >> ");
			int.TryParse(Console.ReadLine(), out int id);

			T midia = null;
			try
			{
				midia = s_repositorio.RetornaPorId(id);
			}
			catch (ArgumentOutOfRangeException)
			{
				Console.WriteLine("Erro: ID não encontrado");
			}

            return midia;
        }

		private int ObterId()
        {
            Console.Write("Digite o id do registro >> ");
			int.TryParse(Console.ReadLine(), out int id);
			return id;
		}

		/// <param name="id">
		///	id do objeto MidiaEntidadeBasica à ser gerado (-1 utiliza <c>ProximoId()<c>)
		/// </param>
        protected abstract T Novo(int id);
    }
}