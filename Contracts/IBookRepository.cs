using Entities.Models;

namespace Contracts;

public interface IBookRepository
{
    IEnumerable<Book> GetAllBooks(bool trackChanges);
}
