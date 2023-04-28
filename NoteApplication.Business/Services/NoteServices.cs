using NoteApplication.Business.Models;
using NoteApplication.Persistence.Entities;
using NoteApplication.Persistence.Interfaces;
using NoteApplication.Persistence.Interfaces.Services;
using NoteApplication.Persistence.Repositories;

namespace NoteApplication.Business.Services
{
    public class NoteServices : INoteService
    {
        private readonly INoteRepository _noteRepository;

       public NoteServices(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        bool INoteService.AddNote(Note note)
        {
            List<String> noteNames = _noteRepository.GetAllNotesName();
            bool isUniqueName = (noteNames.IndexOf(note.Title)) == -1;
            if (isUniqueName)
            {
                return _noteRepository.AddNote(note);
            }
            else
            {
                throw new CustomException(400, "Note Name already present");
            }
        }

        bool INoteService.DeleteNote(Guid Id)
        {
            return _noteRepository.DeleteNote(Id);
        }

        List<Note> INoteService.GetAllNotes()
        {
            return _noteRepository.GetAllNotes();
        }

        Note INoteService.GetNoteById(Guid Id)
        {
            return _noteRepository.GetNoteById(Id);
        }

        bool INoteService.UpdateNote(Note requestNote)
        {
            List<Note> notes = _noteRepository.GetAllNotes();
            bool isUniqueName = notes.Where(note => note.Id != requestNote.Id && note.Title == requestNote.Title).Count() == 0;
            if (isUniqueName)
            {
                return _noteRepository.UpdateNote(requestNote);
            }
            else
            {
                throw new CustomException(400, "Note Name already present");
            }
        }

        public Tuple<List<Note>, int, int, int> GetDashboardData()
        {
            return _noteRepository.GetDashboardData();
        }
    }
}
