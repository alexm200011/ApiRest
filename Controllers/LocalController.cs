using ApiRest.Context;
using ApiRest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocalController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LocalController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Local>>> GetLocals()
        {
            if (_context.locals == null) {
                return Problem("Error: no se encontro contexto en locales"); 
            }
            return await _context.locals.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Local>> GetLocal(int id) {
            if (_context.locals == null)
            {
                return Problem("Error: no se encontro contexto en locales");
            }
            var local = await _context.locals.FindAsync(id);

            if (local == null) { 
                return NotFound("No se encontro el local"); 
            }
            return local;
        }
        [HttpPost]
        public async Task<ActionResult<Local>> PostLocal(Local local) {
            if (_context.locals == null) {
                return Problem("Error: no se encontro el contexto en locales");
            }
            _context.locals.Add(local);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLocal", new { id = local.Id }, local);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Local local) {
            if (id != local.Id) {
                return BadRequest();
            }

            _context.Entry(local).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!LocalExists(id))
                {
                    return NotFound("El local no ha sido encontrado");
                }
                else {
                    throw;
                }
            }
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocal(int id) {
            if (_context.locals == null) {
                return NotFound();
            }
            var local = await _context.locals.FindAsync(id);
            if (local == null) {
                return NotFound();
            }
            try
            {
                _context.locals.Remove(local);
                await _context.SaveChangesAsync();
                return Ok("Local eliminado exitosamente");
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

        private bool LocalExists(int id)
        {
            return (_context.locals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}