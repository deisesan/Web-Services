using System.ComponentModel.DataAnnotations;

namespace TarefasBackEnd.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Senha { get; set; } = string.Empty;
    }
}