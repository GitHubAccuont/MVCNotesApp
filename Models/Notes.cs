using NotesApp.Models;

public class Notes
{
    private readonly NotesDbContext _dbContext;

    public Notes(NotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ChangeStatus(int id, Status status)
    {
        var noteToUpdate = _dbContext.Notes.FirstOrDefault(n => n.id == id);
        if (noteToUpdate != null)
        {
            noteToUpdate.Status = status;
            _dbContext.SaveChanges();
        }
        else
        {
            Console.WriteLine("Некорректный id заметки или статус для смены");
        }
    }

    public void CloseTask(int id)
    {
        var noteToRemove = _dbContext.Notes.FirstOrDefault(n => n.id == id);
        if (noteToRemove != null)
        {
            _dbContext.Notes.Remove(noteToRemove);
            _dbContext.SaveChanges();
        }
        else
        {
            Console.WriteLine("Некорректный id заметки");
        }
    }

    public List<Note> SearchWithStatus(Status status)
    {
        return _dbContext.Notes.Where(n => n.Status == status).ToList();
    }

    public Note SearchWithName(string name)
    {
        return _dbContext.Notes.FirstOrDefault(n => n.Name == name);
    }

    public Note SearchWithId(int id)
    {
        return _dbContext.Notes.FirstOrDefault(n => n.id == id);
    }
}
