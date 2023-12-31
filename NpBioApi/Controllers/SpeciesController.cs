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
            metadata = metadata,
            data = data
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

    //GET: api/Species/CommonName/{commonName}/Parks
    [HttpGet("CommonName/{commonName}/Parks")]
    public async Task<ActionResult<IEnumerable<Park>>> GetParksBySpeciesCommonName(string commonName)
    {
        commonName = commonName.ToLower().Trim();

        var species = await _db.Species
                                .Include(s => s.Park)
                                .Where(s => EF.Functions.Like(
                                               s.CommonNames.ToLower(), $"%, {commonName}, %")
                                            || s.CommonNames.ToLower().StartsWith($"{commonName}, ")
                                            || s.CommonNames.ToLower().EndsWith($", {commonName}")
                                            || s.CommonNames.ToLower() == commonName)
                                .Select(s => s.Park)
                                .Distinct()
                                .ToListAsync();

        if (!species.Any())
        {
            return NotFound($"Species with common name {commonName} not found.");
        }

        return species;
    }

    //GET: api/Species/ScientificName/{scientificName}/Parks
    [HttpGet("ScientificName/{scientificName}/Parks")]
    public async Task<ActionResult<IEnumerable<Park>>> GetParksBySpeciesScientificName(string scientificName)
    {
        scientificName = scientificName.ToLower();

        var species = await _db.Species
                                .Include(s => s.Park)
                                .Where(s => s.ScientificName.ToLower() == scientificName)
                                .Select(s => s.Park)
                                .Distinct()
                                .ToListAsync();

        if (!species.Any())
        {
            return NotFound($"Species with scientific name {scientificName} not found.");
        }

        return species;
    }

#nullable enable
    // GET: api/Species/Search?param={value}
    [HttpGet("Search")]
    public async Task<ActionResult<IEnumerable<Species>>> GetSpeciesByCommonName(string? commonName, string? scientificName)
    {
        IQueryable<Species> query = _db.Species;

        if (!string.IsNullOrEmpty(commonName))
        {
            query = query.Where(s => s.CommonNames.Contains(commonName));
        }

        if (!string.IsNullOrEmpty(scientificName))
        {
            query = query.Where(s => s.ScientificName.Contains(scientificName));
        }

        return await query.ToListAsync();
    }
#nullable disable




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