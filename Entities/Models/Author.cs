using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace Entities.Models;

public class Author
{
    [Column("AuthorId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Author's name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the name is 30 characters.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Author's last name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the last name is 30 characters.")]
    public string LastName { get; set; }
    public string BirthDate { get; set; }
    [Required(ErrorMessage = "Country is a required field.")]
    public string Country { get; set; }
}
