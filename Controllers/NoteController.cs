using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly NotesDbContext _dbContext;

        public NoteController(NotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Note> notes = await _dbContext.Notes.ToListAsync();
            return View(notes);
        }

    }
}
