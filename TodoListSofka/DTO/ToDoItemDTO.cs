using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoListSofka.DTO
{
    public class ToDoItemDTO
    {
        public Guid ItemId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Responsible { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public bool State { get; set; }
    }
}
