using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookstoreApp.Models;

namespace BookstoreApp.Data
{
    public class BookstoreContext : IdentityDbContext<IdentityUser>
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
