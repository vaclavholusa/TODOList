namespace Todolist;

public class TodoItemsRepository
{
    public List<TodoItem> Items { get; set; } = new List<TodoItem>();

    public void Seed()
    {

        Items.AddRange(
            Enumerable.Range(1, 5).Select(i =>
                new TodoItem(i, $"Item #{i}", DateTime.Now)
                {
                    Priority = (Priority)(i % 2),
                    Deadline = DateTime.Today.AddDays(i * 2),
                    Description = $"Generated Description for item #{i}"
                }
            )
        );
    }

    public void AddNew(TodoItem item)
    {
        var maxId = Items.Count > 0
                    ? Items.Max(x => x.Id)
                    : 0;

        item.DateAdded = DateTime.Now;
        item.Id = maxId + 1;

        Items.Add(item);
    }
    
    public TodoItem? GetDetail(int id)
    {
        return Items.FirstOrDefault(x => x.Id == id);
    }

    public void DeleteItem(int id)
    {
        var existingItem = GetDetail(id);
        if (existingItem != null)
        {
            Items.Remove(existingItem);
        }
    }

    public TodoItem UpdateItem(int id, TodoItem updatedItem)
    {
        var item = GetDetail(id);

        item.Title = updatedItem.Title;
        item.Description = updatedItem.Description;
        item.Priority = updatedItem.Priority;
        item.Deadline = updatedItem.Deadline;
        return item;
    }
}

