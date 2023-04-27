using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApplication.Persistence.Entities;
using NoteApplication.Persistence.Interfaces.Services;
using NoteApplication.ViewModels;

namespace NoteApplication.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService) { 
            _noteService = noteService;
        }
        public IActionResult Index()
        {
            var notesList = _noteService.GetNote();
            List<NoteViewModel> viewModel = new List<NoteViewModel>();
            foreach (var note in notesList)
            {
                NoteViewModel viewModelItem = new NoteViewModel();
                viewModelItem.Id = note.Id;
                viewModelItem.Title = note.Title;
                viewModelItem.Description = note.Description;
                viewModel.Add(viewModelItem);
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create() {
            NoteViewModel note = new NoteViewModel();
            return PartialView("_NoteModalPartial", note);
        }
        [HttpPost]
        public IActionResult Create(NoteViewModel note)
        {
            Note requestObject = new Note()
            {
                Id = Guid.NewGuid(),
                Title = note.Title,
                Description = note.Description,
                CreatedBy = "132f31e6-2b3c-47c6-88d9-66ccedce71b8",
                LastEdited = DateTime.UtcNow
            };
            _noteService.AddNote(requestObject);
            return RedirectToAction("Index");
        }

        public IActionResult Detail(Guid id)
        {
            Note note = _noteService.GetNoteById(id);
            NoteViewModel viewModel = new NoteViewModel()
            {
                Title = note.Title,
                Description = note.Description,
            };
            return PartialView("_NoteDetailPartialView",viewModel);
        }
    }
}
