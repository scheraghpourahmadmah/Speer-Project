using Microsoft.EntityFrameworkCore;
using Speer_Project.Model;

namespace Speer_Project.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SharedNote> SharedNotes { get; set; }
    }
}
