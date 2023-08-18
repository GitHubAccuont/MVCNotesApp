using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

public class NotesService
{
    private readonly NotesDbContext _dbContext;

    public NotesService(NotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Note>> ToListAsync()
    {
        return await _dbContext.Notes.ToListAsync();
    }

    public async Task EditAsync(int id, Status status, string group, string name)
    {
        var noteToUpdate = _dbContext.Notes.FirstOrDefault(n => n.Id == id);
        if (noteToUpdate != null)
        {
            noteToUpdate.Status = status;
            noteToUpdate.Group = group;
            noteToUpdate.Name = name;
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

    public async Task DeleteNoteAsync(int id)
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
        return await _dbContext.Notes.FirstOrDefaultAsync(n => n.Id == id);
    }
}
