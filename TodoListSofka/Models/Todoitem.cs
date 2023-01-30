using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Models;

public partial class Todoitem
{
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string Responsible { get; set; } = null!;

    [DefaultValue(false)]
    public bool IsCompleted { get; set; }

    [DefaultValue(true)]
    public bool State { get; set; }
}
/*
CREATE DATABASE TODOLISTDB
USE TODOLISTDB

CREATE TABLE TODOITEMS(
Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
Title varchar(50) not null,
Description varchar(250) not null,
Responsible varchar(250) not null,
IsCompleted bit not null,
State bit not null
)

select* from TODOITEMS
*/