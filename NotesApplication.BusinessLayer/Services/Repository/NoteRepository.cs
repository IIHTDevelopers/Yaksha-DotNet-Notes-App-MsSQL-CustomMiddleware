using NotesApplication.BusinessLayer.ViewModels;
using NotesApplication.DataLayer;
using NotesApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApplication.BusinessLayer.Services.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesDbContext _noteDbContext;
        public NoteRepository(NotesDbContext notesDbContext)
        {
            _noteDbContext = notesDbContext;
        }

        public async Task<Note> AddNote(Note note)
        {
            try
            {
                var result = await _noteDbContext.Notes.AddAsync(note);
                await _noteDbContext.SaveChangesAsync();
                return note;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<Note> DeleteNote(int noteId)
        {
            var note = await _noteDbContext.Notes.FindAsync(noteId);
            try
            {
                _noteDbContext.Notes.Remove(note);
                await _noteDbContext.SaveChangesAsync();
                return note;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            try
            {
                var result = _noteDbContext.Notes.
                OrderByDescending(x => x.NoteId).Take(10).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<Note> GetNoteById(int noteId)
        {
            try
            {
                return await _noteDbContext.Notes.FindAsync(noteId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<Note> UpdateNote(int noteId,string status)
        {
            var note = await _noteDbContext.Notes.FindAsync(noteId);
            try
            {
                note.Status = status;
                _noteDbContext.Notes.Update(note);
                await _noteDbContext.SaveChangesAsync();
                return note;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
