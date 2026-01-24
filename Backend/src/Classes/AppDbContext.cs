using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<WordEntry> Words => Set<WordEntry>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WordEntry>()
            .HasIndex(x => x.Word)
            .IsUnique();
    }
}