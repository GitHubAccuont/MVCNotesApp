using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public class Note
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string Group { get; set; }
    }

    public enum Status
    {
        InProgress,
        Overdue,
        Cancelled
    }
}

