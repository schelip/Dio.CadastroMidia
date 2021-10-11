using System;
using System.Collections.Generic;
using System.Reflection;
using Dio.CadastroMidia.Interfaces;

namespace Dio.CadastroMidia.Classes
{
    public class MidiaRepositorio<T> : IMidiaRepositorio<T> where T: MidiaEntidadeBase
    {
        private List<T> lista = new List<T>();
		public void Substitui(int id, T objeto)
		{
			lista[id] = objeto;
		}

		public void Exclui(int id)
		{
			lista[id].Excluir();
		}

		public void Insere(T objeto)
		{
			lista.Add(objeto);
		}

		public List<T> Lista()
		{
			return lista;
		}

		public int ProximoId()
		{
			return lista.Count;
		}

		public T RetornaPorId(int id)
		{
			return lista[id];
		}

		public void Atualiza(int id, PropertyInfo att, object valor, MidiaEntidadeBase midia)
		{
			try
			{
				att.SetValue(midia, valor);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
    }
}