using System.Collections.Generic;
using System.Reflection;

namespace Dio.CadastroMidia.Interfaces
{
    public interface IRepositorio<T>
    {
        List<T> Lista { get; }
        T RetornaPorId(int id);    
        void Insere(T entidade);        
        void Exclui(int id);     
        void Substitui(int id, T entidade);
        int ProximoId();
        void Atualiza(int id, PropertyInfo att, object valor, T midia);
    }
}