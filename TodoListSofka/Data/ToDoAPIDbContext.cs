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

		public override int SaveChanges()
		{
			foreach (var item in ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Deleted &&
				e.Metadata.GetProperties().Any(x => x.Name == "State")))
			{
				item.State = EntityState.Unchanged;
				item.CurrentValues["State"] = false;
			}

			return base.SaveChanges();
		}
	}
}
