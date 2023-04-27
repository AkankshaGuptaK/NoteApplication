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
            throw new NotImplementedException();
        }

        List<Note> INoteService.GetNote()
        {
            return _noteRepository.GetNote();
        }

        Note INoteService.GetNoteById(Guid Id)
        {
            return _noteRepository.GetNoteById(Id);
        }

        bool INoteService.UpdateNote(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
