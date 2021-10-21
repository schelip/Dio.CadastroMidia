using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace Dio.CadastroMidia.Helpers
{
    public static class Extensions
    {
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }

		public static void Lista(this Type type)
		{
			if (!type.IsEnum)
				throw new ArgumentException();
			
			foreach (int value in System.Enum.GetValues(type))
			{
				Console.WriteLine("{0}-{1}", value, System.Enum.GetName(type, value));
			}
		}

        public static object InvocarMetodo(object obj, string name)
        {
            return obj.GetType().GetMethod(name).Invoke(obj, null);
        }

        public static object InvocarMetodo(object obj, string name, params object[] parameters)
        {
            return obj.GetType().GetMethod(name).Invoke(obj, parameters);
        }

		public static bool isDiretorioVazio(string path)
		{
			return !Directory.EnumerateFileSystemEntries(path).Any();
		}

        public static void Salvar(string path, object obj)
        {
			string filename = path.Split('\\').Last();
			Console.WriteLine($"Salvando {filename} ...");
			try
			{
				using (FileStream fs = new FileStream(path, FileMode.Create))
				using (XmlWriter writer = XmlWriter.Create(fs, new XmlWriterSettings{Indent = true}))
				{
					DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
					serializer.WriteObject(writer, obj);
				}

				Console.WriteLine("Dados salvos com sucesso!");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }

		public static object Carregar(string path, object obj)
		{
			string filename = path.Split('\\').Last();
			Console.WriteLine($"Carregando {filename} ...");
			
			try
			{
				using (FileStream fs = new FileStream(path, FileMode.Open))
				using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
				{

					DataContractSerializer serializer = new DataContractSerializer(obj.GetType());

					obj = serializer.ReadObject(reader, true);
				}

				Console.WriteLine($"{filename} com sucesso!");
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("Nenhum arquivo de dados encontrado");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return obj;
		}
    }
}