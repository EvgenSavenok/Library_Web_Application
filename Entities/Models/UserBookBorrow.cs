using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class UserBookBorrow
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }  

    [ForeignKey("UserId")]
    public User User { get; set; }

    [Required]
    public int BookId { get; set; }  

    [ForeignKey("BookId")]
    public Book Book { get; set; }

    [Required]
    public DateTime BorrowDate { get; set; }

    [Required]
    public DateTime ReturnDate { get; set; }
}

