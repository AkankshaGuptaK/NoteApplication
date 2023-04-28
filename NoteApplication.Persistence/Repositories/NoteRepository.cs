using Microsoft.EntityFrameworkCore;
using NoteApplication.Persistence.Entities;
using NoteApplication.Persistence.Interfaces;
using NoteApplication.Persistence.Interfaces.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Persistence.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly INoteContext _context;
        public NoteRepository(INoteContext dbContext)
        {
            _context = dbContext;
        }


        bool INoteRepository.AddNote(Note note)
        {
            try
            {
                _context.Notes.Add(note);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        bool INoteRepository.DeleteNote(Guid Id)
        {
            try
            {
                _context.Notes.FirstOrDefault(x => x.Id == Id).IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        List<string> INoteRepository.GetAllNotesName()
        {
            return _context.Notes.Select(x => x.Title).AsNoTracking().ToList();
        }

        List<Note> INoteRepository.GetAllNotes()
        {

            return _context.Notes.Include( u => u.User).Where(n => !n.IsDeleted).OrderByDescending(n=> n.LastEdited).AsNoTracking().ToList();
        }

        Note INoteRepository.GetNoteById(Guid Id)
        {
            return _context.Notes.Include( u => u.User).Where(x => x.Id == Id).AsNoTracking()?.FirstOrDefault();
        }

        bool INoteRepository.UpdateNote(Note note)
        {
            try
            {
                _context.Notes.Update(note);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public Tuple<List<Note>,int, int, int> GetDashboardData()
        {
            var noteslist = _context.Notes.AsNoTracking().ToList();
            int totalCount = noteslist.Count();
            int editCount = noteslist.Sum(_ => _.EditCount);
            int deleteCount = noteslist.Where(n => n.IsDeleted).Count();
            return Tuple.Create(noteslist.Where(n=> !n.IsDeleted).ToList(), totalCount, editCount, deleteCount);
        }
    }
}
