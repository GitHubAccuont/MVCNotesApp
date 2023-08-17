using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly NotesService _noteService;

        public NoteController(NotesService noteService)
        {
            _noteService = noteService;
        }

        public async Task<IActionResult> Index()
        {
            List<Note> notes = await _noteService.ToListAsync();
            return View(notes);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            if (ModelState.IsValid)
            {
                await _noteService.AddNoteAsync(note);
                return RedirectToAction("Index");
            }
            return View(note);
        }
    }
}
