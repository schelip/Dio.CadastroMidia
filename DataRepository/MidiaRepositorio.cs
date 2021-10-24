using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Dio.CadastroMidia.Interfaces;

namespace Dio.CadastroMidia.DataRepository
{
	[Serializable()]
    public class MidiaRepositorio<T> : IRepositorio<T> where T: MidiaEntidadeBase
    {
		[DataMember]
		public List<T> Lista { get; private set; } = new List<T>();
		public string Key { get; private set; } = typeof(T).Name;

		public void Substitui(int id, T objeto)
		{
			Lista[id] = objeto;
		}

		public void Exclui(int id)
		{
			Lista[id].Excluir();
		}

		public void Restaura(int id)
		{
			Lista[id].Restaurar();
		}

		public void Insere(T objeto)
		{
			Lista.Add(objeto);
		}

		public int ProximoId()
		{
			return Lista.Last().Id  + 1;
		}

		public T RetornaPorId(int id)
		{
			return Lista[id];
		}

		public void Atualiza(int id, PropertyInfo att, object valor, T midia)
		{
			try
			{
				dynamic novo = Convert.ChangeType(valor, att.PropertyType);
				att.SetValue(midia, novo);
			}
			catch (ArgumentException)
			{
				Console.WriteLine($"Erro ao atualizar atributo {att.Name} de tipo {att.GetType()} (Valor passado: {valor.ToString()})");
			}
		}

		public void Carregar(List<T> novaLista)
		{
			Lista = novaLista;
		}
    }
}