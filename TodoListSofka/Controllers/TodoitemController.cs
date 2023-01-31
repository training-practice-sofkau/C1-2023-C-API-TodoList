using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.DTO;
using TodoListSofka.Models;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoitemController : Controller
    {
        private readonly TodolistdbContext _context;
        private readonly IMapper _mapper;

        public TodoitemController(TodolistdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Metodo que retorna la lista de los items activos y no completos
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            return Ok(await _context.Todoitems.Where(x=> x.State && !x.IsCompleted).ToListAsync());
        }

        //Get por id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id)
        {
            var product = await _context.Todoitems.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(TodoitemDTO dto)
        {
            Todoitem item = _mapper.Map<Todoitem>(dto);
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid id, TodoitemDTO dto)
        {
            var item = await _context.Todoitems.FindAsync(id);//Agregar filtro LINQ
            if (item != null)
            {
                _mapper.Map(dto, item);
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            return NotFound();
        }

        //Metodo para indicar que la tarea se completó
        [HttpPut]
        [Route("updateComplete/{id:Guid}")]
        public async Task<IActionResult> CompleteItem([FromRoute] Guid id)
        {
            var item = await _context.Todoitems.FindAsync(id);//Agregar filtro LINQ
            if (item != null)
            {
                item.IsCompleted = true;
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            return NotFound();
        }

        //Eliminado Logico 
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            var item = await _context.Todoitems.FindAsync(id);//Agregar filtro LINQ
            if (item != null)
            {
                item.State = false;
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            return NotFound();
        }


    }
}
