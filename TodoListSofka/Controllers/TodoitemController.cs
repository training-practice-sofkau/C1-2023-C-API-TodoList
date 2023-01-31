using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListSofka.Data;
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



    }
}
