using BooksStore.Models;

namespace BooksStore.ViewModels
{
    public class BookTitleSearchViewModel
    {
        public IList<Book> Books { get; set; }
        public string SearchString { get; set; }
    }
}
