using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace Application.DataTransferObjects;

public class BookForCreationDto
{
    public string ISBN { get; set; }
    [Required(ErrorMessage = "Book title is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the title is 30 characters.")]
    public string BookTitle { get; set; }
    public BookGenre Genre { get; set; }
    public string Description { get; set; }
    public int AuthorId { get; set; }
    public int Amount { get; set; }
}
