using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model
{
    public class TodoItemAgregar
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        [Required]
        public string? Responsible { get; set; }
        [Required]
        public int IsCompleted { get; set; }

    }
}
