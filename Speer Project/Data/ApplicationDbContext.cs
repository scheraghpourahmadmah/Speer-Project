using Microsoft.EntityFrameworkCore;
using Speer_Project.DTOs;
using Speer_Project.Model;

namespace Speer_Project.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
