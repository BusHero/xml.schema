using Microsoft.EntityFrameworkCore;
using xml.schema.Models;

namespace xml.schema.Data;

public class SchemaContext : DbContext
{
    public SchemaContext(DbContextOptions<SchemaContext> options) : base(options)
    {
    }

    public DbSet<Schema> Schemas { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Schema>().ToTable("Schemas");
    }
}