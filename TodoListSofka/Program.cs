using Microsoft.EntityFrameworkCore;
using TodoListSofka.Model;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
// Add services to the container.


builder.Services.AddDbContext<TodolistContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProgrammerCS")));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
