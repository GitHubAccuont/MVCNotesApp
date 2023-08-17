using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

public class NotesService
{
    private readonly NotesDbContext _dbContext;

    public NotesService(NotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Async
    //

    public async Task<List<Note>> ToListAsync()
    {
        return await _dbContext.Notes.ToListAsync();
    }

    public async Task ChangeStatusAsync(int id, Status status)
    {
        var noteToUpdate = _dbContext.Notes.FirstOrDefault(n => n.id == id);
        if (noteToUpdate != null)
        {
            noteToUpdate.Status = status;
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("Некорректный id заметки или статус для смены");
        }
    }

    public async Task AddNoteAsync(Note note)
    {
        if (note.Name == null) return;
        _dbContext.Notes.Add(note);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CloseTaskAsync(int id)
    {
        var noteToRemove = await SearchWithIdAsync(id);
        if (noteToRemove != null)
        {
            _dbContext.Notes.Remove(noteToRemove);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("Некорректный id заметки");
            return;
        }
    }

    public async Task<List<Note>> SearchWithStatusAsync(Status status)
    {
        return await _dbContext.Notes.Where(n => n.Status == status).ToListAsync();
    }

    public async Task<List<Note>> SearchWithNameAsync(string name)
    {
        return await _dbContext.Notes.Where(n => n.Name.Contains(name)).ToListAsync();
    }

    public async Task<Note> SearchWithIdAsync(int id)
    {
        return await _dbContext.Notes.FirstOrDefaultAsync(n => n.id == id);
    }
}
