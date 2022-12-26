using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApplication.BusinessLayer.Interfaces;
using NotesApplication.BusinessLayer.ViewModels;
using NotesApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApplication.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

       /// <summary>
       /// Add Note By Id
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        [HttpPost]
        [Route("/noteservice/add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddNote([FromBody] NoteViewModel model)
        {
            var noteExists = await _noteService.GetNoteById(model.NoteId);
            if (noteExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Note already exists!" });
            Note note = new Note()
            {
                NoteId = model.NoteId,
                Author = model.Author,
                Description = model.Description,
                Status = model.Status,
                Title = model.Title,
            };
            var result = await _noteService.AddNote(note);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Note creation failed! Please check details and try again." });

            return Ok(new Response { Status = "Success", Message = "Note created successfully!" });

        }

        /// <summary>
        /// Update Note By Id
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/noteservice/update/{noteId}/{status}")]
        public async Task<IActionResult> UpdateNote(int noteId,string status)
        {
            var note = await _noteService.GetNoteById(noteId);
            if (note == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Note With Id = {noteId} cannot be found" });
            }
            else
            {
                var result = await _noteService.UpdateNote(noteId, status);
                return Ok(new Response { Status = "Success", Message = "Note Edited successfully!" });
            }
        }


       /// <summary>
       /// Delete Note By Id
       /// </summary>
       /// <param name="noteId"></param>
       /// <returns></returns>
        [HttpDelete]
        [Route("/noteservice/delete/{noteId}")]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            var note = await _noteService.GetNoteById(noteId);
            if (note == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Note With Id = {noteId} cannot be found" });
            }
            else
            {
                var result = await _noteService.DeleteNote(noteId);
                return Ok(new Response { Status = "Success", Message = "Note deleted successfully!" });
            }
        }

       /// <summary>
       /// Get Note By Note Id
       /// </summary>
       /// <param name="noteId"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("/noteservice/get/{noteId}")]
        public async Task<IActionResult> GetNoteById(int noteId)
        {
            var note = await _noteService.GetNoteById(noteId);
            if (note == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Note With Id = {noteId} cannot be found" });
            }
            else
            {
                return Ok(note);
            }
        }

        /// <summary>
        /// Get List of All Notes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/noteservice/all")]
        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            return await _noteService.GetAllNotes();
        }

    }
}
