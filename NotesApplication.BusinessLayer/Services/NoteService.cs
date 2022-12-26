using NotesApplication.BusinessLayer.Interfaces;
using NotesApplication.BusinessLayer.Services.Repository;
using NotesApplication.BusinessLayer.ViewModels;
using NotesApplication.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesApplication.BusinessLayer.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Note> AddNote(Note note)
        {
            return await _noteRepository.AddNote(note);
        }

        public async Task<Note> DeleteNote(int noteId)
        {
            return await _noteRepository.DeleteNote(noteId);
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            return await _noteRepository.GetAllNotes();
        }

        public async Task<Note> GetNoteById(int noteId)
        {
            return await _noteRepository.GetNoteById(noteId);
        }

        public async Task<Note> UpdateNote(int noteId,string status)
        {
            return await _noteRepository.UpdateNote(noteId, status);
        }
    }
}
