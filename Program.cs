using System;
using System.Reflection;
using Dio.CadastroMidia.Classes;
using Dio.CadastroMidia.Classes.Util;
using Dio.CadastroMidia.Enum;

namespace Dio.CadastroMidia
{
    class Program
    {
		private static SerieCrud SerieCrud { get; set; } = new SerieCrud();

        // Main
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();
			
			Console.WriteLine();

			while (opcaoUsuario.ToUpper() != "X")
			{
				ObterCrud(opcaoUsuario, out var crud);

				opcaoUsuario = (string)Extensions.InvocarMetodo(crud, "InitCrud");

				if (opcaoUsuario == "T")
					opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

		// Util
        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Midia a seu dispor!!!");
			Console.WriteLine("Com qual tipo de midia deseja trabalhar?");

			foreach (int i in System.Enum.GetValues(typeof(Midia))) {
				Console.WriteLine("{0}- {1}", i, System.Enum.GetName(typeof(Midia), i));
			}
			Console.WriteLine("X- Sair");
			Console.Write("Informe a opção desejada: ");
			
			return Console.ReadLine();
		}

		private static void ObterCrud(string opcaoUsuario, out object crud) 
		{
			int.TryParse(opcaoUsuario, out int opcaoMidia);

			string nomeCrud = System.Enum.GetName(typeof(Midia), opcaoMidia) + "Crud";

			crud = typeof(Program)
				.GetProperty(nomeCrud, BindingFlags.NonPublic | BindingFlags.Static)
				.GetValue(null);
		}
    }
}
