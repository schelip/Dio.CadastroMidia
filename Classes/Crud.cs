using System;
using System.Reflection;
using System.Collections.Generic;
using Dio.CadastroMidia.Classes.Util;
using Dio.CadastroMidia.Interfaces;

namespace Dio.CadastroMidia.Classes
{
    public abstract class Crud<T> : ICrud<T> where T: MidiaEntidadeBase
    {
        protected static MidiaRepositorio<T> _repositorio = new MidiaRepositorio<T>();
        public MidiaRepositorio<T> repositorio { get => _repositorio; }
      
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
			Console.WriteLine("Inserindo novo registro");
			
			_repositorio.Insere(Novo(-1));
		}
       
        public void Listar()
		{
			Console.WriteLine("Listando registros");

			List<T> lista = _repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nada cadastrado.");
				return;
			}

			foreach (T midia in lista)
			{
                bool excluido = midia.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", midia.retornaId(), midia.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}
       
        public void Atualizar()
        {
            int id = ObterId();
			T midia = _repositorio.RetornaPorId(ObterId());
			PropertyInfo[] atts = midia.GetType().GetProperties(BindingFlags.NonPublic|BindingFlags.Instance);
			atts = atts.SubArray(0, atts.Length - 2);

			int i = 0;
			foreach (var att in atts)
			{
				Console.WriteLine("{0} - {1}: {2}", i++, att.Name, att.GetValue(midia, null));
			}
			Console.WriteLine("T - TODOS OS CAMPOS");
			
			Console.Write("Informe qual campo deseja atualizar entre as opções acima: ");
			
			string entrada = Console.ReadLine();
			
			if (entrada == "T")
			{
				_repositorio.Substitui(id, Novo(id));
				return;
			}

			int.TryParse(entrada, out int indiceAtributo);

			Console.Write("Informe o novo valor desejado: ");
			string valor = Console.ReadLine();
			_repositorio.Atualiza(id, atts[indiceAtributo], valor, midia);
		}
        
        public void Excluir()
		{
			_repositorio.Exclui(ObterId());
		}
        
        public void Visualizar()
		{
			T midia = _repositorio.RetornaPorId(ObterId());

			Console.WriteLine(midia);
		}

		// Util
        private static string ObterOperacao()
		{
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Inserir");
			Console.WriteLine("3- Atualizar");
			Console.WriteLine("4- Excluir");
			Console.WriteLine("5- Visualizar");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("T- Trocar tipo de midia (Atual: {0})", typeof(T).Name);
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}

        private static int ObterId()
        {
            Console.Write("Digite o id do registro: ");
			int.TryParse(Console.ReadLine(), out int id);
            return id;
        }

		/// <param name="id"> id do objeto MidiaEntidadeBasica à
        /// ser gerado (-1 utiliza <c>ProximoId()<c>) </param>
        protected abstract T Novo(int id);
    }
}