namespace Dio.CadastroMidia.DataRepository
{
    public abstract class EntidadeBase
    {
        protected int Id { get; set; }
        protected bool Excluido { get; set; }

        public int retornaId()
		{
			return this.Id;
		}
        public bool retornaExcluido()
		{
			return this.Excluido;
		}
    }
}