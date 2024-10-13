using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public enum BookGenre
{
    Adventures = 1, 
    LoveStories = 2,
    Horrors = 3,
    FairyTales = 4
}

public class Book
{
    [Column("BookId")]
    public int Id { get; set; }
    public string ISBN { get; set; }
    [Required(ErrorMessage = "Book title is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the title is 30 characters.")]
    public string BookTitle { get; set; }
    [Required(ErrorMessage = "Book genre is a required field.")]
    public BookGenre Genre { get; set; }
    [Required(ErrorMessage = "Book genre is a required field.")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Author is a required field.")]
    public string Author { get; set; }
    public int Amount { get; set; }
    // public DateTime ReceiptTime { get; set; }
    // public DateTime ReturnTime { get; set; }
}
