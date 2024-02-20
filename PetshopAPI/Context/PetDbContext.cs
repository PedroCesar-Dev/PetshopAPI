using Microsoft.EntityFrameworkCore;
using PetshopAPI.Models;

namespace PetshopAPI.Context
{
    public class PetDbContext:DbContext
    {
        public PetDbContext(DbContextOptions<PetDbContext>options):base(options) 
        { 
        }

        public DbSet<Pet> Pets { get; set; }
    }
}
