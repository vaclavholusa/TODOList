namespace Todolist
{
    public enum Priority
    {
        Normal, // 0
        High // 1
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateFinished { get; set; }

        public TodoItem(int id, string title, DateTime dateAdded)
        {
            Id = id;
            Title = title;
            DateAdded = dateAdded;
        }
    }
}
