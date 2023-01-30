using Microsoft.EntityFrameworkCore;
using TodoListSofka.Model;

namespace TodoListSofka.Data
{
	public class ToDoAPIDbContext : DbContext
	{
		public DbSet<TodoItem> Tareas { get; set; }

		public ToDoAPIDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
