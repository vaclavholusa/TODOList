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
                new TodoItem($"Item #{i}", DateTime.Now)
                {
                    Priority = (Priority)(i % 2),
                    Deadline = DateTime.Today.AddDays(i * 2),
                    Description = $"Generated Description for item #{i}"
                }
            )
        );
        context.SaveChanges();
    }

    
    public List<TodoItem> GetItems(DateTime? dateFrom, DateTime? dateTo)
    {
        if(dateFrom == null && dateTo == null)
        {
            return context.Items.ToList();
        }
        else if (dateFrom == null)
        {
            return context.Items.Where(i => i.Deadline < dateTo).ToList();
        }
        else if (dateTo == null)
        {
            return context.Items.Where(i => i.Deadline > dateFrom).ToList();
        }
        else
        {
            return context.Items
                .Where(i => i.Deadline > dateFrom && i.Deadline < dateTo)
                .ToList();
        }
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
    
    public void SetCompleted(int id)
    {
        var item = GetDetail(id);
        item.DateFinished = DateTime.Now;
        context.SaveChanges();
    }

    public void SetPriority(int id, Priority priority)
    {
        var item = GetDetail(id);
        item.Priority = priority;
        context.SaveChanges();
    }
}

