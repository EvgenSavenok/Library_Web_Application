using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;

namespace Entities.DataTransferObjects;

public class BookDto
{
    public int Id { get; set; }
    public string BookTitle { get; set; }
}
