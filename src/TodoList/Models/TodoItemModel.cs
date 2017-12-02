namespace TodoList.Models 
{
    public class TodoItemModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public bool ShowError { get; set; }
    }
}