using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {

        private readonly TodolistContext _dbContext;

        public TodoListController(TodolistContext dbContext)
        {

            _dbContext = dbContext;

        }

        //Metodo listar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetItems()
        {

            var activeRecords = _dbContext.TodoItems.Where(r => r.Estate != 0).ToList();


            return activeRecords;

        }

        //Consultar un solo registro
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<TodoItem>> GetItem([FromRoute] int id)
        {

            var item = await _dbContext.TodoItems.FindAsync(id);
            // var activeRecords = _dbContext.Programmers.Where(r => r.IsActive != 0).ToList();

            if (_dbContext == null)
            {

                return NotFound();

            }

            if (item == null || item.Estate == 0)
            {

                return BadRequest(new
                {

                    code = 400,
                    message = "No existe usuario con ese id por favor ingresar Id valido"

                });

            }

            return Ok(item);
        }


        //Metodo Post
        [HttpPost]
        public async Task<ActionResult> PostItem(TodoItemAgregar item)
        {
            var items = new TodoItem()
            {
                Title = item.Title,
                Descripcion = item.Descripcion,
                Responsible = item.Responsible,
                IsCompleted = item.IsCompleted,
                Estate = 1
            };


            await _dbContext.TodoItems.AddAsync(items);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> ActualizarItem([FromRoute] int id, TodoItemActualizar todoitemAc)
        {
            var item = await _dbContext.TodoItems.FindAsync(id);

            if (item != null)
            {

                item.Title = todoitemAc.Title;
                item.Descripcion = todoitemAc.Descripcion;
                item.Responsible = todoitemAc.Responsible;
                item.IsCompleted = todoitemAc.IsCompleted;


                await _dbContext.SaveChangesAsync();

                return Ok("La tarea se ha actualizado de forma correcta!");
            }

            return NotFound();
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {

            var item = await _dbContext.TodoItems.FindAsync(id);
            var recordToUpdate = _dbContext.TodoItems.FirstOrDefault(r => r.Id == id);

            if (recordToUpdate != null)
            {

                recordToUpdate.Estate = 0;
                _dbContext.SaveChanges();
            }
            else
            {

                return NotFound();
            }


            return Ok(new
            {

                code = 200,
                message = $"El usuario con id {id} fue eliminado"
            });
        }

    }

}

