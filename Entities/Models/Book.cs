using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public enum BookGenre
{
    Adventures, 
    LoveStories,
    Horrors,
    FairyTales
}

public class Book
{
    [Column("BookId")]
    public Guid Id { get; set; }
    public string ISBN { get; set; }
    [Required(ErrorMessage = "Book title is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
    public string BookTitle { get; set; }
    [Required(ErrorMessage = "Book genre is a required field.")]
    public BookGenre Genre { get; set; }
    [Required(ErrorMessage = "Book genre is a required field.")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Author is a required field.")]
    public string Author { get; set; }
    public DateTime ReceiptTime { get; set; }
    public DateTime ReturnTime { get; set; }
}
