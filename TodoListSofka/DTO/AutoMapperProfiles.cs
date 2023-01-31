using AutoMapper;
using TodoListSofka.Models;
using TodoListSofka.DTO;

namespace TodoListSofka.DTO
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Todoitem, TodoitemDTO>().ReverseMap();
        }
    }
}
