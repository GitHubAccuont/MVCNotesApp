using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Models
{
    public class NotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesDbContext(DbContextOptions<NotesDbContext> options)
        : base(options)
        {
        }
    }
}
