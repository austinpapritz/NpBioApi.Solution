using NpBioApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NpBioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParksController : ControllerBase
{
    private readonly NpBioApiContext _db;

    public ParksController(NpBioApiContext db)
    {
        _db = db;
    }

    // GET: api/Parks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Park>>> GetParks()
    {
        return await _db.Parks.ToListAsync();
    }

    // GET: api/Parks/115
    [HttpGet("{id}")]
    public async Task<ActionResult<Park>> GetParkById(int id)
    {
        Park park = await _db.Parks.FindAsync(id);

        if (park == null)
        {
            return NotFound();
        }

        return park;
    }

    // POST api/Parks
    [HttpPost]
    public async Task<ActionResult<Park>> Post(Park park)
    {
        _db.Parks.Add(park);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParkById), new { id = park.Id }, park);
    }

    // PUT: api/Parks/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Park park)
    {
        if (id != park.Id)
        {
            return BadRequest();
        }

        _db.Parks.Update(park);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParkExists(id))
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



    //GET: api/Parks/{id}/Species
    [HttpGet("{id}/Species")]
    public async Task<ActionResult<IEnumerable<Species>>> GetSpeciesByParkId(int id)
    {
        // Check if the park exists.
        var park = await _db.Parks.FindAsync(id);
        if (park == null)
        {
            return NotFound($"Park with ID {id} not found.");
        }

        // Get the species associated with the park
        var speciesInPark = await _db.Species.Where(s => s.ParkId == id).ToListAsync();

        return speciesInPark;
    }

    private bool ParkExists(int id)
    {
        return _db.Parks.Any(p => p.Id == id);
    }

    // DELETE: api/Parks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePark(int id)
    {
        Park park = await _db.Parks.FindAsync(id);

        if (park == null)
        {
            return NotFound();
        }

        _db.Parks.Remove(park);
        await _db.SaveChangesAsync();

        return NoContent();
    }

}