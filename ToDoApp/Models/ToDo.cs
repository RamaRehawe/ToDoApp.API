namespace ToDoApp.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string  Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompleatedDate { get; set;}
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; } 
    }
}
