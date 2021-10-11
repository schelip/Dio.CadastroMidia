using System.Collections.Generic;

namespace Dio.CadastroMidia.Interfaces
{
    public interface IRepositorio<T>
    {
        List<T> Lista();
        T RetornaPorId(int id);        
        void Insere(T entidade);        
        void Exclui(int id);        
        void Substitui(int id, T entidade);
        int ProximoId();
    }
}