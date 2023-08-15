using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NoteController : Controller
    {
        private Notes _notes;

        public NoteController()
        {
            _notes = new Notes();
        }

        public IActionResult Index()
        {
            List<NoteModel> notes = _notes.GetNotes();
            return View(notes);
        }


        // Другие методы действий

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string taskName)
        {
            _notes.AddTask(taskName);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int taskId, int statusId)
        {
            if(statusId<3)
            {
                Status newStatus = (Status)statusId;
                _notes.ChangeStatus(taskId, newStatus);
                return RedirectToAction(nameof(Index));
            }
            else
                return  View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseTask(int taskId)
        {
            _notes.CloseTask(taskId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SearchWithStatus(int searchStatusId)
        {
            if (searchStatusId < 3) 
                try
                {
                    Status searchStatus = (Status)searchStatusId;
                    List<NoteModel> searchResults = _notes.SearchWithStatus(searchStatus);
                    return View("SearchResults", searchResults);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            return View();
        }

        [HttpGet]
        public IActionResult SearchWithName(string searchName)
        {
            NoteModel? searchResult = _notes.SearchWithName(searchName);
            return View("SearchResult", searchResult);
        }

        [HttpGet]
        public IActionResult SearchWithId(int searchId)
        {
            NoteModel? searchResult = _notes.SearchWithId(searchId);
            return View("SearchResult", searchResult);
        }

    }
}
