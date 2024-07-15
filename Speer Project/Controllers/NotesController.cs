using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speer_Project.Data;
using Speer_Project.DTOs;
using Speer_Project.Model;
using System.Security.Claims;

namespace Speer_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public NotesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            
            var notes = _db.Notes.ToList();
            var toReturn = new List<Note>();

            foreach (var note in notes)
            {
                var noteC = new Note
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content
                };
                
                toReturn.Add(noteC);
            }
            return Ok(toReturn);
        }

        [HttpGet("get-one/{id}")]
        public IActionResult GetNote(int id)
        {
            var note = _db.Notes.FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                return NotFound();
            }
            var toReturn = new Note
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content
            };
            return Ok(toReturn);
        }

        [HttpPost("create")]
        public IActionResult Create(NoteDto note)
        {
            var noteToAdd = new Note
            {
                Title = note.Title,
                Content = note.Content,
                UserId = note.UserId
            };
            _db.Notes.Add(noteToAdd);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("update")]
        public ActionResult Update(NoteUpdateDto model)
        {
            
            var user = _db.Notes.Find(model.Id);

            user.Title = model.Title;
            user.Content = model.Content;
            user.UpdatedAt = DateTime.Now;

            if (user == null) return NotFound();

            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var user = _db.Notes.Find(id);
            if (user == null) return NotFound();
            _db.Remove(user);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
