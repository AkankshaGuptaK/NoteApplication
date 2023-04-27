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
            return _context.Notes.FirstOrDefault(x => x.Id == Id).IsDeleted;
        }

        List<string> INoteRepository.GetAllNotesName()
        {
            return _context.Notes.Select(x => x.Title).ToList();
        }

        List<Note> INoteRepository.GetNote()
        {

            return _context.Notes.Include( u => u.User).ToList();
        }

        Note INoteRepository.GetNoteById(Guid Id)
        {
            return _context.Notes.Include( u => u.User).Where(x => x.Id == Id)?.FirstOrDefault();
        }

        bool INoteRepository.UpdateNote(Note note)
        {
            try
            {
                _context.Notes.Update(note);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
