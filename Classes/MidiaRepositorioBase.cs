using System.Collections.Generic;
using Dio.CadastroMidia.Interfaces;

namespace Dio.CadastroMidia.Classes
{
    public abstract class MidiaRepositorioBase<T> where T: MidiaEntidadeBase
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
    }
}