using System.Runtime.Serialization;

namespace Dio.CadastroMidia.DataRepository
{
    [DataContract]
    public abstract class EntidadeBase
    {
        [DataMember]
        public int Id { get; protected set; }
        [DataMember]
        public bool Excluido { get; protected set; }

         public void Excluir() {
            this.Excluido = true;
        }

        public void Restaurar() {
            this.Excluido = false;
        }
    }
}