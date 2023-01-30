using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Data;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : Controller
    {
        private readonly DatabaseFirstBloggingContext dbContext;

        public ToDoListController(DatabaseFirstBloggingContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Se traen todos los items
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var toDoItems = await dbContext.ToDoItems.Where(list => list.State != false).ToListAsync();


                if (toDoItems.Count != 0 && toDoItems != null)
                {
                    return Ok(toDoItems);
                }
                return BadRequest(new { code = 404, message = "No hay elementos para mostrar" });

            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay elementos para mostrar: {e.Message}" });

            }
        }
        //Se trae un item
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTask([FromRoute] Guid id)
        {
            try
            {
                var toDoItems = await dbContext.ToDoItems.Where(list => list.State != false && list.ItemId == id).ToListAsync();

                if (toDoItems.Count != 0 && toDoItems != null)
                {
                    return Ok(toDoItems);
                }
                return BadRequest(new { code = 404, message = "No hay un elemento con este id" });

            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay un elemento con este id: {e.Message}" });

            }
        }
        //Añadir items
        [HttpPost]
        public async Task<IActionResult> AddTask(AddToDoItem addToDoItem)
        {
            try
            {
                var ToDoItem = new ToDoItem()
                {
                    ItemId = Guid.NewGuid(),
                    Title = addToDoItem.Title,
                    Description = addToDoItem.Description,
                    Responsible = addToDoItem.Responsible,
                    IsCompleted = false,
                    State = true
                };

                await dbContext.ToDoItems.AddAsync(ToDoItem);
                await dbContext.SaveChangesAsync();

                return Ok(ToDoItem);
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo añadir el elemento: {e.Message}" });
            }

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, UpdateToDoItem updateToDoItem)
        {
            try
            {
                var ToDoItem = await dbContext.ToDoItems.FindAsync(id);

                if (ToDoItem != null)
                {
                    ToDoItem.Title = updateToDoItem.Title;
                    ToDoItem.Description = updateToDoItem.Description;
                    ToDoItem.Responsible = updateToDoItem.Responsible;
                    ToDoItem.IsCompleted = updateToDoItem.IsCompleted;

                    await dbContext.SaveChangesAsync();
                    return Ok(ToDoItem);
                }
                return BadRequest(new { code = 404, message = "No hay un elemento con este id" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo modificar el elemento: {e.Message}" });
            }


        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            try
            {
                var ToDoItem = await dbContext.ToDoItems.Where(list => list.State != false && list.ItemId == id).ToListAsync();

                if (ToDoItem.Count != 0 && ToDoItem != null)
                {
                    foreach (var item in ToDoItem)
                    {
                        item.State = false;
                    }
                    await dbContext.SaveChangesAsync();
                    return Ok(ToDoItem);
                }
                return BadRequest(new { code = 404, message = "No hay un elemento con este id" });

            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo eliminar el elemento: {e.Message}" });
            }
        }
    }
}
