using Microsoft.AspNetCore.Mvc;
using NoteApplication.Models;
using NoteApplication.Persistence.Interfaces.Services;
using System.Diagnostics;

namespace NoteApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INoteService _noteService;

        public HomeController(ILogger<HomeController> logger,INoteService noteService)
        {
            _logger = logger;
            _noteService = noteService;
        }

        public IActionResult Index()
        {
            var notesList = _noteService.GetNote();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}