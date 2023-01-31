using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            var item = await _context.Todoitems.FindAsync(id);
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
            var item = await _context.Todoitems.FindAsync(id);
            if (item != null)
            {
                item.IsCompleted = true;
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            return NotFound();
        }
        
    }
}
