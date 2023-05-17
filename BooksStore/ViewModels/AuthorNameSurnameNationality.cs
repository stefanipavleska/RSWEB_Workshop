using BooksStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksStore.ViewModels
{
    public class AuthorNameSurnameNationality
    {
        public IList<Author> Authors { get; set; }
        public SelectList Nationalities { get; set; }
        public string AuthorNationality { get; set; }
        public string SearchStringName { get; set; }

        public string SearchStringSurname { get; set; }
    }
}
