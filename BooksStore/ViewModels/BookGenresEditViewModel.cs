using BooksStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksStore.ViewModels
{
    public class BookGenresEditViewModel
    {
        public Book Book { get; set; }

        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}
