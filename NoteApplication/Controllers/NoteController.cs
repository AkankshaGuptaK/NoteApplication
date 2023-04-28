using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApplication.Persistence.Entities;
using NoteApplication.Persistence.Interfaces.Services;
using NoteApplication.ViewModels;
using System.Security.Claims;

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
            var dashboardData = _noteService.GetDashboardData();
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.Notes = new List<NoteViewModel>();
            foreach (var note in dashboardData.Item1)
            {
                NoteViewModel viewModelItem = new NoteViewModel();
                viewModelItem.Id = note.Id;
                viewModelItem.Title = note.Title;
                viewModelItem.Description = note.Description;
                dashboardViewModel.Notes.Add(viewModelItem);
            }
            dashboardViewModel.AddCount = dashboardData.Item2;
            dashboardViewModel.EditCount = dashboardData.Item3;
            dashboardViewModel.DeleteCount = dashboardData.Item4;
            return View(dashboardViewModel);
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
                CreatedBy = HttpContext.User.Claims.Where(_=>_.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value,
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
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
            };
            return PartialView("_NoteDetailPartialView",viewModel);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Note note = _noteService.GetNoteById(id);
            NoteViewModel viewModel = new NoteViewModel()
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                EditCount = note.EditCount
            };
            return PartialView("_NoteEditPartialView", viewModel);
        }
        [HttpPost]
        public IActionResult Edit(NoteViewModel note)
        {

            Note requestObject = new Note()
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                CreatedBy = HttpContext.User.Claims.Where(_ => _.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value,
                LastEdited = DateTime.UtcNow,     
                EditCount = note.EditCount+1,
            };
            _noteService.UpdateNote(requestObject);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Note note = _noteService.GetNoteById(id);
            NoteViewModel viewModel = new NoteViewModel()
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
            };
            return PartialView("_NoteDeletePartialView", viewModel);
        }
        [HttpPost]
        public IActionResult Delete(NoteViewModel noteViewModel)
        {
            _noteService.DeleteNote(noteViewModel.Id);
            return RedirectToAction("Index");
        }
    }
}
