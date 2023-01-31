using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Dto
{
    public class TodoItemComplete
    {
        [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
        public int IsCompleted { get; set; }
    

}
}
