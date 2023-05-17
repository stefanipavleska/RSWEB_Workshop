using System.ComponentModel.DataAnnotations;

namespace BooksStore.Models
{
    public class UserBook
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string AppUser { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
