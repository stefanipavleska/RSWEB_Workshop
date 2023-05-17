using BooksStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksStore.ViewModels
{
    public class BookTitleGenreSearchViewModel
    {
        public IList<Book> Books { get; set; }
        public SelectList Genres { get; set; }
        public string BookGenre { get; set; }
        public string SearchString { get; set; }
    }
}
