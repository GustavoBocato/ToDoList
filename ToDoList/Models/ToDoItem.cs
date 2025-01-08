namespace ToDoListApp.Models
{
    public class ToDoItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public int idToDoList { get; set; }
    }
}
