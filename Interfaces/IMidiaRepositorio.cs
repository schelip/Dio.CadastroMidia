using System.Reflection;
using Dio.CadastroMidia.DataRepository;

namespace Dio.CadastroMidia.Interfaces
{
    public interface IMidiaRepositorio<T> : IRepositorio<T> where T: MidiaEntidadeBase
    {
        void Atualiza(int id, PropertyInfo att, object valor, MidiaEntidadeBase midia);
    }
}