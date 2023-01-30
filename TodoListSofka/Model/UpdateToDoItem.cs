namespace TodoListSofka.Model
{
    public class UpdateToDoItem
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Responsible { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
