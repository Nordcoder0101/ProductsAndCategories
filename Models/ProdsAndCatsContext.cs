using Microsoft.EntityFrameworkCore;

namespace ProdsAndCats.Models
{
  public class ProdsAndCatsContext : DbContext
  {
    public ProdsAndCatsContext(DbContextOptions<ProdsAndCatsContext> options) : base(options) { }
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Association> Association {get;set;}

  }
}