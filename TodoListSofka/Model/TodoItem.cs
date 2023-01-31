using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model
{
    public class TodoItem
    {
        public int Id { get; set; }
        [RegularExpression("^[1-9]{1}[0-9]{7}$",
            ErrorMessage = "Ingresar DNI sin puntos")]
        [Required] public string Title { get; set; } = null!;
        [RegularExpression("^[a-zA-Z]{1,25}$",
           ErrorMessage = "Solo se permiten letras en este campo y debe contener entre 1 y 25 caracteres sin espacios")]
        [Required] public string Description { get; set; } = null!;
        [RegularExpression("^[a-zA-Z]{1,25}$",
           ErrorMessage = "Solo se permiten letras en este campo y debe contener entre 1 y 25 caracteres sin espacios")]
        [Required] public string Responsible { get; set; } = null!;
        [RegularExpression("^[a-zA-Z]{1,25}$",
           ErrorMessage = "Solo se permiten letras en este campo y debe contener entre 1 y 25 caracteres sin espacios")]
        [Required] public string Priority { get; set; } = null!;
        [RegularExpression("^[a-zA-Z]{1,25}$",
           ErrorMessage = "Solo se permiten letras en este campo y debe contener entre 1 y 25 caracteres sin espacios")]

        public bool IsCompleted { get; set; }

        //para el borrado lógico implementar bool o int según su preferencia.
        //si es int puedo colcocar un rango entre o y 1
        public bool State { get; set; }

        public TodoItem(int id, string title, string description, string responsible, 
            bool isCompleted, bool state)
        {
            Id = id;
            Title = title;
            Description = description;
            Responsible = responsible;
            IsCompleted = isCompleted;
            State = state;
        }

        public TodoItem() { }



    }
}
