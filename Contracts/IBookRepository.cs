using Entities.Models;

namespace Contracts;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
    Task<Book> GetBookAsync(int bookId, bool trackChanges);
    void CreateBook(Book book);
    void DeleteBook(Book book);
}
