namespace MultiAtendimento.API.Models.DTOs
{
    public class RetornoPadraoView<T> where T : class, new()
    {
        public string Mensagem { get; set; }
        public T Resultado { get; set; } = new T();
    }
}
