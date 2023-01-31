using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            return Ok(await _context.Todoitems.Where(x => x.State && !x.IsCompleted).ToListAsync());
        }

        //Get por id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id)
        {
            var item = await _context.Todoitems.Where(x => x.Id == id && x.State && !x.IsCompleted).ToListAsync();
            if (item.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(item[0]);
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
            var item = await _context.Todoitems.Where(x => x.Id == id && x.State).ToListAsync();
            if (!item.IsNullOrEmpty())
            {
                _mapper.Map(dto, item[0]);
                await _context.SaveChangesAsync();
                return Ok(item[0]);
            }
            return NotFound();
        }

        //Metodo para indicar que la tarea se completó
        [HttpPut]
        [Route("updateComplete/{id:Guid}")]
        public async Task<IActionResult> CompleteItem([FromRoute] Guid id)
        {
            var item = await _context.Todoitems.Where(x => x.Id == id && x.State).ToListAsync();

            if (!item.IsNullOrEmpty())
            {
                item[0].IsCompleted = true;
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
