using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BooksStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BooksStore.Areas.Identity.Data;

namespace BooksStore.Data
{
    public class BooksStoreContext : IdentityDbContext<BooksStoreUser>
    {
        public BooksStoreContext (DbContextOptions<BooksStoreContext> options)
            : base(options)
        {
        }

        public DbSet<BooksStore.Models.Book> Book { get; set; } = default!;
        public DbSet<BooksStore.Models.Author>? Author { get; set; }

        public DbSet<BooksStore.Models.Genre>? Genre { get; set; }

        public DbSet<BooksStore.Models.Review>? Review { get; set; }

        public DbSet<BooksStore.Models.UserBook>? UserBook { get; set; }
        public DbSet<BooksStore.Models.BookGenre>? BookGenre { get; set; }

        /*  protected override void OnModelCreating(ModelBuilder builder)
          {
              builder.Entity<Book>()
                  .HasOne<Author>(a => a.Author)
                  .WithMany(a => a.Books)
                  .HasForeignKey(p => p.AuthorId);

              builder.Entity<BookGenre>()
                  .HasOne<Book>(a => a.Book)
                  .WithMany(a => a.Genres) // a.BookGenres are "genres"
                  .HasForeignKey(p => p.BookId);

              builder.Entity<BookGenre>()
                  .HasOne<Genre>(p => p.Genre)
                  .WithMany(p => p.Genres) // a.BookGenres are "books"
                  .HasForeignKey(p => p.GenreId);

              builder.Entity<Review>()
                  .HasOne<Book>(p => p.Book)
                  .WithMany(p => p.Reviews)
                  .HasForeignKey(p => p.BookId);

              builder.Entity<UserBook>()
                  .HasOne<Book>(p => p.Book)
                  .WithMany(p => p.UserBooks)
                  .HasForeignKey(p => p.BookId);
          } */

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
