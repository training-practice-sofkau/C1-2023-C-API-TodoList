using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Dto
{
    public class TodoItemActualizar
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        [Required]
        public string? Responsible { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }
}
