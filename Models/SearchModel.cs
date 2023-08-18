using NotesApp.Models;

namespace MVCNotesApp.Models
{
    public class SearchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string Group { get; set; }
    }
}
