using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCNotesApp.Models;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Note> notes = await _noteService.ToListAsync();
            return View(notes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            await _noteService.AddNoteAsync(note);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]    
        public async Task<IActionResult> Delete(int id)
        {
            var noteToDelete = await _noteService.SearchWithIdAsync(id);

            if (noteToDelete == null)
            {
                return NotFound();
            }

            await _noteService.DeleteNoteAsync(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var noteToEdit = await _noteService.SearchWithIdAsync(id);

            if (noteToEdit == null)
            {
                return NotFound();
            }

            var editModel = new EditModel
            {
                Id = noteToEdit.Id,
                Name = noteToEdit.Name,
                Status = noteToEdit.Status,
                Group = noteToEdit.Group
            };

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                await _noteService.EditAsync(model.Id, model.Status, model.Group, model.Name);
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Search(Status? status = null, string name = null, int? id = null)
        {
            List<Note> searchResults = new List<Note>();
            if (status.HasValue)
            {
                searchResults = await _noteService.SearchWithStatusAsync(status.Value);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                searchResults = await _noteService.SearchWithNameAsync(name);
            }
            else if (id.HasValue)
            {
                Note foundNote = await _noteService.SearchWithIdAsync(id.Value);
                if (foundNote != null)
                {
                    searchResults.Add(foundNote);
                }
            }
            else
            {
                searchResults = await _noteService.ToListAsync();
            }

            ViewBag.SearchResults = searchResults;
            return View("Search");
        }

    }
}
