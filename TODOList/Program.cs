using Microsoft.EntityFrameworkCore;
using Todolist;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodolistContext>(opts =>
            opts.UseInMemoryDatabase("Todolist"));

builder.Services.AddScoped<TodoItemsRepository>();

var app = builder.Build();

app.MapGet("/", (TodoItemsRepository repo) => repo.GetItems());
app.MapGet("/seed", (TodoItemsRepository repo) => repo.Seed());
app.MapPost("/new", (TodoItem item, TodoItemsRepository repo) => repo.AddNew(item));


app.MapGet("/{id:int}", (int id, TodoItemsRepository repo) => 
{
    var item = repo.GetDetail(id);
    if (item != null)
    {
        return Results.Ok(item);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("/{id:int}", (int id, TodoItemsRepository repo) =>
{
    repo.DeleteItem(id);
    return Results.NoContent();
});

app.MapPut("/{id:int}", (int id, TodoItem updatedItem, TodoItemsRepository repo) =>
{
    var updated = repo.UpdateItem(id, updatedItem);
    if (updated != null)
    {
        return Results.Ok(updated);
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();
