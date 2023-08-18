using Microsoft.EntityFrameworkCore;

public class NpBioApiContext : DbContext
{
    public DbSet<Park> Parks { get; set; }
    public DbSet<Species> Species { get; set; }
    public NpBioApiContext(DbContextOptions<NpBioApiContext> options) : base(options)
    {
    }

}
