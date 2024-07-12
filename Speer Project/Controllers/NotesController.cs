using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speer_Project.Data;
using Speer_Project.Model;
using System.Security.Claims;

namespace Speer_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public NotesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notes = await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            note.UserId = userId;
            note.CreatedAt = DateTime.UtcNow;
            note.UpdatedAt = DateTime.UtcNow;

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateNote(int id, [FromBody] Note note)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

        //    if (existingNote == null)
        //    {
        //        return NotFound();
        //    }

        //    existingNote.Title = note.Title;
        //    existingNote.Content = note.Content;
        //    existingNote.UpdatedAt = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteNote(int id)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

        //    if (note == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Notes.Remove(note);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpPost("{id}/share")]
        //public async Task<IActionResult> ShareNote(int id,ShareNoteDto shareNoteDto)
        //{

    } //}
    }
