using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Dto
{
    public class TodoItemComplete
    {
        [Required]
        public int IsCompleted { get; set; }
    

}
}
