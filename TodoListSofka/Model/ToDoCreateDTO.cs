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

		[Required]
		public string Priority { get; set; }
		public bool IsCompleted { get; set; }

		public ToDoCreateDTO(string title, string description, string responsible, string priority,
			bool isCompleted)
		{
			Title = title;
			Description = description;
			Responsible = responsible;
			Priority = priority;
			IsCompleted = isCompleted;
		}

		public ToDoCreateDTO() { }
	}
}
