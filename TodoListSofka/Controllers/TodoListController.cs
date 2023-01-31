using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<TodoItem>> GetItem([FromRoute]int id)
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



        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostItem(TodoItem item)
        {

            _dbContext.TodoItems.Add(item);

            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItems), new { id = item.Id }, item);

        }







    }
}
