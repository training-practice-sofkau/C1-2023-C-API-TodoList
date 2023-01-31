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







    }
}
