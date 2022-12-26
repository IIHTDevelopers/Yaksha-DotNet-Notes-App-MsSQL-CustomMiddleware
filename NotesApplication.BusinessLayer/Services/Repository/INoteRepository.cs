using NotesApplication.BusinessLayer.ViewModels;
using NotesApplication.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesApplication.BusinessLayer.Services.Repository
{
    public interface INoteRepository
    {
        Task<Note> AddNote(Note note);
        Task<Note> GetNoteById(int noteId);
        Task<Note> UpdateNote(int noteId, string status);
        Task<Note> DeleteNote(int noteId);
        Task<IEnumerable<Note>> GetAllNotes();
    }
}
