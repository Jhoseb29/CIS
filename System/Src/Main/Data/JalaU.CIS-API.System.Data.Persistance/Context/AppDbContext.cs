using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace JalaU.CIS_API.System.Data.Persistance;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Idea> Ideas { get; set; }
    public DbSet<Vote> Votes { get; set; }
}
