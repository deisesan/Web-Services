namespace TarefasBackEnd.Models
{
    public class Usuario
    {
        public Guid id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}