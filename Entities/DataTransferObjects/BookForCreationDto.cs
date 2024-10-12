using Entities.Models;

namespace Entities.DataTransferObjects;

public class BookForCreationDto
{
    public string ISBN { get; set; }
    public string BookTitle { get; set; }
    public BookGenre Genre { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public DateTime ReceiptTime { get; set; }
    public DateTime ReturnTime { get; set; }
}
