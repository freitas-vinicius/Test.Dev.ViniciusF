using Microsoft.EntityFrameworkCore;
using Test.Dev.ViniciusF.Models;

namespace Test.Dev.ViniciusF.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
      : base(options)

        {
        }

        public DbSet<Studant> Studants { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Breakfest> Breakfests { get; set; }
    }
}
