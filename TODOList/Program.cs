using Todolist;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var repo = new TodoItemsRepository();

app.MapGet("/", () => repo.Items);
app.MapGet("/seed", () => repo.Seed());
app.MapPost("/new", (TodoItem item) => repo.AddNew(item));


app.MapGet("/{id:int}", (int id) => 
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

app.MapDelete("/{id:int}", (int id) =>
{
    repo.DeleteItem(id);
    return Results.NoContent();
});

app.MapPut("/{id:int}", (int id, TodoItem updatedItem) =>
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
