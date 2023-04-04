namespace Todolist;

public class TodoItemsRepository
{
    private readonly TodolistContext context;

    public TodoItemsRepository(TodolistContext context)
    {
        this.context = context;
    }

    public void Seed()
    {

        context.Items.AddRange(
            Enumerable.Range(1, 5).Select(i =>
                new TodoItem(i, $"Item #{i}", DateTime.Now)
                {
                    Priority = (Priority)(i % 2),
                    Deadline = DateTime.Today.AddDays(i * 2),
                    Description = $"Generated Description for item #{i}"
                }
            )
        );
        context.SaveChanges();
    }

    public List<TodoItem> GetItems()
    {
        return context.Items.ToList();
    }

    public void AddNew(TodoItem item)
    {
        var maxId = context.Items.Count() > 0
                    ? context.Items.Max(x => x.Id)
                    : 0;

        item.DateAdded = DateTime.Now;
        item.Id = maxId + 1;

        context.Items.Add(item);
        context.SaveChanges();
    }
    
    public TodoItem? GetDetail(int id)
    {
        return context.Items.FirstOrDefault(x => x.Id == id);
    }

    public void DeleteItem(int id)
    {
        var existingItem = GetDetail(id);
        if (existingItem != null)
        {
            context.Items.Remove(existingItem);
            context.SaveChanges();
        }
    }

    public TodoItem UpdateItem(int id, TodoItem updatedItem)
    {
        var item = GetDetail(id);

        item.Title = updatedItem.Title;
        item.Description = updatedItem.Description;
        item.Priority = updatedItem.Priority;
        item.Deadline = updatedItem.Deadline;
        
        context.SaveChanges();

        return item;
    }
}

