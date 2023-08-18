using Microsoft.EntityFrameworkCore;

public class NpBioApiContext : DbContext
{
    public NpBioApiContext(DbContextOptions<NpBioApiContext> options) : base(options)
    {
    }

    public DbSet<Park> Parks { get; set; }
    public DbSet<Species> Species { get; set; }
}
