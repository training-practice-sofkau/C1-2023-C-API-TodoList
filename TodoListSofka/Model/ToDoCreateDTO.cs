using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model
{
	public class ToDoCreateDTO
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Responsible { get; set; }
		public bool IsCompleted { get; set; }

		public ToDoCreateDTO(string title, string description, string responsible,
			bool isCompleted)
		{
			Title = title;
			Description = description;
			Responsible = responsible;
			IsCompleted = isCompleted;
		}

		public ToDoCreateDTO() { }
	}
}
