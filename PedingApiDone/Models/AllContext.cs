using Microsoft.EntityFrameworkCore;


namespace PedingApiDone.Models;

public class AllContext : DbContext
{
    public AllContext(DbContextOptions<AllContext> options)
        : base(options)
    {
    }

    public DbSet<AllItems> TodoItems { get; set; } = null!;
}