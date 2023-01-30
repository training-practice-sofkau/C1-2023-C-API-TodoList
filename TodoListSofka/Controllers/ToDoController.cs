using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Data;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ToDoController : ControllerBase
	{

		private readonly ToDoAPIDbContext dbContext;

		public ToDoController(ToDoAPIDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public async Task<List<TodoItem>> GetPersonajes()
		{
			//Busca los personajes que no hayan sido eliminados y los retorna
			//var personajeActivo = dbContext.Tareas.Where(r => r.BanActivo != false).ToList();
			//return personajeActivo;

			//Muestra todos los personajes 
			return await dbContext.Tareas.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<Object> Get(int id)
		{
			var personaje = await dbContext.Tareas.FirstOrDefaultAsync(m => m.Id == id);
			if (personaje == null)
				return NotFound("El personaje no existe");
			return Ok(personaje);
		}
	}
}
