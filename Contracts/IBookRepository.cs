using Entities.Models;

namespace Contracts;

public interface IBookRepository
{
    IEnumerable<Book> GetAllBooks(bool trackChanges);
    Book GetBook(int bookId, bool trackChanges);
    void CreateBook(Book book);
}
