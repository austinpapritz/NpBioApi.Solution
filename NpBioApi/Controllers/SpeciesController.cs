using NpBioApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NpBioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpeciesController : ControllerBase
{
    private readonly NpBioApiContext _db;

    public SpeciesController(NpBioApiContext db)
    {
        _db = db;
    }

    // GET: api/Species
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Species>>> GetSpecies()
    {
        return await _db.Species.Take(50).ToListAsync();
    }

    // GET: api/Species/115
    [HttpGet("{id}")]
    public async Task<ActionResult<Species>> GetSpeciesById(int id)
    {
        Species species = await _db.Species.FindAsync(id);

        if (species == null)
        {
            return NotFound();
        }

        return species;
    }

    // POST api/Species
    [HttpPost]
    public async Task<ActionResult<Species>> Post(Species species)
    {
        _db.Species.Add(species);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSpeciesById), new { id = species.Id }, species);
    }

    // PUT: api/Species/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Species species)
    {
        if (id != species.Id)
        {
            return BadRequest();
        }

        _db.Species.Update(species);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SpeciesExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool SpeciesExists(int id)
    {
        return _db.Species.Any(p => p.Id == id);
    }

    // DELETE: api/Species/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpecies(int id)
    {
        Species species = await _db.Species.FindAsync(id);

        if (species == null)
        {
            return NotFound();
        }

        _db.Species.Remove(species);
        await _db.SaveChangesAsync();

        return NoContent();
    }

}