using System.ComponentModel.DataAnnotations;

namespace TarefasBackEnd.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        
        [Required]
        public string Nome { get; set; } = string.Empty;
        public bool Concluida { get; set; }
    }
}