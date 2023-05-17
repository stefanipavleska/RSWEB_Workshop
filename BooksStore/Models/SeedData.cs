using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BooksStore.Areas.Identity.Data;
using BooksStore.Data;

namespace BooksStore.Models
{
    public class SeedData
    {

        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<BooksStoreUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            BooksStoreUser user = await UserManager.FindByEmailAsync("admin@booksstore.com");
            if (user == null)
            {
                var User = new BooksStoreUser();
                User.Email = "admin@booksstore.com";
                User.UserName = "admin@booksstore.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { await UserManager.AddToRoleAsync(User, "Admin"); }

            }

            var roleCheck1 = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck1) { roleResult = await RoleManager.CreateAsync(new IdentityRole("User")); }
            BooksStoreUser user1 = await UserManager.FindByEmailAsync("user@booksstore.com");
            if (user1 == null)
            {
                var User = new BooksStoreUser();
                User.Email = "user@booksstore.com";
                User.UserName = "user@booksstore.com";
                string userPWD = "User123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { await UserManager.AddToRoleAsync(User, "User"); }

            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BooksStoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<BooksStoreContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();

                // Look for any movies.
                if (context.Book.Any() || context.Author.Any() || context.Genre.Any() || context.Review.Any() || context.UserBook.Any())
                {
                    return;   // DB has been seeded
                }
                context.Author.AddRange(
                    new Author
                    {
                        /*Id = 1, */
                        FirstName = "Dan",
                        LastName = "Brown",
                        BirthDate = DateTime.Parse("1964-6-22"),
                        Nationality = "American",
                        Gender = "Male"
                    },
                    new Author
                    {
                        /*Id = 2, */
                        FirstName = "Joanne",
                        LastName = "Rowling",
                        BirthDate = DateTime.Parse("1965-7-31"),
                        Nationality = "British",
                        Gender = "Female"
                    },
                    new Author
                    {
                        /*Id = 3, */
                        FirstName = "Paulo",
                        LastName = "Coelho",
                        BirthDate = DateTime.Parse("1947-8-24"),
                        Nationality = "Brazilian",
                        Gender = "Male"
                    }
                );
                context.SaveChanges();

                context.Genre.AddRange(
                    new Genre { /*Id = 1, */GenreName = "Fantasy" },
                    new Genre { /*Id = 2, */GenreName = "Comedy" },
                    new Genre { /*Id = 3, */GenreName = "Romantic" },
                    new Genre { /*Id = 4, */GenreName = "Action" },
                    new Genre { /*Id = 5, */GenreName = "Tragedy" },
                    new Genre { /*Id = 6, */GenreName = "History" },
                    new Genre { /*Id = 7, */GenreName = "Science" },
                    new Genre { /*Id = 8, */GenreName = "Novel" },
                    new Genre { /*Id = 9, */GenreName = "Fiction" }
                );
                context.SaveChanges();

                context.Book.AddRange(
                    new Book
                    {
                        //Id = 1,
                        Title = "Angels and Demons",
                        YearPublished = 200,
                        NumPages = 768,
                        Description = "An ancient secret brotherhood. A devastating new weapon of destruction. An unthinkable target. When world-renowned Harvard symbologist Robert Langdon is summoned to his first assignment to a Swiss research facility to analyze a mysterious symbol—seared into the chest of a murdered physicist—he discovers evidence of the unimaginable: the resurgence of an ancient secret brotherhood known as the Illuminati...the most powerful underground organization ever to walk the earth.",
                        Publisher = "Simon & Schuster",
                        FrontPage = "AngelsAndDemons.jpg",
                        DownloadUrl = " ",
                        AuthorId = 1
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "And Then There Were None",
                        YearPublished = 1939,
                        NumPages = 272,
                        Description = "Ten people, each with something to hide and something to fear, are invited to an isolated mansion on Indian Island by a host who, surprisingly, fails to appear. On the island they are cut off from everything but each other and the inescapable shadows of their own past lives.",
                        Publisher = "HarperCollins",
                        FrontPage = "And_Then_There_Were_None_First_Edition_Cover_1939.jpg",
                        DownloadUrl = " ",
                        AuthorId = 1
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Black Beauty",
                        YearPublished = 1877,
                        NumPages = 255,
                        Description = "Black Beauty is a touching story written from a horse's point of view. Black Beauty, a handsome, intelligent horse, lives in Victorian England on a peaceful farm with a wonderful master.",
                        Publisher = "Simon & Schuster",
                        FrontPage = "BlackBeautyCoverFirstEd1877.jpeg",
                        DownloadUrl = " ",
                        AuthorId = 1
                    },
                    new Book
                    {
                        //Id = 4,
                        Title = "Harry Potter and the Chamber of Secrets",
                        YearPublished = 1998,
                        NumPages = 251,
                        Description = "The plot follows Harry's second year at Hogwarts School of Witchcraft and Wizardry, during which a series of messages on the walls of the school's corridors warn that the \"Chamber of Secrets\" has been opened and that the \"heir of Slytherin\" would kill all pupils who do not come from all-magical families.",
                        Publisher = "Bloomsbury (UK)",
                        FrontPage = "Harry_Potter_and_the_Chamber_of_Secrets.jpg",
                        DownloadUrl = "",
                        AuthorId = 2
                    },
                    new Book
                    {
                        //Id = 5,
                        Title = "Lolita",
                        YearPublished = 1955,
                        NumPages = 336,
                        Description = "Lolita—the story of a verbose, middle-aged literature professor, sexually obsessed with pre-pubescent girls, and his perverse and destructive relationship with 12-year-old Dolores Haze—became a near-instant bestseller in the US, shifting over 100,000 copies in its first three weeks alone.",
                        Publisher = "Olympia Press",
                        FrontPage = "Lolita_1955.jpeg",
                        DownloadUrl = " ",
                        AuthorId = 2
                    },
                    new Book
                    {
                        //Id = 6,
                        Title = "The Lost Symbol",
                        YearPublished = 2009,
                        NumPages = 528,
                        Description = "The Lost Symbol is best-selling author Dan Brown's third thriller novel following the life of symbologist Robert Langdon as he works to solve the mystery behind the disappearance of his mentor, Peter Solomon, whose severed hand is found in the Capitol Building in Washington DC during a Smithsonian fundraiser.",
                        Publisher = "Doubleday (US)",
                        FrontPage = "LostSymbol.jpg",
                        DownloadUrl = " ",
                        AuthorId = 2
                    },
                    new Book
                    {
                        //Id = 7,
                        Title = "Sophie's World",
                        YearPublished = 1944,
                        NumPages = 518,
                        Description = "One day fourteen-year-old Sophie Amundsen comes home from school to find in her mailbox two notes, with one question on each: Who are you? and Where does the world come from?",
                        Publisher = "Aschehoug",
                        FrontPage = "Sofies_verden.jpg",
                        DownloadUrl = " ",
                        AuthorId = 3
                    },
                    new Book
                    {
                        //Id = 8,
                        Title = "The Alchemist",
                        YearPublished = 1988,
                        NumPages = 163,
                        Description = "The Alchemist is the magical story of Santiago, an Andalusian shepherd boy who yearns to travel in search of a worldly treasure as extravagant as any ever found. From his home in Spain he journeys to the markets of Tangiers and across the Egyptian desert to a fateful encounter with the alchemist.",
                        Publisher = "HarperTorch ",
                        FrontPage = "TheAlchemist.jpg",
                        DownloadUrl = " ",
                        AuthorId = 3
                    }
                );

                context.SaveChanges();

                context.Review.AddRange(
                     new Review
                     {
                         /*Id = 1, */
                         BookId = 1,
                         AppUser = "Tom Smith",
                         Comment = "The book is excellent!",
                         Rating = 10
                     },
                     new Review
                     {
                         /*Id = 2, */
                         BookId = 7,
                         AppUser = "Tom Smith",
                         Comment = "The book is very boring",
                         Rating = 2
                     },
                     new Review
                     {
                         /*Id = 3, */
                         BookId = 4,
                         AppUser = "James Miller",
                         Comment = "The book is excellent!",
                         Rating = 8
                     },
                     new Review
                     {
                         /*Id = 4, */
                         BookId = 1,
                         AppUser = "James Miller",
                         Comment = "The book is great",
                         Rating = 10
                     },
                     new Review
                     {
                         /*Id = 5, */
                         BookId = 2,
                         AppUser = "James Miller",
                         Comment = "The book is excellent!",
                         Rating = 9
                     },
                     new Review
                     {
                         /*Id = 6, */
                         BookId = 3,
                         AppUser = "Lucas James",
                         Comment = "The book is excellent!",
                         Rating = 10
                     },
                     new Review
                     {
                         /*Id = 7, */
                         BookId = 4,
                         AppUser = "Lucas James",
                         Comment = "The book is excellent!",
                         Rating = 8
                     },
                     new Review
                     {
                         /*Id = 8, */
                         BookId = 5,
                         AppUser = "Lucas James",
                         Comment = "The book is great!",
                         Rating = 6
                     },
                     new Review
                     {
                         /*Id = 9, */
                         BookId = 4,
                         AppUser = "Lucas James",
                         Comment = "The book is excellent!",
                         Rating = 8
                     },
                     new Review
                     {
                         /*Id = 10, */
                         BookId = 1,
                         AppUser = "Lucas James",
                         Comment = "The book is excellent!",
                         Rating = 10
                     },
                     new Review
                     {
                         /*Id = 11, */
                         BookId = 2,
                         AppUser = "Tom Smith",
                         Comment = "The book is very boring!",
                         Rating = 2
                     },
                     new Review
                     {
                         /*Id = 12, */
                         BookId = 4,
                         AppUser = "Tom Smith",
                         Comment = "The book is very boring",
                         Rating = 4
                     },
                     new Review
                     {
                         /*Id = 13, */
                         BookId = 5,
                         AppUser = "James Miller",
                         Comment = "The book is very boring",
                         Rating = 1
                     },
                     new Review
                     {
                         /*Id = 14, */
                         BookId = 8,
                         AppUser = "James Miller",
                         Comment = "The book is very boring",
                         Rating = 3
                     }
                 );
                context.SaveChanges();

                context.UserBook.AddRange(
                    new UserBook {/*Id = 1*/AppUser = "Tom Smith", BookId = 1 },
                    new UserBook {/*Id = 1*/AppUser = "James Miller", BookId = 2 },
                    new UserBook {/*Id = 1*/AppUser = "Lucas James", BookId = 3 }
                    );
                context.SaveChanges();

                context.BookGenre.AddRange(
                new BookGenre { BookId = 1, GenreId = 1 },
                new BookGenre { BookId = 1, GenreId = 9 },
                new BookGenre { BookId = 1, GenreId = 10 },
                new BookGenre { BookId = 1, GenreId = 11 },
                new BookGenre { BookId = 2, GenreId = 1 },
                new BookGenre { BookId = 2, GenreId = 9 },
                new BookGenre { BookId = 2, GenreId = 10 },
                new BookGenre { BookId = 2, GenreId = 11 },
                new BookGenre { BookId = 3, GenreId = 1 },
                new BookGenre { BookId = 3, GenreId = 9 },
                new BookGenre { BookId = 3, GenreId = 10 },
                new BookGenre { BookId = 3, GenreId = 11 },
                new BookGenre { BookId = 4, GenreId = 3 },
                new BookGenre { BookId = 4, GenreId = 5 },
                new BookGenre { BookId = 4, GenreId = 10 },
                new BookGenre { BookId = 5, GenreId = 3 },
                new BookGenre { BookId = 5, GenreId = 5 },
                new BookGenre { BookId = 5, GenreId = 10 },
                new BookGenre { BookId = 6, GenreId = 3 },
                new BookGenre { BookId = 6, GenreId = 5 },
                new BookGenre { BookId = 6, GenreId = 10 },
                new BookGenre { BookId = 7, GenreId = 4 },
                new BookGenre { BookId = 7, GenreId = 5 },
                new BookGenre { BookId = 7, GenreId = 1 },
                new BookGenre { BookId = 8, GenreId = 4 },
                new BookGenre { BookId = 8, GenreId = 5 },
                new BookGenre { BookId = 8, GenreId = 1 }
                );
                context.SaveChanges();
            }
        }
    }
}
