using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public class ToDoItem
{
    public Guid ItemId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Responsible { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public bool State { get; set; }

    public ToDoItem(Guid id, string title, string description, string responsible,
            bool isCompleted, bool state)
    {
        ItemId = id;
        Title = title;
        Description = description;
        Responsible = responsible;
        IsCompleted = isCompleted;
        State = state;
    }

    public ToDoItem() { }
}
