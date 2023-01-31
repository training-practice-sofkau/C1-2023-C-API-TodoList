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




    }
}
