using Entities.Models;

namespace Entities.RequestFeatures;

public class BookParameters : RequestParameters
{
    public BookGenre Genre { get; set; }
    public int AuthorId { get; set; }
}
