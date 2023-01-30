CREATE TABLE [dbo].[ToDoItem] (
    [ItemId] UNIQUEIDENTIFIER PRIMARY KEY default NEWID() NOT NULL,
    [Title] NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (500) NOT NULL,
    [Responsible] NVARCHAR (500) NOT NULL,
    [IsCompleted] BIT NOT NULL,
    [State] BIT NOT NULL
);