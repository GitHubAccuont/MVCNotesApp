using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.InProgress;
        public string Group { get; set; } = "Общее";
    }

    public enum Status
    {
        InProgress,
        Overdue,
        Cancelled
    }
}

