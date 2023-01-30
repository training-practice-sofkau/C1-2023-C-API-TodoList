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
			//var personajeActivo = dbContext.Tareas.Where(r => r.State != false).ToList();
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

		[HttpPost]
		public async Task<object> Post(ToDoCreateDTO tareaDto)
		{
			var nuevaTarea = new TodoItem();
			nuevaTarea.Title= tareaDto.Title;
			nuevaTarea.Description= tareaDto.Description;
			nuevaTarea.Responsible= tareaDto.Responsible;
			nuevaTarea.IsCompleted= tareaDto.IsCompleted;
			nuevaTarea.State = true;

			dbContext.Add(nuevaTarea);
			await dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPut]
		public async Task<Object> Put(TodoItem itemData)
		{
			if (itemData == null || itemData.Id == 0)
				return BadRequest("El ID no es correcto. ");

			var personaje = await dbContext.Tareas.FindAsync(itemData.Id);
			if (personaje == null)
				return NotFound("El personaje no existe. ");
			if (personaje.State == false)
				return NotFound("El ha sido eliminado. ");
			personaje.Title = itemData.Title;
			personaje.Description = itemData.Description;
			personaje.Responsible = itemData.Responsible;
			personaje.IsCompleted = itemData.IsCompleted;
			await dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
