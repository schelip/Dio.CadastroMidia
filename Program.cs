using System;
using System.Collections.Generic;
using System.IO;
using Dio.CadastroMidia.Enum;
using Dio.CadastroMidia.Helpers;

namespace Dio.CadastroMidia
{
    class Program
    {
		private static Dictionary<string, object> s_cruds = new Dictionary<string, object>();

        static void Main(string[] args)
        {
			Console.WriteLine();
			Console.WriteLine("DIO Midia a seu dispor!!!");

			ObterCruds();
			Carregar();

            string opcaoUsuario = ObterOpcaoUsuario();
			
			Console.WriteLine();

			while (opcaoUsuario.ToUpper() != "X")
			{
				int.TryParse(opcaoUsuario, out int opcaoMidia);
				var crud = s_cruds[(System.Enum.GetName(typeof(Midia), opcaoMidia))];

				opcaoUsuario = (string)Extensions.InvocarMetodo(crud, "InitCrud");

				if (opcaoUsuario == "T")
					opcaoUsuario = ObterOpcaoUsuario();
			}

			Salvar();

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

		// Util
        private static string ObterOpcaoUsuario()
		{	
			Console.WriteLine("Com qual tipo de midia deseja trabalhar?");

			foreach (int i in System.Enum.GetValues(typeof(Midia))) {
				Console.WriteLine("{0}- {1}", i, System.Enum.GetName(typeof(Midia), i));
			}
			Console.WriteLine("X- Sair");
			Console.Write("Informe a opção desejada: ");
			
			return Console.ReadLine();
		}

		private static void ObterCruds()
		{
			foreach (string midia in System.Enum.GetNames(typeof(Midia)))
			{
				string nomeCrud = "Dio.CadastroMidia.Services." + midia + "Crud";

				Type crudType = Type.GetType(nomeCrud);
				var crud = System.Activator.CreateInstance(crudType);
				s_cruds.Add(midia, crud);
			}
		}

		private static void Carregar()
		{
			string dir = Environment.CurrentDirectory + @"\SavedData\";
			if (!Directory.Exists(dir) || Extensions.isDiretorioVazio(dir))
			{
				Console.WriteLine("Nenhum dado salvo encontrado");
				return;
			}

			foreach (string key in s_cruds.Keys)
			{
				string filename = dir + key + ".xml";
				dynamic crud = s_cruds[key];
				crud.Repositorio.Carregar(Extensions.Carregar(filename, crud.Repositorio.Lista));
			}
		}

		private static void Salvar()
		{
			string dir = Environment.CurrentDirectory + @"\SavedData\";
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			foreach (string key in s_cruds.Keys)
			{
				string filename = dir + key + ".xml";
				dynamic crud = s_cruds[key];
				dynamic list = crud.Repositorio.Lista;
				if (list.Count != 0)
					Extensions.Salvar(filename, crud.Repositorio.Lista);
			}
		}
	}
}
