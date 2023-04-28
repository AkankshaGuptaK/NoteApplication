using NoteApplication.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Persistence.Interfaces.Services
{
    public interface INoteService
    {
        public Note GetNoteById(Guid Id);
        public List<Note> GetAllNotes();
        public bool AddNote(Note note);
        public bool UpdateNote(Note note);
        public bool DeleteNote(Guid Id);
        public Tuple<List<Note>, int, int, int> GetDashboardData();
    }
}
