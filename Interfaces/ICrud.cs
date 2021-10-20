using Dio.CadastroMidia.DataRepository;

namespace Dio.CadastroMidia.Interfaces
{
    public interface ICrud<T> where T : MidiaEntidadeBase
    {
        MidiaRepositorio<T> Repositorio { get; }
        string InitCrud();
        void Inserir();
        void Listar();
        void Atualizar();
        void Excluir();
        void Visualizar();
    }
}