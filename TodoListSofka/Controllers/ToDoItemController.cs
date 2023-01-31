using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Data;
using TodoListSofka.Model;
using TodoListSofka.DTO;


namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : Controller
    {
        //creamos una variable del contexto
        private readonly DatabaseFirstBloggingContext dbContext;

        public ToDoListController(DatabaseFirstBloggingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Se traen todos los items
        [HttpGet("AllItems/Get")]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                //consulta a la db mediante linq + DTO para get de todos los elementos

                var toDoItems = from item in dbContext.ToDoItems
                                where item.State
                                select new GetToDoItemDTO()
                                {
                                    Title = item.Title,
                                    Description = item.Description,
                                    Responsible = item.Responsible,
                                    IsCompleted = item.IsCompleted
                                };

                if (toDoItems.Count() != 0 && toDoItems != null)
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

        //Se traen todos los items sin completar
        [HttpGet("UncompleteItems/Get")]
        public async Task<IActionResult> GetUncompleteItems()
        {
            try
            {
                //consulta a la db mediante linq + DTO para get de elementos sin completar
                var toDoItems = from item in dbContext.ToDoItems
                                where item.State && !item.IsCompleted
                                select new GetToDoItemDTO()
                                {
                                    Title = item.Title,
                                    Description = item.Description,
                                    Responsible = item.Responsible,
                                    IsCompleted = item.IsCompleted
                                };

                if (toDoItems.Count() != 0 && toDoItems != null)
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
        [HttpGet("{id:guid}/Get")]
        public async Task<IActionResult> GetUniqueItem([FromRoute] Guid id)
        {
            try
            {
                //Get de un item con LINQ + DTO
                var toDoItems = from item in dbContext.ToDoItems
                                where item.State && item.ItemId == id
                                select new GetToDoItemDTO()
                                {
                                    Title = item.Title,
                                    Description = item.Description,
                                    Responsible = item.Responsible,
                                    IsCompleted = item.IsCompleted
                                };

                if (toDoItems.Count() != 0 && toDoItems != null)
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

        //Añadir items con DTO
        [HttpPost]
        public async Task<IActionResult> AddItem(AddToDoItemDTO addToDoItemDTO)
        {
            try
            {
                var ToDoItem = new ToDoItem()
                {
                    ItemId = Guid.NewGuid(),
                    Title = addToDoItemDTO.Title,
                    Description = addToDoItemDTO.Description,
                    Responsible = addToDoItemDTO.Responsible,
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

        //Actulizar item completo DTO
        [HttpPut("{id:guid}/UpdateAll")]
        public async Task<IActionResult> UpdateAllItem([FromRoute] Guid id, UpdateToDoItemDTO updateToDoItemDTO)
        {
            try
            {
                var ToDoItem = await dbContext.ToDoItems.Where(list => list.State && list.ItemId == id)
                    .ToListAsync();

                if (ToDoItem.Count() != 0 && ToDoItem != null)
                {
                    foreach (var item in ToDoItem)
                    {
                        item.Title = updateToDoItemDTO.Title;
                        item.Description = updateToDoItemDTO.Description;
                        item.Responsible = updateToDoItemDTO.Responsible;
                        item.IsCompleted = updateToDoItemDTO.IsCompleted;
                    }
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

        //Actulizar el IsCompleted DTO
        [HttpPut("{id:guid}/UpdateIsCompleted")]
        public async Task<IActionResult> UpdateIsCompleted([FromRoute] Guid id, bool isCompleted)
        {
            try
            {
                var ToDoItem = await dbContext.ToDoItems.Where(list => list.State && !list.IsCompleted && list.ItemId == id)
                    .ToListAsync();

                if (ToDoItem.Count() != 0 && ToDoItem != null)
                {
                    foreach (var item in ToDoItem)
                    {
                        item.IsCompleted = isCompleted;
                    }
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

        //delete item con DTO
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            try
            {
                var ToDoItem = await dbContext.ToDoItems.Where(list => list.State && list.ItemId == id)
                    .ToListAsync();

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
