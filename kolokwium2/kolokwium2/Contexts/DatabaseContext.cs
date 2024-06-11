using kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2.Contexts;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Backpack> Backpacks{ get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Item>().HasData(
            new Item {Name = "sydfeu", Weight = 3}
        );
        modelBuilder.Entity<Title>().HasData(
            new Title {Name = "hako"}
        );
        modelBuilder.Entity<Character>().HasData(
            new Character
            {
                FirstName = "Bob",
                LastName = "Smith",
                CurrentWeight = 0,
                MaxWeight = 100
            }
        );


    }
}