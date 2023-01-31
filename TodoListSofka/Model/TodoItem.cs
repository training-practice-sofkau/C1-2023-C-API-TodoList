using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public partial class TodoItem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? Responsible { get; set; }

    public bool IsCompleted { get; set; }

    public int Estate { get; set; }
}
