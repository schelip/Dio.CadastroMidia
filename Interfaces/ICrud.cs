using Dio.CadastroMidia.DataRepository;

namespace Dio.CadastroMidia.Interfaces
{
    public interface ICrud<T> where T : EntidadeBase
    {
        IRepositorio<T> Repositorio { get; }
        string InitCrud();
        void Inserir();
        void Listar();
        void Atualizar();
        void Excluir();
        void Restaurar();
        void LimpaExcluidos();
        void Visualizar();
    }
}