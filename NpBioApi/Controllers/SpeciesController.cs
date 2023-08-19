using NpBioApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    // GET: api/Species returns first 100 species
    // GET: api/Species?page=3 returns the 200th-300th species
    // GET: api/Species?page=2&pageSize=1000 returns the 1000th-2000th species
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Species>>> GetSpecies(int page = 1, int pageSize = 100)
    {
        if (page < 1 || pageSize <= 0)
        {
            return BadRequest("Invalid pagination parameters.");
        }

        // Grab species count and calculate total pages to include in metadata.
        int totalCount = await _db.Species.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Error msg if page number exceeds the last page.
        if (page > totalPages && totalPages > 0)
        {
            return BadRequest("Page number exceeds total pages.");
        }

        // Calculate how many pages to skip then assign species list to data.
        int skip = (page - 1) * pageSize;
        var data = await _db.Species.Skip(skip).Take(pageSize).ToListAsync();


        // Metadata help devs navigate pages.
        var metadata = new PaginationMetadata
        {
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = page,
            TotalPages = totalPages
        };

        // Combine species data and metadata into one response.
        var response = new
        {
            data = data,
            metadata = metadata
        };

        return Ok(response);
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