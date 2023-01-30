using AutoMapper;
using TodoListSofka.Model;
using TodoListSofka.DTO;

namespace TodoListSofka.DTO
{
    public class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration() {
            return new MapperConfiguration(config => {
                config.CreateMap<ToDoItem, ToDoItemDTO>(); //Metodo Get del Controlador
                config.CreateMap<ToDoItemDTO, ToDoItem>(); //Metodo Post del Controlador
            });
        }

    }
}
