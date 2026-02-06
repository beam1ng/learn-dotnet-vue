
using Microsoft.EntityFrameworkCore;
using models.WordModels;

public class AppDbContext : DbContext
{
    public DbSet<WordEntry> Words => Set<WordEntry>();
    public DbSet<WordFives> Fives => Set<WordFives>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
}